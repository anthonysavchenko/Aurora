
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.List
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
            this.periodColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.quantityColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sumColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.authorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.commentColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tillDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.sinceDateEdit = new DevExpress.XtraEditors.DateEdit();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.typeColumn,
            this.creationDateTimeColumn,
            this.periodColumn,
            this.numberColumn,
            this.quantityColumn,
            this.sumColumn,
            this.authorColumn,
            this.commentColumn});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this._gridViewOfListView.GroupFormat = "{1}";
            this._gridViewOfListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ValueSum", this.sumColumn, "Итого: {0:c2}")});
            this._gridViewOfListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
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
            this.creationDateTimeColumn.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.creationDateTimeColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.creationDateTimeColumn.FieldName = "CreationDateTime";
            this.creationDateTimeColumn.Name = "creationDateTimeColumn";
            this.creationDateTimeColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.creationDateTimeColumn.Visible = true;
            this.creationDateTimeColumn.VisibleIndex = 1;
            // 
            // periodColumn
            // 
            this.periodColumn.Caption = "Период";
            this.periodColumn.DisplayFormat.FormatString = "{0:MM.yyyy}";
            this.periodColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodColumn.FieldName = "Period";
            this.periodColumn.Name = "periodColumn";
            this.periodColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.periodColumn.Visible = true;
            this.periodColumn.VisibleIndex = 2;
            // 
            // numberColumn
            // 
            this.numberColumn.Caption = "Номер";
            this.numberColumn.FieldName = "Number";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.Visible = true;
            this.numberColumn.VisibleIndex = 3;
            // 
            // quantityColumn
            // 
            this.quantityColumn.Caption = "Количество";
            this.quantityColumn.FieldName = "Quantity";
            this.quantityColumn.Name = "quantityColumn";
            this.quantityColumn.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.quantityColumn.Visible = true;
            this.quantityColumn.VisibleIndex = 4;
            // 
            // sumColumn
            // 
            this.sumColumn.Caption = "Сумма";
            this.sumColumn.DisplayFormat.FormatString = "{0:c2}";
            this.sumColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumColumn.FieldName = "ValueSum";
            this.sumColumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sumColumn.Name = "sumColumn";
            this.sumColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.sumColumn.Visible = true;
            this.sumColumn.VisibleIndex = 5;
            // 
            // authorColumn
            // 
            this.authorColumn.Caption = "Автор";
            this.authorColumn.FieldName = "AuthorAka";
            this.authorColumn.Name = "authorColumn";
            this.authorColumn.Visible = true;
            this.authorColumn.VisibleIndex = 6;
            // 
            // commentColumn
            // 
            this.commentColumn.Caption = "Комментарий";
            this.commentColumn.FieldName = "Comment";
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.commentColumn.Visible = true;
            this.commentColumn.VisibleIndex = 7;
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 44);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.Size = new System.Drawing.Size(765, 261);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tillDateEdit);
            this.groupBox1.Controls.Add(this.sinceDateEdit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 44);
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
            this.label1.TabIndex = 4;
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
            this.tillDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.tillDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.EditFormat.FormatString = "g";
            this.tillDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.Mask.EditMask = "g";
            this.tillDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
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
            this.sinceDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.sinceDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.EditFormat.FormatString = "g";
            this.sinceDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.Mask.EditMask = "g";
            this.sinceDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sinceDateEdit.Size = new System.Drawing.Size(120, 20);
            this.sinceDateEdit.TabIndex = 0;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn typeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn creationDateTimeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn numberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn quantityColumn;
        private DevExpress.XtraGrid.Columns.GridColumn sumColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit tillDateEdit;
        private DevExpress.XtraEditors.DateEdit sinceDateEdit;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn commentColumn;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn periodColumn;
        private DevExpress.XtraGrid.Columns.GridColumn authorColumn;
    }
}