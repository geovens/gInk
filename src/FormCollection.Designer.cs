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
			this.btPen3 = new System.Windows.Forms.Button();
			this.btPen2 = new System.Windows.Forms.Button();
			this.btPen1 = new System.Windows.Forms.Button();
			this.gpButtons = new System.Windows.Forms.Panel();
			this.btEraser = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.btClear = new System.Windows.Forms.Button();
			this.tiSlide = new System.Windows.Forms.Timer(this.components);
			this.gpButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// btPen3
			// 
			this.btPen3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.btPen3.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btPen3.FlatAppearance.BorderSize = 3;
			this.btPen3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.btPen3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.btPen3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPen3.Location = new System.Drawing.Point(200, 5);
			this.btPen3.Name = "btPen3";
			this.btPen3.Size = new System.Drawing.Size(80, 80);
			this.btPen3.TabIndex = 1;
			this.btPen3.UseVisualStyleBackColor = false;
			this.btPen3.Click += new System.EventHandler(this.btColor_Click);
			// 
			// btPen2
			// 
			this.btPen2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(0)))));
			this.btPen2.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btPen2.FlatAppearance.BorderSize = 3;
			this.btPen2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(0)))));
			this.btPen2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(0)))));
			this.btPen2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPen2.Location = new System.Drawing.Point(100, 5);
			this.btPen2.Name = "btPen2";
			this.btPen2.Size = new System.Drawing.Size(80, 80);
			this.btPen2.TabIndex = 1;
			this.btPen2.UseVisualStyleBackColor = false;
			this.btPen2.Click += new System.EventHandler(this.btColor_Click);
			// 
			// btPen1
			// 
			this.btPen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(250)))));
			this.btPen1.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btPen1.FlatAppearance.BorderSize = 3;
			this.btPen1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(250)))));
			this.btPen1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(250)))));
			this.btPen1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPen1.Location = new System.Drawing.Point(10, 5);
			this.btPen1.Name = "btPen1";
			this.btPen1.Size = new System.Drawing.Size(80, 80);
			this.btPen1.TabIndex = 1;
			this.btPen1.UseVisualStyleBackColor = false;
			this.btPen1.Click += new System.EventHandler(this.btColor_Click);
			// 
			// gpButtons
			// 
			this.gpButtons.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gpButtons.Controls.Add(this.btEraser);
			this.gpButtons.Controls.Add(this.btStop);
			this.gpButtons.Controls.Add(this.btPen1);
			this.gpButtons.Controls.Add(this.btPen3);
			this.gpButtons.Controls.Add(this.btClear);
			this.gpButtons.Controls.Add(this.btPen2);
			this.gpButtons.Location = new System.Drawing.Point(42, 84);
			this.gpButtons.Name = "gpButtons";
			this.gpButtons.Size = new System.Drawing.Size(659, 92);
			this.gpButtons.TabIndex = 3;
			// 
			// btEraser
			// 
			this.btEraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btEraser.FlatAppearance.BorderSize = 0;
			this.btEraser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btEraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btEraser.Location = new System.Drawing.Point(300, 5);
			this.btEraser.Name = "btEraser";
			this.btEraser.Size = new System.Drawing.Size(80, 80);
			this.btEraser.TabIndex = 0;
			this.btEraser.UseVisualStyleBackColor = true;
			this.btEraser.Click += new System.EventHandler(this.btEraser_Click);
			// 
			// btStop
			// 
			this.btStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btStop.FlatAppearance.BorderSize = 0;
			this.btStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btStop.Location = new System.Drawing.Point(500, 5);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(80, 80);
			this.btStop.TabIndex = 0;
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
			this.btClear.Location = new System.Drawing.Point(400, 5);
			this.btClear.Name = "btClear";
			this.btClear.Size = new System.Drawing.Size(80, 80);
			this.btClear.TabIndex = 1;
			this.btClear.UseVisualStyleBackColor = true;
			this.btClear.Click += new System.EventHandler(this.btClear_Click);
			// 
			// tiSlide
			// 
			this.tiSlide.Interval = 15;
			this.tiSlide.Tick += new System.EventHandler(this.tiSlide_Tick);
			// 
			// FormCollection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1410, 921);
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
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button btStop;
		public System.Windows.Forms.Button btClear;
		public System.Windows.Forms.Button btPen3;
		public System.Windows.Forms.Button btPen2;
		public System.Windows.Forms.Button btPen1;
		public System.Windows.Forms.Panel gpButtons;
		public System.Windows.Forms.Button btEraser;
		private System.Windows.Forms.Timer tiSlide;
	}
}

