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
			this.lbSnapshotsavepath = new System.Windows.Forms.Label();
			this.btSnapPath = new System.Windows.Forms.Button();
			this.lbNote = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lbCanvascursor = new System.Windows.Forms.Label();
			this.comboCanvasCursor = new System.Windows.Forms.ComboBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.comboLanguage = new System.Windows.Forms.ComboBox();
			this.cbInkVisibleEnabled = new System.Windows.Forms.CheckBox();
			this.cbPanEnabled = new System.Windows.Forms.CheckBox();
			this.cbAllowDragging = new System.Windows.Forms.CheckBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.cbAllowHotkeyInPointer = new System.Windows.Forms.CheckBox();
			this.lbHkInkVisible = new System.Windows.Forms.Label();
			this.lbHkSnapshot = new System.Windows.Forms.Label();
			this.lbHkClear = new System.Windows.Forms.Label();
			this.lbHkPan = new System.Windows.Forms.Label();
			this.lbHkPointer = new System.Windows.Forms.Label();
			this.lbHkRedo = new System.Windows.Forms.Label();
			this.lbHkUndo = new System.Windows.Forms.Label();
			this.lbHkEraser = new System.Windows.Forms.Label();
			this.lbGlobalHotkey = new System.Windows.Forms.Label();
			this.hiInkVisible = new gInk.HotkeyInputBox();
			this.hiSnapshot = new gInk.HotkeyInputBox();
			this.hiClear = new gInk.HotkeyInputBox();
			this.hiPan = new gInk.HotkeyInputBox();
			this.hiPointer = new gInk.HotkeyInputBox();
			this.hiRedo = new gInk.HotkeyInputBox();
			this.hiUndo = new gInk.HotkeyInputBox();
			this.hiEraser = new gInk.HotkeyInputBox();
			this.hiGlobal = new gInk.HotkeyInputBox();
			this.lbLanguage = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbEraserEnabled
			// 
			this.cbEraserEnabled.AutoSize = true;
			this.cbEraserEnabled.Location = new System.Drawing.Point(39, 81);
			this.cbEraserEnabled.Name = "cbEraserEnabled";
			this.cbEraserEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbEraserEnabled.TabIndex = 0;
			this.cbEraserEnabled.UseVisualStyleBackColor = true;
			this.cbEraserEnabled.CheckedChanged += new System.EventHandler(this.cbEraserEnabled_CheckedChanged);
			// 
			// cbPointerEnabled
			// 
			this.cbPointerEnabled.AutoSize = true;
			this.cbPointerEnabled.Location = new System.Drawing.Point(137, 81);
			this.cbPointerEnabled.Name = "cbPointerEnabled";
			this.cbPointerEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbPointerEnabled.TabIndex = 0;
			this.cbPointerEnabled.UseVisualStyleBackColor = true;
			this.cbPointerEnabled.CheckedChanged += new System.EventHandler(this.cbPointerEnabled_CheckedChanged);
			// 
			// cbSnapEnabled
			// 
			this.cbSnapEnabled.AutoSize = true;
			this.cbSnapEnabled.Location = new System.Drawing.Point(325, 82);
			this.cbSnapEnabled.Name = "cbSnapEnabled";
			this.cbSnapEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbSnapEnabled.TabIndex = 0;
			this.cbSnapEnabled.UseVisualStyleBackColor = true;
			this.cbSnapEnabled.CheckedChanged += new System.EventHandler(this.cbSnapEnabled_CheckedChanged);
			// 
			// cbUndoEnabled
			// 
			this.cbUndoEnabled.AutoSize = true;
			this.cbUndoEnabled.Location = new System.Drawing.Point(377, 82);
			this.cbUndoEnabled.Name = "cbUndoEnabled";
			this.cbUndoEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbUndoEnabled.TabIndex = 0;
			this.cbUndoEnabled.UseVisualStyleBackColor = true;
			this.cbUndoEnabled.CheckedChanged += new System.EventHandler(this.cbUndoEnabled_CheckedChanged);
			// 
			// cbClearEnabled
			// 
			this.cbClearEnabled.AutoSize = true;
			this.cbClearEnabled.Location = new System.Drawing.Point(426, 82);
			this.cbClearEnabled.Name = "cbClearEnabled";
			this.cbClearEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbClearEnabled.TabIndex = 0;
			this.cbClearEnabled.UseVisualStyleBackColor = true;
			this.cbClearEnabled.CheckedChanged += new System.EventHandler(this.cbClearEnabled_CheckedChanged);
			// 
			// cbWidthEnabled
			// 
			this.cbWidthEnabled.AutoSize = true;
			this.cbWidthEnabled.Location = new System.Drawing.Point(222, 83);
			this.cbWidthEnabled.Name = "cbWidthEnabled";
			this.cbWidthEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbWidthEnabled.TabIndex = 0;
			this.cbWidthEnabled.UseVisualStyleBackColor = true;
			this.cbWidthEnabled.CheckedChanged += new System.EventHandler(this.cbWidthEnabled_CheckedChanged);
			// 
			// cbWhiteIcon
			// 
			this.cbWhiteIcon.AutoSize = true;
			this.cbWhiteIcon.Location = new System.Drawing.Point(11, 238);
			this.cbWhiteIcon.Name = "cbWhiteIcon";
			this.cbWhiteIcon.Size = new System.Drawing.Size(116, 17);
			this.cbWhiteIcon.TabIndex = 0;
			this.cbWhiteIcon.Text = "Use white tray icon";
			this.cbWhiteIcon.UseVisualStyleBackColor = true;
			this.cbWhiteIcon.CheckedChanged += new System.EventHandler(this.cbWhiteIcon_CheckedChanged);
			// 
			// tbSnapPath
			// 
			this.tbSnapPath.Location = new System.Drawing.Point(157, 197);
			this.tbSnapPath.Name = "tbSnapPath";
			this.tbSnapPath.Size = new System.Drawing.Size(272, 20);
			this.tbSnapPath.TabIndex = 1;
			this.tbSnapPath.ModifiedChanged += new System.EventHandler(this.tbSnapPath_ModifiedChanged);
			// 
			// lbSnapshotsavepath
			// 
			this.lbSnapshotsavepath.AutoSize = true;
			this.lbSnapshotsavepath.Location = new System.Drawing.Point(8, 202);
			this.lbSnapshotsavepath.Name = "lbSnapshotsavepath";
			this.lbSnapshotsavepath.Size = new System.Drawing.Size(102, 13);
			this.lbSnapshotsavepath.TabIndex = 2;
			this.lbSnapshotsavepath.Text = "Snapshot save path";
			// 
			// btSnapPath
			// 
			this.btSnapPath.Location = new System.Drawing.Point(435, 196);
			this.btSnapPath.Name = "btSnapPath";
			this.btSnapPath.Size = new System.Drawing.Size(31, 23);
			this.btSnapPath.TabIndex = 3;
			this.btSnapPath.Text = "...";
			this.btSnapPath.UseVisualStyleBackColor = true;
			this.btSnapPath.Click += new System.EventHandler(this.btSnapPath_Click);
			// 
			// lbNote
			// 
			this.lbNote.AutoSize = true;
			this.lbNote.Location = new System.Drawing.Point(9, 319);
			this.lbNote.Name = "lbNote";
			this.lbNote.Size = new System.Drawing.Size(316, 13);
			this.lbNote.TabIndex = 4;
			this.lbNote.Text = "Note: pen width panel overides each individual pen width settings";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 50;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lbCanvascursor
			// 
			this.lbCanvascursor.AutoSize = true;
			this.lbCanvascursor.Location = new System.Drawing.Point(9, 167);
			this.lbCanvascursor.Name = "lbCanvascursor";
			this.lbCanvascursor.Size = new System.Drawing.Size(75, 13);
			this.lbCanvascursor.TabIndex = 5;
			this.lbCanvascursor.Text = "Canvas cursor";
			// 
			// comboCanvasCursor
			// 
			this.comboCanvasCursor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCanvasCursor.FormattingEnabled = true;
			this.comboCanvasCursor.Items.AddRange(new object[] {
            "Arrow",
            "Pen tip"});
			this.comboCanvasCursor.Location = new System.Drawing.Point(157, 162);
			this.comboCanvasCursor.Name = "comboCanvasCursor";
			this.comboCanvasCursor.Size = new System.Drawing.Size(272, 21);
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
			this.tabControl1.Size = new System.Drawing.Size(492, 458);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage1.Controls.Add(this.lbLanguage);
			this.tabPage1.Controls.Add(this.pictureBox1);
			this.tabPage1.Controls.Add(this.comboLanguage);
			this.tabPage1.Controls.Add(this.comboCanvasCursor);
			this.tabPage1.Controls.Add(this.cbInkVisibleEnabled);
			this.tabPage1.Controls.Add(this.cbPanEnabled);
			this.tabPage1.Controls.Add(this.cbEraserEnabled);
			this.tabPage1.Controls.Add(this.lbCanvascursor);
			this.tabPage1.Controls.Add(this.cbWidthEnabled);
			this.tabPage1.Controls.Add(this.lbNote);
			this.tabPage1.Controls.Add(this.cbPointerEnabled);
			this.tabPage1.Controls.Add(this.btSnapPath);
			this.tabPage1.Controls.Add(this.cbSnapEnabled);
			this.tabPage1.Controls.Add(this.cbUndoEnabled);
			this.tabPage1.Controls.Add(this.cbClearEnabled);
			this.tabPage1.Controls.Add(this.lbSnapshotsavepath);
			this.tabPage1.Controls.Add(this.cbAllowDragging);
			this.tabPage1.Controls.Add(this.cbWhiteIcon);
			this.tabPage1.Controls.Add(this.tbSnapPath);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(484, 432);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::gInk.Properties.Resources.paneloption;
			this.pictureBox1.Location = new System.Drawing.Point(8, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(468, 53);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// comboLanguage
			// 
			this.comboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLanguage.FormattingEnabled = true;
			this.comboLanguage.Location = new System.Drawing.Point(157, 127);
			this.comboLanguage.Name = "comboLanguage";
			this.comboLanguage.Size = new System.Drawing.Size(272, 21);
			this.comboLanguage.TabIndex = 6;
			this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
			// 
			// cbInkVisibleEnabled
			// 
			this.cbInkVisibleEnabled.AutoSize = true;
			this.cbInkVisibleEnabled.Location = new System.Drawing.Point(275, 82);
			this.cbInkVisibleEnabled.Name = "cbInkVisibleEnabled";
			this.cbInkVisibleEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbInkVisibleEnabled.TabIndex = 0;
			this.cbInkVisibleEnabled.UseVisualStyleBackColor = true;
			this.cbInkVisibleEnabled.CheckedChanged += new System.EventHandler(this.cbInkVisibleEnabled_CheckedChanged);
			// 
			// cbPanEnabled
			// 
			this.cbPanEnabled.AutoSize = true;
			this.cbPanEnabled.Location = new System.Drawing.Point(87, 81);
			this.cbPanEnabled.Name = "cbPanEnabled";
			this.cbPanEnabled.Size = new System.Drawing.Size(15, 14);
			this.cbPanEnabled.TabIndex = 0;
			this.cbPanEnabled.UseVisualStyleBackColor = true;
			this.cbPanEnabled.CheckedChanged += new System.EventHandler(this.cbPanEnabled_CheckedChanged);
			// 
			// cbAllowDragging
			// 
			this.cbAllowDragging.AutoSize = true;
			this.cbAllowDragging.Location = new System.Drawing.Point(11, 268);
			this.cbAllowDragging.Name = "cbAllowDragging";
			this.cbAllowDragging.Size = new System.Drawing.Size(198, 17);
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
			this.tabPage2.Size = new System.Drawing.Size(484, 432);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Pens";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage3.Controls.Add(this.cbAllowHotkeyInPointer);
			this.tabPage3.Controls.Add(this.lbHkInkVisible);
			this.tabPage3.Controls.Add(this.lbHkSnapshot);
			this.tabPage3.Controls.Add(this.lbHkClear);
			this.tabPage3.Controls.Add(this.lbHkPan);
			this.tabPage3.Controls.Add(this.lbHkPointer);
			this.tabPage3.Controls.Add(this.lbHkRedo);
			this.tabPage3.Controls.Add(this.lbHkUndo);
			this.tabPage3.Controls.Add(this.lbHkEraser);
			this.tabPage3.Controls.Add(this.lbGlobalHotkey);
			this.tabPage3.Controls.Add(this.hiInkVisible);
			this.tabPage3.Controls.Add(this.hiSnapshot);
			this.tabPage3.Controls.Add(this.hiClear);
			this.tabPage3.Controls.Add(this.hiPan);
			this.tabPage3.Controls.Add(this.hiPointer);
			this.tabPage3.Controls.Add(this.hiRedo);
			this.tabPage3.Controls.Add(this.hiUndo);
			this.tabPage3.Controls.Add(this.hiEraser);
			this.tabPage3.Controls.Add(this.hiGlobal);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(484, 432);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Hotkeys";
			// 
			// cbAllowHotkeyInPointer
			// 
			this.cbAllowHotkeyInPointer.AutoSize = true;
			this.cbAllowHotkeyInPointer.Location = new System.Drawing.Point(19, 87);
			this.cbAllowHotkeyInPointer.Name = "cbAllowHotkeyInPointer";
			this.cbAllowHotkeyInPointer.Size = new System.Drawing.Size(356, 17);
			this.cbAllowHotkeyInPointer.TabIndex = 18;
			this.cbAllowHotkeyInPointer.Text = "Allow all following hotkeys in mouse pointer mode (may cause a mess):";
			this.cbAllowHotkeyInPointer.UseVisualStyleBackColor = true;
			this.cbAllowHotkeyInPointer.CheckedChanged += new System.EventHandler(this.cbAllowHotkeyInPointer_CheckedChanged);
			// 
			// lbHkInkVisible
			// 
			this.lbHkInkVisible.AutoSize = true;
			this.lbHkInkVisible.Location = new System.Drawing.Point(236, 202);
			this.lbHkInkVisible.Name = "lbHkInkVisible";
			this.lbHkInkVisible.Size = new System.Drawing.Size(57, 13);
			this.lbHkInkVisible.TabIndex = 16;
			this.lbHkInkVisible.Text = "View/Hide";
			// 
			// lbHkSnapshot
			// 
			this.lbHkSnapshot.AutoSize = true;
			this.lbHkSnapshot.Location = new System.Drawing.Point(236, 229);
			this.lbHkSnapshot.Name = "lbHkSnapshot";
			this.lbHkSnapshot.Size = new System.Drawing.Size(52, 13);
			this.lbHkSnapshot.TabIndex = 12;
			this.lbHkSnapshot.Text = "Snapshot";
			// 
			// lbHkClear
			// 
			this.lbHkClear.AutoSize = true;
			this.lbHkClear.Location = new System.Drawing.Point(236, 310);
			this.lbHkClear.Name = "lbHkClear";
			this.lbHkClear.Size = new System.Drawing.Size(31, 13);
			this.lbHkClear.TabIndex = 13;
			this.lbHkClear.Text = "Clear";
			// 
			// lbHkPan
			// 
			this.lbHkPan.AutoSize = true;
			this.lbHkPan.Location = new System.Drawing.Point(236, 147);
			this.lbHkPan.Name = "lbHkPan";
			this.lbHkPan.Size = new System.Drawing.Size(26, 13);
			this.lbHkPan.TabIndex = 10;
			this.lbHkPan.Text = "Pan";
			// 
			// lbHkPointer
			// 
			this.lbHkPointer.AutoSize = true;
			this.lbHkPointer.Location = new System.Drawing.Point(236, 175);
			this.lbHkPointer.Name = "lbHkPointer";
			this.lbHkPointer.Size = new System.Drawing.Size(74, 13);
			this.lbHkPointer.TabIndex = 6;
			this.lbHkPointer.Text = "Mouse pointer";
			this.lbHkPointer.Visible = false;
			// 
			// lbHkRedo
			// 
			this.lbHkRedo.AutoSize = true;
			this.lbHkRedo.Location = new System.Drawing.Point(236, 283);
			this.lbHkRedo.Name = "lbHkRedo";
			this.lbHkRedo.Size = new System.Drawing.Size(33, 13);
			this.lbHkRedo.TabIndex = 7;
			this.lbHkRedo.Text = "Redo";
			// 
			// lbHkUndo
			// 
			this.lbHkUndo.AutoSize = true;
			this.lbHkUndo.Location = new System.Drawing.Point(236, 256);
			this.lbHkUndo.Name = "lbHkUndo";
			this.lbHkUndo.Size = new System.Drawing.Size(33, 13);
			this.lbHkUndo.TabIndex = 4;
			this.lbHkUndo.Text = "Undo";
			// 
			// lbHkEraser
			// 
			this.lbHkEraser.AutoSize = true;
			this.lbHkEraser.Location = new System.Drawing.Point(236, 120);
			this.lbHkEraser.Name = "lbHkEraser";
			this.lbHkEraser.Size = new System.Drawing.Size(37, 13);
			this.lbHkEraser.TabIndex = 4;
			this.lbHkEraser.Text = "Eraser";
			// 
			// lbGlobalHotkey
			// 
			this.lbGlobalHotkey.AutoSize = true;
			this.lbGlobalHotkey.Location = new System.Drawing.Point(17, 18);
			this.lbGlobalHotkey.Name = "lbGlobalHotkey";
			this.lbGlobalHotkey.Size = new System.Drawing.Size(351, 13);
			this.lbGlobalHotkey.TabIndex = 4;
			this.lbGlobalHotkey.Text = "Global hotkey (start drawing, switch between mouse pointer and drawing)";
			// 
			// hiInkVisible
			// 
			this.hiInkVisible.BackColor = System.Drawing.Color.White;
			this.hiInkVisible.ExternalConflictFlag = false;
			this.hiInkVisible.Hotkey = hotkey1;
			this.hiInkVisible.Location = new System.Drawing.Point(340, 202);
			this.hiInkVisible.Name = "hiInkVisible";
			this.hiInkVisible.RequireModifier = false;
			this.hiInkVisible.Size = new System.Drawing.Size(120, 20);
			this.hiInkVisible.TabIndex = 17;
			this.hiInkVisible.Text = "None";
			this.hiInkVisible.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiSnapshot
			// 
			this.hiSnapshot.BackColor = System.Drawing.Color.White;
			this.hiSnapshot.ExternalConflictFlag = false;
			this.hiSnapshot.Hotkey = hotkey2;
			this.hiSnapshot.Location = new System.Drawing.Point(340, 229);
			this.hiSnapshot.Name = "hiSnapshot";
			this.hiSnapshot.RequireModifier = false;
			this.hiSnapshot.Size = new System.Drawing.Size(120, 20);
			this.hiSnapshot.TabIndex = 14;
			this.hiSnapshot.Text = "None";
			this.hiSnapshot.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiClear
			// 
			this.hiClear.BackColor = System.Drawing.Color.White;
			this.hiClear.ExternalConflictFlag = false;
			this.hiClear.Hotkey = hotkey3;
			this.hiClear.Location = new System.Drawing.Point(340, 310);
			this.hiClear.Name = "hiClear";
			this.hiClear.RequireModifier = false;
			this.hiClear.Size = new System.Drawing.Size(120, 20);
			this.hiClear.TabIndex = 15;
			this.hiClear.Text = "None";
			this.hiClear.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiPan
			// 
			this.hiPan.BackColor = System.Drawing.Color.White;
			this.hiPan.ExternalConflictFlag = false;
			this.hiPan.Hotkey = hotkey4;
			this.hiPan.Location = new System.Drawing.Point(340, 147);
			this.hiPan.Name = "hiPan";
			this.hiPan.RequireModifier = false;
			this.hiPan.Size = new System.Drawing.Size(120, 20);
			this.hiPan.TabIndex = 11;
			this.hiPan.Text = "None";
			this.hiPan.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiPointer
			// 
			this.hiPointer.BackColor = System.Drawing.Color.White;
			this.hiPointer.ExternalConflictFlag = false;
			this.hiPointer.Hotkey = hotkey5;
			this.hiPointer.Location = new System.Drawing.Point(340, 175);
			this.hiPointer.Name = "hiPointer";
			this.hiPointer.RequireModifier = false;
			this.hiPointer.Size = new System.Drawing.Size(120, 20);
			this.hiPointer.TabIndex = 8;
			this.hiPointer.Text = "None";
			this.hiPointer.Visible = false;
			this.hiPointer.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiRedo
			// 
			this.hiRedo.BackColor = System.Drawing.Color.White;
			this.hiRedo.ExternalConflictFlag = false;
			this.hiRedo.Hotkey = hotkey6;
			this.hiRedo.Location = new System.Drawing.Point(340, 283);
			this.hiRedo.Name = "hiRedo";
			this.hiRedo.RequireModifier = false;
			this.hiRedo.Size = new System.Drawing.Size(120, 20);
			this.hiRedo.TabIndex = 9;
			this.hiRedo.Text = "None";
			this.hiRedo.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiUndo
			// 
			this.hiUndo.BackColor = System.Drawing.Color.White;
			this.hiUndo.ExternalConflictFlag = false;
			this.hiUndo.Hotkey = hotkey7;
			this.hiUndo.Location = new System.Drawing.Point(340, 256);
			this.hiUndo.Name = "hiUndo";
			this.hiUndo.RequireModifier = false;
			this.hiUndo.Size = new System.Drawing.Size(120, 20);
			this.hiUndo.TabIndex = 5;
			this.hiUndo.Text = "None";
			this.hiUndo.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiEraser
			// 
			this.hiEraser.BackColor = System.Drawing.Color.White;
			this.hiEraser.ExternalConflictFlag = false;
			this.hiEraser.Hotkey = hotkey8;
			this.hiEraser.Location = new System.Drawing.Point(340, 120);
			this.hiEraser.Name = "hiEraser";
			this.hiEraser.RequireModifier = false;
			this.hiEraser.Size = new System.Drawing.Size(120, 20);
			this.hiEraser.TabIndex = 5;
			this.hiEraser.Text = "None";
			this.hiEraser.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// hiGlobal
			// 
			this.hiGlobal.BackColor = System.Drawing.Color.White;
			this.hiGlobal.ExternalConflictFlag = false;
			this.hiGlobal.Hotkey = hotkey9;
			this.hiGlobal.Location = new System.Drawing.Point(19, 39);
			this.hiGlobal.Name = "hiGlobal";
			this.hiGlobal.RequireModifier = true;
			this.hiGlobal.Size = new System.Drawing.Size(120, 20);
			this.hiGlobal.TabIndex = 5;
			this.hiGlobal.Text = "None";
			this.hiGlobal.OnHotkeyChanged += new System.EventHandler(this.hi_OnHotkeyChanged);
			// 
			// lbLanguage
			// 
			this.lbLanguage.AutoSize = true;
			this.lbLanguage.Location = new System.Drawing.Point(9, 130);
			this.lbLanguage.Name = "lbLanguage";
			this.lbLanguage.Size = new System.Drawing.Size(55, 13);
			this.lbLanguage.TabIndex = 8;
			this.lbLanguage.Text = "Language";
			// 
			// FormOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(487, 453);
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
		private System.Windows.Forms.Label lbSnapshotsavepath;
		private System.Windows.Forms.Button btSnapPath;
		private System.Windows.Forms.Label lbNote;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lbCanvascursor;
		private System.Windows.Forms.ComboBox comboCanvasCursor;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label lbGlobalHotkey;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox cbInkVisibleEnabled;
		private System.Windows.Forms.CheckBox cbPanEnabled;
		private System.Windows.Forms.CheckBox cbAllowDragging;
		private HotkeyInputBox hiGlobal;
		private HotkeyInputBox hiEraser;
		private System.Windows.Forms.Label lbHkEraser;
		private HotkeyInputBox hiUndo;
		private System.Windows.Forms.Label lbHkUndo;
		private HotkeyInputBox hiPointer;
		private HotkeyInputBox hiRedo;
		private System.Windows.Forms.Label lbHkPointer;
		private System.Windows.Forms.Label lbHkRedo;
		private HotkeyInputBox hiSnapshot;
		private HotkeyInputBox hiClear;
		private System.Windows.Forms.Label lbHkSnapshot;
		private System.Windows.Forms.Label lbHkClear;
		private HotkeyInputBox hiPan;
		private System.Windows.Forms.Label lbHkPan;
		private HotkeyInputBox hiInkVisible;
		private System.Windows.Forms.Label lbHkInkVisible;
		private System.Windows.Forms.CheckBox cbAllowHotkeyInPointer;
		private System.Windows.Forms.ComboBox comboLanguage;
		private System.Windows.Forms.Label lbLanguage;
	}
}