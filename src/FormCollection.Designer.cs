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
			this.btDock = new System.Windows.Forms.Button();
			this.btPenWidth = new System.Windows.Forms.Button();
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
			this.gpButtons.Controls.Add(this.btDock);
			this.gpButtons.Controls.Add(this.btPenWidth);
			this.gpButtons.Controls.Add(this.btEraser);
			this.gpButtons.Controls.Add(this.btSnap);
			this.gpButtons.Controls.Add(this.btPointer);
			this.gpButtons.Controls.Add(this.btStop);
			this.gpButtons.Controls.Add(this.btClear);
			this.gpButtons.Controls.Add(this.btUndo);
			this.gpButtons.Location = new System.Drawing.Point(24, 48);
			this.gpButtons.Margin = new System.Windows.Forms.Padding(2);
			this.gpButtons.Name = "gpButtons";
			this.gpButtons.Size = new System.Drawing.Size(733, 53);
			this.gpButtons.TabIndex = 3;
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
			this.btDock.Location = new System.Drawing.Point(0, 3);
			this.btDock.Margin = new System.Windows.Forms.Padding(2);
			this.btDock.Name = "btDock";
			this.btDock.Size = new System.Drawing.Size(34, 46);
			this.btDock.TabIndex = 0;
			this.toolTip.SetToolTip(this.btDock, "Dock / Undock");
			this.btDock.UseVisualStyleBackColor = false;
			this.btDock.Click += new System.EventHandler(this.btDock_Click);
			// 
			// btPenWidth
			// 
			this.btPenWidth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btPenWidth.FlatAppearance.BorderSize = 0;
			this.btPenWidth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPenWidth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btPenWidth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPenWidth.Image = global::gInk.Properties.Resources.penwidth;
			this.btPenWidth.Location = new System.Drawing.Point(266, 3);
			this.btPenWidth.Margin = new System.Windows.Forms.Padding(2);
			this.btPenWidth.Name = "btPenWidth";
			this.btPenWidth.Size = new System.Drawing.Size(46, 46);
			this.btPenWidth.TabIndex = 0;
			this.toolTip.SetToolTip(this.btPenWidth, "Pen width");
			this.btPenWidth.UseVisualStyleBackColor = true;
			this.btPenWidth.Click += new System.EventHandler(this.btPenWidth_Click);
			// 
			// btEraser
			// 
			this.btEraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btEraser.FlatAppearance.BorderSize = 0;
			this.btEraser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btEraser.Image = global::gInk.Properties.Resources.eraser;
			this.btEraser.Location = new System.Drawing.Point(326, 3);
			this.btEraser.Margin = new System.Windows.Forms.Padding(2);
			this.btEraser.Name = "btEraser";
			this.btEraser.Size = new System.Drawing.Size(46, 46);
			this.btEraser.TabIndex = 0;
			this.toolTip.SetToolTip(this.btEraser, "Eraser");
			this.btEraser.UseVisualStyleBackColor = true;
			this.btEraser.Click += new System.EventHandler(this.btEraser_Click);
			// 
			// btSnap
			// 
			this.btSnap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btSnap.FlatAppearance.BorderSize = 0;
			this.btSnap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btSnap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btSnap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSnap.Image = global::gInk.Properties.Resources.snap;
			this.btSnap.Location = new System.Drawing.Point(469, 3);
			this.btSnap.Margin = new System.Windows.Forms.Padding(2);
			this.btSnap.Name = "btSnap";
			this.btSnap.Size = new System.Drawing.Size(46, 46);
			this.btSnap.TabIndex = 0;
			this.toolTip.SetToolTip(this.btSnap, "Snapshot");
			this.btSnap.UseVisualStyleBackColor = true;
			this.btSnap.Click += new System.EventHandler(this.btSnap_Click);
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
			this.btPointer.Location = new System.Drawing.Point(383, 3);
			this.btPointer.Margin = new System.Windows.Forms.Padding(2);
			this.btPointer.Name = "btPointer";
			this.btPointer.Size = new System.Drawing.Size(46, 46);
			this.btPointer.TabIndex = 0;
			this.toolTip.SetToolTip(this.btPointer, "Mouse pointer");
			this.btPointer.UseVisualStyleBackColor = true;
			this.btPointer.Click += new System.EventHandler(this.btPointer_Click);
			// 
			// btStop
			// 
			this.btStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btStop.FlatAppearance.BorderSize = 0;
			this.btStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btStop.Image = global::gInk.Properties.Resources.exit;
			this.btStop.Location = new System.Drawing.Point(669, 3);
			this.btStop.Margin = new System.Windows.Forms.Padding(2);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(46, 46);
			this.btStop.TabIndex = 0;
			this.toolTip.SetToolTip(this.btStop, "Exit drawing");
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// btClear
			// 
			this.btClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btClear.FlatAppearance.BorderSize = 0;
			this.btClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btClear.Image = global::gInk.Properties.Resources.garbage;
			this.btClear.Location = new System.Drawing.Point(583, 3);
			this.btClear.Margin = new System.Windows.Forms.Padding(2);
			this.btClear.Name = "btClear";
			this.btClear.Size = new System.Drawing.Size(46, 46);
			this.btClear.TabIndex = 1;
			this.toolTip.SetToolTip(this.btClear, "Clear");
			this.btClear.UseVisualStyleBackColor = true;
			this.btClear.Click += new System.EventHandler(this.btClear_Click);
			// 
			// btUndo
			// 
			this.btUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btUndo.FlatAppearance.BorderSize = 0;
			this.btUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btUndo.Image = global::gInk.Properties.Resources.undo;
			this.btUndo.Location = new System.Drawing.Point(526, 3);
			this.btUndo.Margin = new System.Windows.Forms.Padding(2);
			this.btUndo.Name = "btUndo";
			this.btUndo.Size = new System.Drawing.Size(46, 46);
			this.btUndo.TabIndex = 1;
			this.toolTip.SetToolTip(this.btUndo, "Undo");
			this.btUndo.UseVisualStyleBackColor = true;
			this.btUndo.Click += new System.EventHandler(this.btUndo_Click);
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
			this.gpPenWidth.Location = new System.Drawing.Point(116, 217);
			this.gpPenWidth.Name = "gpPenWidth";
			this.gpPenWidth.Size = new System.Drawing.Size(200, 53);
			this.gpPenWidth.TabIndex = 4;
			this.gpPenWidth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseDown);
			this.gpPenWidth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseMove);
			this.gpPenWidth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpPenWidth_MouseUp);
			// 
			// pboxPenWidthIndicator
			// 
			this.pboxPenWidthIndicator.BackColor = System.Drawing.Color.Orange;
			this.pboxPenWidthIndicator.Location = new System.Drawing.Point(78, 0);
			this.pboxPenWidthIndicator.Name = "pboxPenWidthIndicator";
			this.pboxPenWidthIndicator.Size = new System.Drawing.Size(5, 53);
			this.pboxPenWidthIndicator.TabIndex = 5;
			this.pboxPenWidthIndicator.TabStop = false;
			this.pboxPenWidthIndicator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseDown);
			this.pboxPenWidthIndicator.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseMove);
			this.pboxPenWidthIndicator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pboxPenWidthIndicator_MouseUp);
			// 
			// FormCollection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(863, 526);
			this.Controls.Add(this.gpPenWidth);
			this.Controls.Add(this.gpButtons);
			this.ForeColor = System.Drawing.Color.LawnGreen;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2);
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
		public System.Windows.Forms.Button btEraser;
		private System.Windows.Forms.Timer tiSlide;
		public System.Windows.Forms.Button btDock;
		public System.Windows.Forms.Button btSnap;
		public System.Windows.Forms.Button btPointer;
		public System.Windows.Forms.Button btPenWidth;
		public System.Windows.Forms.ToolTip toolTip;
		public System.Windows.Forms.Panel gpPenWidth;
		private System.Windows.Forms.PictureBox pboxPenWidthIndicator;
	}
}

