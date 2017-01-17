using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

using Taumis.EnterpriseLibrary.Win.Constants;

using Taumis.Infrastructure.Library.UI;

namespace Taumis.Infrastructure.Layout
{
    public class Module : ModuleInit
    {
        private WorkItem _rootWorkItem;

        [InjectionConstructor]
        public Module([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        public override void Load()
        {
            base.Load();

			// Вид панели состояний главного вида оболочки.
			StatusView _statusView = _rootWorkItem.Items.AddNew<StatusView> ("StatusView");
			_rootWorkItem.Workspaces[CommonWorkspaceNames.StatusWorkspace].Show (_statusView);
			
			// Вид главного меню и инструментальной панели.
			ShellLayoutView _shellLayout = _rootWorkItem.Items.AddNew<ShellLayoutView> ("ShellLayoutView");
            _rootWorkItem.Workspaces[CommonWorkspaceNames.LayoutWorkspace].Show(_shellLayout);

            // Визуальное рабочее пространство в виде отдельного независимого окна.
            WindowWorkspace wsp = new WindowWorkspace(_shellLayout.ParentForm);
            _rootWorkItem.Workspaces.Add(wsp, CommonWorkspaceNames.ModalWindows);

			// Мультидокументное рабочее пространство (MDI Workspace).
			MdiWorkspace mdiwsp = new MdiWorkspace (_shellLayout.ParentForm);
            _rootWorkItem.Workspaces.Add(mdiwsp, CommonWorkspaceNames.MDIWindows);
        }
    }
}
