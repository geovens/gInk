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
		public InkCollector IC;

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

		public FormDisplay(Root root)
		{
			Root = root;
			InitializeComponent();

			this.Left = 0;
			this.Top = 0;
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;

			IC = new InkCollector(this.Handle);
			//IC.SetWindowInputRectangle(new Rectangle(0, 0, 100, 100));
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.DefaultDrawingAttributes.Color = Color.Red;
			IC.Enabled = true;

			//Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			//Graphics g = Graphics.FromImage(bmpScreenCapture);
			//g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
			//this.BackgroundImage = bmpScreenCapture;

			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			SetWindowPos(this.Handle, (IntPtr)0, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0004 | 0x0010 | 0x0020);
			//SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 70, 0x00000002);
			SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 0, 0x00000001);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000 | 0x00000020);
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010 | 0x0020);
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
				this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		bool Refreshed = false;
		private void timer1_Tick(object sender, EventArgs e)
		{
			//Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			//Graphics g = Graphics.FromImage(bmpScreenCapture);
			//Root.Form1.DrawToBitmap(bmpScreenCapture, new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
			//this.BackgroundImage = bmpScreenCapture;

			if (!Root.Form1.IC.CollectingInk && !Refreshed)
			{
				this.Refresh();
				Refreshed = true;
			}

			if (Root.Form1.IC.CollectingInk)
			{
				Graphics g = this.CreateGraphics();
				Stroke stroke = IC.Ink.Strokes[IC.Ink.Strokes.Count - 1];
				IC.Renderer.Draw(g, stroke, IC.DefaultDrawingAttributes);
				Refreshed = false;
			}
		}
	}
}
