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

		public Button[] btPen = new Button[10];
		public Bitmap image_exit, image_clear, image_undo, image_snap;
		public Bitmap image_dock, image_dockback;
		public Bitmap image_pencil, image_highlighter, image_pencil_act, image_highlighter_act;
		public Bitmap image_pointer, image_pointer_act;
		public Bitmap[] image_pen = new Bitmap[10];
		public Bitmap[] image_pen_act = new Bitmap[10];
		public Bitmap image_eraser_act, image_eraser;
		public System.Windows.Forms.Cursor cursorred, cursorblue, cursoryellow;

		public int ButtonsEntering = 0;  // -1 = exiting
		public int gpButtonsLeft, gpButtonsTop;

		public FormCollection(Root root)
		{
			Root = root;
			InitializeComponent();

			int cumulatedleft = 40;
			for (int b = 0; b < Root.PenCount; b++)
			{
				btPen[b] = new Button();
				btPen[b].Width = 46;
				btPen[b].Height = 46;
				btPen[b].Left = cumulatedleft;
				cumulatedleft += 50;
				btPen[b].Top = 5;
				btPen[b].FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
				btPen[b].FlatAppearance.BorderSize = 3;
				btPen[b].FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(250, 50, 50);
				btPen[b].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
				btPen[b].ForeColor = System.Drawing.Color.Transparent;
				//btPen[b].Name = "btPen" + b.ToString();
				btPen[b].UseVisualStyleBackColor = false;
				btPen[b].Click += new System.EventHandler(this.btColor_Click);
				btPen[b].BackColor = Root.PenAttr[b].Color;
				btPen[b].FlatAppearance.MouseDownBackColor = Root.PenAttr[b].Color;
				btPen[b].FlatAppearance.MouseOverBackColor = Root.PenAttr[b].Color;

				gpButtons.Controls.Add(btPen[b]);
			}
			if (true)
			{
				btEraser.Visible = true;
				btEraser.Left = cumulatedleft + 40;
				cumulatedleft += 50 + 40;
			}
			if (true)
			{
				btPointer.Visible = true;
				btPointer.Left = cumulatedleft;
				cumulatedleft += 50;
			}
			if (true)
			{
				btSnap.Visible = true;
				btSnap.Left = cumulatedleft + 40;
				cumulatedleft += 50 + 40;
			}
			if (true)
			{
				btUndo.Visible = true;
				btUndo.Left = cumulatedleft;
				cumulatedleft += 50;
			}
			if (true)
			{
				btClear.Visible = true;
				btClear.Left = cumulatedleft;
				cumulatedleft += 50;
			}
			btStop.Left = cumulatedleft + 40;
			gpButtons.Width = btStop.Right + 20;

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
			IC.Stroke += IC_Stroke;
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
			image_undo = new Bitmap(btUndo.Width, btUndo.Height);
			g = Graphics.FromImage(image_undo);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.undo, 0, 0, btUndo.Width, btUndo.Height);
			btUndo.Image = image_undo;
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

			image_pencil = new Bitmap(btPen[2].Width, btPen[2].Height);
			g = Graphics.FromImage(image_pencil);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil, 0, 0, btPen[2].Width, btPen[2].Height);
			image_highlighter = new Bitmap(btPen[2].Width, btPen[2].Height);
			g = Graphics.FromImage(image_highlighter);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter, 0, 0, btPen[2].Width, btPen[2].Height);
			image_pencil_act = new Bitmap(btPen[2].Width, btPen[2].Height);
			g = Graphics.FromImage(image_pencil_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pencil_act, 0, 0, btPen[2].Width, btPen[2].Height);
			image_highlighter_act = new Bitmap(btPen[2].Width, btPen[2].Height);
			g = Graphics.FromImage(image_highlighter_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.highlighter_act, 0, 0, btPen[2].Width, btPen[2].Height);

			image_pointer = new Bitmap(btPointer.Width, btPointer.Height);
			g = Graphics.FromImage(image_pointer);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pointer, 0, 0, btPointer.Width, btPointer.Height);
			image_pointer_act = new Bitmap(btPointer.Width, btPointer.Height);
			g = Graphics.FromImage(image_pointer_act);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.pointer_act, 0, 0, btPointer.Width, btPointer.Height);


			for (int b = 0; b < Root.PenCount; b++)
			{
				if (Root.PenAttr[b].Transparency >= 100)
				{
					image_pen[b] = new Bitmap(btPen[2].Width, btPen[2].Height);
					image_pen[b] = image_highlighter;
					image_pen_act[b] = new Bitmap(btPen[2].Width, btPen[2].Height);
					image_pen_act[b] = image_highlighter_act;
				}
				else
				{
					image_pen[b] = new Bitmap(btPen[2].Width, btPen[2].Height);
					image_pen[b] = image_pencil;
					image_pen_act[b] = new Bitmap(btPen[2].Width, btPen[2].Height);
					image_pen_act[b] = image_pencil_act;
				}
			}

			LastTickTime = DateTime.Parse("1987-01-01");
			tiSlide.Enabled = true;

			ToTransparent();
			ToTopMost();
		}

		private void IC_Stroke(object sender, InkCollectorStrokeEventArgs e)
		{
			SaveUndoStrokes();
		}

		private void SaveUndoStrokes()
		{
			Root.RedoDepth = 0;
			if (Root.UndoDepth < Root.UndoStrokes.GetLength(0) - 1)
				Root.UndoDepth++;

			Root.UndoP++;
			if (Root.UndoP >= Root.UndoStrokes.GetLength(0))
				Root.UndoP = 0;

			if (Root.UndoStrokes[Root.UndoP] == null)
				Root.UndoStrokes[Root.UndoP] = new Ink();
			Root.UndoStrokes[Root.UndoP].DeleteStrokes();
			if (IC.Ink.Strokes.Count > 0)
				Root.UndoStrokes[Root.UndoP].AddStrokesAtRectangle(IC.Ink.Strokes, IC.Ink.Strokes.GetBoundingBox());
		}

		private void IC_CursorDown(object sender, InkCollectorCursorDownEventArgs e)
		{
			Root.FormDisplay.ClearCanvus(Root.FormDisplay.gOneStrokeCanvus);
			Root.FormDisplay.DrawStrokes(Root.FormDisplay.gOneStrokeCanvus);
			Root.FormDisplay.DrawButtons(Root.FormDisplay.gOneStrokeCanvus, false);
		}

		private void IC_MouseDown(object sender, CancelMouseEventArgs e)
		{
			Root.FingerInAction = true;
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
			Root.FingerInAction = false;
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
			Root.SelectPen(Root.CurrentPen);
		}

		public void SelectPen(int pen)
		{
			// -2 = pointer, -1 = erasor, 0+ = pens
			if (pen == -2)
			{
				for (int b = 0; b < Root.PenCount; b++)
					btPen[b].Image = image_pen[b];
				btEraser.Image = image_eraser;
				btPointer.Image = image_pointer_act;
				EnterEraserMode(false);
				Root.Pointer();
			}
			else if (pen == -1)
			{
				for (int b = 0; b < Root.PenCount; b++)
					btPen[b].Image = image_pen[b];
				btEraser.Image = image_eraser_act;
				btPointer.Image = image_pointer;
				EnterEraserMode(true);
				Root.UnPointer();
			}
			else if (pen >= 0)
			{
				IC.DefaultDrawingAttributes = Root.PenAttr[pen];
				for (int b = 0; b < Root.PenCount; b++)
					btPen[b].Image = image_pen[b];
				btPen[pen].Image = image_pen_act[pen];
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
			SaveUndoStrokes();
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
			SelectPen(-2);
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
		short LastZStatus = 0;
		short LastYStatus = 0;
		short LastDStatus = 0;
		short LastESCStatus = 0;
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

			// hotkeys

			const int VK_LCONTROL = 0xA2;
			const int VK_RCONTROL = 0xA3;
			short retVal;

			if (!Root.PointerMode)
			{
				// ESC key : Exit
				retVal = GetKeyState(27);
				if ((retVal & 0x8000) == 0x8000)
				{
					if ((LastESCStatus & 0x8000) == 0x0000)
					{
						if (Root.Snapping > 0)
						{
							ExitSnapping();
						}
						else if (Root.Snapping == 0)
							RetreatAndExit();
					}
				}
				LastESCStatus = retVal;
			}

			if (!Root.FingerInAction)
			{
				// Ctrl + 1 : Pen 1
				retVal = GetKeyState('1');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(0);
					}
				}

				// Ctrl + 2 : Pen 2
				retVal = GetKeyState('2');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(1);
					}
				}

				// Ctrl + 3 : Pen 3
				retVal = GetKeyState('3');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(2);
					}
				}

				// Ctrl + 4 : Pen 4
				retVal = GetKeyState('4');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(3);
					}
				}

				// Ctrl + 5 : Pen 5
				retVal = GetKeyState('5');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(4);
					}
				}

				// Ctrl + 0 : Eraser
				retVal = GetKeyState('0');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(-1);
					}
				}
			}

			if (!Root.FingerInAction && !Root.PointerMode && Root.Snapping <= 0)
			{
				// Ctrl + Z : Undo
				retVal = GetKeyState('Z');
				if ((retVal & 0x8000) == 0x8000)
				{
					if ((LastZStatus & 0x8000) == 0x0000)
					{
						short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
						if ((control & 0x8000) == 0x8000)
						{
							Root.UndoInk();
						}
					}
				}
				LastZStatus = retVal;

				// Ctrl + Y : Redo
				retVal = GetKeyState('Y');
				if ((retVal & 0x8000) == 0x8000)
				{
					if ((LastYStatus & 0x8000) == 0x0000)
					{
						short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
						if ((control & 0x8000) == 0x8000)
						{
							Root.RedoInk();
						}
					}
				}
				LastYStatus = retVal;

				// Ctrl + P : Pointer
				retVal = GetKeyState('P');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						SelectPen(-2);
					}
				}

				// Ctrl + S : Snap
				retVal = GetKeyState('S');
				if ((retVal & 0x8000) == 0x8000)
				{
					short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
					if ((control & 0x8000) == 0x8000)
					{
						btSnap_Click(null, null);
					}
				}

				// Ctrl + D : Clear
				retVal = GetKeyState('D');
				if ((retVal & 0x8000) == 0x8000)
				{
					if ((LastDStatus & 0x8000) == 0x0000)
					{
						short control = (short)(GetKeyState(VK_LCONTROL) | GetKeyState(VK_RCONTROL));
						if ((control & 0x8000) == 0x8000)
						{
							btClear_Click(null, null);
						}
					}
				}
				LastDStatus = retVal;
			}

			if (Root.Snapping < 0)
				Root.Snapping++;
		}

		public void btClear_Click(object sender, EventArgs e)
		{
			Root.ClearInk();
			SaveUndoStrokes();
		}

		private void btUndo_Click(object sender, EventArgs e)
		{
			Root.UndoInk();
		}

		public void btColor_Click(object sender, EventArgs e)
		{
			for (int b = 0; b < Root.PenCount; b++)
				if ((Button)sender == btPen[b])
				{
					SelectPen(b);
				}
		}

		public void btEraser_Click(object sender, EventArgs e)
		{
			SelectPen(-1);
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
