using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace gInk
{
	public partial class FormButtonHitter : Form
	{
		Root Root;
		FormCollection FC;

		public FormButtonHitter(Root root)
		{
			Root = root;
			FC = Root.FormCollection;
			InitializeComponent();

			this.Left = FC.gpButtons.Left;
			this.Top = FC.gpButtons.Top;
			this.Width = FC.gpButtons.Width;
			this.Height = FC.gpButtons.Height;
		}

		private void FormButtonHitter_Click(object sender, EventArgs e)
		{
			MouseEventArgs m = (MouseEventArgs)e;
			foreach (Control control in FC.gpButtons.Controls)
			{
				if (control.GetType() == typeof(Button))
				{
					if (m.X >= control.Left && m.X <= control.Right)
						((Button)control).PerformClick();
				}
			}
		}

		
		public void ToTopMost()
		{
			UInt32 dwExStyle = GetWindowLong(this.Handle, -20);
			SetWindowLong(this.Handle, -20, dwExStyle | 0x00080000);
			//SetLayeredWindowAttributes(this.Handle, 0x00FFFFFF, 200, 0x2);
			SetWindowPos(this.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0020);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				this.Left = FC.gpButtons.Left;
				this.Top = FC.gpButtons.Top;
			}
		}

		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		[DllImport("user32.dll", SetLastError = true)]
		static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
		[DllImport("user32.dll")]
		public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
	}
}
