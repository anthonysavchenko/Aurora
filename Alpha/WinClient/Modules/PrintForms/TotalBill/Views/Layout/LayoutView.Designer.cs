﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add Foundational Module" recipe.
//
// The LayoutView usercontrol defines a layout decoupled from the shell. 
// It provides a left and right workspace, menu bar, tool bar and status bar.
// These ui elements are added as extension sites.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-290-Automation%20Add%20Foundational%20Module.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.Layout
{
	partial class LayoutView {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
            this._moduleWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabSmartPartPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.smartPartPlaceholder1 = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _moduleWorkspace
            // 
            this._moduleWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this._moduleWorkspace.Location = new System.Drawing.Point(0, 0);
            this._moduleWorkspace.Name = "_moduleWorkspace";
            this._moduleWorkspace.Size = new System.Drawing.Size(1354, 571);
            this._moduleWorkspace.TabIndex = 1;
            this._moduleWorkspace.Text = "_moduleWorkspace";
            // 
            // tabSmartPartPlaceholder
            // 
            this.tabSmartPartPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.tabSmartPartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSmartPartPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.tabSmartPartPlaceholder.Margin = new System.Windows.Forms.Padding(0);
            this.tabSmartPartPlaceholder.Name = "tabSmartPartPlaceholder";
            this.tabSmartPartPlaceholder.Size = new System.Drawing.Size(1354, 571);
            this.tabSmartPartPlaceholder.SmartPartName = "ReportView";
            this.tabSmartPartPlaceholder.TabIndex = 2;
            this.tabSmartPartPlaceholder.Text = "TabbedView";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabSmartPartPlaceholder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.smartPartPlaceholder1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1354, 571);
            this.splitContainer1.SplitterDistance = 542;
            this.splitContainer1.TabIndex = 3;
            // 
            // smartPartPlaceholder1
            // 
            this.smartPartPlaceholder1.BackColor = System.Drawing.Color.Transparent;
            this.smartPartPlaceholder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPartPlaceholder1.Location = new System.Drawing.Point(0, 0);
            this.smartPartPlaceholder1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPartPlaceholder1.Name = "smartPartPlaceholder1";
            this.smartPartPlaceholder1.Size = new System.Drawing.Size(1354, 25);
            this.smartPartPlaceholder1.SmartPartName = "StatusView";
            this.smartPartPlaceholder1.TabIndex = 3;
            this.smartPartPlaceholder1.Text = "TabbedView";
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._moduleWorkspace);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1354, 571);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _moduleWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder tabSmartPartPlaceholder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder smartPartPlaceholder1;
	}
}
