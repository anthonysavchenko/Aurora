namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Item
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
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FileNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ResultColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BuildingsWithNoErrorsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BuildingsWithErrorsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TableGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FileDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DirectoryPathTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DirectoryPathLabel = new System.Windows.Forms.Label();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.MonthLabel = new System.Windows.Forms.Label();
            this.MonthTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.MissingBuildingsTextBox = new System.Windows.Forms.TextBox();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.TableGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.FileNameColumn,
            this.ResultColumn,
            this.BuildingsWithNoErrorsColumn,
            this.BuildingsWithErrorsColumn,
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
            // FileNameColumn
            // 
            this.FileNameColumn.Caption = "Имя файла";
            this.FileNameColumn.FieldName = "FileName";
            this.FileNameColumn.Name = "FileNameColumn";
            this.FileNameColumn.Visible = true;
            this.FileNameColumn.VisibleIndex = 0;
            // 
            // ResultColumn
            // 
            this.ResultColumn.Caption = "Результат обработки";
            this.ResultColumn.FieldName = "Result";
            this.ResultColumn.Name = "ResultColumn";
            this.ResultColumn.Visible = true;
            this.ResultColumn.VisibleIndex = 1;
            // 
            // BuildingsWithNoErrorsColumn
            // 
            this.BuildingsWithNoErrorsColumn.Caption = "Домов без ошибок";
            this.BuildingsWithNoErrorsColumn.FieldName = "BuildingsWithNoErrors";
            this.BuildingsWithNoErrorsColumn.Name = "BuildingsWithNoErrorsColumn";
            this.BuildingsWithNoErrorsColumn.Visible = true;
            this.BuildingsWithNoErrorsColumn.VisibleIndex = 2;
            // 
            // BuildingsWithErrorsColumn
            // 
            this.BuildingsWithErrorsColumn.Caption = "Домов с ошибками";
            this.BuildingsWithErrorsColumn.FieldName = "BuildingsWithErrors";
            this.BuildingsWithErrorsColumn.Name = "BuildingsWithErrorsColumn";
            this.BuildingsWithErrorsColumn.Visible = true;
            this.BuildingsWithErrorsColumn.VisibleIndex = 3;
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
            _gridControlOfListView.Size = new System.Drawing.Size(709, 191);
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
            this.TableGroupBox.Controls.Add(this.groupBox4);
            this.TableGroupBox.Controls.Add(this.groupBox3);
            this.TableGroupBox.Controls.Add(_gridControlOfListView);
            this.TableGroupBox.Location = new System.Drawing.Point(3, 191);
            this.TableGroupBox.Name = "TableGroupBox";
            this.TableGroupBox.Size = new System.Drawing.Size(718, 369);
            this.TableGroupBox.TabIndex = 4;
            this.TableGroupBox.TabStop = false;
            this.TableGroupBox.Text = "Файлы";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.FileDescriptionTextBox);
            this.groupBox3.Location = new System.Drawing.Point(6, 291);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(709, 72);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Результат обработки выбранного файла";
            // 
            // FileDescriptionTextBox
            // 
            this.FileDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileDescriptionTextBox.Location = new System.Drawing.Point(6, 19);
            this.FileDescriptionTextBox.MaxLength = 250;
            this.FileDescriptionTextBox.Multiline = true;
            this.FileDescriptionTextBox.Name = "FileDescriptionTextBox";
            this.FileDescriptionTextBox.ReadOnly = true;
            this.FileDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FileDescriptionTextBox.Size = new System.Drawing.Size(700, 47);
            this.FileDescriptionTextBox.TabIndex = 48;
            // 
            // DirectoryPathTextBox
            // 
            this.DirectoryPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryPathTextBox.Location = new System.Drawing.Point(234, 10);
            this.DirectoryPathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DirectoryPathTextBox.Name = "DirectoryPathTextBox";
            this.DirectoryPathTextBox.ReadOnly = true;
            this.DirectoryPathTextBox.Size = new System.Drawing.Size(484, 20);
            this.DirectoryPathTextBox.TabIndex = 50;
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
            this.DescriptionTextBox.Size = new System.Drawing.Size(709, 47);
            this.DescriptionTextBox.TabIndex = 48;
            // 
            // DirectoryPathLabel
            // 
            this.DirectoryPathLabel.AutoSize = true;
            this.DirectoryPathLabel.BackColor = System.Drawing.Color.Transparent;
            this.DirectoryPathLabel.Location = new System.Drawing.Point(190, 13);
            this.DirectoryPathLabel.Name = "DirectoryPathLabel";
            this.DirectoryPathLabel.Size = new System.Drawing.Size(39, 13);
            this.DirectoryPathLabel.TabIndex = 47;
            this.DirectoryPathLabel.Text = "Папка";
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
            this.NoteTextBox.Size = new System.Drawing.Size(709, 47);
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
            this.groupBox2.Size = new System.Drawing.Size(718, 72);
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
            this.groupBox1.Size = new System.Drawing.Size(718, 72);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Комментарий";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.MissingBuildingsTextBox);
            this.groupBox4.Location = new System.Drawing.Point(6, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(709, 72);
            this.groupBox4.TabIndex = 53;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Дома, отсутствующие в файлах";
            // 
            // MissingBuildingsTextBox
            // 
            this.MissingBuildingsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MissingBuildingsTextBox.Location = new System.Drawing.Point(6, 19);
            this.MissingBuildingsTextBox.MaxLength = 250;
            this.MissingBuildingsTextBox.Multiline = true;
            this.MissingBuildingsTextBox.Name = "MissingBuildingsTextBox";
            this.MissingBuildingsTextBox.ReadOnly = true;
            this.MissingBuildingsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MissingBuildingsTextBox.Size = new System.Drawing.Size(700, 47);
            this.MissingBuildingsTextBox.TabIndex = 48;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.MonthTextBox);
            this.Controls.Add(this.DirectoryPathTextBox);
            this.Controls.Add(this.MonthLabel);
            this.Controls.Add(this.DirectoryPathLabel);
            this.Controls.Add(this.TableGroupBox);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(724, 563);
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.TableGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn FileNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn DescriptionColumn;
        private System.Windows.Forms.GroupBox TableGroupBox;
        private System.Windows.Forms.TextBox DirectoryPathTextBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label DirectoryPathLabel;
        private System.Windows.Forms.TextBox NoteTextBox;
        private System.Windows.Forms.Label MonthLabel;
        private System.Windows.Forms.TextBox MonthTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox FileDescriptionTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.Columns.GridColumn BuildingsWithNoErrorsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn BuildingsWithErrorsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ResultColumn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox MissingBuildingsTextBox;
    }
}