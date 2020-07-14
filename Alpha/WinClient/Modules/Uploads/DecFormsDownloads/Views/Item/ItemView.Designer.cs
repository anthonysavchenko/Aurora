namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Item
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.SubGridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.EmailColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FileNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AttachmentDescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridControlOfListView = new DevExpress.XtraGrid.GridControl();
            this.GridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ReceivedColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SubjectColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FromAddressColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmailDescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SubGridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // SubGridViewOfListView
            // 
            this.SubGridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EmailColumn,
            this.FileNameColumn,
            this.AttachmentDescriptionColumn});
            this.SubGridViewOfListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.SubGridViewOfListView.GridControl = this.GridControlOfListView;
            this.SubGridViewOfListView.Name = "SubGridViewOfListView";
            this.SubGridViewOfListView.OptionsBehavior.Editable = false;
            this.SubGridViewOfListView.OptionsDetail.ShowDetailTabs = false;
            this.SubGridViewOfListView.OptionsSelection.UseIndicatorForSelection = false;
            this.SubGridViewOfListView.OptionsView.ShowGroupPanel = false;
            // 
            // EmailColumn
            // 
            this.EmailColumn.Caption = "Email";
            this.EmailColumn.FieldName = "Email";
            this.EmailColumn.Name = "EmailColumn";
            // 
            // FileNameColumn
            // 
            this.FileNameColumn.Caption = "Имя файла";
            this.FileNameColumn.FieldName = "FileName";
            this.FileNameColumn.Name = "FileNameColumn";
            this.FileNameColumn.Visible = true;
            this.FileNameColumn.VisibleIndex = 0;
            // 
            // AttachmentDescriptionColumn
            // 
            this.AttachmentDescriptionColumn.Caption = "Результат обработки";
            this.AttachmentDescriptionColumn.FieldName = "AttachmentDescription";
            this.AttachmentDescriptionColumn.Name = "AttachmentDescriptionColumn";
            this.AttachmentDescriptionColumn.Visible = true;
            this.AttachmentDescriptionColumn.VisibleIndex = 1;
            // 
            // GridControlOfListView
            // 
            this.GridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.SubGridViewOfListView;
            gridLevelNode1.RelationName = "Level1";
            this.GridControlOfListView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridControlOfListView.Location = new System.Drawing.Point(0, 0);
            this.GridControlOfListView.MainView = this.GridViewOfListView;
            this.GridControlOfListView.Name = "GridControlOfListView";
            this.GridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.GridControlOfListView.Size = new System.Drawing.Size(765, 305);
            this.GridControlOfListView.TabIndex = 2;
            this.GridControlOfListView.TabStop = false;
            this.GridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewOfListView,
            this.SubGridViewOfListView});
            // 
            // GridViewOfListView
            // 
            this.GridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.ReceivedColumn,
            this.SubjectColumn,
            this.FromAddressColumn,
            this.EmailDescriptionColumn});
            this.GridViewOfListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Silver;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = true;
            this.GridViewOfListView.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.GridViewOfListView.GridControl = this.GridControlOfListView;
            this.GridViewOfListView.Name = "GridViewOfListView";
            this.GridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.GridViewOfListView.OptionsBehavior.Editable = false;
            this.GridViewOfListView.OptionsDetail.ShowDetailTabs = false;
            this.GridViewOfListView.OptionsView.ShowGroupPanel = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // ReceivedColumn
            // 
            this.ReceivedColumn.Caption = "Время получения";
            this.ReceivedColumn.DisplayFormat.FormatString = "dd:MM:yyyy HH:mm:ss";
            this.ReceivedColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ReceivedColumn.FieldName = "Received";
            this.ReceivedColumn.Name = "ReceivedColumn";
            this.ReceivedColumn.Visible = true;
            this.ReceivedColumn.VisibleIndex = 0;
            // 
            // SubjectColumn
            // 
            this.SubjectColumn.Caption = "Тема";
            this.SubjectColumn.FieldName = "Subject";
            this.SubjectColumn.Name = "SubjectColumn";
            this.SubjectColumn.Visible = true;
            this.SubjectColumn.VisibleIndex = 1;
            // 
            // FromAddressColumn
            // 
            this.FromAddressColumn.Caption = "Отправитель";
            this.FromAddressColumn.FieldName = "FromAddress";
            this.FromAddressColumn.Name = "FromAddressColumn";
            this.FromAddressColumn.Visible = true;
            this.FromAddressColumn.VisibleIndex = 2;
            // 
            // EmailDescriptionColumn
            // 
            this.EmailDescriptionColumn.Caption = "Результат обработки";
            this.EmailDescriptionColumn.FieldName = "EmailDescription";
            this.EmailDescriptionColumn.Name = "EmailDescriptionColumn";
            this.EmailDescriptionColumn.Visible = true;
            this.EmailDescriptionColumn.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ReadOnly = true;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControlOfListView);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this.SubGridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl GridControlOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn FromAddressColumn;
        private DevExpress.XtraGrid.Columns.GridColumn SubjectColumn;
        private DevExpress.XtraGrid.Columns.GridColumn EmailDescriptionColumn;
        private DevExpress.XtraGrid.Views.Grid.GridView SubGridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ReceivedColumn;
        private DevExpress.XtraGrid.Columns.GridColumn EmailColumn;
        private DevExpress.XtraGrid.Columns.GridColumn FileNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn AttachmentDescriptionColumn;
    }
}