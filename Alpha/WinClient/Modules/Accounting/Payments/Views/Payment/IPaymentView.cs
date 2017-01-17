using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Payment
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IPaymentView : IBaseView
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        string Account { set; }

        /// <summary>
        /// Посредник
        /// </summary>
        string Intermediary { set; }

        /// <summary>
        /// Period
        /// </summary>
        DateTime Period { set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        decimal Value { set; }

        /// <summary>
        /// Собственник
        /// </summary>
        string Owner { set; }

        /// <summary>
        /// Улица
        /// </summary>
        string Street { set; }

        /// <summary>
        /// Дом
        /// </summary>
        string House { set; }

        /// <summary>
        /// Квартира
        /// </summary>
        string Apartment { set; }

        /// <summary>
        /// Площадь
        /// </summary>
        decimal Square { set; }

        /// <summary>
        /// Источник данных для таблицы распределения платежа по услугам
        /// </summary>
        DataTable PaymentOperPoses { set; }

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        void ShowDomainOnView();
    }
}