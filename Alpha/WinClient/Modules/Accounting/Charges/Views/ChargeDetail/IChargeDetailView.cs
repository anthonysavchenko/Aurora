using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.ChargeDetail
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IChargeDetailView : IBaseView
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        string Account { set; }

        /// <summary>
        /// Period
        /// </summary>
        DateTime PeriodCharged { set; }

        /// <summary>
        /// Сумма начисления
        /// </summary>
        decimal Value { set; }

        /// <summary>
        /// Сумма льготы
        /// </summary>
        decimal BenefitValue { set; }

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
        /// Показывать ли ссылку на квитанцию
        /// </summary>
        bool BillLinkEnabled { set; }

        /// <summary>
        /// Показывать ли ссылку на перерасчет
        /// </summary>
        bool RechargeLinkEnabled { set; }

        /// <summary>
        /// Источник данных для таблицы распределения платежа по услугам
        /// </summary>
        DataTable ChargeOperPoses { set; }

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        void ShowDomainOnView();
    }
}