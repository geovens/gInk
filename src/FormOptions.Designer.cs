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
			this.tbHotkey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.comboCanvasCursor = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cbEraserEnabled
			// 
			this.cbEraserEnabled.AutoSize = true;
			this.cbEraserEnabled.Location = new System.Drawing.Point(350, 20);
			this.cbEraserEnabled.Name = "cbEraserEnabled";
			this.cbEraserEnabled.Size = new System.Drawing.Size(132, 16);
			this.cbEraserEnabled.TabIndex = 0;
			this.cbEraserEnabled.Text = "Show eraser button";
			this.cbEraserEnabled.UseVisualStyleBackColor = true;
			this.cbEraserEnabled.CheckedChanged += new System.EventHandler(this.cbEraserEnabled_CheckedChanged);
			// 
			// cbPointerEnabled
			// 
			this.cbPointerEnabled.AutoSize = true;
			this.cbPointerEnabled.Location = new System.Drawing.Point(350, 40);
			this.cbPointerEnabled.Name = "cbPointerEnabled";
			this.cbPointerEnabled.Size = new System.Drawing.Size(138, 16);
			this.cbPointerEnabled.TabIndex = 0;
			this.cbPointerEnabled.Text = "Show pointer button";
			this.cbPointerEnabled.UseVisualStyleBackColor = true;
			this.cbPointerEnabled.CheckedChanged += new System.EventHandler(this.cbPointerEnabled_CheckedChanged);
			// 
			// cbSnapEnabled
			// 
			this.cbSnapEnabled.AutoSize = true;
			this.cbSnapEnabled.Location = new System.Drawing.Point(350, 100);
			this.cbSnapEnabled.Name = "cbSnapEnabled";
			this.cbSnapEnabled.Size = new System.Drawing.Size(144, 16);
			this.cbSnapEnabled.TabIndex = 0;
			this.cbSnapEnabled.Text = "Show snapshot button";
			this.cbSnapEnabled.UseVisualStyleBackColor = true;
			this.cbSnapEnabled.CheckedChanged += new System.EventHandler(this.cbSnapEnabled_CheckedChanged);
			// 
			// cbUndoEnabled
			// 
			this.cbUndoEnabled.AutoSize = true;
			this.cbUndoEnabled.Location = new System.Drawing.Point(350, 118);
			this.cbUndoEnabled.Name = "cbUndoEnabled";
			this.cbUndoEnabled.Size = new System.Drawing.Size(120, 16);
			this.cbUndoEnabled.TabIndex = 0;
			this.cbUndoEnabled.Text = "Show undo button";
			this.cbUndoEnabled.UseVisualStyleBackColor = true;
			this.cbUndoEnabled.CheckedChanged += new System.EventHandler(this.cbUndoEnabled_CheckedChanged);
			// 
			// cbClearEnabled
			// 
			this.cbClearEnabled.AutoSize = true;
			this.cbClearEnabled.Location = new System.Drawing.Point(350, 136);
			this.cbClearEnabled.Name = "cbClearEnabled";
			this.cbClearEnabled.Size = new System.Drawing.Size(126, 16);
			this.cbClearEnabled.TabIndex = 0;
			this.cbClearEnabled.Text = "Show clear button";
			this.cbClearEnabled.UseVisualStyleBackColor = true;
			this.cbClearEnabled.CheckedChanged += new System.EventHandler(this.cbClearEnabled_CheckedChanged);
			// 
			// cbWidthEnabled
			// 
			this.cbWidthEnabled.AutoSize = true;
			this.cbWidthEnabled.Location = new System.Drawing.Point(350, 60);
			this.cbWidthEnabled.Name = "cbWidthEnabled";
			this.cbWidthEnabled.Size = new System.Drawing.Size(240, 16);
			this.cbWidthEnabled.TabIndex = 0;
			this.cbWidthEnabled.Text = "Show global pen width control button";
			this.cbWidthEnabled.UseVisualStyleBackColor = true;
			this.cbWidthEnabled.CheckedChanged += new System.EventHandler(this.cbWidthEnabled_CheckedChanged);
			// 
			// cbWhiteIcon
			// 
			this.cbWhiteIcon.AutoSize = true;
			this.cbWhiteIcon.Location = new System.Drawing.Point(350, 170);
			this.cbWhiteIcon.Name = "cbWhiteIcon";
			this.cbWhiteIcon.Size = new System.Drawing.Size(138, 16);
			this.cbWhiteIcon.TabIndex = 0;
			this.cbWhiteIcon.Text = "Use white tray icon";
			this.cbWhiteIcon.UseVisualStyleBackColor = true;
			this.cbWhiteIcon.CheckedChanged += new System.EventHandler(this.cbWhiteIcon_CheckedChanged);
			// 
			// tbSnapPath
			// 
			this.tbSnapPath.Location = new System.Drawing.Point(350, 250);
			this.tbSnapPath.Name = "tbSnapPath";
			this.tbSnapPath.Size = new System.Drawing.Size(168, 21);
			this.tbSnapPath.TabIndex = 1;
			this.tbSnapPath.ModifiedChanged += new System.EventHandler(this.tbSnapPath_ModifiedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(347, 234);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "Snapshot save path";
			// 
			// btSnapPath
			// 
			this.btSnapPath.Location = new System.Drawing.Point(528, 249);
			this.btSnapPath.Name = "btSnapPath";
			this.btSnapPath.Size = new System.Drawing.Size(31, 21);
			this.btSnapPath.TabIndex = 3;
			this.btSnapPath.Text = "...";
			this.btSnapPath.UseVisualStyleBackColor = true;
			this.btSnapPath.Click += new System.EventHandler(this.btSnapPath_Click);
			// 
			// tbHotkey
			// 
			this.tbHotkey.Location = new System.Drawing.Point(350, 292);
			this.tbHotkey.Name = "tbHotkey";
			this.tbHotkey.ReadOnly = true;
			this.tbHotkey.Size = new System.Drawing.Size(168, 21);
			this.tbHotkey.TabIndex = 1;
			this.tbHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHotkey_KeyDown);
			this.tbHotkey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbHotkey_KeyUp);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(347, 276);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(221, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "Hotkey (press ESC to disable hotkey)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(367, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(275, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "(overides each individual pen width settings)";
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
			this.label4.Location = new System.Drawing.Point(348, 205);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 12);
			this.label4.TabIndex = 5;
			this.label4.Text = "Canvas cursor";
			// 
			// comboBox1
			// 
			this.comboCanvasCursor.FormattingEnabled = true;
			this.comboCanvasCursor.Items.AddRange(new object[] {
            "Arrow",
            "Pen tip"});
			this.comboCanvasCursor.Location = new System.Drawing.Point(447, 202);
			this.comboCanvasCursor.Name = "comboBox1";
			this.comboCanvasCursor.Size = new System.Drawing.Size(121, 20);
			this.comboCanvasCursor.TabIndex = 6;
			this.comboCanvasCursor.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// FormOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(651, 344);
			this.Controls.Add(this.comboCanvasCursor);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btSnapPath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbHotkey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbSnapPath);
			this.Controls.Add(this.cbWhiteIcon);
			this.Controls.Add(this.cbClearEnabled);
			this.Controls.Add(this.cbUndoEnabled);
			this.Controls.Add(this.cbSnapEnabled);
			this.Controls.Add(this.cbPointerEnabled);
			this.Controls.Add(this.cbWidthEnabled);
			this.Controls.Add(this.cbEraserEnabled);
			this.MaximizeBox = false;
			this.Name = "FormOptions";
			this.Text = "Options - gInk";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOptions_FormClosing);
			this.Load += new System.EventHandler(this.FormOptions_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.TextBox tbHotkey;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboCanvasCursor;
	}
}