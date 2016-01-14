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

		public FormCollection(Root root)
		{
			Root = root;
			InitializeComponent();

			this.Left = 0;
			this.Top = 0;
			this.MinimumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;
			gpButtons.Left = this.Width - gpButtons.Width - 30;
			gpButtons.Top = this.Height - gpButtons.Height - 60;

			IC = new InkCollector(this.Handle);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.Ink = Root.FormDisplay.IC.Ink;
			//IC.DefaultDrawingAttributes.PenTip = PenTip.Rectangle;
			IC.DefaultDrawingAttributes.AntiAliased = false;
			IC.Enabled = true;

			Image exitimage = new Bitmap(btStop.Width, btStop.Height);
			Console.WriteLine(btStop.Width);
			Graphics g = Graphics.FromImage(exitimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.exit, 0, 0, btStop.Width, btStop.Height);
			btStop.Image = exitimage;
			Image clearimage = new Bitmap(btClear.Width, btClear.Height);
			g = Graphics.FromImage(clearimage);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(global::gInk.Properties.Resources.garbage, 0, 0, btClear.Width, btClear.Height);
			btClear.Image = clearimage;

			ToTopMost();
		}

		public void ToTopMost()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0020);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
				Root.StopInk();
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			Root.StopInk();
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
	}
}
