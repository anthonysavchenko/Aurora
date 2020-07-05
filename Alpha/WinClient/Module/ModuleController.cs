using Taumis.Infrastructure.Interface;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Module
{
    public class ModuleController : WorkItemController
    {
        public override void Run()
        {
            IDataMapperService trans = WorkItem.RootWorkItem.Services.Get<IDataMapperService>();

            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.BenefitTypeDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.ResidentDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.ServiceTypeDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.ServiceDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.ContractorDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.IntermediaryDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.StreetDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.BuildingDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.UserDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.PrivateCounterDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.PrivateCounterValueDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.CommonCounterDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.CommonCounterValueDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.CommonCounterCoefficientDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.PublicPlaceDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.BankDetailDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.PublicPlaceServiceVolumeDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.RechargePercentCorrectionDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.CounterValueCollectDistrictDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.ElectricitySharedCounterVolumeDataMapper());

            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.ChargeOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.ChargeOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.RechargeOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.RechargeOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.BenefitOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.BenefitOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.OverpaymentCorrectionOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.OverpaymentCorrectionOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.PaymentOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.PaymentOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.PaymentCorrectionOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.PaymentCorrectionOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.OverpaymentOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.OverpaymentOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.ChargeCorrectionOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.ChargeCorrectionOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.BenefitCorrectionOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.BenefitCorrectionOperPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.RebenefitOperDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper.RebenefitOperPosDataMapper());

            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.CustomerPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.CustomerDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.PaymentSetDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.BillSetDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DebtBillDocDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.ChargeSetDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RechargeSetDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RegularBillDocDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RegularBillDocSeviceTypePosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RegularBillDocCounterPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RegularBillDocSharedCounterPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.TotalBillDocDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.TotalBillDocPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RouteFormDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RouteFormPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.FillFormDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.FillFormPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DecFormsUploadDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DecFormsUploadPosDataMapper());
        }
    }
}