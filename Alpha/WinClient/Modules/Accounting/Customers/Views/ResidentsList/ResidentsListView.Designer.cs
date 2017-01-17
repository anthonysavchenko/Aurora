
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    partial class ResidentsListView
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
            this.gridControlOfResidentsListView = new DevExpress.XtraGrid.GridControl();
            this.gridViewOfResidentsListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.surnameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.firstNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.patronymicColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BenefitTypeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BenefitTypeRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.residentDocumentColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ownerRelationshipColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ownerRelationshipRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.residentsCountLabel = new System.Windows.Forms.Label();
            this.benefitResidentsCountLabel = new System.Windows.Forms.Label();
            this.nonbenefitResidentsCountLabel = new System.Windows.Forms.Label();
            this.ResidentsCountLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.BenefitResidentsCountLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.NonbenefitResidentsCountLabelControl = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfResidentsListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfResidentsListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BenefitTypeRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ownerRelationshipRepositoryItemLookUpEdit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlOfResidentsListView
            // 
            this.gridControlOfResidentsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfResidentsListView.Location = new System.Drawing.Point(0, 0);
            this.gridControlOfResidentsListView.MainView = this.gridViewOfResidentsListView;
            this.gridControlOfResidentsListView.Name = "gridControlOfResidentsListView";
            this.gridControlOfResidentsListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.BenefitTypeRepositoryItemLookUpEdit,
            this.ownerRelationshipRepositoryItemLookUpEdit});
            this.gridControlOfResidentsListView.Size = new System.Drawing.Size(707, 178);
            this.gridControlOfResidentsListView.TabIndex = 0;
            this.gridControlOfResidentsListView.UseEmbeddedNavigator = true;
            this.gridControlOfResidentsListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOfResidentsListView});
            this.gridControlOfResidentsListView.DataSourceChanged += new System.EventHandler(this.gridControlOfResidentsListView_DataSourceChanged);
            // 
            // gridViewOfResidentsListView
            // 
            this.gridViewOfResidentsListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewOfResidentsListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewOfResidentsListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDColumn,
            this.surnameColumn,
            this.firstNameColumn,
            this.patronymicColumn,
            this.BenefitTypeColumn,
            this.residentDocumentColumn,
            this.ownerRelationshipColumn});
            this.gridViewOfResidentsListView.GridControl = this.gridControlOfResidentsListView;
            this.gridViewOfResidentsListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewOfResidentsListView.Name = "gridViewOfResidentsListView";
            this.gridViewOfResidentsListView.OptionsView.ShowGroupPanel = false;
            this.gridViewOfResidentsListView.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewOfResidentsListView_InitNewRow);
            // 
            // IDColumn
            // 
            this.IDColumn.Caption = "ID";
            this.IDColumn.FieldName = "ID";
            this.IDColumn.Name = "IDColumn";
            // 
            // surnameColumn
            // 
            this.surnameColumn.Caption = "Фамилия";
            this.surnameColumn.FieldName = "Surname";
            this.surnameColumn.Name = "surnameColumn";
            this.surnameColumn.Visible = true;
            this.surnameColumn.VisibleIndex = 0;
            // 
            // firstNameColumn
            // 
            this.firstNameColumn.Caption = "Имя";
            this.firstNameColumn.FieldName = "FirstName";
            this.firstNameColumn.Name = "firstNameColumn";
            this.firstNameColumn.Visible = true;
            this.firstNameColumn.VisibleIndex = 1;
            // 
            // patronymicColumn
            // 
            this.patronymicColumn.Caption = "Отчество";
            this.patronymicColumn.FieldName = "Patronymic";
            this.patronymicColumn.Name = "patronymicColumn";
            this.patronymicColumn.Visible = true;
            this.patronymicColumn.VisibleIndex = 2;
            // 
            // BenefitTypeColumn
            // 
            this.BenefitTypeColumn.Caption = "Льгота";
            this.BenefitTypeColumn.ColumnEdit = this.BenefitTypeRepositoryItemLookUpEdit;
            this.BenefitTypeColumn.FieldName = "BenefitType";
            this.BenefitTypeColumn.Name = "BenefitTypeColumn";
            this.BenefitTypeColumn.Visible = true;
            this.BenefitTypeColumn.VisibleIndex = 3;
            // 
            // BenefitTypeRepositoryItemLookUpEdit
            // 
            this.BenefitTypeRepositoryItemLookUpEdit.AutoHeight = false;
            this.BenefitTypeRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BenefitTypeRepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Вид льготы")});
            this.BenefitTypeRepositoryItemLookUpEdit.DisplayMember = "Name";
            this.BenefitTypeRepositoryItemLookUpEdit.Name = "BenefitTypeRepositoryItemLookUpEdit";
            this.BenefitTypeRepositoryItemLookUpEdit.NullText = "";
            this.BenefitTypeRepositoryItemLookUpEdit.ValueMember = "ID";
            // 
            // residentDocumentColumn
            // 
            this.residentDocumentColumn.Caption = "Документ";
            this.residentDocumentColumn.FieldName = "ResidentDocument";
            this.residentDocumentColumn.Name = "residentDocumentColumn";
            this.residentDocumentColumn.Visible = true;
            this.residentDocumentColumn.VisibleIndex = 4;
            // 
            // ownerRelationshipColumn
            // 
            this.ownerRelationshipColumn.Caption = "Связь с собственником";
            this.ownerRelationshipColumn.ColumnEdit = this.ownerRelationshipRepositoryItemLookUpEdit;
            this.ownerRelationshipColumn.FieldName = "OwnerRelationship";
            this.ownerRelationshipColumn.Name = "ownerRelationshipColumn";
            this.ownerRelationshipColumn.Visible = true;
            this.ownerRelationshipColumn.VisibleIndex = 5;
            // 
            // ownerRelationshipRepositoryItemLookUpEdit
            // 
            this.ownerRelationshipRepositoryItemLookUpEdit.AutoHeight = false;
            this.ownerRelationshipRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ownerRelationshipRepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Связь")});
            this.ownerRelationshipRepositoryItemLookUpEdit.DisplayMember = "Name";
            this.ownerRelationshipRepositoryItemLookUpEdit.Name = "ownerRelationshipRepositoryItemLookUpEdit";
            this.ownerRelationshipRepositoryItemLookUpEdit.ValueMember = "ID";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.residentsCountLabel);
            this.panel1.Controls.Add(this.benefitResidentsCountLabel);
            this.panel1.Controls.Add(this.nonbenefitResidentsCountLabel);
            this.panel1.Controls.Add(this.ResidentsCountLabelControl);
            this.panel1.Controls.Add(this.BenefitResidentsCountLabelControl);
            this.panel1.Controls.Add(this.NonbenefitResidentsCountLabelControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 25);
            this.panel1.TabIndex = 1;
            // 
            // residentsCountLabel
            // 
            this.residentsCountLabel.AutoSize = true;
            this.residentsCountLabel.Location = new System.Drawing.Point(296, 6);
            this.residentsCountLabel.Name = "residentsCountLabel";
            this.residentsCountLabel.Size = new System.Drawing.Size(25, 13);
            this.residentsCountLabel.TabIndex = 8;
            this.residentsCountLabel.Text = "000";
            // 
            // benefitResidentsCountLabel
            // 
            this.benefitResidentsCountLabel.AutoSize = true;
            this.benefitResidentsCountLabel.Location = new System.Drawing.Point(231, 6);
            this.benefitResidentsCountLabel.Name = "benefitResidentsCountLabel";
            this.benefitResidentsCountLabel.Size = new System.Drawing.Size(25, 13);
            this.benefitResidentsCountLabel.TabIndex = 7;
            this.benefitResidentsCountLabel.Text = "000";
            // 
            // nonbenefitResidentsCountLabel
            // 
            this.nonbenefitResidentsCountLabel.AutoSize = true;
            this.nonbenefitResidentsCountLabel.Location = new System.Drawing.Point(105, 6);
            this.nonbenefitResidentsCountLabel.Name = "nonbenefitResidentsCountLabel";
            this.nonbenefitResidentsCountLabel.Size = new System.Drawing.Size(25, 13);
            this.nonbenefitResidentsCountLabel.TabIndex = 6;
            this.nonbenefitResidentsCountLabel.Text = "000";
            // 
            // ResidentsCountLabelControl
            // 
            this.ResidentsCountLabelControl.Location = new System.Drawing.Point(262, 6);
            this.ResidentsCountLabelControl.Name = "ResidentsCountLabelControl";
            this.ResidentsCountLabelControl.Size = new System.Drawing.Size(28, 13);
            this.ResidentsCountLabelControl.TabIndex = 5;
            this.ResidentsCountLabelControl.Text = "Всего";
            // 
            // BenefitResidentsCountLabelControl
            // 
            this.BenefitResidentsCountLabelControl.Location = new System.Drawing.Point(136, 6);
            this.BenefitResidentsCountLabelControl.Name = "BenefitResidentsCountLabelControl";
            this.BenefitResidentsCountLabelControl.Size = new System.Drawing.Size(89, 13);
            this.BenefitResidentsCountLabelControl.TabIndex = 4;
            this.BenefitResidentsCountLabelControl.Text = "Имеющих льготы";
            // 
            // NonbenefitResidentsCountLabelControl
            // 
            this.NonbenefitResidentsCountLabelControl.Location = new System.Drawing.Point(3, 6);
            this.NonbenefitResidentsCountLabelControl.Name = "NonbenefitResidentsCountLabelControl";
            this.NonbenefitResidentsCountLabelControl.Size = new System.Drawing.Size(96, 13);
            this.NonbenefitResidentsCountLabelControl.TabIndex = 3;
            this.NonbenefitResidentsCountLabelControl.Text = "Не имеющих льгот";
            // 
            // ResidentsListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfResidentsListView);
            this.Controls.Add(this.panel1);
            this.Name = "ResidentsListView";
            this.Size = new System.Drawing.Size(707, 203);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfResidentsListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfResidentsListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BenefitTypeRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ownerRelationshipRepositoryItemLookUpEdit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfResidentsListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfResidentsListView;
        private DevExpress.XtraGrid.Columns.GridColumn IDColumn;
        private DevExpress.XtraGrid.Columns.GridColumn patronymicColumn;
        private DevExpress.XtraGrid.Columns.GridColumn BenefitTypeColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit BenefitTypeRepositoryItemLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn surnameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn firstNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn residentDocumentColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ownerRelationshipColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ownerRelationshipRepositoryItemLookUpEdit;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl NonbenefitResidentsCountLabelControl;
        private DevExpress.XtraEditors.LabelControl BenefitResidentsCountLabelControl;
        private DevExpress.XtraEditors.LabelControl ResidentsCountLabelControl;
        private System.Windows.Forms.Label residentsCountLabel;
        private System.Windows.Forms.Label benefitResidentsCountLabel;
        private System.Windows.Forms.Label nonbenefitResidentsCountLabel;
    }
}

