using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    [SmartPart]
    public partial class ResidentsListView : BaseSimpleListView, IResidentsListView
    {
        public ResidentsListView()
        {
            InitializeComponent();
            Initialize(gridControlOfResidentsListView, gridViewOfResidentsListView, "ID", true);
        }

        [CreateNew]
        public new ResidentsListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ResidentsListViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Типы льгот
        /// </summary>
        public DataTable BenefitTypes
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfResidentsListView, "BenefitType");
            }
        }

        /// <summary>
        /// Виды связей с собственником
        /// </summary>
        public DataTable OwnerRelationships
        {
            set
            {
                GetBaseSimpleListViewMapper.DomainToView(value, gridViewOfResidentsListView, "OwnerRelationship");
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(gridViewOfResidentsListView, "Surname");
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(gridViewOfResidentsListView, "FirstName");
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(gridViewOfResidentsListView, "Patronymic");
            }
        }

        /// <summary>
        /// Льгота
        /// </summary>
        public BenefitType BenefitType
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain<BenefitType>(gridViewOfResidentsListView, "BenefitType");
            }
        }

        /// <summary>
        /// Связь с собственником
        /// </summary>
        public byte OwnerRelationship
        {
            get
            {
                byte _id = 0;

                if (gridViewOfResidentsListView.State == GridState.Editing)
                {
                    gridViewOfResidentsListView.UpdateCurrentRow();
                }

                RepositoryItemLookUpEdit _column = (RepositoryItemLookUpEdit)gridViewOfResidentsListView.Columns["OwnerRelationship"].ColumnEdit;
                if (_column.ValueMember != null)
                {
                    object _value = _column.GetDataSourceRowByDisplayValue(gridViewOfResidentsListView.GetFocusedRowCellDisplayText("OwnerRelationship"));

                    if (_value != null)
                    {
                        _id = (byte)((DataRowView)_value).Row["ID"];
                    }
                }

                return _id;
            }
        }

        /// <summary>
        /// Документ
        /// </summary>
        public string ResidentDocument
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(gridViewOfResidentsListView, "ResidentDocument");
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

        /// <summary>
        /// Доступна ли для редактирования колонка связь с собственником
        /// </summary>
        public bool OwnerRelationshipEnabled
        {
            set
            {
                RepositoryItemLookUpEdit _column =
                    (RepositoryItemLookUpEdit)gridViewOfResidentsListView.Columns["OwnerRelationship"].ColumnEdit;
                _column.ReadOnly = !value;
            }
        }

        /// <summary>
        /// Количество жильцов, не имеющих льготы
        /// </summary>
        public int NonbenefitResidentsCount
        {
            set
            {
                nonbenefitResidentsCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Количество жильцов, имеющих льготы
        /// </summary>
        public int BenefitResidentsCount
        {
            set
            {
                benefitResidentsCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        public int ResidentsCount
        {
            set
            {
                residentsCountLabel.Text = value.ToString();
            }
        }

        private void gridControlOfResidentsListView_DataSourceChanged(object sender, EventArgs e)
        {
            Presenter.SetResidentsCount();
        }

        private void gridViewOfResidentsListView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridViewOfResidentsListView.SetRowCellValue(e.RowHandle, gridViewOfResidentsListView.Columns["OwnerRelationship"], (byte)Resident.Relationship.Unknown);
        }
    }
}