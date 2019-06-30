using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gInk
{
	public partial class HotkeyInputBox : TextBox
	{
		//private int _Key;
		//private bool _Control, _Alt, _Shift, _Win;
		private Hotkey _Hotkey = new Hotkey();
		//public int Key { get { return _Key; } set { _Key = value; UpdateText(); } }
		//public bool Control { get { return _Control; } set { _Control = value; UpdateText(); } }
		//public bool Alt { get { return _Alt; } set { _Alt = value;  UpdateText(); } }
		//public bool Shift { get { return _Shift; } set { _Shift = value; UpdateText(); } }
		//public bool Win { get { return _Win; } set { _Win = value; UpdateText(); } }
		public Hotkey Hotkey { get { return _Hotkey; } set { _Hotkey = value; UpdateText(); } }

		public bool RequireModifier { get; set; }

		private bool HotkeyJustSet = false;

		public HotkeyInputBox()
		{
			InitializeComponent();

			Width = 150;
		}

		protected void UpdateText()
		{
			if (Hotkey.Key > 0)
			{
				Text = "";
				if (Hotkey.Control)
					Text += "Ctrl + ";
				if (Hotkey.Alt)
					Text += "Alt + ";
				if (Hotkey.Shift)
					Text += "Shift + ";
				if (Hotkey.Win)
					Text += "Win + ";
				Text += (char)Hotkey.Key;
			}
			else
			{
				Text = "None";
			}
		}
		/*
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		*/
		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.SuppressKeyPress = true;

			Keys modifierKeys = e.Modifiers;
			Keys pressedKey = e.KeyData ^ modifierKeys;

			if (pressedKey == Keys.Escape || pressedKey == Keys.Delete)
			{
				Text = "None";
				Hotkey.Key = 0;
				Hotkey.Control = false;
				Hotkey.Alt = false;
				Hotkey.Shift = false;
				Hotkey.Win = false;
			}

			else if (modifierKeys != Keys.None)
			{
				BackColor = Color.LimeGreen;
				Text = "";
				if ((modifierKeys & Keys.Control) > 0)
					Text += "Ctrl + ";
				if ((modifierKeys & Keys.Alt) > 0)
					Text += "Alt + ";
				if ((modifierKeys & Keys.Shift) > 0)
					Text += "Shift + ";
				if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)
					Text += "Win + ";

				if (pressedKey >= Keys.A && pressedKey <= Keys.Z || pressedKey >= Keys.D0 && pressedKey <= Keys.D9)
				{
					Text += (char)pressedKey;
				}
			}


			if ((!RequireModifier || modifierKeys != Keys.None) && Hotkey.IsValidKey(pressedKey))
			{
				Hotkey.Key = (int)pressedKey;
				Hotkey.Control = (modifierKeys & Keys.Control) > 0;
				Hotkey.Alt = (modifierKeys & Keys.Alt) > 0;
				Hotkey.Shift = (modifierKeys & Keys.Shift) > 0;
				Hotkey.Win = (modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0;
				HotkeyJustSet = true;
				BackColor = Color.White;
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			Keys modifierKeys = e.Modifiers;
			Keys pressedKey = e.KeyData ^ modifierKeys;

			if (modifierKeys != Keys.None && !HotkeyJustSet)
			{
				Text = "";
				if ((modifierKeys & Keys.Control) > 0)
					Text += "Ctrl + ";
				if ((modifierKeys & Keys.Alt) > 0)
					Text += "Alt + ";
				if ((modifierKeys & Keys.Shift) > 0)
					Text += "Shift + ";
				if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)
					Text += "Win + ";

				if (Hotkey.IsValidKey(pressedKey))
					Text += (char)pressedKey;
			}

			if (modifierKeys == Keys.None)
			{
				UpdateText();
				BackColor = Color.White;
				HotkeyJustSet = false;
			}
		}

	}
}
