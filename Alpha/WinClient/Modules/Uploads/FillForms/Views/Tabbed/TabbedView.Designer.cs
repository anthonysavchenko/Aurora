namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Views.Tabbed
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
            this.tabDetail = new System.Windows.Forms.TabPage();
            this._itemViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.tabList = new System.Windows.Forms.TabPage();
            this._listViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this._tabWorkspace = new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();
            this.tabDetail.SuspendLayout();
            this.tabList.SuspendLayout();
            this._tabWorkspace.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this._itemViewPlaceholder);
            this.tabDetail.Location = new System.Drawing.Point(4, 22);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(721, 397);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Tag = "tabDetail";
            this.tabDetail.Text = "Детали";
            this.tabDetail.UseVisualStyleBackColor = true;
            // 
            // _itemViewPlaceholder
            // 
            this._itemViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this._itemViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemViewPlaceholder.Location = new System.Drawing.Point(3, 3);
            this._itemViewPlaceholder.Margin = new System.Windows.Forms.Padding(6);
            this._itemViewPlaceholder.Name = "_itemViewPlaceholder";
            this._itemViewPlaceholder.Size = new System.Drawing.Size(715, 391);
            this._itemViewPlaceholder.SmartPartName = "ItemView";
            this._itemViewPlaceholder.TabIndex = 0;
            this._itemViewPlaceholder.Text = "_itemViewPlaceholder";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this._listViewPlaceholder);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(721, 397);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "Список";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // _listViewPlaceholder
            // 
            this._listViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this._listViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewPlaceholder.Location = new System.Drawing.Point(3, 3);
            this._listViewPlaceholder.Name = "_listViewPlaceholder";
            this._listViewPlaceholder.Size = new System.Drawing.Size(715, 391);
            this._listViewPlaceholder.SmartPartName = "ListView";
            this._listViewPlaceholder.TabIndex = 0;
            this._listViewPlaceholder.Text = "_listViewPlaceholder";
            // 
            // _tabWorkspace
            // 
            this._tabWorkspace.Controls.Add(this.tabList);
            this._tabWorkspace.Controls.Add(this.tabDetail);
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
            this.tabDetail.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this._tabWorkspace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabDetail;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder _itemViewPlaceholder;
        private System.Windows.Forms.TabPage tabList;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder _listViewPlaceholder;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace _tabWorkspace;

    }
}

