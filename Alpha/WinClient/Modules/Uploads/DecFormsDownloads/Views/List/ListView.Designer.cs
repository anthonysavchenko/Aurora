
namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.List
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
            this.NumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CreatedColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AuthorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmailsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FilesColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ErrorsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NoteColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tillDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.sinceDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.NumberColumn,
            this.CreatedColumn,
            this.AuthorColumn,
            this.EmailsColumn,
            this.FilesColumn,
            this.ErrorsColumn,
            this.NoteColumn,
            this.DescriptionColumn});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.GroupFormat = "";
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.ShowGroupPanel = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // NumberColumn
            // 
            this.NumberColumn.Caption = "Номер";
            this.NumberColumn.FieldName = "Number";
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.Visible = true;
            this.NumberColumn.VisibleIndex = 0;
            // 
            // CreatedColumn
            // 
            this.CreatedColumn.Caption = "Время загрузки";
            this.CreatedColumn.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            this.CreatedColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.CreatedColumn.FieldName = "Created";
            this.CreatedColumn.Name = "CreatedColumn";
            this.CreatedColumn.Visible = true;
            this.CreatedColumn.VisibleIndex = 1;
            // 
            // AuthorColumn
            // 
            this.AuthorColumn.Caption = "Автор";
            this.AuthorColumn.FieldName = "Author";
            this.AuthorColumn.Name = "AuthorColumn";
            this.AuthorColumn.Visible = true;
            this.AuthorColumn.VisibleIndex = 2;
            // 
            // EmailsColumn
            // 
            this.EmailsColumn.Caption = "Прочитано писем";
            this.EmailsColumn.FieldName = "Emails";
            this.EmailsColumn.Name = "EmailsColumn";
            this.EmailsColumn.Visible = true;
            this.EmailsColumn.VisibleIndex = 3;
            // 
            // FilesColumn
            // 
            this.FilesColumn.Caption = "Скачано файлов";
            this.FilesColumn.FieldName = "Files";
            this.FilesColumn.Name = "FilesColumn";
            this.FilesColumn.Visible = true;
            this.FilesColumn.VisibleIndex = 4;
            // 
            // ErrorsColumn
            // 
            this.ErrorsColumn.Caption = "Произошло ошибок";
            this.ErrorsColumn.FieldName = "Errors";
            this.ErrorsColumn.Name = "ErrorsColumn";
            this.ErrorsColumn.Visible = true;
            this.ErrorsColumn.VisibleIndex = 5;
            // 
            // NoteColumn
            // 
            this.NoteColumn.Caption = "Комментарий";
            this.NoteColumn.FieldName = "Note";
            this.NoteColumn.Name = "NoteColumn";
            this.NoteColumn.Visible = true;
            this.NoteColumn.VisibleIndex = 6;
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 65);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.Size = new System.Drawing.Size(765, 240);
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
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Дата загрузки с:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "по:";
            // 
            // tillDateEdit
            // 
            this.tillDateEdit.EditValue = null;
            this.tillDateEdit.Location = new System.Drawing.Point(292, 25);
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
            this.sinceDateEdit.Location = new System.Drawing.Point(134, 25);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 65);
            this.panel1.TabIndex = 5;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Caption = "Результат обработки";
            this.DescriptionColumn.FieldName = "Description";
            this.DescriptionColumn.Name = "DescriptionColumn";
            this.DescriptionColumn.Visible = true;
            this.DescriptionColumn.VisibleIndex = 7;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Controls.Add(this.panel1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn EmailsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CreatedColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit tillDateEdit;
        private DevExpress.XtraEditors.DateEdit sinceDateEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn NumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn AuthorColumn;
        private DevExpress.XtraGrid.Columns.GridColumn NoteColumn;
        private DevExpress.XtraGrid.Columns.GridColumn FilesColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ErrorsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn DescriptionColumn;
    }
}