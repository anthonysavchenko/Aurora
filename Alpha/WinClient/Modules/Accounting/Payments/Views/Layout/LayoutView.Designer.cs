using Microsoft.Practices.CompositeUI.WinForms;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Layout
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
            this.zoneWorkspace1 = new Microsoft.Practices.CompositeUI.WinForms.ZoneWorkspace();
            this.tabbedViewSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            ((System.ComponentModel.ISupportInitialize)(this.zoneWorkspace1)).BeginInit();
            this.zoneWorkspace1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zoneWorkspace1
            // 
            this.zoneWorkspace1.Controls.Add(this.tabbedViewSmartPartPlaceholder);
            this.zoneWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoneWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.zoneWorkspace1.Name = "zoneWorkspace1";
            this.zoneWorkspace1.Size = new System.Drawing.Size(1356, 575);
            this.zoneWorkspace1.TabIndex = 0;
            // 
            // tabbedViewSmartPartPlaceholder
            // 
            this.tabbedViewSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.tabbedViewSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabbedViewSmartPartPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.tabbedViewSmartPartPlaceholder.Name = "tabbedViewSmartPartPlaceholder";
            this.tabbedViewSmartPartPlaceholder.Size = new System.Drawing.Size(1356, 575);
            this.tabbedViewSmartPartPlaceholder.SmartPartName = "TabbedView";
            this.tabbedViewSmartPartPlaceholder.TabIndex = 2;
            this.tabbedViewSmartPartPlaceholder.Text = "smartPartPlaceholder1";
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zoneWorkspace1);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1356, 575);
            ((System.ComponentModel.ISupportInitialize)(this.zoneWorkspace1)).EndInit();
            this.zoneWorkspace1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.ZoneWorkspace zoneWorkspace1;
        private SmartPartPlaceholder tabbedViewSmartPartPlaceholder;

    }
}
