using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;
using Taumis.EnterpriseLibrary.Win.Services;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItemPos = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Resident;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public partial class ResidentsListViewPresenter : BaseSimplePositionsListViewPresenter<IResidentsListView, DomItemPos, DomItem, IUnitOfWork>
    {
        /// <summary>
        /// Список позиций домена
        /// </summary>
        protected override IDictionary<string, DomItemPos> Lines
        {
            get
            {
                return CurrentDomainWithPositions.Residents;
            }
        }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public override DataTable GetElemList()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Surname", typeof(string));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("Patronymic", typeof(string));
            table.Columns.Add("BenefitType", typeof(string));
            table.Columns.Add("ResidentDocument", typeof(string));
            table.Columns.Add("OwnerRelationship", typeof(byte));

            foreach (DomItemPos _resident in Lines.Values)
            {
                table.Rows.Add(
                    _resident.ID,
                    _resident.Surname,
                    _resident.FirstName,
                    _resident.Patronymic,
                    _resident.BenefitType != null ? _resident.BenefitType.ID : string.Empty,
                    _resident.ResidentDocument,
                    (byte)_resident.OwnerRelationship
                );
            }
            return table;
        }

        /// <summary>
        /// Обновить общий список элементов.
        /// </summary>
        public override void RefreshList()
        {
            base.RefreshList();
            SetResidentsCount();
        }

        /// <summary>
        /// Обновляет позиции документа
        /// </summary>
        /// <param name="dictionary">Словарь позиций документа</param>
        protected override void SetDomainPositions(Dictionary<string, DomItemPos> dictionary)
        {
            CurrentDomainWithPositions.Lines.Clear();
            foreach (var _line in dictionary)
            {
                CurrentDomainWithPositions.Lines.Add(_line.Key, _line.Value);
            }
        }

        private DataTable GetBenefitTypes()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));
            _table.Rows.Add(-1, string.Empty);

            using (Entities entities = new Entities())
            {
                IQueryable<BenefitTypes> _query = from benefitTypes in entities.BenefitTypes select benefitTypes;

                foreach (BenefitTypes benefitType in _query)
                {
                    _table.Rows.Add(
                        benefitType.ID,
                        benefitType.Name);
                }
            }

            return _table;
        }

        private DataTable GetOwnerRelationships()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(byte));
            _table.Columns.Add("Name", typeof(string));

            _table.Rows.Add((byte)DomItemPos.Relationship.Unknown, string.Empty);
            _table.Rows.Add((byte)DomItemPos.Relationship.Father, "Отец");
            _table.Rows.Add((byte)DomItemPos.Relationship.Mother, "Мать");
            _table.Rows.Add((byte)DomItemPos.Relationship.Son, "Сын");
            _table.Rows.Add((byte)DomItemPos.Relationship.Daughter, "Дочь");
            _table.Rows.Add((byte)DomItemPos.Relationship.Grandson, "Внук");
            _table.Rows.Add((byte)DomItemPos.Relationship.Granddaughter, "Внучка");
            _table.Rows.Add((byte)DomItemPos.Relationship.Relative, "Родственник");
            _table.Rows.Add((byte)DomItemPos.Relationship.Owner, "Собственник");

            return _table;
        }

        /// <summary>
        /// Обновить справочные данные в комбобоксах таблицы
        /// </summary>
        protected override void RefreshRefBooks()
        {
            View.BenefitTypes = GetBenefitTypes();
            View.OwnerRelationships = GetOwnerRelationships();
        }

        /// <summary>
        /// Устанавливает ссылку на владельца позиции
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        /// <param name="_positionOwner">Владелец позиции</param>
        protected override void SetPositionOwner(DomItemPos _curItem, DomItem _positionOwner)
        {
            _curItem.Doc = _positionOwner;
        }

        /// <summary>
        /// Наполняет позицию данными с вида
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        protected override void FillCurrentPosition(DomItemPos _curItem)
        {
            _curItem.Surname = View.Surname;
            _curItem.FirstName = View.FirstName;
            _curItem.Patronymic = View.Patronymic;
            _curItem.BenefitType = View.BenefitType;
            _curItem.OwnerRelationship = (DomItemPos.Relationship)View.OwnerRelationship;
            _curItem.ResidentDocument = View.ResidentDocument;
        }

        /// <summary>
        /// Проверяет корректность введенных данных
        /// </summary>
        /// <param name="curItem">Объект домена для проверки</param>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns>Признак успешности проверки</returns>
        protected override bool CheckItem(DomItemPos curItem, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(curItem.Surname))
            {
                message += "- Фамилия\r\n";
            }

            if (curItem.BenefitType != null)
            {
                if (string.IsNullOrEmpty(curItem.FirstName))
                {
                    message += "- Имя\r\n";
                }

                if (string.IsNullOrEmpty(curItem.Patronymic))
                {
                    message += "- Отчество\r\n";
                }

                if (string.IsNullOrEmpty(curItem.ResidentDocument))
                {
                    message += "- Документ\r\n";
                }
            }

            return String.IsNullOrEmpty(message);
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(_coll, handler);
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection _coll, EventHandler handler)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(_coll, handler);
        }

        public void SetResidentsCount()
        {
            int _benefitResidentsCount = Lines.Values.Count(r => r.BenefitType != null);

            View.BenefitResidentsCount = _benefitResidentsCount;
            View.NonbenefitResidentsCount = Lines.Values.Count(r => r.BenefitType == null);
            View.ResidentsCount = Lines.Count;

            IItemView _itemView = WorkItem.SmartParts.Get<ItemView>("ItemView");

            if (_benefitResidentsCount > 0)
            {
                _itemView.LiftPresenceEnabled = true;
                _itemView.RubbishChutePresenceEnabled = true;
            }
            else
            {
                _itemView.LiftPresenceEnabled = false;
                _itemView.LiftPresence = false;
                _itemView.RubbishChutePresenceEnabled = false;
                _itemView.RubbishChutePresence = false;
            }
        }

        public override void DeleteElem()
        {
            base.DeleteElem();
            SetResidentsCount();
        }

        public override bool NavigatorBtnEndEdit()
        {
            bool _res = base.NavigatorBtnEndEdit();
            RefreshList();
            return _res;
        }

        #region Overrides of BaseSimpleListViewPresenter<IResidentsListView,Resident>

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Добавить"
        /// </summary>
        public override bool NavigatorBtnAppend()
        {
            IItemView _itemView = WorkItem.SmartParts.Get<ItemView>("ItemView");
            View.OwnerRelationshipEnabled = _itemView.OwnerType == DomItem.OwnerTypes.PhysicalPerson;
            return base.NavigatorBtnAppend();
        }

        /// <summary>
        /// Производит сохранение элемента
        /// </summary>
        /// <param name="_curItem">Объект домена позиции</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(DomItemPos _curItem)
        {
            if (_curItem.OwnerRelationship == DomItemPos.Relationship.Owner)
            {
                foreach (DomItemPos _resident in Lines.Values)
                {
                    if (_resident.ID != _curItem.ID && _resident.OwnerRelationship == DomItemPos.Relationship.Owner)
                    {
                        _resident.OwnerRelationship = DomItemPos.Relationship.Unknown;
                    }
                }

                IItemView _itemView = WorkItem.SmartParts.Get<ItemView>("ItemView");
                _itemView.UpdatePhysicalPerson(_curItem.Surname, _curItem.FirstName, _curItem.Patronymic);
            }

            return base.SaveItem(_curItem);
        }

        #endregion
    }
}