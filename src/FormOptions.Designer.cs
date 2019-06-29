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
			this.label2 = new System.Windows.Forms.Label();
			this.hiGlobal = new gInk.HotkeyInputBox();
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
			this.tabPage3.Controls.Add(this.hiGlobal);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(484, 397);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Hotkeys";
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
			// hiGlobal
			// 
			this.hiGlobal.Location = new System.Drawing.Point(19, 45);
			this.hiGlobal.Name = "hiGlobal";
			this.hiGlobal.RequireModifier = false;
			this.hiGlobal.Size = new System.Drawing.Size(100, 21);
			this.hiGlobal.TabIndex = 5;
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
	}
}