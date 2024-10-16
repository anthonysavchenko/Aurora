﻿
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.List
{
    partial class ListView
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
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.typeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.creationDateTimeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.paymentDateColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.intermediaryColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.quantityWoCorrectionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sumWoCorrectionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.quantityColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sumColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.authorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.commentColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tillDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.sinceDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.disableGroupingLinkLabel = new System.Windows.Forms.LinkLabel();
            this.groupByIntermediaryLinkLabel = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 45);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.Size = new System.Drawing.Size(765, 260);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.typeColumn,
            this.creationDateTimeColumn,
            this.paymentDateColumn,
            this.numberColumn,
            this.intermediaryColumn,
            this.quantityWoCorrectionColumn,
            this.sumWoCorrectionColumn,
            this.quantityColumn,
            this.sumColumn,
            this.authorColumn,
            this.commentColumn});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.GroupFormat = "{1}";
            this._gridViewOfListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueSum", this.sumColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueSumWoCorrection", this.sumWoCorrectionColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QuantityWoCorrection", this.quantityWoCorrectionColumn, ""),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", this.quantityColumn, "")});
            this._gridViewOfListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this._gridViewOfListView.OptionsView.ShowFooter = true;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // typeColumn
            // 
            this.typeColumn.Caption = "Тип";
            this.typeColumn.FieldName = "TypeAka";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.Visible = true;
            this.typeColumn.VisibleIndex = 0;
            // 
            // creationDateTimeColumn
            // 
            this.creationDateTimeColumn.Caption = "Время создания";
            this.creationDateTimeColumn.DisplayFormat.FormatString = "dd.MM.yyyy hh:mm";
            this.creationDateTimeColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.creationDateTimeColumn.FieldName = "CreationDateTime";
            this.creationDateTimeColumn.Name = "creationDateTimeColumn";
            this.creationDateTimeColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.creationDateTimeColumn.Visible = true;
            this.creationDateTimeColumn.VisibleIndex = 1;
            // 
            // paymentDateColumn
            // 
            this.paymentDateColumn.Caption = "Дата платежа";
            this.paymentDateColumn.FieldName = "PaymentDate";
            this.paymentDateColumn.Name = "paymentDateColumn";
            this.paymentDateColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.paymentDateColumn.Visible = true;
            this.paymentDateColumn.VisibleIndex = 2;
            // 
            // numberColumn
            // 
            this.numberColumn.Caption = "Номер";
            this.numberColumn.FieldName = "Number";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.Visible = true;
            this.numberColumn.VisibleIndex = 3;
            // 
            // intermediaryColumn
            // 
            this.intermediaryColumn.Caption = "Посредник";
            this.intermediaryColumn.FieldName = "Intermediary";
            this.intermediaryColumn.Name = "intermediaryColumn";
            this.intermediaryColumn.Visible = true;
            this.intermediaryColumn.VisibleIndex = 4;
            // 
            // quantityWoCorrectionColumn
            // 
            this.quantityWoCorrectionColumn.Caption = "Количество без корректировок";
            this.quantityWoCorrectionColumn.FieldName = "QuantityWoCorrection";
            this.quantityWoCorrectionColumn.Name = "quantityWoCorrectionColumn";
            this.quantityWoCorrectionColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.quantityWoCorrectionColumn.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.quantityWoCorrectionColumn.Visible = true;
            this.quantityWoCorrectionColumn.VisibleIndex = 5;
            // 
            // sumWoCorrectionColumn
            // 
            this.sumWoCorrectionColumn.Caption = "Сумма без корректировок";
            this.sumWoCorrectionColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.sumWoCorrectionColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumWoCorrectionColumn.FieldName = "ValueSumWoCorrection";
            this.sumWoCorrectionColumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumWoCorrectionColumn.Name = "sumWoCorrectionColumn";
            this.sumWoCorrectionColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueSumWoCorrection", "{0:0.00}")});
            this.sumWoCorrectionColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.sumWoCorrectionColumn.Visible = true;
            this.sumWoCorrectionColumn.VisibleIndex = 6;
            // 
            // quantityColumn
            // 
            this.quantityColumn.Caption = "Общее количество";
            this.quantityColumn.FieldName = "Quantity";
            this.quantityColumn.Name = "quantityColumn";
            this.quantityColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.quantityColumn.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.quantityColumn.Visible = true;
            this.quantityColumn.VisibleIndex = 7;
            // 
            // sumColumn
            // 
            this.sumColumn.Caption = "Общая сумма";
            this.sumColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.sumColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumColumn.FieldName = "ValueSum";
            this.sumColumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumColumn.Name = "sumColumn";
            this.sumColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueSum", "{0:0.00}")});
            this.sumColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.sumColumn.Visible = true;
            this.sumColumn.VisibleIndex = 8;
            // 
            // authorColumn
            // 
            this.authorColumn.Caption = "Автор";
            this.authorColumn.FieldName = "AuthorAka";
            this.authorColumn.Name = "authorColumn";
            this.authorColumn.Visible = true;
            this.authorColumn.VisibleIndex = 9;
            // 
            // commentColumn
            // 
            this.commentColumn.Caption = "Комментарий";
            this.commentColumn.FieldName = "Comment";
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.commentColumn.Visible = true;
            this.commentColumn.VisibleIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tillDateEdit);
            this.groupBox1.Controls.Add(this.sinceDateEdit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 45);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Дата создания с:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "по:";
            // 
            // tillDateEdit
            // 
            this.tillDateEdit.EditValue = null;
            this.tillDateEdit.Location = new System.Drawing.Point(266, 16);
            this.tillDateEdit.Name = "tillDateEdit";
            this.tillDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tillDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tillDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.tillDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.tillDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.tillDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.EditFormat.FormatString = "g";
            this.tillDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.Mask.EditMask = "g";
            this.tillDateEdit.Size = new System.Drawing.Size(120, 20);
            this.tillDateEdit.TabIndex = 1;
            // 
            // sinceDateEdit
            // 
            this.sinceDateEdit.EditValue = null;
            this.sinceDateEdit.Location = new System.Drawing.Point(108, 16);
            this.sinceDateEdit.Name = "sinceDateEdit";
            this.sinceDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sinceDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sinceDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.sinceDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.sinceDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.sinceDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.EditFormat.FormatString = "g";
            this.sinceDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.Mask.EditMask = "g";
            this.sinceDateEdit.Size = new System.Drawing.Size(120, 20);
            this.sinceDateEdit.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.disableGroupingLinkLabel);
            this.groupBox2.Controls.Add(this.groupByIntermediaryLinkLabel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(572, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 45);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Группировка";
            // 
            // disableGroupingLinkLabel
            // 
            this.disableGroupingLinkLabel.AutoSize = true;
            this.disableGroupingLinkLabel.Location = new System.Drawing.Point(95, 23);
            this.disableGroupingLinkLabel.Name = "disableGroupingLinkLabel";
            this.disableGroupingLinkLabel.Size = new System.Drawing.Size(93, 13);
            this.disableGroupingLinkLabel.TabIndex = 1;
            this.disableGroupingLinkLabel.TabStop = true;
            this.disableGroupingLinkLabel.Text = "Без группировки";
            this.disableGroupingLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.disableGroupingLinkLabel_LinkClicked);
            // 
            // groupByIntermediaryLinkLabel
            // 
            this.groupByIntermediaryLinkLabel.AutoSize = true;
            this.groupByIntermediaryLinkLabel.Location = new System.Drawing.Point(6, 22);
            this.groupByIntermediaryLinkLabel.Name = "groupByIntermediaryLinkLabel";
            this.groupByIntermediaryLinkLabel.Size = new System.Drawing.Size(83, 13);
            this.groupByIntermediaryLinkLabel.TabIndex = 0;
            this.groupByIntermediaryLinkLabel.TabStop = true;
            this.groupByIntermediaryLinkLabel.Text = "По посреднику";
            this.groupByIntermediaryLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.groupByIntermediaryLinkLabel_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 45);
            this.panel1.TabIndex = 5;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Controls.Add(this.panel1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn typeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn creationDateTimeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn numberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn intermediaryColumn;
        private DevExpress.XtraGrid.Columns.GridColumn quantityWoCorrectionColumn;
        private DevExpress.XtraGrid.Columns.GridColumn sumWoCorrectionColumn;
        private DevExpress.XtraGrid.Columns.GridColumn quantityColumn;
        private DevExpress.XtraGrid.Columns.GridColumn sumColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit tillDateEdit;
        private DevExpress.XtraEditors.DateEdit sinceDateEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel groupByIntermediaryLinkLabel;
        private System.Windows.Forms.LinkLabel disableGroupingLinkLabel;
        private DevExpress.XtraGrid.Columns.GridColumn commentColumn;
        private DevExpress.XtraGrid.Columns.GridColumn authorColumn;
        private DevExpress.XtraGrid.Columns.GridColumn paymentDateColumn;
        private System.Windows.Forms.Label label1;
    }
}