namespace gInk
{
	partial class FormOptions
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			gInk.Hotkey hotkey1 = new gInk.Hotkey();
			gInk.Hotkey hotkey2 = new gInk.Hotkey();
			gInk.Hotkey hotkey3 = new gInk.Hotkey();
			gInk.Hotkey hotkey4 = new gInk.Hotkey();
			gInk.Hotkey hotkey5 = new gInk.Hotkey();
			gInk.Hotkey hotkey6 = new gInk.Hotkey();
			gInk.Hotkey hotkey7 = new gInk.Hotkey();
			gInk.Hotkey hotkey8 = new gInk.Hotkey();
			gInk.Hotkey hotkey9 = new gInk.Hotkey();
			this.cbEraserEnabled = new System.Windows.Forms.CheckBox();
			this.cbPointerEnabled = new System.Windows.Forms.CheckBox();
			this.cbSnapEnabled = new System.Windows.Forms.CheckBox();
			this.cbUndoEnabled = new System.Windows.Forms.CheckBox();
			this.cbClearEnabled = new System.Windows.Forms.CheckBox();
			this.cbWidthEnabled = new System.Windows.Forms.CheckBox();
			this.cbWhiteIcon = new System.Windows.Forms.CheckBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tbSnapPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btSnapPath = new System.Windows.Forms.Button();
			this.lbNote = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.comboCanvasCursor = new System.Windows.Forms.ComboBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.cbInkVisibleEnabled = new System.Windows.Forms.CheckBox();
			this.cbPanEnabled = new System.Windows.Forms.CheckBox();
			this.cbAllowDragging = new System.Windows.Forms.CheckBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.hiInkVisible = new gInk.HotkeyInputBox();
			this.label11 = new System.Windows.Forms.Label();
			this.hiSnapshot = new gInk.HotkeyInputBox();
			this.hiClear = new gInk.HotkeyInputBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.hiPan = new gInk.HotkeyInputBox();
			this.label10 = new System.Windows.Forms.Label();
			this.hiPointer = new gInk.HotkeyInputBox();
			this.hiRedo = new gInk.HotkeyInputBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.hiUndo = new gInk.HotkeyInputBox();
			this.hiEraser = new gInk.HotkeyInputBox();
			this.label5 = new System.Windows.Forms.Label();
			this.hiGlobal = new gInk.HotkeyInputBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbEraserEnabled
			// 
			this.cbEraserEnabled.AutoSize = true;
			this.cbEraserEnabled.Location = new System.Drawing.Point(39, 75);
			this.cbEraserEnabled.Name = "cbEraserEnabled";
			this.cbEraserEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbEraserEnabled.TabIndex = 0;
			this.cbEraserEnabled.UseVisualStyleBackColor = true;
			this.cbEraserEnabled.CheckedChanged += new System.EventHandler(this.cbEraserEnabled_CheckedChanged);
			// 
			// cbPointerEnabled
			// 
			this.cbPointerEnabled.AutoSize = true;
			this.cbPointerEnabled.Location = new System.Drawing.Point(137, 75);
			this.cbPointerEnabled.Name = "cbPointerEnabled";
			this.cbPointerEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbPointerEnabled.TabIndex = 0;
			this.cbPointerEnabled.UseVisualStyleBackColor = true;
			this.cbPointerEnabled.CheckedChanged += new System.EventHandler(this.cbPointerEnabled_CheckedChanged);
			// 
			// cbSnapEnabled
			// 
			this.cbSnapEnabled.AutoSize = true;
			this.cbSnapEnabled.Location = new System.Drawing.Point(325, 76);
			this.cbSnapEnabled.Name = "cbSnapEnabled";
			this.cbSnapEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbSnapEnabled.TabIndex = 0;
			this.cbSnapEnabled.UseVisualStyleBackColor = true;
			this.cbSnapEnabled.CheckedChanged += new System.EventHandler(this.cbSnapEnabled_CheckedChanged);
			// 
			// cbUndoEnabled
			// 
			this.cbUndoEnabled.AutoSize = true;
			this.cbUndoEnabled.Location = new System.Drawing.Point(377, 76);
			this.cbUndoEnabled.Name = "cbUndoEnabled";
			this.cbUndoEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbUndoEnabled.TabIndex = 0;
			this.cbUndoEnabled.UseVisualStyleBackColor = true;
			this.cbUndoEnabled.CheckedChanged += new System.EventHandler(this.cbUndoEnabled_CheckedChanged);
			// 
			// cbClearEnabled
			// 
			this.cbClearEnabled.AutoSize = true;
			this.cbClearEnabled.Location = new System.Drawing.Point(426, 76);
			this.cbClearEnabled.Name = "cbClearEnabled";
			this.cbClearEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbClearEnabled.TabIndex = 0;
			this.cbClearEnabled.UseVisualStyleBackColor = true;
			this.cbClearEnabled.CheckedChanged += new System.EventHandler(this.cbClearEnabled_CheckedChanged);
			// 
			// cbWidthEnabled
			// 
			this.cbWidthEnabled.AutoSize = true;
			this.cbWidthEnabled.Location = new System.Drawing.Point(222, 77);
			this.cbWidthEnabled.Name = "cbWidthEnabled";
			this.cbWidthEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbWidthEnabled.TabIndex = 0;
			this.cbWidthEnabled.UseVisualStyleBackColor = true;
			this.cbWidthEnabled.CheckedChanged += new System.EventHandler(this.cbWidthEnabled_CheckedChanged);
			// 
			// cbWhiteIcon
			// 
			this.cbWhiteIcon.AutoSize = true;
			this.cbWhiteIcon.Location = new System.Drawing.Point(11, 186);
			this.cbWhiteIcon.Name = "cbWhiteIcon";
			this.cbWhiteIcon.Size = new System.Drawing.Size(138, 16);
			this.cbWhiteIcon.TabIndex = 0;
			this.cbWhiteIcon.Text = "Use white tray icon";
			this.cbWhiteIcon.UseVisualStyleBackColor = true;
			this.cbWhiteIcon.CheckedChanged += new System.EventHandler(this.cbWhiteIcon_CheckedChanged);
			// 
			// tbSnapPath
			// 
			this.tbSnapPath.Location = new System.Drawing.Point(132, 149);
			this.tbSnapPath.Name = "tbSnapPath";
			this.tbSnapPath.Size = new System.Drawing.Size(297, 21);
			this.tbSnapPath.TabIndex = 1;
			this.tbSnapPath.ModifiedChanged += new System.EventHandler(this.tbSnapPath_ModifiedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 153);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "Snapshot save path";
			// 
			// btSnapPath
			// 
			this.btSnapPath.Location = new System.Drawing.Point(435, 148);
			this.btSnapPath.Name = "btSnapPath";
			this.btSnapPath.Size = new System.Drawing.Size(31, 21);
			this.btSnapPath.TabIndex = 3;
			this.btSnapPath.Text = "...";
			this.btSnapPath.UseVisualStyleBackColor = true;
			this.btSnapPath.Click += new System.EventHandler(this.btSnapPath_Click);
			// 
			// lbNote
			// 
			this.lbNote.AutoSize = true;
			this.lbNote.Location = new System.Drawing.Point(9, 242);
			this.lbNote.Name = "lbNote";
			this.lbNote.Size = new System.Drawing.Size(395, 12);
			this.lbNote.TabIndex = 4;
			this.lbNote.Text = "Note: pen width panel overides each individual pen width settings";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 50;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 123);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 12);
			this.label4.TabIndex = 5;
			this.label4.Text = "Canvas cursor";
			// 
			// comboCanvasCursor
			// 
			this.comboCanvasCursor.FormattingEnabled = true;
			this.comboCanvasCursor.Items.AddRange(new object[] {
            "Arrow",
            "Pen tip"});
			this.comboCanvasCursor.Location = new System.Drawing.Point(132, 119);
			this.comboCanvasCursor.Name = "comboCanvasCursor";
			this.comboCanvasCursor.Size = new System.Drawing.Size(297, 20);
			this.comboCanvasCursor.TabIndex = 6;
			this.comboCanvasCursor.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(0, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(492, 423);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage1.Controls.Add(this.pictureBox1);
			this.tabPage1.Controls.Add(this.comboCanvasCursor);
			this.tabPage1.Controls.Add(this.cbInkVisibleEnabled);
			this.tabPage1.Controls.Add(this.cbPanEnabled);
			this.tabPage1.Controls.Add(this.cbEraserEnabled);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.cbWidthEnabled);
			this.tabPage1.Controls.Add(this.lbNote);
			this.tabPage1.Controls.Add(this.cbPointerEnabled);
			this.tabPage1.Controls.Add(this.btSnapPath);
			this.tabPage1.Controls.Add(this.cbSnapEnabled);
			this.tabPage1.Controls.Add(this.cbUndoEnabled);
			this.tabPage1.Controls.Add(this.cbClearEnabled);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.cbAllowDragging);
			this.tabPage1.Controls.Add(this.cbWhiteIcon);
			this.tabPage1.Controls.Add(this.tbSnapPath);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(484, 397);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::gInk.Properties.Resources.paneloption;
			this.pictureBox1.Location = new System.Drawing.Point(8, 14);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(468, 53);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// cbInkVisibleEnabled
			// 
			this.cbInkVisibleEnabled.AutoSize = true;
			this.cbInkVisibleEnabled.Location = new System.Drawing.Point(275, 76);
			this.cbInkVisibleEnabled.Name = "cbInkVisibleEnabled";
			this.cbInkVisibleEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbInkVisibleEnabled.TabIndex = 0;
			this.cbInkVisibleEnabled.UseVisualStyleBackColor = true;
			this.cbInkVisibleEnabled.CheckedChanged += new System.EventHandler(this.cbInkVisibleEnabled_CheckedChanged);
			// 
			// cbPanEnabled
			// 
			this.cbPanEnabled.AutoSize = true;
			this.cbPanEnabled.Location = new System.Drawing.Point(87, 75);
			this.cbPanEnabled.Name = "cbPanEnabled";
			this.cbPanEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbPanEnabled.TabIndex = 0;
			this.cbPanEnabled.UseVisualStyleBackColor = true;
			this.cbPanEnabled.CheckedChanged += new System.EventHandler(this.cbPanEnabled_CheckedChanged);
			// 
			// cbAllowDragging
			// 
			this.cbAllowDragging.AutoSize = true;
			this.cbAllowDragging.Location = new System.Drawing.Point(11, 214);
			this.cbAllowDragging.Name = "cbAllowDragging";
			this.cbAllowDragging.Size = new System.Drawing.Size(246, 16);
			this.cbAllowDragging.TabIndex = 0;
			this.cbAllowDragging.Text = "Allow dragging toolbar (experimental)";
			this.cbAllowDragging.UseVisualStyleBackColor = true;
			this.cbAllowDragging.CheckedChanged += new System.EventHandler(this.cbAllowDragging_CheckedChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(484, 397);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Pens";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage3.Controls.Add(this.hiInkVisible);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.hiSnapshot);
			this.tabPage3.Controls.Add(this.hiClear);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.hiPan);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.hiPointer);
			this.tabPage3.Controls.Add(this.hiRedo);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Controls.Add(this.hiUndo);
			this.tabPage3.Controls.Add(this.hiEraser);
			this.tabPage3.Controls.Add(this.label5);
			this.tabPage3.Controls.Add(this.hiGlobal);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(484, 397);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Hotkeys";
			// 
			// hiInkVisible
			// 
			this.hiInkVisible.Hotkey = hotkey1;
			this.hiInkVisible.Location = new System.Drawing.Point(309, 175);
			this.hiInkVisible.Name = "hiInkVisible";
			this.hiInkVisible.RequireModifier = false;
			this.hiInkVisible.Size = new System.Drawing.Size(150, 21);
			this.hiInkVisible.TabIndex = 17;
			this.hiInkVisible.Text = "None";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(236, 175);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(59, 12);
			this.label11.TabIndex = 16;
			this.label11.Text = "View/Hide";
			// 
			// hiSnapshot
			// 
			this.hiSnapshot.Hotkey = hotkey2;
			this.hiSnapshot.Location = new System.Drawing.Point(309, 200);
			this.hiSnapshot.Name = "hiSnapshot";
			this.hiSnapshot.RequireModifier = false;
			this.hiSnapshot.Size = new System.Drawing.Size(150, 21);
			this.hiSnapshot.TabIndex = 14;
			this.hiSnapshot.Text = "None";
			// 
			// hiClear
			// 
			this.hiClear.Hotkey = hotkey3;
			this.hiClear.Location = new System.Drawing.Point(309, 275);
			this.hiClear.Name = "hiClear";
			this.hiClear.RequireModifier = false;
			this.hiClear.Size = new System.Drawing.Size(150, 21);
			this.hiClear.TabIndex = 15;
			this.hiClear.Text = "None";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(236, 200);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(53, 12);
			this.label8.TabIndex = 12;
			this.label8.Text = "Snapshot";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(236, 275);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 12);
			this.label9.TabIndex = 13;
			this.label9.Text = "Clear";
			// 
			// hiPan
			// 
			this.hiPan.Hotkey = hotkey4;
			this.hiPan.Location = new System.Drawing.Point(309, 125);
			this.hiPan.Name = "hiPan";
			this.hiPan.RequireModifier = false;
			this.hiPan.Size = new System.Drawing.Size(150, 21);
			this.hiPan.TabIndex = 11;
			this.hiPan.Text = "None";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(236, 125);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(23, 12);
			this.label10.TabIndex = 10;
			this.label10.Text = "Pan";
			// 
			// hiPointer
			// 
			this.hiPointer.Hotkey = hotkey5;
			this.hiPointer.Location = new System.Drawing.Point(309, 150);
			this.hiPointer.Name = "hiPointer";
			this.hiPointer.RequireModifier = false;
			this.hiPointer.Size = new System.Drawing.Size(150, 21);
			this.hiPointer.TabIndex = 8;
			this.hiPointer.Text = "None";
			this.hiPointer.Visible = false;
			// 
			// hiRedo
			// 
			this.hiRedo.Hotkey = hotkey6;
			this.hiRedo.Location = new System.Drawing.Point(309, 250);
			this.hiRedo.Name = "hiRedo";
			this.hiRedo.RequireModifier = false;
			this.hiRedo.Size = new System.Drawing.Size(150, 21);
			this.hiRedo.TabIndex = 9;
			this.hiRedo.Text = "None";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(236, 150);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(83, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "Mouse pointer";
			this.label6.Visible = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(236, 250);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 12);
			this.label7.TabIndex = 7;
			this.label7.Text = "Redo";
			// 
			// hiUndo
			// 
			this.hiUndo.Hotkey = hotkey7;
			this.hiUndo.Location = new System.Drawing.Point(309, 225);
			this.hiUndo.Name = "hiUndo";
			this.hiUndo.RequireModifier = false;
			this.hiUndo.Size = new System.Drawing.Size(150, 21);
			this.hiUndo.TabIndex = 5;
			this.hiUndo.Text = "None";
			// 
			// hiEraser
			// 
			this.hiEraser.Hotkey = hotkey8;
			this.hiEraser.Location = new System.Drawing.Point(309, 100);
			this.hiEraser.Name = "hiEraser";
			this.hiEraser.RequireModifier = false;
			this.hiEraser.Size = new System.Drawing.Size(150, 21);
			this.hiEraser.TabIndex = 5;
			this.hiEraser.Text = "None";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(236, 225);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "Undo";
			// 
			// hiGlobal
			// 
			this.hiGlobal.Hotkey = hotkey9;
			this.hiGlobal.Location = new System.Drawing.Point(19, 40);
			this.hiGlobal.Name = "hiGlobal";
			this.hiGlobal.RequireModifier = true;
			this.hiGlobal.Size = new System.Drawing.Size(100, 21);
			this.hiGlobal.TabIndex = 5;
			this.hiGlobal.Text = "None";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(236, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "Eraser";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(431, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "Global hotkey (start drawing, switch between mouse pointer and drawing)";
			// 
			// FormOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(487, 418);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.Name = "FormOptions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options - gInk";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOptions_FormClosing);
			this.Load += new System.EventHandler(this.FormOptions_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox cbEraserEnabled;
		private System.Windows.Forms.CheckBox cbPointerEnabled;
		private System.Windows.Forms.CheckBox cbSnapEnabled;
		private System.Windows.Forms.CheckBox cbUndoEnabled;
		private System.Windows.Forms.CheckBox cbClearEnabled;
		private System.Windows.Forms.CheckBox cbWidthEnabled;
		private System.Windows.Forms.CheckBox cbWhiteIcon;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox tbSnapPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btSnapPath;
		private System.Windows.Forms.Label lbNote;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboCanvasCursor;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox cbInkVisibleEnabled;
		private System.Windows.Forms.CheckBox cbPanEnabled;
		private System.Windows.Forms.CheckBox cbAllowDragging;
		private HotkeyInputBox hiGlobal;
		private HotkeyInputBox hiEraser;
		private System.Windows.Forms.Label label3;
		private HotkeyInputBox hiUndo;
		private System.Windows.Forms.Label label5;
		private HotkeyInputBox hiPointer;
		private HotkeyInputBox hiRedo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private HotkeyInputBox hiSnapshot;
		private HotkeyInputBox hiClear;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private HotkeyInputBox hiPan;
		private System.Windows.Forms.Label label10;
		private HotkeyInputBox hiInkVisible;
		private System.Windows.Forms.Label label11;
	}
}