using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Ink;

namespace gInk
{
	public partial class FormCollection : Form
	{
		public Root Root;
		public InkOverlay IC;
		Image exitimage, clearimage, eraseractimage, eraserinactimage;
		public int Entering = 1;

		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		[DllImport("user32.dll", SetLastError = true)]
		static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
		[DllImport("user32.dll")]
		public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();

		public FormCollection(Root root)
		{
			Root = root;
			InitializeComponent();

			this.Left = 0;
			this.Top = 0;
			this.MinimumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;
			gpButtons.Left = this.Width;
			gpButtons.Top = this.Height - gpButtons.Height - 60;

			IC = new InkOverlay(this.Handle);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.EraserMode = InkOverlayEraserMode.StrokeErase;
			IC.CursorInRange += IC_CursorInRange;
			IC.Ink = Root.FormDisplay.IC.Ink;
			//IC.DefaultDrawingAttributes.PenTip = PenTip.Rectangle;
			IC.DefaultDrawingAttributes.AntiAliased = false;
			IC.Enabled = true;

			exitimage = new Bitmap(btStop.Width, btStop.Height);
			Console.WriteLine(btStop.Width);
			Graphics g = Graphics.FromImage(exitimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.exit, 0, 0, btStop.Width, btStop.Height);
			btStop.Image = exitimage;
			clearimage = new Bitmap(btClear.Width, btClear.Height);
			g = Graphics.FromImage(clearimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.garbage, 0, 0, btClear.Width, btClear.Height);
			btClear.Image = clearimage;
			eraseractimage = new Bitmap(btEraser.Width, btEraser.Height);
			g = Graphics.FromImage(eraseractimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.eraseract, 0, 0, btEraser.Width, btEraser.Height);
			eraserinactimage = new Bitmap(btEraser.Width, btEraser.Height);
			g = Graphics.FromImage(eraserinactimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.eraserinact, 0, 0, btEraser.Width, btEraser.Height);
			btEraser.Image = eraserinactimage;

			ToTopMost();
		}

		private void IC_CursorInRange(object sender, InkCollectorCursorInRangeEventArgs e)
		{
			if (e.Cursor.Inverted && Root.EraserMode == false)
			{
				Root.EraserMode = true;
				EnterEraserMode(Root.EraserMode);
			}
			else if (!e.Cursor.Inverted && Root.EraserMode == true && !Root.EraserLock)
			{
				Root.EraserMode = false;
				EnterEraserMode(Root.EraserMode);
				//Root.FormDisplay.Refresh();
			}
		}

		public void ToTopMost()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0020);
		}

		public void EnterEraserMode(bool enter)
		{
			if (enter)
			{
				IC.EditingMode = InkOverlayEditingMode.Delete;
				btEraser.Image = eraseractimage;
				Root.FormDisplay.DrawButtons();
				Root.FormDisplay.timer1.Interval = 300;
			}
			else
			{
				IC.EditingMode = InkOverlayEditingMode.Ink;
				btEraser.Image = eraserinactimage;
				Root.FormDisplay.DrawButtons();
				Root.FormDisplay.timer1.Interval = 30;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
			{
				Entering = -1;
				tiSlide.Enabled = true;
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			Entering = -1;
			tiSlide.Enabled = true;
		}

		private void tiSlide_Tick(object sender, EventArgs e)
		{
			if (Entering == 1)
			{
				gpButtons.Left -= 15;
				Root.FormDisplay.DrawButtons();
				if (gpButtons.Left <= this.Width - gpButtons.Width)
					tiSlide.Enabled = false;
			}
			else if (Entering == -1)
			{
				gpButtons.Left += 15;
				Root.FormDisplay.DrawButtons();
				if (gpButtons.Left >= this.Width)
				{
					tiSlide.Enabled = false;
					Root.StopInk();
				}
			}
		}

		private void btClear_Click(object sender, EventArgs e)
		{
			Root.ClearInk();
		}

		private void btColor_Click(object sender, EventArgs e)
		{
			if ((Button)sender == btColorBlue)
			{
				Root.SetInkColor(Color.FromArgb(0, 0, 220));
			}
			else if ((Button)sender == btColorYellow)
			{
				Root.SetInkColor(Color.FromArgb(220, 220, 0));
			}
			else if ((Button)sender == btColorRed)
			{
				Root.SetInkColor(Color.FromArgb(220, 0, 0));
			}
		}

		private void btEraser_Click(object sender, EventArgs e)
		{
			Root.EraserLock = !Root.EraserLock;
			Root.EraserMode = !Root.EraserMode;
			EnterEraserMode(Root.EraserMode);
		}
	}
}
