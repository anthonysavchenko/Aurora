﻿using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Taumis.EnterpriseLibrary.Win.Common.Properties;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;

namespace Taumis.Infrastructure.Library.Services
{
    /// <summary>
    /// Сервис загрузки модулей
    /// </summary>
    public class DependentModuleLoaderService : IModuleLoaderService
    {
        Dictionary<Assembly, ModuleMetadata> loadedModules = new Dictionary<Assembly, ModuleMetadata>();
        TraceSource traceSource = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleLoaderService"/> class with the
        /// provided trace source.
        /// </summary>
        /// <param name="traceSource">The trace source for tracing. If null is
        /// passed, the service does not perform tracing.</param>
        [InjectionConstructor]
        public DependentModuleLoaderService([ClassNameTraceSource] TraceSource traceSource)
        {
            this.traceSource = traceSource;
        }

        /// <summary>
        /// See <see cref="IModuleLoaderService.ModuleLoaded"/> for more information.
        /// </summary>
        public event EventHandler<DataEventArgs<LoadedModuleInfo>> ModuleLoaded;

        /// <summary>
        /// See <see cref="IModuleLoaderService.LoadedModules"/> for more information.
        /// </summary>
        public IList<LoadedModuleInfo> LoadedModules
        {
            get
            {
                List<LoadedModuleInfo> result = new List<LoadedModuleInfo>();

                foreach (ModuleMetadata module in loadedModules.Values)
                    result.Add(module.ToLoadedModuleInfo());

                return result.AsReadOnly();
            }
        }

        /// <summary>
        /// See <see cref="IModuleLoaderService.Load(WorkItem, IModuleInfo[])"/> for more information.
        /// </summary>
        public void Load(WorkItem workItem, params IModuleInfo[] modules)
        {
            Guard.ArgumentNotNull(workItem, "workItem");
            Guard.ArgumentNotNull(modules, "modules");

            InnerLoad(workItem, modules);
        }

        /// <summary>
        /// See <see cref="IModuleLoaderService.Load(WorkItem, Assembly[])"/> for more information.
        /// </summary>
        public void Load(WorkItem workItem, params Assembly[] assemblies)
        {
            Guard.ArgumentNotNull(workItem, "workItem");
            Guard.ArgumentNotNull(assemblies, "assemblies");

            List<IModuleInfo> modules = new List<IModuleInfo>();

            foreach (Assembly assembly in assemblies)
                modules.Add(new ModuleInfo(assembly));

            InnerLoad(workItem, modules.ToArray());
        }

        /// <summary>
        /// Fires the ModuleLoaded event.
        /// </summary>
        /// <param name="module">The module that was loaded.</param>
        protected virtual void OnModuleLoaded(LoadedModuleInfo module)
        {
            if (ModuleLoaded != null)
                ModuleLoaded(this, new DataEventArgs<LoadedModuleInfo>(module));
        }

        private void InnerLoad(WorkItem workItem, IModuleInfo[] modules)
        {
            if (modules.Length == 0)
                return;

            IModuleInfo[] allowedModules = FilterModulesBasedOnRole(modules);
            LoadAssemblies(allowedModules);
            List<ModuleMetadata> loadOrder = GetLoadOrder();

            foreach (ModuleMetadata module in loadOrder)
                module.LoadServices(workItem);

            foreach (ModuleMetadata module in loadOrder)
                module.InitializeWorkItemExtensions(workItem);

            foreach (ModuleMetadata module in loadOrder)
                module.InitializeModuleClasses(workItem);

            foreach (ModuleMetadata module in loadOrder)
                module.NotifyOfLoadedModule(OnModuleLoaded);
        }

        private IModuleInfo[] FilterModulesBasedOnRole(IModuleInfo[] modules)
        {
            List<IModuleInfo> allowedModules = new List<IModuleInfo>();

            foreach (IModuleInfo module in modules)
            {
                if (module.AllowedRoles.Count == 0)
                    allowedModules.Add(module);
                else
                {
                    foreach (string role in module.AllowedRoles)
                    {
                        if (Thread.CurrentPrincipal.IsInRole(role))
                        {
                            allowedModules.Add(module);
                            break;
                        }
                    }
                }
            }

            return allowedModules.ToArray();
        }

        private List<ModuleMetadata> GetLoadOrder()
        {
            Dictionary<string, ModuleMetadata> indexedInfo = new Dictionary<string, ModuleMetadata>();
            ModuleDependencySolver solver = new ModuleDependencySolver();
            List<ModuleMetadata> result = new List<ModuleMetadata>();

            foreach (ModuleMetadata data in loadedModules.Values)
            {
                if (indexedInfo.ContainsKey(data.Name))
                    throw new ModuleLoadException(String.Format(CultureInfo.CurrentCulture,
                        Resources.DuplicatedModule, data.Name));

                indexedInfo.Add(data.Name, data);
                solver.AddModule(data.Name);

                foreach (string dependency in data.Dependencies)
                    solver.AddDependency(data.Name, dependency);
            }

            if (solver.ModuleCount > 0)
            {
                string[] loadOrder = solver.Solve();

                for (int i = 0; i < loadOrder.Length; i++)
                    result.Add(indexedInfo[loadOrder[i]]);
            }

            return result;
        }

        private void LoadAssemblies(IModuleInfo[] modules)
        {
            foreach (IModuleInfo module in modules)
            {
                GuardLegalAssemblyFile(module);
                Assembly assembly = LoadAssembly(module.AssemblyFile);

                if (!loadedModules.ContainsKey(assembly))
                    loadedModules.Add(assembly, new ModuleMetadata(assembly, traceSource, module));
            }
        }

        private Assembly LoadAssembly(string assemblyFile)
        {
            Guard.ArgumentNotNullOrEmptyString(assemblyFile, "assemblyFile");

            assemblyFile = GetModulePath(assemblyFile);

            FileInfo file = new FileInfo(assemblyFile);
            Assembly assembly = null;

            try
            {
                assembly = Assembly.LoadFrom(file.FullName);
            }
            catch (Exception ex)
            {
                throw new ModuleLoadException(assemblyFile, ex.Message, ex);
            }

            if (traceSource != null)
                traceSource.TraceInformation(Resources.LogModuleAssemblyLoaded, file.FullName);

            return assembly;
        }

        private string GetModulePath(string assemblyFile)
        {
            if (!Path.IsPathRooted(assemblyFile))
                assemblyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyFile);

            return assemblyFile;
        }

        #region Guards

        private void GuardLegalAssemblyFile(IModuleInfo modInfo)
        {
            Guard.ArgumentNotNull(modInfo, "modInfo");
            Guard.ArgumentNotNull(modInfo.AssemblyFile, "modInfo.AssemblyFile");

            string assemblyFilePath = GetModulePath(modInfo.AssemblyFile);

            if (!File.Exists(assemblyFilePath))
                throw new ModuleLoadException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.ModuleNotFound, assemblyFilePath));
        }

        #endregion

        #region Helper classes

        class ModuleMetadata
        {
            Assembly assembly;
            Type moduleControllerType = null;
            string usecaseName = null;
            bool loadedServices = false;
            bool extensionsInitialized = false;
            bool modulesInitialzed = false;
            string name = null;
            bool notified = false;

            List<string> dependencies = new List<string>();
            List<Type> moduleTypes = new List<Type>();
            List<IModule> moduleClasses = new List<IModule>();
            List<string> roles = new List<string>();
            List<ServiceMetadata> services = new List<ServiceMetadata>();
            List<KeyValuePair<Type, Type>> workItemExtensions = new List<KeyValuePair<Type, Type>>();
            List<Type> workItemRootExtensions = new List<Type>();

            TraceSource traceSource;

            public ModuleMetadata(Assembly assembly, TraceSource traceSource, IModuleInfo moduleInfo)
            {
                this.assembly = assembly;
                this.traceSource = traceSource;

                if (moduleInfo is IDependentModuleInfo)
                {
                    name = ((IDependentModuleInfo)moduleInfo).Name;
                    dependencies.AddRange(((IDependentModuleInfo)moduleInfo).Dependencies);
                }
                else
                {
                    foreach (ModuleAttribute attr in assembly.GetCustomAttributes(typeof(ModuleAttribute), true))
                        name = attr.Name;

                    foreach (ModuleDependencyAttribute attr in assembly.GetCustomAttributes(typeof(ModuleDependencyAttribute), true))
                        dependencies.Add(attr.Name);
                }

                foreach (Type type in assembly.GetExportedTypes())
                {
                    foreach (ServiceAttribute attr in type.GetCustomAttributes(typeof(ServiceAttribute), true))
                        services.Add(new ServiceMetadata(type, attr.RegisterAs ?? type, attr.AddOnDemand));

                    foreach (WorkItemExtensionAttribute attr in type.GetCustomAttributes(typeof(WorkItemExtensionAttribute), true))
                        workItemExtensions.Add(new KeyValuePair<Type, Type>(attr.WorkItemType, type));

                    foreach (RootWorkItemExtensionAttribute attr in type.GetCustomAttributes(typeof(RootWorkItemExtensionAttribute), true))
                        workItemRootExtensions.Add(type);

                    if (!type.IsAbstract && typeof(IModule).IsAssignableFrom(type))
                        moduleTypes.Add(type);

                    if (!type.IsAbstract && typeof(ICommonModuleController).IsAssignableFrom(type))
                    {
                        moduleTypes.Add(typeof(CommonModule));
                        moduleControllerType = type;
                        foreach (UsecaseNameAttribute attribute in type.GetCustomAttributes(typeof(UsecaseNameAttribute), false))
                        {
                            usecaseName = attribute.UsecaseName;
                            break;
                        }
                    }
                }
            }

            public IEnumerable<string> Dependencies
            {
                get { return dependencies; }
            }

            public string Name
            {
                get
                {
                    if (name == null)
                        name = assembly.FullName;

                    return name;
                }
                set { name = value; }
            }

            public void LoadServices(WorkItem workItem)
            {
                if (loadedServices)
                    return;

                loadedServices = true;
                EnsureModuleClassesExist(workItem);

                try
                {
                    foreach (IModule moduleClass in moduleClasses)
                    {
                        moduleClass.AddServices();

                        if (traceSource != null)
                            traceSource.TraceInformation(Resources.AddServicesCalled, moduleClass.GetType());
                    }

                    foreach (ServiceMetadata svc in services)
                    {
                        if (svc.AddOnDemand)
                        {
                            workItem.Services.AddOnDemand(svc.InstanceType, svc.RegistrationType);

                            if (traceSource != null)
                                traceSource.TraceInformation(Resources.ServiceAddedOnDemand, Name, svc.InstanceType);
                        }
                        else
                        {
                            workItem.Services.AddNew(svc.InstanceType, svc.RegistrationType);

                            if (traceSource != null)
                                traceSource.TraceInformation(Resources.ServiceAdded, Name, svc.InstanceType);
                        }
                    }
                }
                catch (Exception ex) { ThrowModuleLoadException(ex); }
            }

            private void EnsureModuleClassesExist(WorkItem workItem)
            {
                if (moduleClasses.Count == moduleTypes.Count)
                    return;

                try
                {
                    foreach (Type moduleType in moduleTypes)
                    {
                        IModule module = (IModule)workItem.Items.AddNew(moduleType);
                        moduleClasses.Add(module);

                        if (traceSource != null)
                            traceSource.TraceInformation(Resources.LogModuleAdded, moduleType);
                    }
                }
                catch (FileNotFoundException ex) { ThrowModuleReferenceException(ex); }
                catch (Exception ex) { ThrowModuleLoadException(ex); }
            }

            public void InitializeModuleClasses(WorkItem workItem)
            {
                if (modulesInitialzed)
                    return;

                modulesInitialzed = true;
                EnsureModuleClassesExist(workItem);

                try
                {
                    foreach (IModule module in moduleClasses)
                    {
                        if (module is CommonModule)
                        {
                            (module as CommonModule).Load(moduleControllerType, usecaseName);
                        }
                        else
                        {
                            module.Load();
                        }

                        if (traceSource != null)
                            traceSource.TraceInformation(Resources.ModuleStartCalled, module.GetType());
                    }
                }
                catch (FileNotFoundException ex) { ThrowModuleReferenceException(ex); }
                catch (Exception ex) { ThrowModuleLoadException(ex); }
            }

            public void InitializeWorkItemExtensions(WorkItem workItem)
            {
                if (extensionsInitialized)
                    return;

                extensionsInitialized = true;

                IWorkItemExtensionService svc = workItem.Services.Get<IWorkItemExtensionService>();

                if (svc == null)
                    return;

                foreach (KeyValuePair<Type, Type> kvp in workItemExtensions)
                    svc.RegisterExtension(kvp.Key, kvp.Value);

                foreach (Type type in workItemRootExtensions)
                    svc.RegisterRootExtension(type);
            }

            public void NotifyOfLoadedModule(Action<LoadedModuleInfo> action)
            {
                if (notified)
                    return;

                notified = true;
                action(ToLoadedModuleInfo());
            }

            public LoadedModuleInfo ToLoadedModuleInfo()
            {
                return new LoadedModuleInfo(assembly, Name, roles, dependencies);
            }

            private void ThrowModuleLoadException(Exception innerException)
            {
                throw new ModuleLoadException(Name,
                        String.Format(CultureInfo.CurrentCulture,
                                            Resources.FailedToLoadModule,
                                            assembly.FullName, innerException.Message),
                        innerException);
            }

            private void ThrowModuleReferenceException(Exception innerException)
            {
                throw new ModuleLoadException(Name,
                        Resources.ReferencedAssemblyNotFound,
                        innerException);
            }
        }

        class ServiceMetadata
        {
            public bool AddOnDemand = false;
            public Type InstanceType = null;
            public Type RegistrationType = null;

            public ServiceMetadata(Type instanceType, Type registrationType, bool addOnDemand)
            {
                this.InstanceType = instanceType;
                this.RegistrationType = registrationType;
                this.AddOnDemand = addOnDemand;
            }
        }

        class ClassNameTraceSourceAttribute : TraceSourceAttribute
        {
            /// <summary>
            /// Initializes the attribute using the <see cref="IModuleLoaderService"/> 
            /// interface namespace as the source name.
            /// </summary>
            public ClassNameTraceSourceAttribute()
                : base(typeof(ModuleLoaderService).FullName)
            {
            }
        }

        #endregion
    }
}
