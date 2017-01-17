namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Tabbed
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
            this._tabWorkspace = new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();
            this.tabList = new System.Windows.Forms.TabPage();
            this.viewSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this.detailSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this._tabWorkspace.SuspendLayout();
            this.tabList.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabWorkspace
            // 
            this._tabWorkspace.Controls.Add(this.tabList);
            this._tabWorkspace.Controls.Add(this.tabDetail);
            this._tabWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabWorkspace.Location = new System.Drawing.Point(0, 0);
            this._tabWorkspace.Name = "_tabWorkspace";
            this._tabWorkspace.SelectedIndex = 0;
            this._tabWorkspace.Size = new System.Drawing.Size(720, 454);
            this._tabWorkspace.TabIndex = 1;
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.viewSmartPartPlaceholder);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(712, 428);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "Обзор";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // viewSmartPartPlaceholder
            // 
            this.viewSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.viewSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewSmartPartPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.viewSmartPartPlaceholder.Name = "viewSmartPartPlaceholder";
            this.viewSmartPartPlaceholder.Size = new System.Drawing.Size(706, 422);
            this.viewSmartPartPlaceholder.SmartPartName = "ListView";
            this.viewSmartPartPlaceholder.TabIndex = 0;
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this.detailSmartPartPlaceholder);
            this.tabDetail.Location = new System.Drawing.Point(4, 22);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(712, 428);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Text = "Детали";
            this.tabDetail.UseVisualStyleBackColor = true;
            // 
            // detailSmartPartPlaceholder
            // 
            this.detailSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.detailSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailSmartPartPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.detailSmartPartPlaceholder.Name = "detailSmartPartPlaceholder";
            this.detailSmartPartPlaceholder.Size = new System.Drawing.Size(706, 422);
            this.detailSmartPartPlaceholder.SmartPartName = "ItemView";
            this.detailSmartPartPlaceholder.TabIndex = 0;
            // 
            // TabbedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tabWorkspace);
            this.Name = "TabbedView";
            this.Size = new System.Drawing.Size(720, 454);
            this._tabWorkspace.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace _tabWorkspace;
        private System.Windows.Forms.TabPage tabList;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder viewSmartPartPlaceholder;
        private System.Windows.Forms.TabPage tabDetail;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder detailSmartPartPlaceholder;
    }
}