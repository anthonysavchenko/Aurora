namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Form
{
    partial class FormView
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
            this.paymentPosGridControl = new DevExpress.XtraGrid.GridControl();
            this.FormGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BuildingGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CounterGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CurrentValueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PrevValueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CoefficientGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ResultGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DescriptionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PaymentTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.RowDescriptionTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.paymentPosGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormGridView)).BeginInit();
            this.PaymentTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentPosGridControl
            // 
            this.paymentPosGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paymentPosGridControl.Location = new System.Drawing.Point(3, 3);
            this.paymentPosGridControl.MainView = this.FormGridView;
            this.paymentPosGridControl.Name = "paymentPosGridControl";
            this.paymentPosGridControl.Size = new System.Drawing.Size(570, 321);
            this.paymentPosGridControl.TabIndex = 0;
            this.paymentPosGridControl.TabStop = false;
            this.paymentPosGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.FormGridView});
            // 
            // FormGridView
            // 
            this.FormGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.BuildingGridColumn,
            this.CounterGridColumn,
            this.CurrentValueGridColumn,
            this.PrevValueGridColumn,
            this.CoefficientGridColumn,
            this.ResultGridColumn,
            this.DescriptionColumn});
            this.FormGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.FormGridView.GridControl = this.paymentPosGridControl;
            this.FormGridView.Name = "FormGridView";
            this.FormGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.FormGridView.OptionsBehavior.Editable = false;
            this.FormGridView.OptionsCustomization.AllowGroup = false;
            this.FormGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.FormGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.FormGridView.OptionsView.ShowGroupPanel = false;
            this.FormGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.paymentPosGridView_FocusedRowChanged);
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // BuildingGridColumn
            // 
            this.BuildingGridColumn.Caption = "Дом";
            this.BuildingGridColumn.FieldName = "Building";
            this.BuildingGridColumn.Name = "BuildingGridColumn";
            this.BuildingGridColumn.Visible = true;
            this.BuildingGridColumn.VisibleIndex = 0;
            // 
            // CounterGridColumn
            // 
            this.CounterGridColumn.Caption = "Счетчик";
            this.CounterGridColumn.FieldName = "Counter";
            this.CounterGridColumn.Name = "CounterGridColumn";
            this.CounterGridColumn.Visible = true;
            this.CounterGridColumn.VisibleIndex = 1;
            // 
            // CurrentValueGridColumn
            // 
            this.CurrentValueGridColumn.Caption = "Текущие показания";
            this.CurrentValueGridColumn.FieldName = "CurrentValue";
            this.CurrentValueGridColumn.Name = "CurrentValueGridColumn";
            this.CurrentValueGridColumn.Visible = true;
            this.CurrentValueGridColumn.VisibleIndex = 2;
            // 
            // PrevValueGridColumn
            // 
            this.PrevValueGridColumn.Caption = "Предыдущие показания";
            this.PrevValueGridColumn.FieldName = "PrevValue";
            this.PrevValueGridColumn.Name = "PrevValueGridColumn";
            this.PrevValueGridColumn.Visible = true;
            this.PrevValueGridColumn.VisibleIndex = 3;
            // 
            // CoefficientGridColumn
            // 
            this.CoefficientGridColumn.Caption = "Коэффициент";
            this.CoefficientGridColumn.FieldName = "Coefficient";
            this.CoefficientGridColumn.Name = "CoefficientGridColumn";
            this.CoefficientGridColumn.Visible = true;
            this.CoefficientGridColumn.VisibleIndex = 4;
            // 
            // ResultGridColumn
            // 
            this.ResultGridColumn.Caption = "Результат обработки";
            this.ResultGridColumn.FieldName = "Result";
            this.ResultGridColumn.Name = "ResultGridColumn";
            this.ResultGridColumn.Visible = true;
            this.ResultGridColumn.VisibleIndex = 5;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Caption = "Результат обработки";
            this.DescriptionColumn.FieldName = "Description";
            this.DescriptionColumn.Name = "DescriptionColumn";
            // 
            // PaymentTypeGroupBox
            // 
            this.PaymentTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PaymentTypeGroupBox.Controls.Add(this.RowDescriptionTextBox);
            this.PaymentTypeGroupBox.Location = new System.Drawing.Point(3, 330);
            this.PaymentTypeGroupBox.Name = "PaymentTypeGroupBox";
            this.PaymentTypeGroupBox.Size = new System.Drawing.Size(570, 75);
            this.PaymentTypeGroupBox.TabIndex = 2;
            this.PaymentTypeGroupBox.TabStop = false;
            this.PaymentTypeGroupBox.Text = "Результат обработки  выбранной строки";
            // 
            // RowDescriptionTextBox
            // 
            this.RowDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RowDescriptionTextBox.Location = new System.Drawing.Point(6, 19);
            this.RowDescriptionTextBox.MaxLength = 250;
            this.RowDescriptionTextBox.Multiline = true;
            this.RowDescriptionTextBox.Name = "RowDescriptionTextBox";
            this.RowDescriptionTextBox.ReadOnly = true;
            this.RowDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RowDescriptionTextBox.Size = new System.Drawing.Size(558, 47);
            this.RowDescriptionTextBox.TabIndex = 49;
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.paymentPosGridControl);
            this.Controls.Add(this.PaymentTypeGroupBox);
            this.Name = "FormView";
            this.Size = new System.Drawing.Size(576, 408);
            ((System.ComponentModel.ISupportInitialize)(this.paymentPosGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormGridView)).EndInit();
            this.PaymentTypeGroupBox.ResumeLayout(false);
            this.PaymentTypeGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl paymentPosGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView FormGridView;
        private System.Windows.Forms.GroupBox PaymentTypeGroupBox;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn DescriptionColumn;
        private System.Windows.Forms.TextBox RowDescriptionTextBox;
        private DevExpress.XtraGrid.Columns.GridColumn BuildingGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CounterGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CurrentValueGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ResultGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PrevValueGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CoefficientGridColumn;
    }
}