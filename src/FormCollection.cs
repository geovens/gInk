using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
//using System.Windows.Input;
using Microsoft.Ink;

namespace gInk
{
	public partial class FormCollection : Form
	{
		public Root Root;
		public InkOverlay IC;

		public Bitmap image_exit, image_clear, image_snap;
		public Bitmap image_dock, image_dockback;
		public Bitmap image_pencil, image_highlighter, image_pencil_act, image_highlighter_act;
		public Bitmap image_pointer, image_pointer_act;
		public Bitmap image_pen1, image_pen2, image_pen3, image_pen4, image_pen5;
		public Bitmap image_pen1_act, image_pen2_act, image_pen3_act, image_pen4_act, image_pen5_act;
		public Bitmap image_eraser_act, image_eraser;
		public System.Windows.Forms.Cursor cursorred, cursorblue, cursoryellow;

		public int ButtonsEntering = 0;  // -1 = exiting
		public int gpButtonsLeft, gpButtonsTop;

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
			this.DoubleBuffered = true;

			gpButtonsLeft = Screen.PrimaryScreen.WorkingArea.Right - gpButtons.Width + (Screen.PrimaryScreen.WorkingArea.Left - SystemInformation.VirtualScreen.Left);
			gpButtonsTop = Screen.PrimaryScreen.WorkingArea.Bottom - gpButtons.Height - 10 + (Screen.PrimaryScreen.WorkingArea.Top - SystemInformation.VirtualScreen.Top);
			gpButtons.Left = gpButtonsLeft + gpButtons.Width;
			gpButtons.Top = gpButtonsTop;

			IC = new InkOverlay(this.Handle);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.AutoRedraw = false;
			IC.DynamicRendering = false;
			IC.EraserMode = InkOverlayEraserMode.StrokeErase;
			IC.CursorInRange += IC_CursorInRange;
			IC.MouseDown += IC_MouseDown;
			IC.MouseMove += IC_MouseMove;
			IC.MouseUp += IC_MouseUp;
			IC.CursorDown += IC_CursorDown;
			IC.DefaultDrawingAttributes.Width = 80;
			IC.DefaultDrawingAttributes.Transparency = 30;
			IC.DefaultDrawingAttributes.AntiAliased = true;

			cursorred = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorred.Handle);
			//cursoryellow = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursoryellow.Handle);
			//cursorblue = new System.Windows.Forms.Cursor(gInk.Properties.Resources.cursorblue.Handle);
			IC.Cursor = cursorred;
			this.Cursor = cursorred;
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
			g.DrawImage(global::gInk.Properties.Resources.eraser_act, 0, 0, btEraser.Width, btEraser.Height);
			image_eraser = new Bitmap(btEraser.Width, btEraser.Height);
			g = Graphics.FromImage(image_eraser);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.eraser, 0, 0, btEraser.Width, btEraser.Height);
			btEraser.Image = image_eraser;
			image_snap = new Bitmap(btSnap.Width, btSnap.Height);
			g = Graphics.FromImage(image_snap);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.snap, 0, 0, btSnap.Width, btSnap.Height);
			btSnap.Image = image_snap;
			image_dock = new Bitmap(btDock.Width, btDock.Height);
			g = Graphics.FromImage(image_dock);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.dock, 0, 0, btDock.Width, btDock.Height);
			image_dockback = new Bitmap(btDock.Width, btDock.Height);
			g = Graphics.FromImage(image_dockback);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.dockback, 0, 0, btDock.Width, btDock.Height);
			if (Root.Docked)
				btDock.Image = image_dockback;
			else
				btDock.Image = image_dock;

			image_pencil = new Bitmap(btPen3.Width, btPen3.Height);
			g = Graphics.FromImage(image_pencil);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil, 0, 0, btPen3.Width, btPen3.Height);
			image_highlighter = new Bitmap(btPen3.Width, btPen3.Height);
			g = Graphics.FromImage(image_highlighter);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter, 0, 0, btPen3.Width, btPen3.Height);
			image_pencil_act = new Bitmap(btPen3.Width, btPen3.Height);
			g = Graphics.FromImage(image_pencil_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil_act, 0, 0, btPen3.Width, btPen3.Height);
			image_highlighter_act = new Bitmap(btPen3.Width, btPen3.Height);
			g = Graphics.FromImage(image_highlighter_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter_act, 0, 0, btPen3.Width, btPen3.Height);

			image_pointer = new Bitmap(btPointer.Width, btPointer.Height);
			g = Graphics.FromImage(image_pointer);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pointer, 0, 0, btPointer.Width, btPointer.Height);
			image_pointer_act = new Bitmap(btPointer.Width, btPointer.Height);
			g = Graphics.FromImage(image_pointer_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pointer_act, 0, 0, btPointer.Width, btPointer.Height);

			btPen1.BackColor = Root.Pen1.Color;
			btPen2.BackColor = Root.Pen2.Color;
			btPen3.BackColor = Root.Pen3.Color;
			btPen4.BackColor = Root.Pen4.Color;
			btPen5.BackColor = Root.Pen5.Color;
			btPen1.FlatAppearance.MouseDownBackColor = Root.Pen1.Color;
			btPen2.FlatAppearance.MouseDownBackColor = Root.Pen2.Color;
			btPen3.FlatAppearance.MouseDownBackColor = Root.Pen3.Color;
			btPen4.FlatAppearance.MouseDownBackColor = Root.Pen4.Color;
			btPen5.FlatAppearance.MouseDownBackColor = Root.Pen5.Color;
			btPen1.FlatAppearance.MouseOverBackColor = Root.Pen1.Color;
			btPen2.FlatAppearance.MouseOverBackColor = Root.Pen2.Color;
			btPen3.FlatAppearance.MouseOverBackColor = Root.Pen3.Color;
			btPen4.FlatAppearance.MouseOverBackColor = Root.Pen4.Color;
			btPen5.FlatAppearance.MouseOverBackColor = Root.Pen5.Color;
			if (Root.Pen1.Transparency >= 100)
			{
				image_pen1 = image_highlighter;
				image_pen1_act = image_highlighter_act;
			}
			else
			{
				image_pen1 = image_pencil;
				image_pen1_act = image_pencil_act;
			}
			if (Root.Pen2.Transparency >= 100)
			{
				image_pen2 = image_highlighter;
				image_pen2_act = image_highlighter_act;
			}
			else
			{
				image_pen2 = image_pencil;
				image_pen2_act = image_pencil_act;
			}
			if (Root.Pen3.Transparency >= 100)
			{
				image_pen3 = image_highlighter;
				image_pen3_act = image_highlighter_act;
			}
			else
			{
				image_pen3 = image_pencil;
				image_pen3_act = image_pencil_act;
			}
			if (Root.Pen4.Transparency >= 100)
			{
				image_pen4 = image_highlighter;
				image_pen4_act = image_highlighter_act;
			}
			else
			{
				image_pen4 = image_pencil;
				image_pen4_act = image_pencil_act;
			}
			if (Root.Pen5.Transparency >= 100)
			{
				image_pen5 = image_highlighter;
				image_pen5_act = image_highlighter_act;
			}
			else
			{
				image_pen5 = image_pencil;
				image_pen5_act = image_pencil_act;
			}

			LastTickTime = DateTime.Parse("1987-01-01");
			tiSlide.Enabled = true;

			ToTransparent();
			ToTopMost();
		}

		private void IC_CursorDown(object sender, InkCollectorCursorDownEventArgs e)
		{
			Root.FormDisplay.ClearCanvus(Root.FormDisplay.gOneStrokeCanvus);
			Root.FormDisplay.DrawStrokes(Root.FormDisplay.gOneStrokeCanvus);
			Root.FormDisplay.DrawButtons(Root.FormDisplay.gOneStrokeCanvus, false);
		}

		private void IC_MouseDown(object sender, CancelMouseEventArgs e)
		{
			if (Root.Snapping == 1)
			{
				Root.SnappingX = e.X;
				Root.SnappingY = e.Y;
				Root.SnappingRect = new Rectangle(e.X, e.Y, 0, 0);
				Root.Snapping = 2;
			}
		}

		private void IC_MouseMove(object sender, CancelMouseEventArgs e)
		{
			if (Root.Snapping == 2)
			{
				int left = Math.Min(Root.SnappingX, e.X);
				int top = Math.Min(Root.SnappingY, e.Y);
				int width = Math.Abs(Root.SnappingX - e.X);
				int height = Math.Abs(Root.SnappingY - e.Y);
				Root.SnappingRect = new Rectangle(left, top, width, height);
			}
		}

		private void IC_MouseUp(object sender, CancelMouseEventArgs e)
		{
			if (Root.Snapping == 2)
			{
				int left = Math.Min(Root.SnappingX, e.X);
				int top = Math.Min(Root.SnappingY, e.Y);
				int width = Math.Abs(Root.SnappingX - e.X);
				int height = Math.Abs(Root.SnappingY - e.Y);
				if (width < 5 || height < 5)
				{
					left = 0;
					top = 0;
					width = this.Width;
					height = this.Height;
				}
				Root.SnappingRect = new Rectangle(left, top, width, height);
				Root.UponTakingSnap = true;
				ExitSnapping();
			}
		}

		private void IC_CursorInRange(object sender, InkCollectorCursorInRangeEventArgs e)
		{
			if (e.Cursor.Inverted && Root.CurrentPen != 0)
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
			else if (!e.Cursor.Inverted && Root.CurrentPen != 0)
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

		public void ToTransparent()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 1, 0x2);	
		}

		public void ToTopMost()
		{
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0020);
		}

		public void ToThrough()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			//SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			//SetWindowPos(this.Handle, (IntPtr)0, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0004 | 0x0010 | 0x0020);
			//SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 1, 0x2);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000 | 0x00000020);
			//SetWindowPos(this.Handle, (IntPtr)(1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010 | 0x0020);
		}

		public void ToUnThrough()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			//SetWindowLong(this.Handle, -20, (uint)(dwExStyle & ~0x00080000 & ~0x0020));
			SetWindowLong(this.Handle, -20, (uint)(dwExStyle & ~0x0020));
			//SetWindowPos(this.Handle, (IntPtr)(-2), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010 | 0x0020);

			//dwExStyle = GetWindowLong(this.Handle, -20);
			//SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			//SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 1, 0x2);
			//SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0020);
		}

		public void EnterEraserMode(bool enter)
		{
			if (enter)
			{
				IC.EditingMode = InkOverlayEditingMode.Delete;
				Root.EraserMode = true;
			}
			else
			{
				IC.EditingMode = InkOverlayEditingMode.Ink;
				Root.EraserMode = false;
			}
		}

		public void ExitSnapping()
		{
			IC.SetWindowInputRectangle(new Rectangle(0, 0, this.Width, this.Height));
			Root.SnappingX = -1;
			Root.SnappingY = -1;
			Root.Snapping = -60;
		}

		public void SelectPen(int pen)
		{
			// -1 = pointer, 0 = erasor, >0 = pens
			if (pen == -1)
			{
				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer_act;
				EnterEraserMode(false);
				Root.Pointer();
			}
			else if (pen == 0)
			{
				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser_act;
				btPointer.Image = image_pointer;
				EnterEraserMode(true);
				Root.UnPointer();
			}
			else if (pen == 1)
			{
				IC.DefaultDrawingAttributes = Root.Pen1;

				btPen1.Image = image_pen1_act;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer;
				EnterEraserMode(false);
				Root.UnPointer();
			}
			else if (pen == 2)
			{
				IC.DefaultDrawingAttributes = Root.Pen2;

				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2_act;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer;
				EnterEraserMode(false);
				Root.UnPointer();
			}
			else if (pen == 3)
			{
				IC.DefaultDrawingAttributes = Root.Pen3;

				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3_act;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer;
				EnterEraserMode(false);
				Root.UnPointer();
			}
			else if (pen == 4)
			{
				IC.DefaultDrawingAttributes = Root.Pen4;

				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4_act;
				btPen5.Image = image_pen5;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer;
				EnterEraserMode(false);
				Root.UnPointer();
			}
			else if (pen == 5)
			{
				IC.DefaultDrawingAttributes = Root.Pen5;

				btPen1.Image = image_pen1;
				btPen2.Image = image_pen2;
				btPen3.Image = image_pen3;
				btPen4.Image = image_pen4;
				btPen5.Image = image_pen5_act;
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer;
				EnterEraserMode(false);
				Root.UnPointer();
			}
			Root.CurrentPen = pen;
			Root.UponButtonsUpdate |= 0x2;
		}

		public void RetreatAndExit()
		{
			ToThrough();
			Root.ClearInk();
			LastTickTime = DateTime.Now;
			ButtonsEntering = -1;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		public void btDock_Click(object sender, EventArgs e)
		{
			LastTickTime = DateTime.Now;
			if (!Root.Docked)
			{
				Root.Dock();
			}
			else
			{
				Root.UnDock();
			}
		}

		public void btPointer_Click(object sender, EventArgs e)
		{
			SelectPen(-1);
		}

		public void btSnap_Click(object sender, EventArgs e)
		{
			if (Root.Snapping > 0)
				return;

			IC.SetWindowInputRectangle(new Rectangle(0, 0, 1, 1));
			Root.SnappingX = -1;
			Root.SnappingY = -1;
			Root.SnappingRect = new Rectangle(0, 0, 0, 0);
			Root.Snapping = 1;
			Root.UnPointer();
		}

		public void btStop_Click(object sender, EventArgs e)
		{
			RetreatAndExit();
		}

		DateTime LastTickTime;
		private void tiSlide_Tick(object sender, EventArgs e)
		{
			// ignore the first tick
			if (LastTickTime.Year == 1987)
			{
				LastTickTime = DateTime.Now;
				return;
			}

			int primwidth = Screen.PrimaryScreen.WorkingArea.Width;
			int primheight = Screen.PrimaryScreen.WorkingArea.Height;
			int primright = Screen.PrimaryScreen.WorkingArea.Right;
			int primbottom = Screen.PrimaryScreen.WorkingArea.Bottom;

			int aimedleft = gpButtonsLeft;
			if (ButtonsEntering == 0)
			{
				if (Root.Snapping > 0)
					aimedleft = gpButtonsLeft + gpButtons.Width + 5;
				else if (Root.Docked)
					aimedleft = gpButtonsLeft + gpButtons.Width - btDock.Right;
				else
					aimedleft = gpButtonsLeft;
			}
			else if (ButtonsEntering == -1)
				aimedleft = gpButtonsLeft + gpButtons.Width;

			if (gpButtons.Left > aimedleft)
			{
				float dleft = gpButtons.Left - aimedleft;
				dleft /= 70;
				if (dleft > 8) dleft = 8;
				dleft *= (float)(DateTime.Now - LastTickTime).TotalMilliseconds;
				if (dleft > 120) dleft = 230;
				if (dleft < 1) dleft = 1;
				gpButtons.Left -= (int)dleft;
				LastTickTime = DateTime.Now;
				if (gpButtons.Left < aimedleft)
				{
					gpButtons.Left = aimedleft;
				}
				Root.UponButtonsUpdate |= 0x1;
			}
			else if (gpButtons.Left < aimedleft)
			{
				float dleft = aimedleft - gpButtons.Left;
				dleft /= 70;
				if (dleft > 8) dleft = 8;
				// fast exiting when not docked
				if (ButtonsEntering == -1 && !Root.Docked)
					dleft = 8;
				dleft *= (float)(DateTime.Now - LastTickTime).TotalMilliseconds;
				if (dleft > 120) dleft = 120;
				if (dleft < 1) dleft = 1;
				// fast exiting when docked
				if (ButtonsEntering == -1 && dleft == 1)
					dleft = 2;
				gpButtons.Left += (int)dleft;
				LastTickTime = DateTime.Now;
				if (gpButtons.Left > aimedleft)
				{
					gpButtons.Left = aimedleft;
				}
				Root.UponButtonsUpdate |= 0x1;
			}

			if (ButtonsEntering == -1 && gpButtons.Left == aimedleft)
			{
				tiSlide.Enabled = false;
				Root.StopInk();
				return;
			}

			if (!Root.PointerMode && !this.TopMost)
				ToTopMost();

			short retVal = GetKeyState(27);
			if ((retVal & 0x8000) == 0x8000)
			{
				if (Root.Snapping > 0)
				{
					ExitSnapping();
				}
				else if (Root.Snapping == 0)
					RetreatAndExit();
			}

			if (Root.Snapping < 0)
				Root.Snapping++;
		}

		public void btClear_Click(object sender, EventArgs e)
		{
			Root.ClearInk();
		}

		public void btColor_Click(object sender, EventArgs e)
		{
			if ((Button)sender == btPen1)
			{
				SelectPen(1);
			}
			else if ((Button)sender == btPen2)
			{
				SelectPen(2);
			}
			else if ((Button)sender == btPen3)
			{
				SelectPen(3);
			}
			else if ((Button)sender == btPen4)
			{
				SelectPen(4);
			}
			else if ((Button)sender == btPen5)
			{
				SelectPen(5);
			}
		}

		public void btEraser_Click(object sender, EventArgs e)
		{
			SelectPen(0);
		}

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
	}
}
