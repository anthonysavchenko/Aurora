using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Список улиц
        /// </summary>
        DataTable Streets { set; }

        /// <summary>
        /// Улица
        /// </summary>
        Street Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        string ZipCode { get; set; }

        /// <summary>
        /// Количество этажей
        /// </summary>
        short FloorCount { get; set; }

        /// <summary>
        /// Количество подъездов
        /// </summary>
        byte EntranceCount { get; set; }

        /// <summary>
        /// Площадь
        /// </summary>
        decimal Area { set; }

        /// <summary>
        /// Отапливаемая площадь
        /// </summary>
        decimal HeatedArea { set; }

        /// <summary>
        /// Площадь нежлых помещений
        /// </summary>
        decimal NonResidentialPlaceArea { get; set; }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        int ResindentsCount { set; }

        /// <summary>
        /// Примечание
        /// </summary>
        string Note { get; set; }

        /// <summary>
        /// Код ФИАС
        /// </summary>
        string FiasID { get; set; }

        /// <summary>
        /// Список банковских реквизитов
        /// </summary>
        DataTable BankDetailsSource { set; }

        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        BankDetail BankDetail { get; set; }
    }
}