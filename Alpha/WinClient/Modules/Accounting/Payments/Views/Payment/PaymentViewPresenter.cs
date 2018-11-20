using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Payment
{
    public class PaymentViewPresenter : BaseDomainPresenter<IPaymentView>
    {
        /// <summary>
        /// Отображает домен на view
        /// </summary>
        public void ShowDomainOnView()
        {
            PaymentOper _domItem =
                GetItem<PaymentOper>((string)WorkItem.State[ModuleStateNames.CURRENT_PAYMENT_OPER_ID]);

            View.Account = _domItem.Customer.Account;
            View.Owner =
                _domItem.Customer.OwnerType == OwnerType.PhysicalPerson
                    ? _domItem.Customer.PhysicalPersonShortName
                    : _domItem.Customer.JuridicalPersonFullName;
            View.Apartment = _domItem.Customer.Apartment;
            View.House = _domItem.Customer.Building.Number;
            View.Street = _domItem.Customer.Building.Street.Name;
            View.Square = _domItem.Customer.Square;

            View.Intermediary =
                _domItem.PaymentSet.Intermediary != null
                    ? _domItem.PaymentSet.Intermediary.Name
                    : string.Empty;
            View.Period = _domItem.PaymentPeriod;
            View.Value = Math.Abs(_domItem.Value);

            View.PaymentOperPoses =
                DataMapper<PaymentOperPos, IPaymentOperPosDataMapper>().GetList(_domItem.ID);
        }
    }
}