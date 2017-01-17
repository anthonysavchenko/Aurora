using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Library.Services;

namespace Taumis.Infrastructure.Library
{
    public abstract class SmartClientApplication<TWorkItem, TShell> : FormShellApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
    {
        protected override void AddServices()
        {
            base.AddServices();

            RootWorkItem.Services.AddNew<ProfileCatalogModuleInfoStore, IModuleInfoStore>();
            RootWorkItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
            RootWorkItem.Services.Remove<IModuleEnumerator>();
            RootWorkItem.Services.Remove<IModuleLoaderService>();
            RootWorkItem.Services.AddNew<DependentModuleLoaderService, IModuleLoaderService>();

            // Сервис добавления элементов в главное меню.
            RootWorkItem.Services.AddNew<MenuExtendService>();

            // Сервис подключения обработчиков изменения значений контролов.
            RootWorkItem.Services.AddNew<ChangeEventHandlerService, IChangeEventHandlerService>();
        }
    }
} 
