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
	public partial class FormDisplay : Form
	{
		public Root Root;
		IntPtr Canvus;
		IntPtr canvusDc;
		IntPtr BlankCanvus;
		IntPtr blankcanvusDc;
		Graphics gCanvus;
		//Bitmap ScreenBitmap;
		IntPtr hScreenBitmap;
		IntPtr memscreenDc;

		Bitmap gpButtonsImage;
		SolidBrush TransparentBrush;
		SolidBrush SemiTransparentBrush;

		byte[] screenbits;
		byte[] lastscreenbits;

		public FormDisplay(Root root)
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

			Bitmap InitCanvus = new Bitmap(this.Width, this.Height);
			Canvus = InitCanvus.GetHbitmap(Color.FromArgb(0));
			//BlankCanvus = InitCanvus.GetHbitmap(Color.FromArgb(0));
			InitCanvus.Dispose();

			IntPtr screenDc = GetDC(IntPtr.Zero);
			canvusDc = CreateCompatibleDC(screenDc);
			SelectObject(canvusDc, Canvus);
			//blankcanvusDc = CreateCompatibleDC(screenDc);
			//SelectObject(blankcanvusDc, BlankCanvus);
			gCanvus = Graphics.FromHdc(canvusDc);
			gCanvus.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
			if (Root.AutoScroll)
			{
				hScreenBitmap = InitCanvus.GetHbitmap(Color.FromArgb(0));
				memscreenDc = CreateCompatibleDC(screenDc);
				SelectObject(memscreenDc, hScreenBitmap);
				screenbits = new byte[50000000];
				lastscreenbits = new byte[50000000];
			}
			ReleaseDC(IntPtr.Zero, screenDc);

			//this.DoubleBuffered = true;

			gpButtonsImage = new Bitmap(1000, 100);
			TransparentBrush = new SolidBrush(Color.Transparent);
			SemiTransparentBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 255));


			ToTopMost();
		}

		public void ToTopMost()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			//SetWindowPos(this.Handle, (IntPtr)0, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0004 | 0x0010 | 0x0020);
			//SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000 | 0x00000020);
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010 | 0x0020);
		}

		public void ClearCanvus()
		{
			gCanvus.Clear(Color.Transparent);
		}

		public void DrawSnapping(Rectangle rect)
		{
			gCanvus.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			if (rect.Width > 0 && rect.Height > 0)
			{
				gCanvus.FillRectangle(SemiTransparentBrush, new Rectangle(0, 0, rect.Left, this.Height));
				gCanvus.FillRectangle(SemiTransparentBrush, new Rectangle(rect.Right, 0, this.Width - rect.Right, this.Height));
				gCanvus.FillRectangle(SemiTransparentBrush, new Rectangle(rect.Left, 0, rect.Width, rect.Top));
				gCanvus.FillRectangle(SemiTransparentBrush, new Rectangle(rect.Left, rect.Bottom, rect.Width, this.Height - rect.Bottom));
				Pen pen = new Pen(Color.FromArgb(200, 80, 80, 80));
				pen.Width = 3;
				gCanvus.DrawRectangle(pen, rect);
			}
			else
			{
				gCanvus.FillRectangle(SemiTransparentBrush, new Rectangle(0, 0, this.Width, this.Height));
			}
			gCanvus.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
		}

		public void DrawButtons(bool redrawbuttons, bool exiting = false)
		{
			int top = Root.FormCollection.gpButtons.Top;
			int height = Root.FormCollection.gpButtons.Height;
			int left = Root.FormCollection.gpButtons.Left;
			int width = Root.FormCollection.gpButtons.Width;
			if (redrawbuttons)
				Root.FormCollection.gpButtons.DrawToBitmap(gpButtonsImage, new Rectangle(0, 0, width, height));
			
			if (exiting)
				gCanvus.FillRectangle(TransparentBrush, left - 120, top, width + 80, height);
			gCanvus.DrawImage(gpButtonsImage, left, top);
		}

		public void DrawStrokes()
		{
			Root.FormCollection.IC.Renderer.Draw(gCanvus, Root.FormCollection.IC.Ink.Strokes);
		}

		// drawing attributes not working
		public void DrawLastStroke()
		{
			Stroke stroke = Root.FormCollection.IC.Ink.Strokes[Root.FormCollection.IC.Ink.Strokes.Count - 1];
			if (!stroke.Deleted)
				Root.FormCollection.IC.Renderer.Draw(gCanvus, stroke, Root.FormCollection.IC.DefaultDrawingAttributes);
		}

		public void MoveStrokes(int dy)
		{
			Point pt1 = new Point(0, 0);
			Point pt2 = new Point(0, 100);
			Root.FormCollection.IC.Renderer.PixelToInkSpace(gCanvus, ref pt1);
			Root.FormCollection.IC.Renderer.PixelToInkSpace(gCanvus, ref pt2);
			float unitperpixel = (pt2.Y - pt1.Y) / 100.0f;
			float shouldmove = dy * unitperpixel;
			foreach (Stroke stroke in Root.FormCollection.IC.Ink.Strokes)
				if (!stroke.Deleted)
					stroke.Move(0, shouldmove);
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateFormDisplay(true);
		}


		public uint N1(int i, int j)
		{
			//return BitConverter.ToUInt32(screenbits, (this.Width * j + i) * 4);
			Nlastp1 = (this.Width * j + i) * 4 + 1;
			return screenbits[Nlastp1];
		}
		public uint N2(int i, int j)
		{
			//return BitConverter.ToUInt32(screenbits, (this.Width * j + i) * 4);
			Nlastp2 = (this.Width * j + i) * 4 + 1;
			return screenbits[Nlastp2];
		}
		public uint L(int i, int j)
		{
			//return BitConverter.ToUInt32(lastscreenbits, (this.Width * j + i) * 4);
			Llastp = (this.Width * j + i) * 4 + 1;
			return lastscreenbits[Llastp];
		}
		int Nlastp1, Nlastp2, Llastp;
		public uint Nnext1()
		{
			Nlastp1 += 40;
			return screenbits[Nlastp1];
		}
		public uint Nnext2()
		{
			Nlastp2 += 40;
			return screenbits[Nlastp2];
		}
		public uint Lnext()
		{
			Llastp += 40;
			return lastscreenbits[Llastp];
		}
		public int Test()
		{		
			IntPtr screenDc = GetDC(IntPtr.Zero);

			// big time consuming, but not CPU consuming
			BitBlt(memscreenDc, Width / 4, 0, Width / 2, this.Height, screenDc, Width / 4, 0, 0x00CC0020);
			// <1% CPU
			GetBitmapBits(hScreenBitmap, this.Width * this.Height * 4, screenbits);
			
			int dj;
			int maxidpixels = 0;
			float maxidchdrio = 0;
			int maxdj = 0;
			
			
			// 25% CPU with 1x10x10 sample rate?
			int istart = Width / 2 - Width / 4;
			int iend = Width / 2 + Width / 4;
			for (dj = -Height * 3 / 8 + 1; dj < Height * 3 / 8 - 1; dj++)
			{
				int chdpixels = 0, idpixels = 0;
				for (int j = Height / 2 - Height / 8; j < Height / 2 + Height / 8; j += 10)
				{
					L(istart - 10, j);
					N1(istart - 10, j);
					N2(istart - 10, j + dj);
					for (int i = istart; i < iend; i += 10)
					{
						//uint l = Lnext();
						//uint n1 = Nnext1();
						//uint n2 = Nnext2();
						//if (l != n1)
						//{
						//	chdpixels++;
						//	if (l == n2)
						//		idpixels++;
						//}
						

						if (Lnext() == Nnext2())
							idpixels++;
					}
				}

				//float idchdrio = (float)idpixels / chdpixels;
				if (idpixels > maxidpixels)
				//if (idchdrio > maxidchdrio)
				{
					//maxidchdrio = idchdrio;
					maxidpixels = idpixels;
					maxdj = dj;
				}
			}

			//if (maxidchdrio < 0.1 || maxidpixels < 30)
			if (maxidpixels < 100)
				maxdj = 0;
			

			// 2% CPU
			IntPtr pscreenbits = Marshal.UnsafeAddrOfPinnedArrayElement(screenbits, (int)(this.Width * this.Height * 4 * 0.375));
			IntPtr plastscreenbits = Marshal.UnsafeAddrOfPinnedArrayElement(lastscreenbits, (int)(this.Width * this.Height * 4 * 0.375));
			memcpy(plastscreenbits, pscreenbits, this.Width * this.Height * 4 / 4);

			ReleaseDC(IntPtr.Zero, screenDc);
			return maxdj;
		}

		public void UpdateFormDisplay(bool draw)
		{
			IntPtr screenDc = GetDC(IntPtr.Zero);

			//Display-rectangle
			Size size = new Size(this.Width, this.Height);
			Point pointSource = new Point(0, 0);
			Point topPos = new Point(this.Left, this.Top);

			//Set up blending options
			BLENDFUNCTION blend = new BLENDFUNCTION();
			blend.BlendOp = AC_SRC_OVER;
			blend.BlendFlags = 0;
			blend.SourceConstantAlpha = 255;  // additional alpha multiplier to the whole image. value 255 means multiply with 1.
			blend.AlphaFormat = AC_SRC_ALPHA;

			if (draw)
				UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, canvusDc, ref pointSource, 0, ref blend, ULW_ALPHA);
			else
				UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, blankcanvusDc, ref pointSource, 0, ref blend, ULW_ALPHA);

			//Clean-up
			ReleaseDC(IntPtr.Zero, screenDc);	
		}

		int stackmove = 0;
		int Tick = 0;
		DateTime TickStartTime;
		private void timer1_Tick(object sender, EventArgs e)
		{
			Tick++;
			/*
			if (Tick == 1)
				TickStartTime = DateTime.Now;
			else if (Tick % 60 == 0)
			{
				Console.WriteLine(60 / (DateTime.Now - TickStartTime).TotalMilliseconds * 1000);
				TickStartTime = DateTime.Now;
			}
			*/

			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == false)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				UpdateFormDisplay(true);
				
				//DrawLastStroke();
				//UpdateFormDisplay();
			}

			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == true)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				UpdateFormDisplay(true);
			}

			if (Root.Snapping > 0)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				DrawSnapping(new Rectangle(200, 200, 200, 200));
				UpdateFormDisplay(true);

			}

			if (Root.AutoScroll)
			{
				int moved = Test();
				stackmove += moved;

				if (stackmove != 0 && Tick % 10 == 1)
				{
					MoveStrokes(stackmove);
					ClearCanvus();
					DrawStrokes();
					DrawButtons(false);
					UpdateFormDisplay(true);
					stackmove = 0;
				}
			}
		}

		private void FormDisplay_FormClosed(object sender, FormClosedEventArgs e)
		{
			DeleteObject(Canvus);
			//DeleteObject(BlankCanvus);
			DeleteDC(canvusDc);
			if (Root.AutoScroll)
			{
				DeleteObject(hScreenBitmap);
				DeleteDC(memscreenDc);
			}
		}

		[DllImport("user32.dll")]
		static extern IntPtr GetDC(IntPtr hWnd);
		[DllImport("user32.dll")]
		static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);
		[DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
		public static extern bool DeleteDC([In] IntPtr hdc);
		[DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
		static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);
		[DllImport("gdi32.dll", EntryPoint = "SelectObject")]
		public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);
		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
		[DllImport("gdi32.dll")]
		public static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidthSrc, int nHeightSrc, long dwRop);


		[StructLayout(LayoutKind.Sequential)]
		public struct BLENDFUNCTION
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;

			public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
			{
				BlendOp = op;
				BlendFlags = flags;
				SourceConstantAlpha = alpha;
				AlphaFormat = format;
			}
		}

		const int ULW_ALPHA = 2;
		const int AC_SRC_OVER = 0x00;
		const int AC_SRC_ALPHA = 0x01;


		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		[DllImport("user32.dll", SetLastError = true)]
		static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
		[DllImport("user32.dll")]
		public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

		[DllImport("gdi32.dll")]
		static extern int GetBitmapBits(IntPtr hbmp, int cbBuffer, [Out] byte[] lpvBits);
		[DllImport("gdi32.dll")]
		static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

		[DllImport("gdi32.dll")]
		static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);

	}
}
