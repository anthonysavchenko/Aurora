using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue
{
    [SmartPart]
    public partial class CounterValueView : BaseSimpleListView, ICounterValueView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new CounterValueViewPresenter Presenter
        {
            set => base.Presenter = value;
            get => (CounterValueViewPresenter)base.Presenter;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public CounterValueView()
        {
            InitializeComponent();
            Initialize(counterValueGridControl, counterValueGridView, "ID", true);
        }

        #region Implementation of ICounterValueView

        /// <summary>
        /// Период
        /// </summary>
        public DateTime Period
        {
            get
            {
                DateTime _period = GetBaseSimpleListViewMapper.ViewToDomainSimpleType<DateTime>(counterValueGridView, "Period");
                return new DateTime(_period.Year, _period.Month, 1);
            }
        }

        /// <summary>
        /// Значение/показание
        /// </summary>
        public decimal Value => GetBaseSimpleListViewMapper.ViewToDomainSimpleType<decimal>(counterValueGridView, "Value");

        /// <summary>
        /// Флаг показания по норме
        /// </summary>
        public bool ByNorm => GetBaseSimpleListViewMapper.ViewToDomainSimpleType<bool>(counterValueGridView, "ByNorm");

        /// <summary>
        /// Определяет доступность пользователю кнопок редактирования
        /// </summary>
        public bool NavigationButtonsEnabled
        {
            set => counterValueGridControl.EmbeddedNavigator.Enabled = value;
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(handler);
        }

        #endregion

        private void byNormRepositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _checkEdit = (CheckEdit)sender;
            if (counterValueGridView.GetFocusedRowCellValue("Value") == DBNull.Value)
            {
                DateTime? _period = counterValueGridView.GetFocusedRowCellValue("Period") as DateTime?;
                counterValueGridView.PostEditor();
                counterValueGridView.SetFocusedRowCellValue("Value", _checkEdit.Checked ? Presenter.GetNormValue(_period) : 0);
            }
        }
    }
}