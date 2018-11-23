using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Factories
{
    public interface IWizardCommandDispatcherFactory
    {
        ICommandDispatcher Create(IServerTimeService sts, PaymentDistributionService pds, IExcelService excelService);
    }
}