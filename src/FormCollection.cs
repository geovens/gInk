using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using Microsoft.Ink;

namespace gInk
{
	public partial class FormCollection : Form
	{
		public Root Root;
		public InkOverlay IC;
		Image exitimage, clearimage, eraseractimage, eraserinactimage;
		Image checkimage;
		System.Windows.Forms.Cursor cursorred, cursorblue, cursoryellow;
		int gpButtonsLeft, gpButtonsTop;
		public int ButtonsEntering = 1;

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
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern short GetKeyState(int keyCode);

		public FormCollection(Root root)
		{
			Root = root;
			InitializeComponent();

			this.Left = SystemInformation.VirtualScreen.Left;
			this.Top = SystemInformation.VirtualScreen.Top;
			int targetbottom = 0;
			foreach (Screen screen in Screen.AllScreens)
			{
				if (screen.WorkingArea.Bottom > targetbottom)
					targetbottom = screen.WorkingArea.Bottom;
			}
			int virwidth = SystemInformation.VirtualScreen.Width;
			this.Width = virwidth;
			this.Height = targetbottom - this.Top;
			Console.WriteLine(this.Left.ToString() + " " + this.Top.ToString());
			Console.WriteLine(this.Width.ToString() + " " + this.Height.ToString());
			this.DoubleBuffered = true;
			gpButtonsLeft = Screen.PrimaryScreen.WorkingArea.Right - gpButtons.Width + (Screen.PrimaryScreen.WorkingArea.Left - SystemInformation.VirtualScreen.Left);
			gpButtonsTop = Screen.PrimaryScreen.WorkingArea.Bottom - gpButtons.Height - 10 + (Screen.PrimaryScreen.WorkingArea.Top - SystemInformation.VirtualScreen.Top);
			gpButtons.Left = gpButtonsLeft + gpButtons.Width;
			gpButtons.Top = gpButtonsTop;

			IC = new InkOverlay(this.Handle);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.EraserMode = InkOverlayEraserMode.StrokeErase;
			IC.CursorInRange += IC_CursorInRange;
			IC.MouseUp += IC_MouseUp;
			IC.Ink = Root.FormDisplay.IC.Ink;
			IC.DefaultDrawingAttributes.Width = 80;
			//IC.DefaultDrawingAttributes.RasterOperation = RasterOperation.Black;
			IC.DefaultDrawingAttributes.Transparency = 30;
			IC.DefaultDrawingAttributes.AntiAliased = true;

			cursorred = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorred.Handle);
			cursoryellow = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursoryellow.Handle);
			cursorblue = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorblue.Handle);
			IC.Cursor = cursorred;
			
			IC.Enabled = true;

			exitimage = new Bitmap(btStop.Width, btStop.Height);
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

			//checkimage = new Bitmap(btColorRed.Width, btColorRed.Height);
			//g = Graphics.FromImage(checkimage);
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			//g.DrawImage(global::gInk.Properties.Resources.check, 0, 0, btColorRed.Width, btColorRed.Height);
			//btColorRed.Image = checkimage;
			Bitmap pencilimage = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(pencilimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil1, 0, 0, btColorRed.Width, btColorRed.Height);
			Bitmap highlighterimage = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(highlighterimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter1, 0, 0, btColorRed.Width, btColorRed.Height);
			btColorRed.Image = pencilimage;
			btColorBlue.Image = pencilimage;
			btColorYellow.Image = highlighterimage;

			LastTick = DateTime.Now;
			tiSlide.Enabled = true;

			ToTopMost();
		}

		private void IC_MouseUp(object sender, CancelMouseEventArgs e)
		{
			//if (Root.EraserMode)
			{
				//Root.FormDisplay.DrawButtons(true);
			}
		}

		private void IC_CursorInRange(object sender, InkCollectorCursorInRangeEventArgs e)
		{
			if (e.Cursor.Inverted && Root.EraserMode == false)
			{
				Root.EraserMode = true;
				EnterEraserMode(Root.EraserMode);
				Root.FormDisplay.DrawButtons(true);
				Root.FormDisplay.UpdateFormDisplay();
			}
			else if (!e.Cursor.Inverted && Root.EraserMode == true && !Root.EraserLock)
			{
				Root.EraserMode = false;
				EnterEraserMode(Root.EraserMode);
				Root.FormDisplay.DrawButtons(true);
				Root.FormDisplay.UpdateFormDisplay();
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
				Root.FormDisplay.DrawButtons(true);
				Root.FormDisplay.timer1.Interval = 300;
			}
			else
			{
				IC.EditingMode = InkOverlayEditingMode.Ink;
				btEraser.Image = eraserinactimage;
				Root.FormDisplay.DrawButtons(true);
				Root.FormDisplay.timer1.Interval = 30;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			LastTick = DateTime.Now;
			ButtonsEntering = -1;
			//tiSlide.Enabled = true;
		}

		DateTime LastTick;
		private void tiSlide_Tick(object sender, EventArgs e)
		{
			int primwidth = Screen.PrimaryScreen.WorkingArea.Width;
			int primheight = Screen.PrimaryScreen.WorkingArea.Height;
			int primright = Screen.PrimaryScreen.WorkingArea.Right;
			int primbottom = Screen.PrimaryScreen.WorkingArea.Bottom;
			if (ButtonsEntering == 1)
			{
				gpButtons.Left -= (int)(DateTime.Now - LastTick).TotalMilliseconds * 2;
				LastTick = DateTime.Now;
				if (gpButtons.Left <= gpButtonsLeft)
				{
					gpButtons.Left = gpButtonsLeft;
					ButtonsEntering = 0;
				}
				Root.FormDisplay.DrawButtons(false);
				Root.FormDisplay.UpdateFormDisplay();
			}
			else if (ButtonsEntering == -1)
			{
				gpButtons.Left += (int)(DateTime.Now - LastTick).TotalMilliseconds * 2;
				LastTick = DateTime.Now;
				Root.FormDisplay.DrawButtons(false);
				Root.FormDisplay.UpdateFormDisplay();
				if (gpButtons.Left >= gpButtonsLeft + gpButtons.Width)
				{
					tiSlide.Enabled = false;
					Root.StopInk();
				}
			}
			else
			{
				short retVal = GetKeyState(27);
				if ((retVal & 0x8000) == 0x8000)
				{
					LastTick = DateTime.Now;
					ButtonsEntering = -1;
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
				Root.SetInkColor(Color.FromArgb(50, 50, 250));
				//btColorBlue.Image = checkimage;
				//btColorYellow.Image = null;
				//btColorRed.Image = null;
				IC.DefaultDrawingAttributes.Width = 80;
				IC.DefaultDrawingAttributes.Transparency = 30;
				//IC.Cursor = cursorblue;  causing error
			}
			else if ((Button)sender == btColorYellow)
			{
				Root.SetInkColor(Color.FromArgb(240, 240, 0));
				//btColorBlue.Image = null;
				//btColorYellow.Image = checkimage;
				//btColorRed.Image = null;
				IC.DefaultDrawingAttributes.Width = 800;
				IC.DefaultDrawingAttributes.Transparency = 160;
				//IC.Cursor = cursoryellow;  causing error
			}
			else if ((Button)sender == btColorRed)
			{
				Root.SetInkColor(Color.FromArgb(250, 50, 50));
				//btColorBlue.Image = null;
				//btColorYellow.Image = null;
				//btColorRed.Image = checkimage;
				IC.DefaultDrawingAttributes.Width = 80;
				IC.DefaultDrawingAttributes.Transparency = 30;
				//IC.Cursor = cursorred;  causing error
			}
			Root.FormDisplay.DrawButtons(true);
			Root.FormDisplay.UpdateFormDisplay();
		}

		private void btEraser_Click(object sender, EventArgs e)
		{
			Root.EraserLock = !Root.EraserLock;
			Root.EraserMode = !Root.EraserMode;
			EnterEraserMode(Root.EraserMode);
			Root.FormDisplay.DrawButtons(true);
			Root.FormDisplay.UpdateFormDisplay();
		}
	}
}
