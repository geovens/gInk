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
		Bitmap Canvus;
		Bitmap ScreenBitmap;
		Graphics g;

		Bitmap gpButtonsImage;
		SolidBrush TransparentBrush;

		
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
			Canvus = new Bitmap(this.Width, this.Height);
			ScreenBitmap = new Bitmap(this.Width, this.Height);
			this.BackgroundImage = new Bitmap(this.Width, this.Height);
			this.DoubleBuffered = true;

			gpButtonsImage = new Bitmap(1000, 100);
			TransparentBrush = new SolidBrush(Color.Transparent);

			//IC = new InkOverlay(this);
			//IC.CollectionMode = CollectionMode.InkOnly;
			//IC.DefaultDrawingAttributes.Width = 60;
			//IC.DefaultDrawingAttributes.RasterOperation = RasterOperation.Black;
			//IC.DefaultDrawingAttributes.Transparency = 60;
			//IC.DefaultDrawingAttributes.AntiAliased = true;
			//IC.Enabled = true;

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
			g = Graphics.FromImage(Canvus);
			g.Clear(Color.Transparent);
		}

		public void DrawButtons(bool redrawbuttons, bool exiting = false)
		{
			int top = Root.FormCollection.gpButtons.Top;
			int height = Root.FormCollection.gpButtons.Height;
			int left = Root.FormCollection.gpButtons.Left;
			int width = Root.FormCollection.gpButtons.Width;
			if (redrawbuttons)
				Root.FormCollection.gpButtons.DrawToBitmap(gpButtonsImage, new Rectangle(0, 0, width, height));
			g = Graphics.FromImage(Canvus);
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
			if (exiting)
				g.FillRectangle(TransparentBrush, left - 120, top, width + 80, height);
			g.DrawImage(gpButtonsImage, left, top);
		}

		public void DrawStrokes()
		{
			//foreach (Stroke stroke in Root.FormCollection.IC.Ink.Strokes)
			//	if (!stroke.Deleted && stroke.DrawingAttributes.AntiAliased == false)
			//		Console.WriteLine("not anti-aliased?");
			Root.FormCollection.IC.Renderer.Draw(Canvus, Root.FormCollection.IC.Ink.Strokes);
		}

		
		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateFormDisplay();
		}

		
		Random random = new Random(DateTime.Now.Millisecond);
		int j0 = 9000;
		uint[] lastc = new uint[801];
		byte[] canvusbits = new byte[50000000];
		byte[] screenbits = new byte[50000000];
		public uint C(int i, int j)
		{
			return BitConverter.ToUInt32(canvusbits, (this.Width * j + i) * 4);
		}
		public uint S(int i, int j)
		{
			return BitConverter.ToUInt32(screenbits, (this.Width * j + i) * 4);
		}
		public bool Test()
		{
			bool moved = false;

			IntPtr screenDc = GetDC(IntPtr.Zero);
			IntPtr canvusDc = CreateCompatibleDC(screenDc);
			IntPtr hBitmap = Canvus.GetHbitmap(Color.FromArgb(0));
			IntPtr oldBitmap = SelectObject(canvusDc, hBitmap);
			IntPtr screenbitmapDc = CreateCompatibleDC(screenDc);
			IntPtr hscreenBitmap = ScreenBitmap.GetHbitmap(Color.FromArgb(0));
			IntPtr oldscreenBitmap = SelectObject(screenbitmapDc, hscreenBitmap);

			GetBitmapBits(hBitmap, this.Width * this.Height * 4, canvusbits);
			BitBlt(screenbitmapDc, 0, 0, this.Width, this.Height, screenDc, 0, 0, 0x00CC0020);
			GetBitmapBits(hscreenBitmap, this.Width * this.Height * 4, screenbits);
			
		 //Console.WriteLine(C(this.Width / 2, this.Height / 2));

			
			int iindex;
			int matchn = 0;
			int matchj = 9000;
			for (int j = 5; j < this.Height - 5; j++)
			{
				iindex = -1;
				bool linematch = true;
				for (int i = Canvus.Width / 2 - Canvus.Width / 4; linematch && i < Canvus.Width / 2 + Canvus.Width / 4; i += 2)
				{
					iindex++;
					if (lastc[iindex] == 0x7E0649B8)
						continue;
					uint c = C(i, j);
					if (c != 0x00000000)
						continue;

					uint minb = 0xFF, ming = 0xFF, minr = 0xFF;
					uint lastcb = (lastc[iindex] >> 4) & 0xFF;
					uint lastcg = (lastc[iindex] >> 2) & 0xFF;
					uint lastcr = lastc[iindex] & 0xFF;
					for (int scan = 0; scan <= 0; scan++)
					{
						c = S(i, j + scan);
						uint cb = (c >> 4) & 0xFF;
						uint cg = (c >> 2) & 0xFF;
						uint cr = c & 0xFF;
						if (Math.Abs(cb - lastcb) < minb) minb = (uint)Math.Abs(cb - lastcb);
						if (Math.Abs(cg - lastcg) < ming) ming = (uint)Math.Abs(cg - lastcg);
						if (Math.Abs(cr - lastcr) < minr) minr = (uint)Math.Abs(cr - lastcr);
					}
					
					if (minb > 10 || ming > 10 || minr > 10)
						linematch = false;
				}
				if (linematch)
				{
					matchn++;
					if (Math.Abs(j - j0) < Math.Abs(matchj - j0))
						matchj = j;
				}

			}

			if (matchn > 0 && matchn <= 5)
			{
				if (matchj != j0)
				{
					g = Graphics.FromImage(Canvus);
					Point po1 = new Point(1, 100);
					Point po2 = new Point(1, 200);
					Root.FormCollection.IC.Renderer.PixelToInkSpace(g, ref po1);
					Root.FormCollection.IC.Renderer.PixelToInkSpace(g, ref po2);
					foreach (Stroke stroke in Root.FormCollection.IC.Ink.Strokes)
					{
						stroke.Move(0, (matchj - j0) * (po2.Y - po1.Y) / 100.0f);
					}
					moved = true;
				}
			}

			if (moved || j0 == 9000)
			{
				float maxdr = 0;
				int maxdrj0 = 300;
				uint[] crs = new uint[801];
				for (int j0 = Canvus.Height / 2 - 100; j0 < Canvus.Height / 2 + 100; j0 += 2)
				{
					uint sumr = 0;
					iindex = -1;
					for (int i = Canvus.Width / 2 - Canvus.Width / 4; i < Canvus.Width / 2 + Canvus.Width / 4; i += 2)
					{
						iindex++;
						uint c = S(i, j0);
						uint cr = c & 0xFF;
						sumr += cr;
						crs[iindex] = cr;
					}
					iindex++;
					float aver = (float)sumr / iindex;

					float sumdr = 0;
					for (int ii = 0; ii < iindex; ii++)
					{
						sumdr += (crs[ii] - aver) * (crs[ii] - aver);
					}
					if (sumdr > maxdr)
					{
						maxdr = sumdr;
						maxdrj0 = j0;
					}
				}
				j0 = maxdrj0;
				Console.WriteLine(j0);
			}

			iindex = -1;
			for (int i = Canvus.Width / 2 - Canvus.Width / 4; i < Canvus.Width / 2 + Canvus.Width / 4; i += 2)
			{
				iindex++;
				uint c = C(i, j0);
				if (c != 0x00000000)
				{
					lastc[iindex] = 0x7E0649B8;
					continue;
				}
				c = S(i, j0);
				lastc[iindex] = c;
			}
			
			ReleaseDC(IntPtr.Zero, screenDc);
			if (hBitmap != IntPtr.Zero)
			{
				SelectObject(canvusDc, oldBitmap);
				DeleteObject(hBitmap);
			}
			DeleteDC(canvusDc);
			if (hscreenBitmap != IntPtr.Zero)
			{
				SelectObject(screenbitmapDc, oldscreenBitmap);
				DeleteObject(hscreenBitmap);
			}
			DeleteDC(screenbitmapDc);
			return moved;
		}

		public void UpdateFormDisplay()
		{
			IntPtr screenDc = GetDC(IntPtr.Zero);
			IntPtr memDc = CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;

			try
			{
				//Display-image
				//Bitmap bmp = new Bitmap(Canvus);
				hBitmap = Canvus.GetHbitmap(Color.FromArgb(0));  //Set the fact that background is transparent
				oldBitmap = SelectObject(memDc, hBitmap);

				//Display-rectangle
				Size size = Canvus.Size;
				Point pointSource = new Point(0, 0);
				Point topPos = new Point(this.Left, this.Top);

				//Set up blending options
				BLENDFUNCTION blend = new BLENDFUNCTION();
				blend.BlendOp = AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = 255;  // additional alpha multiplier to the whole image. value 255 means multiply with 1.
				blend.AlphaFormat = AC_SRC_ALPHA;

				UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW_ALPHA);

				//Clean-up
				//bmp.Dispose();
				ReleaseDC(IntPtr.Zero, screenDc);
				if (hBitmap != IntPtr.Zero)
				{
					SelectObject(memDc, oldBitmap);
					DeleteObject(hBitmap);
				}
				DeleteDC(memDc);
			}
			catch (Exception)
			{
				Console.WriteLine("Catched");
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == false)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				UpdateFormDisplay();
			}

			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == true)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				UpdateFormDisplay();
			}

			bool moved = Test();
			if (moved)
			{
				ClearCanvus();
				DrawStrokes();
				DrawButtons(false);
				UpdateFormDisplay();
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
		public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, long dwRop);
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
	}
}
