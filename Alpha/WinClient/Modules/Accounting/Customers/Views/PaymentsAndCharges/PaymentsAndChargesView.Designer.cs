
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.PaymentsAndCharges
{
    partial class PaymentsAndChargesView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.SubGridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.OperationNameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OperationRepositoryItemHyperLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.LinkOperationIDGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OperationTypeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TimeCreatedGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ValueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridControlOfListView = new DevExpress.XtraGrid.GridControl();
            this.GridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PeriodGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OpeningBalanceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChargedGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rechargedGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.payableGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PayedGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.debtGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClosingBalanceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TotalBillLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.SubGridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationRepositoryItemHyperLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SubGridViewOfListView
            // 
            this.SubGridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.OperationNameGridColumn,
            this.LinkOperationIDGridColumn,
            this.OperationTypeGridColumn,
            this.TimeCreatedGridColumn,
            this.ValueGridColumn});
            this.SubGridViewOfListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.SubGridViewOfListView.GridControl = this.GridControlOfListView;
            this.SubGridViewOfListView.Name = "SubGridViewOfListView";
            this.SubGridViewOfListView.OptionsBehavior.Editable = false;
            this.SubGridViewOfListView.OptionsDetail.ShowDetailTabs = false;
            this.SubGridViewOfListView.OptionsSelection.UseIndicatorForSelection = false;
            this.SubGridViewOfListView.OptionsView.ShowGroupPanel = false;
            this.SubGridViewOfListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.TimeCreatedGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.OperationTypeGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.SubGridViewOfListView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.SubGridViewOfListView_RowCellClick);
            this.SubGridViewOfListView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.SubGridViewOfListView_RowStyle);
            // 
            // OperationNameGridColumn
            // 
            this.OperationNameGridColumn.Caption = "Операция";
            this.OperationNameGridColumn.ColumnEdit = this.OperationRepositoryItemHyperLinkEdit;
            this.OperationNameGridColumn.FieldName = "OperationName";
            this.OperationNameGridColumn.Name = "OperationNameGridColumn";
            this.OperationNameGridColumn.OptionsColumn.AllowShowHide = false;
            this.OperationNameGridColumn.Visible = true;
            this.OperationNameGridColumn.VisibleIndex = 0;
            // 
            // OperationRepositoryItemHyperLinkEdit
            // 
            this.OperationRepositoryItemHyperLinkEdit.AutoHeight = false;
            this.OperationRepositoryItemHyperLinkEdit.Name = "OperationRepositoryItemHyperLinkEdit";
            // 
            // LinkOperationIDGridColumn
            // 
            this.LinkOperationIDGridColumn.Caption = "LinkOperationID";
            this.LinkOperationIDGridColumn.FieldName = "LinkOperationID";
            this.LinkOperationIDGridColumn.Name = "LinkOperationIDGridColumn";
            // 
            // OperationTypeGridColumn
            // 
            this.OperationTypeGridColumn.Caption = "OperationType";
            this.OperationTypeGridColumn.FieldName = "OperationType";
            this.OperationTypeGridColumn.Name = "OperationTypeGridColumn";
            // 
            // TimeCreatedGridColumn
            // 
            this.TimeCreatedGridColumn.Caption = "Дата";
            this.TimeCreatedGridColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.TimeCreatedGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeCreatedGridColumn.FieldName = "DateCreated";
            this.TimeCreatedGridColumn.Name = "TimeCreatedGridColumn";
            this.TimeCreatedGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.TimeCreatedGridColumn.Visible = true;
            this.TimeCreatedGridColumn.VisibleIndex = 1;
            // 
            // ValueGridColumn
            // 
            this.ValueGridColumn.Caption = "Сумма";
            this.ValueGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.ValueGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ValueGridColumn.FieldName = "Value";
            this.ValueGridColumn.Name = "ValueGridColumn";
            this.ValueGridColumn.Visible = true;
            this.ValueGridColumn.VisibleIndex = 2;
            // 
            // GridControlOfListView
            // 
            this.GridControlOfListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.LevelTemplate = this.SubGridViewOfListView;
            gridLevelNode1.RelationName = "Level1";
            this.GridControlOfListView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridControlOfListView.Location = new System.Drawing.Point(3, 46);
            this.GridControlOfListView.MainView = this.GridViewOfListView;
            this.GridControlOfListView.Name = "GridControlOfListView";
            this.GridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.OperationRepositoryItemHyperLinkEdit});
            this.GridControlOfListView.Size = new System.Drawing.Size(710, 409);
            this.GridControlOfListView.TabIndex = 1;
            this.GridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewOfListView,
            this.SubGridViewOfListView});
            // 
            // GridViewOfListView
            // 
            this.GridViewOfListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewOfListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PeriodGridColumn,
            this.OpeningBalanceGridColumn,
            this.ChargedGridColumn,
            this.benefitGridColumn,
            this.actGridColumn,
            this.rechargedGridColumn,
            this.payableGridColumn,
            this.PayedGridColumn,
            this.debtGridColumn,
            this.ClosingBalanceGridColumn});
            this.GridViewOfListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.GridViewOfListView.GridControl = this.GridControlOfListView;
            this.GridViewOfListView.GroupFormat = "{1}";
            this.GridViewOfListView.Name = "GridViewOfListView";
            this.GridViewOfListView.OptionsBehavior.Editable = false;
            this.GridViewOfListView.OptionsDetail.ShowDetailTabs = false;
            this.GridViewOfListView.OptionsView.ShowGroupPanel = false;
            // 
            // PeriodGridColumn
            // 
            this.PeriodGridColumn.Caption = "Период";
            this.PeriodGridColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.PeriodGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.PeriodGridColumn.FieldName = "Period";
            this.PeriodGridColumn.Name = "PeriodGridColumn";
            this.PeriodGridColumn.Visible = true;
            this.PeriodGridColumn.VisibleIndex = 0;
            // 
            // OpeningBalanceGridColumn
            // 
            this.OpeningBalanceGridColumn.Caption = "Входящее сальдо";
            this.OpeningBalanceGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.OpeningBalanceGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.OpeningBalanceGridColumn.FieldName = "OpeningBalance";
            this.OpeningBalanceGridColumn.Name = "OpeningBalanceGridColumn";
            this.OpeningBalanceGridColumn.Visible = true;
            this.OpeningBalanceGridColumn.VisibleIndex = 1;
            // 
            // ChargedGridColumn
            // 
            this.ChargedGridColumn.Caption = "Начислено";
            this.ChargedGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.ChargedGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ChargedGridColumn.FieldName = "Charged";
            this.ChargedGridColumn.Name = "ChargedGridColumn";
            this.ChargedGridColumn.Visible = true;
            this.ChargedGridColumn.VisibleIndex = 2;
            // 
            // benefitGridColumn
            // 
            this.benefitGridColumn.Caption = "Льготы";
            this.benefitGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.benefitGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.benefitGridColumn.FieldName = "Benefit";
            this.benefitGridColumn.Name = "benefitGridColumn";
            this.benefitGridColumn.Visible = true;
            this.benefitGridColumn.VisibleIndex = 3;
            // 
            // actGridColumn
            // 
            this.actGridColumn.Caption = "Акты";
            this.actGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.actGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.actGridColumn.FieldName = "Act";
            this.actGridColumn.Name = "actGridColumn";
            this.actGridColumn.Visible = true;
            this.actGridColumn.VisibleIndex = 4;
            // 
            // rechargedGridColumn
            // 
            this.rechargedGridColumn.Caption = "Перерасчеты";
            this.rechargedGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.rechargedGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rechargedGridColumn.FieldName = "Recharged";
            this.rechargedGridColumn.Name = "rechargedGridColumn";
            this.rechargedGridColumn.Visible = true;
            this.rechargedGridColumn.VisibleIndex = 5;
            // 
            // payableGridColumn
            // 
            this.payableGridColumn.Caption = "К оплате";
            this.payableGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.payableGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.payableGridColumn.FieldName = "Payable";
            this.payableGridColumn.Name = "payableGridColumn";
            this.payableGridColumn.Visible = true;
            this.payableGridColumn.VisibleIndex = 6;
            // 
            // PayedGridColumn
            // 
            this.PayedGridColumn.Caption = "Оплачено";
            this.PayedGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.PayedGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PayedGridColumn.FieldName = "Payed";
            this.PayedGridColumn.Name = "PayedGridColumn";
            this.PayedGridColumn.Visible = true;
            this.PayedGridColumn.VisibleIndex = 7;
            // 
            // debtGridColumn
            // 
            this.debtGridColumn.Caption = "Долг";
            this.debtGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.debtGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.debtGridColumn.FieldName = "Debt";
            this.debtGridColumn.Name = "debtGridColumn";
            this.debtGridColumn.Visible = true;
            this.debtGridColumn.VisibleIndex = 8;
            // 
            // ClosingBalanceGridColumn
            // 
            this.ClosingBalanceGridColumn.Caption = "Исходящее сальдо";
            this.ClosingBalanceGridColumn.DisplayFormat.FormatString = "{0:N2}";
            this.ClosingBalanceGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ClosingBalanceGridColumn.FieldName = "ClosingBalance";
            this.ClosingBalanceGridColumn.Name = "ClosingBalanceGridColumn";
            this.ClosingBalanceGridColumn.Visible = true;
            this.ClosingBalanceGridColumn.VisibleIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TotalBillLinkLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(716, 40);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // TotalBillLinkLabel
            // 
            this.TotalBillLinkLabel.AutoSize = true;
            this.TotalBillLinkLabel.Location = new System.Drawing.Point(13, 16);
            this.TotalBillLinkLabel.Name = "TotalBillLinkLabel";
            this.TotalBillLinkLabel.Size = new System.Drawing.Size(119, 13);
            this.TotalBillLinkLabel.TabIndex = 3;
            this.TotalBillLinkLabel.TabStop = true;
            this.TotalBillLinkLabel.Text = "Квитанция на доплату";
            this.TotalBillLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TotalBillLinkLabel_LinkClicked);
            // 
            // PaymentsAndChargesView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GridControlOfListView);
            this.Name = "PaymentsAndChargesView";
            this.Size = new System.Drawing.Size(716, 458);
            ((System.ComponentModel.ISupportInitialize)(this.SubGridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationRepositoryItemHyperLinkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl GridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn OpeningBalanceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ClosingBalanceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PeriodGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ChargedGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PayedGridColumn;
        private DevExpress.XtraGrid.Views.Grid.GridView SubGridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn OperationNameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn TimeCreatedGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ValueGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit OperationRepositoryItemHyperLinkEdit;
        private DevExpress.XtraGrid.Columns.GridColumn OperationIDGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn OperationTypeGridColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel TotalBillLinkLabel;
        private DevExpress.XtraGrid.Columns.GridColumn LinkOperationIDGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn actGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rechargedGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn payableGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn debtGridColumn;



    }
}

