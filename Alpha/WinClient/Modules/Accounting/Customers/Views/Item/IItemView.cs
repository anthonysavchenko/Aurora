using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Улицы
        /// </summary>
        DataTable Streets { set; }

        /// <summary>
        /// Дома
        /// </summary>
        DataTable Buildings { set; }

        /// <summary>
        /// Тип собственника
        /// </summary>
        DomItem.OwnerTypes OwnerType { set; get; }

        /// <summary>
        /// Полное имя физического лица
        /// </summary>
        string PhysicalPersonFullName { set; get; }

        /// <summary>
        /// Краткое имя физического лица
        /// </summary>
        string PhysicalPersonShortName { set; get; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        string JuridicalPersonFullName { set; get; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        string Account { set; get; }

        /// <summary>
        /// Улица
        /// </summary>
        Street Street { set; get; }

        /// <summary>
        /// Дом
        /// </summary>
        Building Building { set; get; }

        /// <summary>
        /// Этаж
        /// </summary>
        short Floor { get; set; }

        /// <summary>
        /// Подъезд
        /// </summary>
        byte Entrance { get; set; }

        /// <summary>
        /// Последний этаж
        /// </summary>
        short FloorMax { set; }

        /// <summary>
        /// Последний подъезд
        /// </summary>
        byte EntranceMax { set; }

        /// <summary>
        /// Квартира
        /// </summary>
        string Apartment { set; get; }

        /// <summary>
        /// Площадь
        /// </summary>
        decimal Square { set; get; }

        /// <summary>
        /// Количество комнат
        /// </summary>
        int RoomsCount { set; get; }

        /// <summary>
        /// В собственности
        /// </summary>
        bool IsPrivate { set; get; }

        /// <summary>
        /// Наличие лифта
        /// </summary>
        bool LiftPresence { get; set; }

        /// <summary>
        /// Наличие мусоропровода
        /// </summary>
        bool RubbishChutePresence { get; set; }

        /// <summary>
        /// Доступен ли контрол "Наличие лифта"
        /// </summary>
        bool LiftPresenceEnabled { set; }

        /// <summary>
        /// Доступен ли контрол "Наличие мусоропровода"
        /// </summary>
        bool RubbishChutePresenceEnabled { set; }

        /// <summary>
        /// Примечание
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Доступ к личному кабинету
        /// </summary>
        bool WebAccess { get; set; }

        /// <summary>
        /// Email для доступа на веб сайт
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Флаг реструктуризации долга
        /// </summary>
        bool DebtsRepayment { get; set; }

        /// <summary>
        /// Обновляет данные о физ. лице
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="patronymic">Отчество</param>
        void UpdatePhysicalPerson(string surname, string firstName, string patronymic);
    }
}