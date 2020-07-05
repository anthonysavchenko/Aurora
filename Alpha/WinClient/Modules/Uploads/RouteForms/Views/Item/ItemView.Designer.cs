namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.RouteForms.Views.Item
{
    partial class ItemView
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.Band1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.ID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.AccountColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.OwnerColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ApartmentColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CounterBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.CounterNumberColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CounterTypeColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CounterCapacityColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Band3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.DebtColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PayedColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ValueBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.PrevDateColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PrevValueColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PrevDayValueColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.PrevNightValueColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Band5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.PhoneColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.NoteColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ReadOnly = true;
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this._gridViewOfListView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._gridViewOfListView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.Band1,
            this.CounterBand,
            this.Band3,
            this.ValueBand,
            this.Band5});
            this._gridViewOfListView.ColumnPanelRowHeight = 35;
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.ID,
            this.AccountColumn,
            this.OwnerColumn,
            this.ApartmentColumn,
            this.CounterNumberColumn,
            this.CounterTypeColumn,
            this.CounterCapacityColumn,
            this.DebtColumn,
            this.PayedColumn,
            this.PrevDateColumn,
            this.PrevValueColumn,
            this.PrevDayValueColumn,
            this.PrevNightValueColumn,
            this.PhoneColumn,
            this.NoteColumn});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Silver;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = true;
            this._gridViewOfListView.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.ShowGroupPanel = false;
            // 
            // Band1
            // 
            this.Band1.Columns.Add(this.ID);
            this.Band1.Columns.Add(this.AccountColumn);
            this.Band1.Columns.Add(this.OwnerColumn);
            this.Band1.Columns.Add(this.ApartmentColumn);
            this.Band1.Name = "Band1";
            this.Band1.VisibleIndex = 0;
            this.Band1.Width = 213;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            this.ID.Width = 71;
            // 
            // AccountColumn
            // 
            this.AccountColumn.Caption = "Лицевой счет";
            this.AccountColumn.FieldName = "Account";
            this.AccountColumn.Name = "AccountColumn";
            this.AccountColumn.Visible = true;
            this.AccountColumn.Width = 71;
            // 
            // OwnerColumn
            // 
            this.OwnerColumn.Caption = "Фамилия И. О.";
            this.OwnerColumn.FieldName = "Owner";
            this.OwnerColumn.Name = "OwnerColumn";
            this.OwnerColumn.Visible = true;
            this.OwnerColumn.Width = 71;
            // 
            // ApartmentColumn
            // 
            this.ApartmentColumn.Caption = "Квартира";
            this.ApartmentColumn.FieldName = "Apartment";
            this.ApartmentColumn.Name = "ApartmentColumn";
            this.ApartmentColumn.Visible = true;
            this.ApartmentColumn.Width = 71;
            // 
            // CounterBand
            // 
            this.CounterBand.AppearanceHeader.Options.UseTextOptions = true;
            this.CounterBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CounterBand.Caption = "Прибор учета";
            this.CounterBand.Columns.Add(this.CounterNumberColumn);
            this.CounterBand.Columns.Add(this.CounterTypeColumn);
            this.CounterBand.Columns.Add(this.CounterCapacityColumn);
            this.CounterBand.Name = "CounterBand";
            this.CounterBand.VisibleIndex = 1;
            this.CounterBand.Width = 213;
            // 
            // CounterNumberColumn
            // 
            this.CounterNumberColumn.Caption = "Номер";
            this.CounterNumberColumn.FieldName = "CounterNumber";
            this.CounterNumberColumn.Name = "CounterNumberColumn";
            this.CounterNumberColumn.Visible = true;
            this.CounterNumberColumn.Width = 71;
            // 
            // CounterTypeColumn
            // 
            this.CounterTypeColumn.Caption = "Тип";
            this.CounterTypeColumn.FieldName = "CounterType";
            this.CounterTypeColumn.Name = "CounterTypeColumn";
            this.CounterTypeColumn.Visible = true;
            this.CounterTypeColumn.Width = 71;
            // 
            // CounterCapacityColumn
            // 
            this.CounterCapacityColumn.Caption = "Разрядность";
            this.CounterCapacityColumn.FieldName = "CounterCapacity";
            this.CounterCapacityColumn.Name = "CounterCapacityColumn";
            this.CounterCapacityColumn.Visible = true;
            this.CounterCapacityColumn.Width = 71;
            // 
            // Band3
            // 
            this.Band3.Columns.Add(this.DebtColumn);
            this.Band3.Columns.Add(this.PayedColumn);
            this.Band3.Name = "Band3";
            this.Band3.VisibleIndex = 2;
            this.Band3.Width = 142;
            // 
            // DebtColumn
            // 
            this.DebtColumn.Caption = "Долг";
            this.DebtColumn.FieldName = "Debt";
            this.DebtColumn.Name = "DebtColumn";
            this.DebtColumn.Visible = true;
            this.DebtColumn.Width = 71;
            // 
            // PayedColumn
            // 
            this.PayedColumn.Caption = "Последняя оплата";
            this.PayedColumn.FieldName = "Payed";
            this.PayedColumn.Name = "PayedColumn";
            this.PayedColumn.Visible = true;
            this.PayedColumn.Width = 71;
            // 
            // ValueBand
            // 
            this.ValueBand.AppearanceHeader.Options.UseTextOptions = true;
            this.ValueBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ValueBand.Caption = "Предыдущие показания";
            this.ValueBand.Columns.Add(this.PrevDateColumn);
            this.ValueBand.Columns.Add(this.PrevValueColumn);
            this.ValueBand.Columns.Add(this.PrevDayValueColumn);
            this.ValueBand.Columns.Add(this.PrevNightValueColumn);
            this.ValueBand.Name = "ValueBand";
            this.ValueBand.VisibleIndex = 3;
            this.ValueBand.Width = 284;
            // 
            // PrevDateColumn
            // 
            this.PrevDateColumn.Caption = "Дата подачи";
            this.PrevDateColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.PrevDateColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.PrevDateColumn.FieldName = "PrevDate";
            this.PrevDateColumn.Name = "PrevDateColumn";
            this.PrevDateColumn.Visible = true;
            this.PrevDateColumn.Width = 71;
            // 
            // PrevValueColumn
            // 
            this.PrevValueColumn.Caption = "Общие";
            this.PrevValueColumn.FieldName = "PrevValue";
            this.PrevValueColumn.Name = "PrevValueColumn";
            this.PrevValueColumn.Visible = true;
            this.PrevValueColumn.Width = 71;
            // 
            // PrevDayValueColumn
            // 
            this.PrevDayValueColumn.Caption = "День";
            this.PrevDayValueColumn.FieldName = "PrevDayValue";
            this.PrevDayValueColumn.Name = "PrevDayValueColumn";
            this.PrevDayValueColumn.Visible = true;
            this.PrevDayValueColumn.Width = 71;
            // 
            // PrevNightValueColumn
            // 
            this.PrevNightValueColumn.Caption = "Ночь";
            this.PrevNightValueColumn.FieldName = "PrevNightValue";
            this.PrevNightValueColumn.Name = "PrevNightValueColumn";
            this.PrevNightValueColumn.Visible = true;
            this.PrevNightValueColumn.Width = 71;
            // 
            // Band5
            // 
            this.Band5.Columns.Add(this.PhoneColumn);
            this.Band5.Columns.Add(this.NoteColumn);
            this.Band5.Name = "Band5";
            this.Band5.VisibleIndex = 4;
            this.Band5.Width = 174;
            // 
            // PhoneColumn
            // 
            this.PhoneColumn.Caption = "Телефон";
            this.PhoneColumn.FieldName = "Phone";
            this.PhoneColumn.Name = "PhoneColumn";
            this.PhoneColumn.Visible = true;
            this.PhoneColumn.Width = 71;
            // 
            // NoteColumn
            // 
            this.NoteColumn.Caption = "Примечание";
            this.NoteColumn.FieldName = "Note";
            this.NoteColumn.Name = "NoteColumn";
            this.NoteColumn.Visible = true;
            this.NoteColumn.Width = 103;
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 0);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            _gridControlOfListView.Size = new System.Drawing.Size(765, 305);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.TabStop = false;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView _gridViewOfListView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn AccountColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn OwnerColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ApartmentColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CounterNumberColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CounterTypeColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn CounterCapacityColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn DebtColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PayedColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PrevDateColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PrevValueColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PrevDayValueColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PrevNightValueColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PhoneColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NoteColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Band1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand CounterBand;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Band3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand ValueBand;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Band5;
    }
}