namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.ServiceTypes
{
    partial class LayoutView
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
            if (disposing && (components != null))
            {
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
            this.tabWorkspace1 = new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.tabWorkspace1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabWorkspace1
            // 
            this.tabWorkspace1.Controls.Add(this.tabPage1);
            this.tabWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.tabWorkspace1.Name = "tabWorkspace1";
            this.tabWorkspace1.SelectedIndex = 0;
            this.tabWorkspace1.Size = new System.Drawing.Size(640, 480);
            this.tabWorkspace1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listSmartPartPlaceholder);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 454);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Обзор";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listSmartPartPlaceholder
            // 
            this.listSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.listSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSmartPartPlaceholder.Location = new System.Drawing.Point(3, 3);
            this.listSmartPartPlaceholder.Name = "listSmartPartPlaceholder";
            this.listSmartPartPlaceholder.Size = new System.Drawing.Size(626, 448);
            this.listSmartPartPlaceholder.SmartPartName = "ListView";
            this.listSmartPartPlaceholder.TabIndex = 0;
            this.listSmartPartPlaceholder.Text = "smartPartPlaceholder1";
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabWorkspace1);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(640, 480);
            this.tabWorkspace1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWorkspace1;
        private System.Windows.Forms.TabPage tabPage1;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder listSmartPartPlaceholder;


    }
}
