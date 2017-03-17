﻿namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.streetLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.zipCodeTextBox = new System.Windows.Forms.MaskedTextBox();
            this.counterViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.counterValueViewPlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bankDetailsLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nonResidentialPlaceAreaSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.squareTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.fiasIdTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.residentsCountTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.entranceSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.floorCountSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.countersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.publicPlacePlaceholder = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.publicPlaceAndCountersSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label14 = new System.Windows.Forms.Label();
            this.heatedAreaTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankDetailsLookUpEdit.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nonResidentialPlaceAreaSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entranceSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorCountSpinEdit.Properties)).BeginInit();
            this.countersTableLayoutPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.publicPlaceAndCountersSplitContainer)).BeginInit();
            this.publicPlaceAndCountersSplitContainer.Panel1.SuspendLayout();
            this.publicPlaceAndCountersSplitContainer.Panel2.SuspendLayout();
            this.publicPlaceAndCountersSplitContainer.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Почтовый индекс";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Улица";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(108, 39);
            this.numberTextBox.MaxLength = 50;
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(43, 20);
            this.numberTextBox.TabIndex = 1;
            // 
            // streetLookUpEdit
            // 
            this.streetLookUpEdit.Location = new System.Drawing.Point(108, 13);
            this.streetLookUpEdit.Name = "streetLookUpEdit";
            this.streetLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.streetLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Наименование")});
            this.streetLookUpEdit.Properties.DisplayMember = "Name";
            this.streetLookUpEdit.Properties.ValueMember = "ID";
            this.streetLookUpEdit.Size = new System.Drawing.Size(200, 20);
            this.streetLookUpEdit.TabIndex = 0;
            this.streetLookUpEdit.Tag = "Aka";
            // 
            // zipCodeTextBox
            // 
            this.zipCodeTextBox.Location = new System.Drawing.Point(108, 66);
            this.zipCodeTextBox.Mask = "000000";
            this.zipCodeTextBox.Name = "zipCodeTextBox";
            this.zipCodeTextBox.Size = new System.Drawing.Size(43, 20);
            this.zipCodeTextBox.TabIndex = 6;
            // 
            // counterViewPlaceholder
            // 
            this.counterViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.counterViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterViewPlaceholder.Location = new System.Drawing.Point(3, 16);
            this.counterViewPlaceholder.Name = "counterViewPlaceholder";
            this.counterViewPlaceholder.Size = new System.Drawing.Size(353, 105);
            this.counterViewPlaceholder.SmartPartName = "CounterView";
            this.counterViewPlaceholder.TabIndex = 2;
            // 
            // counterValueViewPlaceholder
            // 
            this.counterValueViewPlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.counterValueViewPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterValueViewPlaceholder.Location = new System.Drawing.Point(3, 16);
            this.counterValueViewPlaceholder.Name = "counterValueViewPlaceholder";
            this.counterValueViewPlaceholder.Size = new System.Drawing.Size(353, 105);
            this.counterValueViewPlaceholder.SmartPartName = "CounterValueView";
            this.counterValueViewPlaceholder.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.counterViewPlaceholder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 124);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Приборы учета";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.counterValueViewPlaceholder);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(368, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 124);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Показания";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.bankDetailsLookUpEdit);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.fiasIdTextBox);
            this.panel1.Controls.Add(this.noteTextBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 222);
            this.panel1.TabIndex = 8;
            // 
            // bankDetailsLookUpEdit
            // 
            this.bankDetailsLookUpEdit.Location = new System.Drawing.Point(459, 102);
            this.bankDetailsLookUpEdit.Name = "bankDetailsLookUpEdit";
            this.bankDetailsLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.bankDetailsLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Account", "Расчетный счет"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Банк"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AccountAndBank", "AccountAndBank", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.bankDetailsLookUpEdit.Properties.DisplayMember = "AccountAndBank";
            this.bankDetailsLookUpEdit.Properties.ValueMember = "ID";
            this.bankDetailsLookUpEdit.Size = new System.Drawing.Size(267, 20);
            this.bankDetailsLookUpEdit.TabIndex = 24;
            this.bankDetailsLookUpEdit.Tag = "Aka";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(390, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Реквизиты";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.heatedAreaTextBox);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.nonResidentialPlaceAreaSpinEdit);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.squareTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(530, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 93);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Площадь";
            // 
            // nonResidentialPlaceAreaSpinEdit
            // 
            this.nonResidentialPlaceAreaSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nonResidentialPlaceAreaSpinEdit.Location = new System.Drawing.Point(65, 66);
            this.nonResidentialPlaceAreaSpinEdit.Name = "nonResidentialPlaceAreaSpinEdit";
            this.nonResidentialPlaceAreaSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nonResidentialPlaceAreaSpinEdit.Properties.Mask.EditMask = "n2";
            this.nonResidentialPlaceAreaSpinEdit.Size = new System.Drawing.Size(85, 20);
            this.nonResidentialPlaceAreaSpinEdit.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Нежилая";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(156, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "кв. м.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Жилая ";
            // 
            // squareTextBox
            // 
            this.squareTextBox.Location = new System.Drawing.Point(65, 14);
            this.squareTextBox.Name = "squareTextBox";
            this.squareTextBox.ReadOnly = true;
            this.squareTextBox.Size = new System.Drawing.Size(85, 20);
            this.squareTextBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "кв. м.";
            // 
            // fiasIdTextBox
            // 
            this.fiasIdTextBox.Location = new System.Drawing.Point(111, 102);
            this.fiasIdTextBox.MaxLength = 36;
            this.fiasIdTextBox.Name = "fiasIdTextBox";
            this.fiasIdTextBox.Size = new System.Drawing.Size(206, 20);
            this.fiasIdTextBox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Код ФИАС";
            // 
            // residentsCountTextBox
            // 
            this.residentsCountTextBox.Location = new System.Drawing.Point(136, 66);
            this.residentsCountTextBox.Name = "residentsCountTextBox";
            this.residentsCountTextBox.ReadOnly = true;
            this.residentsCountTextBox.Size = new System.Drawing.Size(60, 20);
            this.residentsCountTextBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Количество жильцов";
            // 
            // entranceSpinEdit
            // 
            this.entranceSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.entranceSpinEdit.Location = new System.Drawing.Point(136, 39);
            this.entranceSpinEdit.Name = "entranceSpinEdit";
            this.entranceSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.entranceSpinEdit.Properties.Mask.EditMask = "n0";
            this.entranceSpinEdit.Size = new System.Drawing.Size(60, 20);
            this.entranceSpinEdit.TabIndex = 17;
            // 
            // floorCountSpinEdit
            // 
            this.floorCountSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.floorCountSpinEdit.Location = new System.Drawing.Point(136, 13);
            this.floorCountSpinEdit.Name = "floorCountSpinEdit";
            this.floorCountSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.floorCountSpinEdit.Properties.Mask.EditMask = "n0";
            this.floorCountSpinEdit.Size = new System.Drawing.Size(60, 20);
            this.floorCountSpinEdit.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Количество подъездов";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Количество этажей";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteTextBox.Location = new System.Drawing.Point(3, 147);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(724, 69);
            this.noteTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Примечание";
            // 
            // countersTableLayoutPanel
            // 
            this.countersTableLayoutPanel.ColumnCount = 2;
            this.countersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.countersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.countersTableLayoutPanel.Controls.Add(this.groupBox2, 1, 0);
            this.countersTableLayoutPanel.Controls.Add(this.groupBox1, 0, 0);
            this.countersTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countersTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.countersTableLayoutPanel.Name = "countersTableLayoutPanel";
            this.countersTableLayoutPanel.RowCount = 1;
            this.countersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.countersTableLayoutPanel.Size = new System.Drawing.Size(730, 130);
            this.countersTableLayoutPanel.TabIndex = 9;
            // 
            // publicPlacePlaceholder
            // 
            this.publicPlacePlaceholder.BackColor = System.Drawing.Color.Transparent;
            this.publicPlacePlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publicPlacePlaceholder.Location = new System.Drawing.Point(3, 16);
            this.publicPlacePlaceholder.Name = "publicPlacePlaceholder";
            this.publicPlacePlaceholder.Size = new System.Drawing.Size(724, 112);
            this.publicPlacePlaceholder.SmartPartName = "PublicPlaceView";
            this.publicPlacePlaceholder.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.publicPlacePlaceholder);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(730, 131);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Площадь МОП";
            // 
            // publicPlaceAndCountersSplitContainer
            // 
            this.publicPlaceAndCountersSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publicPlaceAndCountersSplitContainer.Location = new System.Drawing.Point(0, 222);
            this.publicPlaceAndCountersSplitContainer.Name = "publicPlaceAndCountersSplitContainer";
            this.publicPlaceAndCountersSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // publicPlaceAndCountersSplitContainer.Panel1
            // 
            this.publicPlaceAndCountersSplitContainer.Panel1.Controls.Add(this.groupBox4);
            // 
            // publicPlaceAndCountersSplitContainer.Panel2
            // 
            this.publicPlaceAndCountersSplitContainer.Panel2.Controls.Add(this.countersTableLayoutPanel);
            this.publicPlaceAndCountersSplitContainer.Size = new System.Drawing.Size(730, 265);
            this.publicPlaceAndCountersSplitContainer.SplitterDistance = 131;
            this.publicPlaceAndCountersSplitContainer.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Отаплив.";
            // 
            // heatedAreaTextBox
            // 
            this.heatedAreaTextBox.Location = new System.Drawing.Point(65, 40);
            this.heatedAreaTextBox.Name = "heatedAreaTextBox";
            this.heatedAreaTextBox.ReadOnly = true;
            this.heatedAreaTextBox.Size = new System.Drawing.Size(85, 20);
            this.heatedAreaTextBox.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(156, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "кв. м.";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.streetLookUpEdit);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.zipCodeTextBox);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.numberTextBox);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(314, 93);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Адрес";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.floorCountSpinEdit);
            this.groupBox6.Controls.Add(this.entranceSpinEdit);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.residentsCountTextBox);
            this.groupBox6.Location = new System.Drawing.Point(323, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(201, 93);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.publicPlaceAndCountersSplitContainer);
            this.Controls.Add(this.panel1);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(730, 487);
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankDetailsLookUpEdit.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nonResidentialPlaceAreaSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entranceSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorCountSpinEdit.Properties)).EndInit();
            this.countersTableLayoutPanel.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.publicPlaceAndCountersSplitContainer.Panel1.ResumeLayout(false);
            this.publicPlaceAndCountersSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.publicPlaceAndCountersSplitContainer)).EndInit();
            this.publicPlaceAndCountersSplitContainer.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numberTextBox;
        private DevExpress.XtraEditors.LookUpEdit streetLookUpEdit;
        private System.Windows.Forms.MaskedTextBox zipCodeTextBox;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder counterViewPlaceholder;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder counterValueViewPlaceholder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox squareTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SpinEdit entranceSpinEdit;
        private DevExpress.XtraEditors.SpinEdit floorCountSpinEdit;
        private System.Windows.Forms.TextBox residentsCountTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox fiasIdTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SpinEdit nonResidentialPlaceAreaSpinEdit;
        private System.Windows.Forms.TableLayoutPanel countersTableLayoutPanel;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder publicPlacePlaceholder;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.LookUpEdit bankDetailsLookUpEdit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.SplitContainer publicPlaceAndCountersSplitContainer;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox heatedAreaTextBox;
        private System.Windows.Forms.Label label14;
    }
}
