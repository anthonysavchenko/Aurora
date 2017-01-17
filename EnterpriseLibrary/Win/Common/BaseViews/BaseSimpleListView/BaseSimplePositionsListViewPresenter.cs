using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using Taumis.Domain.DocLine;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Презентер вида списка позиций
    /// </summary>
    /// <typeparam name="TListView">Тип вида списка позиций</typeparam>
    /// <typeparam name="TPosition">Тип позиции</typeparam>
    /// <typeparam name="TDomainWithPositions">Тип домена, содержащего позиции</typeparam>
    /// <typeparam name="TUnitOfWork">Тип UOW</typeparam>
    public abstract class BaseSimplePositionsListViewPresenter<TListView, TPosition, TDomainWithPositions, TUnitOfWork> : BaseSimpleListViewPresenter<TListView, TPosition>
        where TListView : IBaseSimpleListView
        where TPosition : DomainObject, IDocLine, new()
        where TDomainWithPositions : DomainObject
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public TUnitOfWork UOW { protected get; set; }

        /// <summary>
        /// Имя состояния для хранения копии позиций
        /// </summary>
        private readonly string _positionBackupStateName = String.Format("state://{0}", Guid.NewGuid());

        #region Overrided

        /// <summary>
        /// Возвращает текущий объект позиции
        /// </summary>
        protected override sealed TPosition GetCurrentItem()
        {
            TPosition _res;
            Lines.TryGetValue(View.GetCurrentItemId(), out _res);
            return _res;
        }

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override sealed TPosition CreateNewItem()
        {
            TPosition _res = base.CreateNewItem();
            SetPositionOwner(_res, CurrentDomainWithPositions);
            return _res;
        }

        /// <summary>
        /// Собрать данные с вида в домен
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        protected override sealed void GetItemFromView(TPosition _curItem)
        {
            if (Lines.ContainsKey(_curItem.ID))
            {
                Lines.Remove(_curItem.ID);
            }

            FillCurrentPosition(_curItem);
        }

        /// <summary>
        /// Производит сохранение элемента
        /// </summary>
        /// <param name="_curItem">Объект домена позиции</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool SaveItem(TPosition _curItem)
        {
            Lines.Add(_curItem.ID, _curItem);
            return string.IsNullOrEmpty(_curItem.IsNew ? UOW.registerNew(_curItem) : UOW.registerDirty(_curItem));
        }

        /// <summary>
        /// Удаление текущего элемента домена из базы данных
        /// </summary>
        public override void DeleteElem()
        {
            TDomainWithPositions _person = CurrentDomainWithPositions;
            string _posId = View.GetCurrentItemId();

            UOW.registerRemoved((TPosition)Lines[_posId]);
            Lines.Remove(_posId);
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.Modified;
        }

        /// <summary>
        /// Обработка нажатия на кнопку навигатора "Закончить редактирование"
        /// </summary>
        /// <returns>Возвращает true, если событие обработано</returns>
        public override bool NavigatorBtnEndEdit()
        {
            SavePositionsState();

            base.NavigatorBtnEndEdit();

            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.Modified;
            return false;
        }

        #endregion

        /// <summary>
        /// Сохраняет текущее состояние позиций
        /// </summary>
        private void SavePositionsState()
        {
            Dictionary<string, TPosition> _copy = new Dictionary<string, TPosition>(Lines.Count);

            foreach (TPosition _pos in Lines.Values)
            {
                _copy.Add(_pos.ID, (TPosition)_pos.Clone());
            }

            WorkItem.State[_positionBackupStateName] = _copy;
        }

        #region Для наследников

        /// <summary>
        /// Обновляет позиции домена
        /// </summary>
        /// <param name="_dictionary">Словарь позиций домена</param>
        protected abstract void SetDomainPositions(Dictionary<string, TPosition> _dictionary);

        /// <summary>
        /// Наполняет позицию данными с вида
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        protected abstract void FillCurrentPosition(TPosition _curItem);

        /// <summary>
        /// Устанавливает ссылку на владельца позиции
        /// </summary>
        /// <param name="_curItem">Домен позиции</param>
        /// <param name="_positionOwner">Владелец позиции</param>
        protected abstract void SetPositionOwner(TPosition _curItem, TDomainWithPositions _positionOwner);

        /// <summary>
        /// Список позиций домена
        /// </summary>
        protected abstract IDictionary<string, TPosition> Lines { get; }

        /// <summary>
        /// Редактируемый домен с позициями
        /// </summary>
        protected virtual TDomainWithPositions CurrentDomainWithPositions
        {
            get
            {
                return (TDomainWithPositions)WorkItem.State[CommonStateNames.CurrentItem];
            }
        }

        /// <summary>
        /// Восстанавливает состояние позиций
        /// </summary>
        protected void RestoreSavedPositions()
        {
            SetDomainPositions((Dictionary<string, TPosition>)WorkItem.State[_positionBackupStateName]);
        }

        #endregion
    }
}