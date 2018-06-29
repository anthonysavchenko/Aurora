using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.DbBackup;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Factories
{
    public class WizardCommandDispatcherFactory : IWizardCommandDispatcherFactory
    {
        public ICommandDispatcher Create(IServerTimeService sts, PaymentDistributionService pds, IExcelService excelService)
        {
            var _dispatcher = new CommandDispatcher();
            _dispatcher.AddHandlers(
                new CommandHandlerAdapter<ApplyPercentCorrectionCommand>(new ApplyPercentCorrectionCommandHandler()),
                new CommandHandlerAdapter<CalculateBenefitsCommand>(new CalculateBenefitsCommandHandler()),
                new CommandHandlerAdapter<CalculateChargeCommand>(new CalculateChargeCommandHandler()),
                new CommandHandlerAdapter<CalculateDebtDistributionCommand>(new CalculateDebtDistributionCommandHandler(_dispatcher)),
                new CommandHandlerAdapter<CreateBillCommand>(new CreateBillCommandHandler()),
                new CommandHandlerAdapter<CreateChargeCommand>(new CreateChargeCommandHandler()),
                new CommandHandlerAdapter<CreateChargeCorrectionCommand>(new CreateChargeCorrectionCommandHandler()),
                new CommandHandlerAdapter<CreateOverpaymentCommand>(new CreateOverpaymentCommandHandler(pds)),
                new CommandHandlerAdapter<CreateRechargeCommand>(new CreateRechargeCommandHandler()),
                new CommandHandlerAdapter<CreateRechargeSetCommand>(new CreateRechargeSetCommandHandler()),
                new CommandHandlerAdapter<ParseDebtFileCommand>(new ParseDebtFileCommandHandler(excelService)),
                new CommandHandlerAdapter<RegisterChargeCommand>(new RegisterChargeCommandHandler(pds, _dispatcher)),
                new CommandHandlerAdapter<RegisterRechargeCommand>(new RegisterRechargeCommandHandler(_dispatcher)),
                new CommandHandlerAdapter<RegisterDebtCommand>(new RegisterDebtCommandHandler(_dispatcher)),
                new CommandHandlerAdapter<BackupDbCommand>(new BackupDbCommandHandler(sts)));

            return _dispatcher;
        }
    }
}
