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
		Image image_exit, image_clear, image_eraser_act, image_eraser;
		Bitmap image_pencil, image_highlighter, image_pencil_act, image_highlighter_act;
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
			IC.DefaultDrawingAttributes.Transparency = 30;
			IC.DefaultDrawingAttributes.AntiAliased = true;

			cursorred = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorred.Handle);
			cursoryellow = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursoryellow.Handle);
			cursorblue = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorblue.Handle);
			IC.Cursor = cursorred;
			
			IC.Enabled = true;

			image_exit = new Bitmap(btStop.Width, btStop.Height);
			Graphics g = Graphics.FromImage(image_exit);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.exit, 0, 0, btStop.Width, btStop.Height);
			btStop.Image = image_exit;
			image_clear = new Bitmap(btClear.Width, btClear.Height);
			g = Graphics.FromImage(image_clear);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.garbage, 0, 0, btClear.Width, btClear.Height);
			btClear.Image = image_clear;
			image_eraser_act = new Bitmap(btEraser.Width, btEraser.Height);
			g = Graphics.FromImage(image_eraser_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.eraseract, 0, 0, btEraser.Width, btEraser.Height);
			image_eraser = new Bitmap(btEraser.Width, btEraser.Height);
			g = Graphics.FromImage(image_eraser);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.eraser, 0, 0, btEraser.Width, btEraser.Height);
			btEraser.Image = image_eraser;

			//checkimage = new Bitmap(btColorRed.Width, btColorRed.Height);
			//g = Graphics.FromImage(checkimage);
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			//g.DrawImage(global::gInk.Properties.Resources.check, 0, 0, btColorRed.Width, btColorRed.Height);
			//btColorRed.Image = checkimage;
			image_pencil = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(image_pencil);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil1, 0, 0, btColorRed.Width, btColorRed.Height);
			image_highlighter = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(image_highlighter);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter1, 0, 0, btColorRed.Width, btColorRed.Height);
			image_pencil_act = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(image_pencil_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil_act, 0, 0, btColorRed.Width, btColorRed.Height);
			image_highlighter_act = new Bitmap(btColorRed.Width, btColorRed.Height);
			g = Graphics.FromImage(image_highlighter_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter_act, 0, 0, btColorRed.Width, btColorRed.Height);
			btColorRed.Image = image_pencil_act;
			btColorBlue.Image = image_pencil;
			btColorYellow.Image = image_highlighter;

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
				EnterEraserMode(true);
				/*
				// temperary eraser icon light
				if (btEraser.Image == image_eraser)
				{
					btEraser.Image = image_eraser_act;
					Root.FormDisplay.DrawButtons(true);
					Root.FormDisplay.UpdateFormDisplay();
				}
				*/
			}
			else if (!e.Cursor.Inverted && Root.EraserMode == false)
			{
				EnterEraserMode(false);
				/*
				if (btEraser.Image == image_eraser_act)
				{
					btEraser.Image = image_eraser;
					Root.FormDisplay.DrawButtons(true);
					Root.FormDisplay.UpdateFormDisplay();
				}
				*/
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
			}
			else
			{
				IC.EditingMode = InkOverlayEditingMode.Ink;
			}
		}

		public void SelectPen(int pen)
		{
			if (pen == 0)
			{
				btColorBlue.Image = image_pencil;
				btColorYellow.Image = image_highlighter;
				btColorRed.Image = image_pencil;
				btEraser.Image = image_eraser_act;
				Root.EraserMode = true;
				EnterEraserMode(true);
			}
			else if (pen == 1)
			{
				IC.DefaultDrawingAttributes.Color = Color.FromArgb(250, 50, 50);
				IC.DefaultDrawingAttributes.Width = 80;
				IC.DefaultDrawingAttributes.Transparency = 30;

				btColorBlue.Image = image_pencil;
				btColorYellow.Image = image_highlighter;
				btColorRed.Image = image_pencil_act;
				btEraser.Image = image_eraser;
				Root.EraserMode = false;
				EnterEraserMode(false);
			}
			else if (pen == 2)
			{
				IC.DefaultDrawingAttributes.Color = Color.FromArgb(240, 240, 0);
				IC.DefaultDrawingAttributes.Width = 800;
				IC.DefaultDrawingAttributes.Transparency = 160;

				btColorBlue.Image = image_pencil;
				btColorYellow.Image = image_highlighter_act;
				btColorRed.Image = image_pencil;
				btEraser.Image = image_eraser;
				Root.EraserMode = false;
				EnterEraserMode(false);
			}
			else if (pen == 3)
			{
				IC.DefaultDrawingAttributes.Color = Color.FromArgb(50, 50, 250);
				IC.DefaultDrawingAttributes.Width = 80;
				IC.DefaultDrawingAttributes.Transparency = 30;

				btColorBlue.Image = image_pencil_act;
				btColorYellow.Image = image_highlighter;
				btColorRed.Image = image_pencil;
				btEraser.Image = image_eraser;
				Root.EraserMode = false;
				EnterEraserMode(false);
			}
			Root.CurrentPen = pen;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			LastTick = DateTime.Now;
			ButtonsEntering = -1;
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
				SelectPen(3);
				try
				{
					IC.Cursor = cursorblue;  //causing error
				}
				catch { }
			}
			else if ((Button)sender == btColorYellow)
			{
				SelectPen(2);
				try
				{
					IC.Cursor = cursoryellow; //causing error
				}
				catch { }
			}
			else if ((Button)sender == btColorRed)
			{
				SelectPen(1);
				try
				{
					IC.Cursor = cursorred;  //causing error
				}
				catch { }
			}
			Root.FormDisplay.DrawButtons(true);
			Root.FormDisplay.UpdateFormDisplay();
		}

		private void btEraser_Click(object sender, EventArgs e)
		{
			SelectPen(0);
			Root.FormDisplay.DrawButtons(true);
			Root.FormDisplay.UpdateFormDisplay();
		}
	}
}
