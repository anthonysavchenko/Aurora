using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Services
{
    public interface IWizardService
    {
        DataTable GetServiceTable();
    }
}