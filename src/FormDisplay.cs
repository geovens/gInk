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
		public InkOverlay IC;
		Graphics g;
		Bitmap gpButtonsImage;
		Bitmap Canvus;
		SolidBrush WhiteBrush;

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
			this.BackgroundImage = new Bitmap(this.Width, this.Height);
			this.DoubleBuffered = true;

			gpButtonsImage = new Bitmap(1000, 100);
			WhiteBrush = new SolidBrush(Color.White);

			IC = new InkOverlay(this);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.DefaultDrawingAttributes.Width = 60;
			//IC.DefaultDrawingAttributes.RasterOperation = RasterOperation.Black;
			IC.DefaultDrawingAttributes.Transparency = 60;
			IC.DefaultDrawingAttributes.AntiAliased = true;
			IC.Enabled = true;

			ToTopMost();
		}

		public void ToTopMost()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			//SetWindowPos(this.Handle, (IntPtr)0, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0004 | 0x0010 | 0x0020);
			//SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000 | 0x00000020);
			//SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010 | 0x0020);
		}

		public void DrawButtons()
		{
			int top = Root.FormCollection.gpButtons.Top;
			int height = Root.FormCollection.gpButtons.Height;
			int left = Root.FormCollection.gpButtons.Left;
			int width = Root.FormCollection.gpButtons.Width;
			Root.FormCollection.gpButtons.DrawToBitmap(gpButtonsImage, new Rectangle(0, 0, width, height));
			//g = IC.CreateGraphics();
			g = Graphics.FromImage(Canvus);
			g.FillRectangle(WhiteBrush, left - 20, top, width + 40, height); 
			g.DrawImage(gpButtonsImage, left, top);
			//UpdateFormDisplay(Canvus);
		}

		public void DrawCanvus()
		{
			g = Graphics.FromImage(Canvus);
			g.Clear(Color.Transparent);
			//IC.Renderer.Draw(g, IC.Ink.Strokes);
			//foreach (Stroke stroke in IC.Ink.Strokes)
			//	if (!stroke.Deleted)
			//		IC.Renderer.Draw(Canvus, stroke, stroke.DrawingAttributes);
			IC.Renderer.Draw(Canvus, IC.Ink.Strokes);
		}

		
		protected override void OnPaint(PaintEventArgs e)
		{
			UpdateFormDisplay(Canvus);
		}
		

		public void UpdateFormDisplay(Image image)
		{
			if (image == null)
				Console.WriteLine("NULL");

			IntPtr screenDc = GetDC(IntPtr.Zero);
			IntPtr memDc = CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;

			try
			{
				//Display-image
				Bitmap bmp = new Bitmap(image);
				hBitmap = bmp.GetHbitmap(Color.FromArgb(0));  //Set the fact that background is transparent
				oldBitmap = SelectObject(memDc, hBitmap);

				//Display-rectangle
				Size size = bmp.Size;
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
				bmp.Dispose();
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
			/*
			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == false)
			{
				DrawCanvus();
				UpdateFormDisplay(this.BackgroundImage);
			}

			if (Root.FormCollection.IC.CollectingInk && Root.EraserMode == true)
			{
				DrawCanvus();
				UpdateFormDisplay(this.BackgroundImage);
			}
			*/

			DrawCanvus();
			DrawButtons();

			//for (int i = 0; i < this.Width; i++)
			//	for (int j = 0; j < this.Height; j++)
			//	{
			//		Color c = Canvus.GetPixel(i, j);
			//		int a = (c.R + c.B + c.G) / 3;
			//		Color newc = Color.FromArgb(a, c);
			//		Canvus.SetPixel(i, j, newc);
			//	}

			//Graphics gfxBack = Graphics.FromImage(this.BackgroundImage);
			//IntPtr hdcBack = gfxBack.GetHdc();
			//Graphics gfxIC = Graphics.FromImage(Canvus);
			//IntPtr hdcIC = gfxIC.GetHdc();
			//BitBlt(hdcBack, 5, 5, this.Width, this.Height, hdcIC, 0, 0, 0x00CC0020);
			//gfxBack.ReleaseHdc(hdcBack);
			//gfxIC.ReleaseHdc(hdcIC);


			UpdateFormDisplay(Canvus);
			//this.Refresh();

		}
	}
}
