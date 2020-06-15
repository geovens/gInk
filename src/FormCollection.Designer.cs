namespace gInk
{
	partial class FormCollection
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
			this.gpButtons = new System.Windows.Forms.Panel();
			this.btInkVisible = new System.Windows.Forms.Button();
			this.btPan = new System.Windows.Forms.Button();
			this.btDock = new System.Windows.Forms.Button();
			this.btPenWidth = new System.Windows.Forms.Button();

            this.btHand = new System.Windows.Forms.Button();
            this.btLine = new System.Windows.Forms.Button();
            this.btRect = new System.Windows.Forms.Button();
            this.btOval = new System.Windows.Forms.Button();
            this.btStAr = new System.Windows.Forms.Button();
            this.btEnAr = new System.Windows.Forms.Button();

            this.btEraser = new System.Windows.Forms.Button();
			this.btSnap = new System.Windows.Forms.Button();
			this.btPointer = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.btClear = new System.Windows.Forms.Button();
			this.btUndo = new System.Windows.Forms.Button();
			this.tiSlide = new System.Windows.Forms.Timer(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.gpPenWidth = new System.Windows.Forms.Panel();
			this.pboxPenWidthIndicator = new System.Windows.Forms.PictureBox();
			this.gpButtons.SuspendLayout();
			this.gpPenWidth.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pboxPenWidthIndicator)).BeginInit();
			this.SuspendLayout();
			// 
			// gpButtons
			// 
			this.gpButtons.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gpButtons.Controls.Add(this.btInkVisible);
			this.gpButtons.Controls.Add(this.btPan);
			this.gpButtons.Controls.Add(this.btDock);
			this.gpButtons.Controls.Add(this.btPenWidth);

            this.gpButtons.Controls.Add(this.btHand);
            this.gpButtons.Controls.Add(this.btLine);
            this.gpButtons.Controls.Add(this.btRect);
            this.gpButtons.Controls.Add(this.btOval);
            this.gpButtons.Controls.Add(this.btStAr);
            this.gpButtons.Controls.Add(this.btEnAr);


            this.gpButtons.Controls.Add(this.btEraser);
			this.gpButtons.Controls.Add(this.btSnap);
			this.gpButtons.Controls.Add(this.btPointer);
			this.gpButtons.Controls.Add(this.btStop);
			this.gpButtons.Controls.Add(this.btClear);
			this.gpButtons.Controls.Add(this.btUndo);
			this.gpButtons.Location = new System.Drawing.Point(36, 72);
			this.gpButtons.Name = "gpButtons";
			this.gpButtons.Size = new System.Drawing.Size(1242, 80);
			this.gpButtons.TabIndex = 3;
			this.gpButtons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.gpButtons.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.gpButtons.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btInkVisible
			// 
			this.btInkVisible.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btInkVisible.FlatAppearance.BorderSize = 0;
			this.btInkVisible.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btInkVisible.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btInkVisible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btInkVisible.Image = global::gInk.Properties.Resources.visible;
			this.btInkVisible.Location = new System.Drawing.Point(1053, 4);
			this.btInkVisible.Name = "btInkVisible";
			this.btInkVisible.Size = new System.Drawing.Size(69, 69);
			this.btInkVisible.TabIndex = 3;
			this.toolTip.SetToolTip(this.btInkVisible, "Ink visible");
			this.btInkVisible.UseVisualStyleBackColor = true;
			this.btInkVisible.Click += new System.EventHandler(this.btInkVisible_Click);
			this.btInkVisible.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btInkVisible.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btInkVisible.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btPan
			// 
			this.btPan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btPan.FlatAppearance.BorderSize = 0;
			this.btPan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPan.Image = global::gInk.Properties.Resources.pan;
			this.btPan.Location = new System.Drawing.Point(978, 4);
			this.btPan.Name = "btPan";
			this.btPan.Size = new System.Drawing.Size(69, 69);
			this.btPan.TabIndex = 2;
			this.toolTip.SetToolTip(this.btPan, "Pan");
			this.btPan.UseVisualStyleBackColor = true;
			this.btPan.Click += new System.EventHandler(this.btPan_Click);
			this.btPan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btPan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btPan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btDock
			// 
			this.btDock.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btDock.FlatAppearance.BorderSize = 0;
			this.btDock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btDock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDock.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.btDock.Image = global::gInk.Properties.Resources.dock;
			this.btDock.Location = new System.Drawing.Point(0, 4);
			this.btDock.Name = "btDock";
			this.btDock.Size = new System.Drawing.Size(51, 69);
			this.btDock.TabIndex = 0;
			this.toolTip.SetToolTip(this.btDock, "Dock / Undock");
			this.btDock.UseVisualStyleBackColor = false;
			this.btDock.Click += new System.EventHandler(this.btDock_Click);
			this.btDock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btDock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btDock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btPenWidth
			// 
			this.btPenWidth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btPenWidth.FlatAppearance.BorderSize = 0;
			this.btPenWidth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPenWidth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPenWidth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPenWidth.Image = global::gInk.Properties.Resources.penwidth;
			this.btPenWidth.Location = new System.Drawing.Point(399, 4);
			this.btPenWidth.Name = "btPenWidth";
			this.btPenWidth.Size = new System.Drawing.Size(69, 69);
			this.btPenWidth.TabIndex = 0;
			this.toolTip.SetToolTip(this.btPenWidth, "Pen width");
			this.btPenWidth.UseVisualStyleBackColor = true;
			this.btPenWidth.Click += new System.EventHandler(this.btPenWidth_Click);
			this.btPenWidth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btPenWidth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btPenWidth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);

            // 
            // btHand
            // 
            this.btHand.FlatAppearance.BorderSize = 0;
            this.btHand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btHand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btHand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btHand.ForeColor = System.Drawing.Color.Transparent;
            this.btHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btHand.BackgroundImage = global::gInk.Properties.Resources.tool_hand;
            this.btHand.Location = new System.Drawing.Point(489, 4);
            this.btHand.Name = "btHand";
            this.btHand.Size = new System.Drawing.Size(69, 69);
            this.btHand.TabIndex = 0;
            this.toolTip.SetToolTip(this.btHand, "Hand Drawing");
            this.btHand.UseVisualStyleBackColor = false;
            this.btHand.Click += new System.EventHandler(this.btTool_Click);
            this.btHand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btHand.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btHand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
            // 
            // btLine
            // 
            this.btLine.FlatAppearance.BorderSize = 0;
            this.btLine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btLine.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLine.ForeColor = System.Drawing.Color.Transparent;
            this.btLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLine.BackgroundImage = global::gInk.Properties.Resources.tool_line;
            this.btLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLine.Location = new System.Drawing.Point(489, 4);
            this.btLine.Name = "btLine";
            this.btLine.Size = new System.Drawing.Size(69, 69);
            this.btLine.TabIndex = 0;
            this.toolTip.SetToolTip(this.btLine, "Line Drawing");
            this.btLine.UseVisualStyleBackColor = false;
            this.btLine.Click += new System.EventHandler(this.btTool_Click);
            this.btLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btLine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
            // 
            // btRect
            // 
            this.btRect.FlatAppearance.BorderSize = 0;
            this.btRect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btRect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btRect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRect.ForeColor = System.Drawing.Color.Transparent;
            this.btRect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRect.BackgroundImage = global::gInk.Properties.Resources.tool_rect;
            this.btRect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRect.Location = new System.Drawing.Point(489, 4);
            this.btRect.Name = "btRect";
            this.btRect.Size = new System.Drawing.Size(69, 69);
            this.btRect.TabIndex = 0;
            this.toolTip.SetToolTip(this.btRect, "Rect Shape");
            this.btRect.UseVisualStyleBackColor = false;
            this.btRect.Click += new System.EventHandler(this.btTool_Click);
            this.btRect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btRect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btRect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
            // 
            // btOval
            // 
            this.btOval.FlatAppearance.BorderSize = 0;
            this.btOval.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btOval.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btOval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOval.ForeColor = System.Drawing.Color.Transparent;
            this.btOval.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btOval.BackgroundImage = global::gInk.Properties.Resources.tool_oval;
            this.btOval.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btOval.Location = new System.Drawing.Point(489, 4);
            this.btOval.Name = "btOval";
            this.btOval.Size = new System.Drawing.Size(69, 69);
            this.btOval.TabIndex = 0;
            this.toolTip.SetToolTip(this.btOval, "Oval Shape");
            this.btOval.UseVisualStyleBackColor = false;
            this.btOval.Click += new System.EventHandler(this.btTool_Click);
            this.btOval.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btOval.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btOval.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
            // 
            // btStAr
            // 
            this.btStAr.FlatAppearance.BorderSize = 0;
            this.btStAr.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btStAr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btStAr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btStAr.ForeColor = System.Drawing.Color.Transparent;
            this.btStAr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btStAr.BackgroundImage = global::gInk.Properties.Resources.tool_stAr;
            this.btStAr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btStAr.Location = new System.Drawing.Point(489, 4);
            this.btStAr.Name = "btStAr";
            this.btStAr.Size = new System.Drawing.Size(69, 69);
            this.btStAr.TabIndex = 0;
            this.toolTip.SetToolTip(this.btStAr, "Starting Arrow Shape");
            this.btStAr.UseVisualStyleBackColor = false;
            this.btStAr.Click += new System.EventHandler(this.btTool_Click);
            this.btStAr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btStAr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btStAr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
            // 
            // btEnAr
            // 
            this.btEnAr.FlatAppearance.BorderSize = 0;
            this.btEnAr.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btEnAr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btEnAr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEnAr.ForeColor = System.Drawing.Color.Transparent;
            this.btEnAr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btEnAr.BackgroundImage = global::gInk.Properties.Resources.tool_enAr;
            this.btEnAr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btEnAr.Location = new System.Drawing.Point(489, 4);
            this.btEnAr.Name = "btEnAr";
            this.btEnAr.Size = new System.Drawing.Size(69, 69);
            this.btEnAr.TabIndex = 0;
            this.toolTip.SetToolTip(this.btEnAr, "Ending Arrow Shape");
            this.btEnAr.UseVisualStyleBackColor = false;
            this.btEnAr.Click += new System.EventHandler(this.btTool_Click);
            this.btEnAr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
            this.btEnAr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
            this.btEnAr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);


            // 
            // btEraser
            // 
			this.btEraser.FlatAppearance.BorderSize = 0;
			this.btEraser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btEraser.ForeColor = System.Drawing.Color.Transparent;
			this.btEraser.Image = global::gInk.Properties.Resources.eraser;
			this.btEraser.Location = new System.Drawing.Point(489, 4);
			this.btEraser.Name = "btEraser";
			this.btEraser.Size = new System.Drawing.Size(69, 69);
			this.btEraser.TabIndex = 0;
			this.toolTip.SetToolTip(this.btEraser, "Eraser");
			this.btEraser.UseVisualStyleBackColor = false;
			this.btEraser.Click += new System.EventHandler(this.btEraser_Click);
			this.btEraser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btEraser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btEraser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btSnap
			// 
			this.btSnap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btSnap.FlatAppearance.BorderSize = 0;
			this.btSnap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btSnap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btSnap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSnap.Image = global::gInk.Properties.Resources.snap;
			this.btSnap.Location = new System.Drawing.Point(704, 4);
			this.btSnap.Name = "btSnap";
			this.btSnap.Size = new System.Drawing.Size(69, 69);
			this.btSnap.TabIndex = 0;
			this.toolTip.SetToolTip(this.btSnap, "Snapshot");
			this.btSnap.UseVisualStyleBackColor = true;
			this.btSnap.Click += new System.EventHandler(this.btSnap_Click);
			this.btSnap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btSnap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btSnap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btPointer
			// 
			this.btPointer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btPointer.FlatAppearance.BorderSize = 0;
			this.btPointer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPointer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPointer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPointer.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.btPointer.Image = global::gInk.Properties.Resources.pointer;
			this.btPointer.Location = new System.Drawing.Point(574, 4);
			this.btPointer.Name = "btPointer";
			this.btPointer.Size = new System.Drawing.Size(69, 69);
			this.btPointer.TabIndex = 0;
			this.toolTip.SetToolTip(this.btPointer, "Mouse pointer");
			this.btPointer.UseVisualStyleBackColor = true;
			this.btPointer.Click += new System.EventHandler(this.btPointer_Click);
			this.btPointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btPointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btPointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btStop
			// 
			this.btStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btStop.FlatAppearance.BorderSize = 0;
			this.btStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btStop.Image = global::gInk.Properties.Resources.exit;
			this.btStop.Location = new System.Drawing.Point(1140, 4);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(69, 69);
			this.btStop.TabIndex = 0;
			this.toolTip.SetToolTip(this.btStop, "Exit drawing");
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			this.btStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btStop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btClear
			// 
			this.btClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btClear.FlatAppearance.BorderSize = 0;
			this.btClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btClear.Image = global::gInk.Properties.Resources.garbage;
			this.btClear.Location = new System.Drawing.Point(874, 4);
			this.btClear.Name = "btClear";
			this.btClear.Size = new System.Drawing.Size(69, 69);
			this.btClear.TabIndex = 1;
			this.toolTip.SetToolTip(this.btClear, "Clear");
			this.btClear.UseVisualStyleBackColor = true;
			this.btClear.Click += new System.EventHandler(this.btClear_Click);
			this.btClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btClear.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btClear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// btUndo
			// 
			this.btUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btUndo.FlatAppearance.BorderSize = 0;
			this.btUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btUndo.Image = global::gInk.Properties.Resources.undo;
			this.btUndo.Location = new System.Drawing.Point(789, 4);
			this.btUndo.Name = "btUndo";
			this.btUndo.Size = new System.Drawing.Size(69, 69);
			this.btUndo.TabIndex = 1;
			this.toolTip.SetToolTip(this.btUndo, "Undo");
			this.btUndo.UseVisualStyleBackColor = true;
			this.btUndo.Click += new System.EventHandler(this.btUndo_Click);
			this.btUndo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseDown);
			this.btUndo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseMove);
			this.btUndo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpButtons_MouseUp);
			// 
			// tiSlide
			// 
			this.tiSlide.Interval = 15;
			this.tiSlide.Tick += new System.EventHandler(this.tiSlide_Tick);
			// 
			// gpPenWidth
			// 
			this.gpPenWidth.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gpPenWidth.BackgroundImage = global::gInk.Properties.Resources.penwidthpanel;
			this.gpPenWidth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.gpPenWidth.Controls.Add(this.pboxPenWidthIndicator);
			this.gpPenWidth.Location = new System.Drawing.Point(174, 326);
			this.gpPenWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gpPenWidth.Name = "gpPenWidth";
			this.gpPenWidth.Size = new System.Drawing.Size(300, 80);
			this.gpPenWidth.TabIndex = 4;
			this.gpPenWidth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseDown);
			this.gpPenWidth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseMove);
			this.gpPenWidth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseUp);
			// 
			// pboxPenWidthIndicator
			// 
			this.pboxPenWidthIndicator.BackColor = System.Drawing.Color.Orange;
			this.pboxPenWidthIndicator.Location = new System.Drawing.Point(117, 0);
			this.pboxPenWidthIndicator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pboxPenWidthIndicator.Name = "pboxPenWidthIndicator";
			this.pboxPenWidthIndicator.Size = new System.Drawing.Size(8, 80);
			this.pboxPenWidthIndicator.TabIndex = 5;
			this.pboxPenWidthIndicator.TabStop = false;
			this.pboxPenWidthIndicator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseDown);
			this.pboxPenWidthIndicator.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseMove);
			this.pboxPenWidthIndicator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseUp);
			// 
			// FormCollection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1294, 789);
			this.Controls.Add(this.gpPenWidth);
			this.Controls.Add(this.gpButtons);
			this.ForeColor = System.Drawing.Color.LawnGreen;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCollection";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.gpButtons.ResumeLayout(false);
			this.gpPenWidth.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pboxPenWidthIndicator)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button btStop;
		public System.Windows.Forms.Button btClear;
		public System.Windows.Forms.Button btUndo;
		public System.Windows.Forms.Panel gpButtons;

        public System.Windows.Forms.Button btHand;
        public System.Windows.Forms.Button btLine;
        public System.Windows.Forms.Button btRect;
        public System.Windows.Forms.Button btOval;
        public System.Windows.Forms.Button btStAr;
        public System.Windows.Forms.Button btEnAr;

        public System.Windows.Forms.Button btEraser;
		private System.Windows.Forms.Timer tiSlide;
		public System.Windows.Forms.Button btDock;
		public System.Windows.Forms.Button btSnap;
		public System.Windows.Forms.Button btPointer;
		public System.Windows.Forms.Button btPenWidth;
		public System.Windows.Forms.ToolTip toolTip;
		public System.Windows.Forms.Panel gpPenWidth;
		private System.Windows.Forms.PictureBox pboxPenWidthIndicator;
		public System.Windows.Forms.Button btPan;
		public System.Windows.Forms.Button btInkVisible;
	}
}

