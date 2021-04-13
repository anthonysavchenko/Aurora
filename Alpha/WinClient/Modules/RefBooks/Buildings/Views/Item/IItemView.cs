using System.Data;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Улица
        /// </summary>
        string Street
        {
            get;
            set;
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        string BuildingNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        string BuildingPartNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Месяц последнего МЛ
        /// </summary>
        string RouteFormLastMonth { set; }

        /// <summary>
        /// Количество абонентов
        /// </summary>
        int CustomersCount { set; }

        /// <summary>
        /// Количество ИПУ
        /// </summary>
        int CountersCount { set; }

        /// <summary>
        /// Норматив
        /// </summary>
        decimal NormCoefficient
        {
            get;
            set;
        }

        /// <summary>
        /// МОП
        /// </summary>
        decimal CollectiveSquare
        {
            get;
            set;
        }

        /// <summary>
        /// Месяц последней расшифровки
        /// </summary>
        string CalculationFormLastMonth { set; }

        /// <summary>
        /// Договор
        /// </summary>
        string BuildingContract { set; }

        /// <summary>
        /// Примечание
        /// </summary>
        string Note
        {
            get;
            set;
        }
    }
}
