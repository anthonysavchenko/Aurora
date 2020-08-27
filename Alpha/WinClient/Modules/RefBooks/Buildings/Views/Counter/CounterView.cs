using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Drawing;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

//using BaseSimpleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
{
    [SmartPart]
    public partial class CounterView : BaseSimpleListView, ICounterView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new CounterViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (CounterViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public CounterView()
        {
            InitializeComponent();
            Initialize(counterGridControl, counterGridView, "ID", true);
        }

        #region Implementation of ICounterView

        /// <summary>
        /// Таблица с данными услуг
        /// </summary>
        public DataTable UtilityServices
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, counterGridView, "UtilityService");
            }
        }

        /// <summary>
        /// Услуга
        /// </summary>
        public UtilityService UtilityService
        {
            get
            {
                return GetBaseSimpleListViewMapper
                    .ViewToDomainSimpleType<UtilityService>(counterGridView, "UtilityService");
            }
        }

        /// <summary>
        /// Номер счетчика
        /// </summary>
        public string CounterNumber
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(counterGridView, "CounterNumber")
                    .Trim();
            }
        }

        /// <summary>
        /// Коэффициент
        /// </summary>
        public byte Coefficient
        {
            get
            {
                return GetBaseSimpleListViewMapper
                    .ViewToDomainSimpleType<byte>(counterGridView, "Coefficient");
            } 
        }

        public DateTime? CheckedSince
        {
            get
            {
                return GetBaseSimpleListViewMapper
                    .ViewToDomainSimpleType<DateTime?>(counterGridView, "CheckedSince");
            }
        }

        public DateTime? CheckedTill
        {
            get
            {
                return GetBaseSimpleListViewMapper
                    .ViewToDomainSimpleType<DateTime?>(counterGridView, "CheckedTill");
            }
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindActivate(EventHandler handler)
        {
            Presenter.BindChangeHandlers(Controls, handler);
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void BindDeactivate(EventHandler handler)
        {
            Presenter.UnBindChangeHandlers(Controls, handler);
        }

        #endregion

        private void counterGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                object checkedTillRaw = ((DevExpress.XtraGrid.Views.Grid.GridView)sender).GetRowCellValue(
                    e.RowHandle,
                    "CheckedTill");

                DateTime? checkedTill =
                    checkedTillRaw != DBNull.Value
                        ? (DateTime?)checkedTillRaw
                        : null;

                if (checkedTill != null)
                {
                    DateTime now = Presenter.ServerTime.GetDateTimeInfo().Now;

                    if (checkedTill.Value.AddMonths(-3) < now)
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 200, 200);
                        e.Appearance.BackColor2 = Color.FromArgb(250, 200, 200);
                    }
                }
            }
        }
    }
}
