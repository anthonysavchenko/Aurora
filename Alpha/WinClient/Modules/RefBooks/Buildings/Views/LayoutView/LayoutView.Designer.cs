namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Layout
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
            this.listSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.SuspendLayout();
            // 
            // listSmartPartPlaceholder
            // 
            this.listSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.listSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSmartPartPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.listSmartPartPlaceholder.Name = "listSmartPartPlaceholder";
            this.listSmartPartPlaceholder.Size = new System.Drawing.Size(640, 480);
            this.listSmartPartPlaceholder.SmartPartName = "TabbedView";
            this.listSmartPartPlaceholder.TabIndex = 0;
            this.listSmartPartPlaceholder.Text = "smartPartPlaceholder1";
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listSmartPartPlaceholder);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder listSmartPartPlaceholder;


    }
}
