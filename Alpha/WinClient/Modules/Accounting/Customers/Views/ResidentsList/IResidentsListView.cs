using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Интерфейс вида списка позиций документа.
    /// </summary>
    public interface IResidentsListView : IBaseSimpleListView
    {
        /// <summary>
        /// Типы льгот
        /// </summary>
        DataTable BenefitTypes { set; }

        /// <summary>
        /// Виды связей с собственником
        /// </summary>
        DataTable OwnerRelationships { set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Имя
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        string Patronymic { get; }

        /// <summary>
        /// Льгота
        /// </summary>
        BenefitType BenefitType { get; }

        /// <summary>
        /// Связь с собственником
        /// </summary>
        byte OwnerRelationship { get; }

        /// <summary>
        /// Документ
        /// </summary>
        string ResidentDocument { get; }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        void BindActivate(EventHandler handler);

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        void BindDeactivate(EventHandler handler);

        /// <summary>
        /// Доступна ли для редактирования колонка связь с собственником
        /// </summary>
        bool OwnerRelationshipEnabled { set; }

        /// <summary>
        /// Количество жильцов, не имеющих льготы
        /// </summary>
        int NonbenefitResidentsCount { set; }

        /// <summary>
        /// Количество жильцов, имеющих льготы
        /// </summary>
        int BenefitResidentsCount { set; }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        int ResidentsCount { set; }
    }
}