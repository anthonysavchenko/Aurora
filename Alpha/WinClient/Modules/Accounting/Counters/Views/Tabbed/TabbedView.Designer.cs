namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Tabbed
{
    partial class TabbedView
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
            this.tabWizard = new System.Windows.Forms.TabPage();
            this.smartPartPlaceholder2 = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.tabPayment = new System.Windows.Forms.TabPage();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this._itemViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.tabList = new System.Windows.Forms.TabPage();
            this._listViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.topViewSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this._tabWorkspace = new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();
            this.tabWizard.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.tabList.SuspendLayout();
            this._tabWorkspace.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabWizard
            // 
            this.tabWizard.Controls.Add(this.smartPartPlaceholder2);
            this.tabWizard.Location = new System.Drawing.Point(4, 22);
            this.tabWizard.Name = "tabWizard";
            this.tabWizard.Padding = new System.Windows.Forms.Padding(3);
            this.tabWizard.Size = new System.Drawing.Size(721, 397);
            this.tabWizard.TabIndex = 3;
            this.tabWizard.Text = "Внесение данных";
            this.tabWizard.UseVisualStyleBackColor = true;
            // 
            // smartPartPlaceholder2
            // 
            this.smartPartPlaceholder2.BackColor = System.Drawing.Color.Transparent;
            this.smartPartPlaceholder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPartPlaceholder2.Location = new System.Drawing.Point(3, 3);
            this.smartPartPlaceholder2.Name = "smartPartPlaceholder2";
            this.smartPartPlaceholder2.Size = new System.Drawing.Size(715, 391);
            this.smartPartPlaceholder2.SmartPartName = "WizardView";
            this.smartPartPlaceholder2.TabIndex = 2;
            this.smartPartPlaceholder2.Text = "WizardSmartPartPlaceholder";
            // 
            // tabPayment
            // 
            this.tabPayment.Location = new System.Drawing.Point(4, 22);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.Size = new System.Drawing.Size(721, 397);
            this.tabPayment.TabIndex = 2;
            this.tabPayment.Text = "Платеж";
            this.tabPayment.UseVisualStyleBackColor = true;
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this._itemViewPlaceholder);
            this.tabDetail.Location = new System.Drawing.Point(4, 22);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Size = new System.Drawing.Size(721, 397);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Tag = "tabDetail";
            this.tabDetail.Text = "Показания";
            this.tabDetail.UseVisualStyleBackColor = true;
            // 
            // _itemViewPlaceholder
            // 
            this._itemViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this._itemViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemViewPlaceholder.Location = new System.Drawing.Point(0, 0);
            this._itemViewPlaceholder.Margin = new System.Windows.Forms.Padding(6);
            this._itemViewPlaceholder.Name = "_itemViewPlaceholder";
            this._itemViewPlaceholder.Size = new System.Drawing.Size(721, 397);
            this._itemViewPlaceholder.SmartPartName = "ItemView";
            this._itemViewPlaceholder.TabIndex = 0;
            this._itemViewPlaceholder.Text = "_itemViewPlaceholder";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this._listViewPlaceholder);
            this.tabList.Controls.Add(this.topViewSmartPartPlaceholder);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(721, 397);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "Приборы учета";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // _listViewPlaceholder
            // 
            this._listViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this._listViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewPlaceholder.Location = new System.Drawing.Point(3, 133);
            this._listViewPlaceholder.Name = "_listViewPlaceholder";
            this._listViewPlaceholder.Size = new System.Drawing.Size(715, 261);
            this._listViewPlaceholder.SmartPartName = "ListView";
            this._listViewPlaceholder.TabIndex = 0;
            this._listViewPlaceholder.Text = "_listViewPlaceholder";
            // 
            // topViewSmartPartPlaceholder
            // 
            this.topViewSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.topViewSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Top;
            this.topViewSmartPartPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.topViewSmartPartPlaceholder.Margin = new System.Windows.Forms.Padding(0);
            this.topViewSmartPartPlaceholder.Name = "topViewSmartPartPlaceholder";
            this.topViewSmartPartPlaceholder.Size = new System.Drawing.Size(715, 130);
            this.topViewSmartPartPlaceholder.SmartPartName = "TopView";
            this.topViewSmartPartPlaceholder.TabIndex = 5;
            // 
            // _tabWorkspace
            // 
            this._tabWorkspace.Controls.Add(this.tabList);
            this._tabWorkspace.Controls.Add(this.tabDetail);
            this._tabWorkspace.Controls.Add(this.tabWizard);
            this._tabWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabWorkspace.Location = new System.Drawing.Point(0, 0);
            this._tabWorkspace.Name = "_tabWorkspace";
            this._tabWorkspace.SelectedIndex = 0;
            this._tabWorkspace.Size = new System.Drawing.Size(729, 423);
            this._tabWorkspace.TabIndex = 0;
            // 
            // TabbedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tabWorkspace);
            this.Name = "TabbedView";
            this.Size = new System.Drawing.Size(729, 423);
            this.tabWizard.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this._tabWorkspace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabWizard;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder smartPartPlaceholder2;
        private System.Windows.Forms.TabPage tabPayment;
        private System.Windows.Forms.TabPage tabDetail;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder _itemViewPlaceholder;
        private System.Windows.Forms.TabPage tabList;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder _listViewPlaceholder;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace _tabWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder topViewSmartPartPlaceholder;
    }
}

