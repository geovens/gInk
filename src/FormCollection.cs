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
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;

			IC = new InkCollector(this.Handle);
			IC.CollectionMode = CollectionMode.InkOnly;
			IC.Ink = Root.FormDisplay.IC.Ink;
			IC.DefaultDrawingAttributes.Color = Color.Red;
			IC.Enabled = true;

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

		private void button1_Click(object sender, EventArgs e)
		{
			Root.StopInk();
		}
	}
}
