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
	public partial class FormOptions : Form
	{
		public Root Root;

		private bool HotkeyJustSet = false;

		public FormOptions(Root root)
		{
			Root = root;
			InitializeComponent();
		}

		private void FormOptions_Load(object sender, EventArgs e)
		{
			Root.UnsetHotkey();

			if (Root.EraserEnabled)
				cbEraserEnabled.Checked = true;
			if (Root.PointerEnabled)
				cbPointerEnabled.Checked = true;
			if (Root.SnapEnabled)
				cbSnapEnabled.Checked = true;
			if (Root.UndoEnabled)
				cbUndoEnabled.Checked = true;
			if (Root.ClearEnabled)
				cbClearEnabled.Checked = true;
			if (Root.PenWidthEnabled)
				cbWidthEnabled.Checked = true;

			if (Root.WhiteTrayIcon)
				cbWhiteIcon.Checked = true;

			tbSnapPath.Text = Root.SnapshotBasePath;

			tbHotkey.BackColor = Color.White;
			if (Root.Hotkey > 0)
			{
				tbHotkey.Text = "";
				if (Root.Hotkey_Control) tbHotkey.Text += "Ctrl + ";
				if (Root.Hotkey_Alt) tbHotkey.Text += "Alt + ";
				if (Root.Hotkey_Shift) tbHotkey.Text += "Shift + ";
				if (Root.Hotkey_Win) tbHotkey.Text += "Win + ";
				tbHotkey.Text += (char)Root.Hotkey;
			}
			else
			{
				tbHotkey.Text = "None";
			}

			Label[] lbPens = new Label[10];
			CheckBox[] cbPens = new CheckBox[10];
			PictureBox[] pboxPens = new PictureBox[10];
			TextBox[] tbPensAlpha = new TextBox[10];
			TextBox[] tbPensWidth = new TextBox[10];
			for (int p = 0; p < 10; p++)
			{
				int top = p * 25 + 40;
				lbPens[p] = new Label();
				lbPens[p].Left = 20;
				lbPens[p].Top = top;
				lbPens[p].Text = "Pen " + p.ToString();

				cbPens[p] = new CheckBox();
				cbPens[p].Left = 50;
				cbPens[p].Top = top;
				cbPens[p].Text = "";
				cbPens[p].Checked = Root.PenEnabled[p];

				pboxPens[p] = new PictureBox();
				pboxPens[p].Left = 70;
				pboxPens[p].Top = top;
				pboxPens[p].Width = 15;
				pboxPens[p].Height = 15;
				pboxPens[p].BackColor = Root.PenAttr[p].Color;

				this.Controls.Add(lbPens[p]);
				this.Controls.Add(cbPens[p]);
				this.Controls.Add(pboxPens[p]);
			}
		}

		private void FormOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			Root.SetHotkey();

			Root.SaveOptions("pens.ini");
			Root.SaveOptions("config.ini");
		}

		private void cbWidthEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.PenWidthEnabled = cbWidthEnabled.Checked;
		}

		private void cbEraserEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.EraserEnabled = cbEraserEnabled.Checked;
		}

		private void cbPointerEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.PointerEnabled = cbPointerEnabled.Checked;
		}

		private void cbSnapEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.SnapEnabled = cbSnapEnabled.Checked;
		}

		private void cbUndoEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.UndoEnabled = cbUndoEnabled.Checked;
		}

		private void cbClearEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.ClearEnabled = cbClearEnabled.Checked;
		}

		private void cbWhiteIcon_CheckedChanged(object sender, EventArgs e)
		{
			Root.WhiteTrayIcon = cbWhiteIcon.Checked;
			Root.SetTrayIconColor();
		}

		private void btSnapPath_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1 = new FolderBrowserDialog();
			folderBrowserDialog1.SelectedPath = Root.SnapshotBasePath;

			DialogResult result = folderBrowserDialog1.ShowDialog();

			if (result == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
			{
				tbSnapPath.Text = folderBrowserDialog1.SelectedPath;
				Root.SnapshotBasePath = folderBrowserDialog1.SelectedPath;
			}
		}

		private void tbHotkey_KeyDown(object sender, KeyEventArgs e)
		{
			Keys modifierKeys = e.Modifiers;
			Keys pressedKey = e.KeyData ^ modifierKeys;

			if (pressedKey == Keys.Escape)
			{
				tbHotkey.Text = "None";
				Root.Hotkey = 0;
			}

			if (modifierKeys != Keys.None)
			{
				tbHotkey.BackColor = Color.LimeGreen;
				tbHotkey.Text = "";
				if ((modifierKeys & Keys.Control) > 0)
					tbHotkey.Text += "Ctrl + ";
				if ((modifierKeys & Keys.Alt) > 0)
					tbHotkey.Text += "Alt + ";
				if ((modifierKeys & Keys.Shift) > 0)
					tbHotkey.Text += "Shift + ";
				if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)
					tbHotkey.Text += "Win + ";

				if (pressedKey >= Keys.A && pressedKey <= Keys.Z || pressedKey >= Keys.D0 && pressedKey <= Keys.D9)
					tbHotkey.Text += (char)pressedKey;
			}

			if (modifierKeys != Keys.None && (pressedKey >= Keys.A && pressedKey <= Keys.Z || pressedKey >= Keys.D0 && pressedKey <= Keys.D9))
			{
				if ((modifierKeys & Keys.Control) > 0)
					Root.Hotkey_Control = true;
				else
					Root.Hotkey_Control = false;
				if ((modifierKeys & Keys.Alt) > 0)
					Root.Hotkey_Alt = true;
				else
					Root.Hotkey_Alt = false;
				if ((modifierKeys & Keys.Shift) > 0)
					Root.Hotkey_Shift = true;
				else
					Root.Hotkey_Shift = false;
				if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)
					Root.Hotkey_Win = true;
				else
					Root.Hotkey_Win = false;
				Root.Hotkey = (char)pressedKey;

				HotkeyJustSet = true;
				tbHotkey.BackColor = Color.White;
			}
		}

		private void tbHotkey_KeyUp(object sender, KeyEventArgs e)
		{
			Keys modifierKeys = e.Modifiers;
			Keys pressedKey = e.KeyData ^ modifierKeys;

			if (modifierKeys != Keys.None && !HotkeyJustSet)
			{
				tbHotkey.Text = "";
				if ((modifierKeys & Keys.Control) > 0)
					tbHotkey.Text += "Ctrl + ";
				if ((modifierKeys & Keys.Alt) > 0)
					tbHotkey.Text += "Alt + ";
				if ((modifierKeys & Keys.Shift) > 0)
					tbHotkey.Text += "Shift + ";
				if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)
					tbHotkey.Text += "Win + ";

				if (pressedKey >= Keys.A && pressedKey <= Keys.Z || pressedKey >= Keys.D0 && pressedKey <= Keys.D9)
					tbHotkey.Text += (char)pressedKey;
			}

			if (modifierKeys == Keys.None)
			{
				tbHotkey.BackColor = Color.White;
				if (Root.Hotkey > 0)
				{
					tbHotkey.Text = "";
					if (Root.Hotkey_Control) tbHotkey.Text += "Ctrl + ";
					if (Root.Hotkey_Alt) tbHotkey.Text += "Alt + ";
					if (Root.Hotkey_Shift) tbHotkey.Text += "Shift + ";
					if (Root.Hotkey_Win) tbHotkey.Text += "Win + ";
					tbHotkey.Text += (char)Root.Hotkey;
				}
				else
				{
					tbHotkey.Text = "None";
				}
				HotkeyJustSet = false;
			}

		}
	}
}
