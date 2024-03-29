﻿using System;
using System.Web;
using CustomerWebSite.App_Start.Identity;
using CustomerWebSite.Identity;
using CustomerWebSite.Models;
using CustomerWebSite.Services;
using CustomerWebSite.Services.Home;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Taumis.Alpha.Server.Core.Services;
using Taumis.Alpha.Server.Core.Services.ServerTime;
using Taumis.Alpha.Server.Core.Services.Settings;
using Taumis.Alpha.Server.Infrastructure.Data;
using Taumis.Alpha.Server.Infrastructure.Services;

namespace CustomerWebSite.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IAlphaDbContext, AlphaDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<IUserStore<ApplicationUser, int>, UserStore>();
            container.RegisterType<IServerTimeService, ServerTimeService>();
            container.RegisterType<IBillService, BillService>();
            container.RegisterType<IMutualSettlementService, MutualSettlementService>();
            container.RegisterType<IRegularBillService, RegularBillService>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IPaymentsAndChargesService, PaymentsAndChargesService>();
            container.RegisterType<IAuthorizationService, AuthorizationService>();
        }
    }
}
