namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Item
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
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BuildingColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TableGroupBox = new System.Windows.Forms.GroupBox();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.MonthLabel = new System.Windows.Forms.Label();
            this.MonthTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PosDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.CounterNumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CoefficientColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CurrentValueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PrevValueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CurrentDateColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ResultColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.TableGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.BuildingColumn,
            this.CounterNumberColumn,
            this.CoefficientColumn,
            this.CurrentValueColumn,
            this.PrevValueColumn,
            this.CurrentDateColumn,
            this.ResultColumn,
            this.DescriptionColumn});
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
            this._gridViewOfListView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this._gridViewOfListView.OptionsView.ShowGroupPanel = false;
            this._gridViewOfListView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this._gridViewOfListView_FocusedRowChanged);
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // BuildingColumn
            // 
            this.BuildingColumn.Caption = "Дом";
            this.BuildingColumn.FieldName = "Building";
            this.BuildingColumn.Name = "BuildingColumn";
            this.BuildingColumn.Visible = true;
            this.BuildingColumn.VisibleIndex = 0;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Caption = "Результат обработки";
            this.DescriptionColumn.FieldName = "Description";
            this.DescriptionColumn.Name = "DescriptionColumn";
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            _gridControlOfListView.Location = new System.Drawing.Point(6, 16);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            _gridControlOfListView.Size = new System.Drawing.Size(758, 200);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.TabStop = false;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // TableGroupBox
            // 
            this.TableGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableGroupBox.Controls.Add(this.groupBox3);
            this.TableGroupBox.Controls.Add(_gridControlOfListView);
            this.TableGroupBox.Location = new System.Drawing.Point(3, 191);
            this.TableGroupBox.Name = "TableGroupBox";
            this.TableGroupBox.Size = new System.Drawing.Size(767, 300);
            this.TableGroupBox.TabIndex = 4;
            this.TableGroupBox.TabStop = false;
            this.TableGroupBox.Text = "Файлы";
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePathTextBox.Location = new System.Drawing.Point(234, 10);
            this.FilePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.ReadOnly = true;
            this.FilePathTextBox.Size = new System.Drawing.Size(533, 20);
            this.FilePathTextBox.TabIndex = 50;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTextBox.Location = new System.Drawing.Point(6, 19);
            this.DescriptionTextBox.MaxLength = 250;
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionTextBox.Size = new System.Drawing.Size(758, 47);
            this.DescriptionTextBox.TabIndex = 48;
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.DirectoryLabel.Location = new System.Drawing.Point(193, 13);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(36, 13);
            this.DirectoryLabel.TabIndex = 47;
            this.DirectoryLabel.Text = "Файл";
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteTextBox.Location = new System.Drawing.Point(6, 19);
            this.NoteTextBox.MaxLength = 250;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.ReadOnly = true;
            this.NoteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NoteTextBox.Size = new System.Drawing.Size(758, 47);
            this.NoteTextBox.TabIndex = 45;
            // 
            // MonthLabel
            // 
            this.MonthLabel.AutoSize = true;
            this.MonthLabel.BackColor = System.Drawing.Color.Transparent;
            this.MonthLabel.Location = new System.Drawing.Point(10, 13);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(90, 13);
            this.MonthLabel.TabIndex = 47;
            this.MonthLabel.Text = "Учетный период";
            // 
            // MonthTextBox
            // 
            this.MonthTextBox.Location = new System.Drawing.Point(105, 10);
            this.MonthTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.MonthTextBox.Name = "MonthTextBox";
            this.MonthTextBox.ReadOnly = true;
            this.MonthTextBox.Size = new System.Drawing.Size(70, 20);
            this.MonthTextBox.TabIndex = 50;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.DescriptionTextBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(767, 72);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Общий результат обработки";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.NoteTextBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 72);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Комментарий";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.PosDescriptionTextBox);
            this.groupBox3.Location = new System.Drawing.Point(6, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(758, 72);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Результат обработки выбранной строки";
            // 
            // PosDescriptionTextBox
            // 
            this.PosDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PosDescriptionTextBox.Location = new System.Drawing.Point(6, 19);
            this.PosDescriptionTextBox.MaxLength = 250;
            this.PosDescriptionTextBox.Multiline = true;
            this.PosDescriptionTextBox.Name = "PosDescriptionTextBox";
            this.PosDescriptionTextBox.ReadOnly = true;
            this.PosDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PosDescriptionTextBox.Size = new System.Drawing.Size(749, 47);
            this.PosDescriptionTextBox.TabIndex = 48;
            // 
            // CounterNumberColumn
            // 
            this.CounterNumberColumn.Caption = "Номер счетчика";
            this.CounterNumberColumn.FieldName = "CounterNumber";
            this.CounterNumberColumn.Name = "CounterNumberColumn";
            this.CounterNumberColumn.Visible = true;
            this.CounterNumberColumn.VisibleIndex = 1;
            // 
            // CoefficientColumn
            // 
            this.CoefficientColumn.Caption = "Коэффициент";
            this.CoefficientColumn.FieldName = "Coefficient";
            this.CoefficientColumn.Name = "CoefficientColumn";
            this.CoefficientColumn.Visible = true;
            this.CoefficientColumn.VisibleIndex = 2;
            // 
            // CurrentValueColumn
            // 
            this.CurrentValueColumn.Caption = "Текущие показания";
            this.CurrentValueColumn.FieldName = "CurrentValue";
            this.CurrentValueColumn.Name = "CurrentValueColumn";
            this.CurrentValueColumn.Visible = true;
            this.CurrentValueColumn.VisibleIndex = 3;
            // 
            // PrevValueColumn
            // 
            this.PrevValueColumn.Caption = "Предыдущие показания";
            this.PrevValueColumn.FieldName = "PrevValue";
            this.PrevValueColumn.Name = "PrevValueColumn";
            this.PrevValueColumn.Visible = true;
            this.PrevValueColumn.VisibleIndex = 4;
            // 
            // CurrentDateColumn
            // 
            this.CurrentDateColumn.Caption = "Дата снятия";
            this.CurrentDateColumn.FieldName = "CurrentDate";
            this.CurrentDateColumn.Name = "CurrentDateColumn";
            this.CurrentDateColumn.Visible = true;
            this.CurrentDateColumn.VisibleIndex = 5;
            // 
            // ResultColumn
            // 
            this.ResultColumn.Caption = "Результат обработки";
            this.ResultColumn.FieldName = "Result";
            this.ResultColumn.Name = "ResultColumn";
            this.ResultColumn.Visible = true;
            this.ResultColumn.VisibleIndex = 6;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.MonthTextBox);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.MonthLabel);
            this.Controls.Add(this.DirectoryLabel);
            this.Controls.Add(this.TableGroupBox);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(773, 494);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.TableGroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn BuildingColumn;
        private DevExpress.XtraGrid.Columns.GridColumn DescriptionColumn;
        private System.Windows.Forms.GroupBox TableGroupBox;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.TextBox NoteTextBox;
        private System.Windows.Forms.Label MonthLabel;
        private System.Windows.Forms.TextBox MonthTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox PosDescriptionTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.Columns.GridColumn CounterNumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CoefficientColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CurrentValueColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PrevValueColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CurrentDateColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ResultColumn;
    }
}