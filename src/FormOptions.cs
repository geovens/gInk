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

		Label[] lbPens = new Label[10];
		CheckBox[] cbPens = new CheckBox[10];
		PictureBox[] pboxPens = new PictureBox[10];
		ComboBox[] comboPensAlpha = new ComboBox[10];
		ComboBox[] comboPensWidth = new ComboBox[10];

		Label[] lbHotkeyPens = new Label[10];
		HotkeyInputBox[] hiPens = new HotkeyInputBox[10];

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
			if (Root.PanEnabled)
				cbPanEnabled.Checked = true;
			if (Root.InkVisibleEnabled)
				cbInkVisibleEnabled.Checked = true;

			if (Root.WhiteTrayIcon)
				cbWhiteIcon.Checked = true;
			if (Root.AllowDraggingToolbar)
				cbAllowDragging.Checked = true;
			if (Root.AllowHotkeyInPointerMode)
				cbAllowHotkeyInPointer.Checked = true;

			comboCanvasCursor.SelectedIndex = Root.CanvasCursor;

			tbSnapPath.Text = Root.SnapshotBasePath;
			
			lbNote.ForeColor = Color.Black;

			Label lbcbPens = new Label();
			lbcbPens.Left = 90;
			lbcbPens.Width = 35;
			lbcbPens.Top = 15;
			lbcbPens.Text = "Show";
			tabPage2.Controls.Add(lbcbPens);
			Label lbpboxPens = new Label();
			lbpboxPens.Left = 125;
			lbpboxPens.Width = 35;
			lbpboxPens.Top = 15;
			lbpboxPens.Text = "Color";
			tabPage2.Controls.Add(lbpboxPens);
			Label lbcomboPensAlpha = new Label();
			lbcomboPensAlpha.Left = 160;
			lbcomboPensAlpha.Width = 55;
			lbcomboPensAlpha.Top = 15;
			lbcomboPensAlpha.Text = "Alpha";
			tabPage2.Controls.Add(lbcomboPensAlpha);
			Label lbcomboPensWidth = new Label();
			lbcomboPensWidth.Left = 250;
			lbcomboPensWidth.Width = 55;
			lbcomboPensWidth.Top = 15;
			lbcomboPensWidth.Text = "Width";
			tabPage2.Controls.Add(lbcomboPensWidth);

			for (int p = 0; p < Root.MaxPenCount; p++)
			{
				int top = p * 25 + 40;
				lbPens[p] = new Label();
				lbPens[p].Left = 40;
				lbPens[p].Width = 40;
				lbPens[p].Top = top;
				lbPens[p].Text = "Pen " + p.ToString();

				cbPens[p] = new CheckBox();
				cbPens[p].Left = 90;
				cbPens[p].Width = 15;
				cbPens[p].Top = top - 5;
				cbPens[p].Text = "";
				cbPens[p].Checked = Root.PenEnabled[p];
				cbPens[p].CheckedChanged += cbPens_CheckedChanged;

				pboxPens[p] = new PictureBox();
				pboxPens[p].Left = 125;
				pboxPens[p].Top = top;
				pboxPens[p].Width = 15;
				pboxPens[p].Height = 15;
				pboxPens[p].BackColor = Root.PenAttr[p].Color;
				pboxPens[p].Click += pboxPens_Click;

				comboPensAlpha[p] = new ComboBox();
				comboPensAlpha[p].Items.AddRange(new object[] { "Pencil", "Highlighter" });
				comboPensAlpha[p].Left = 160;
				comboPensAlpha[p].Top = top - 2;
				comboPensAlpha[p].Width = 70;
				comboPensAlpha[p].Text = (255 - Root.PenAttr[p].Transparency).ToString();
				comboPensAlpha[p].TextChanged += comboPensAlpha_TextChanged;

				comboPensWidth[p] = new ComboBox();
				comboPensWidth[p].Items.AddRange(new object[] {"Thin","Normal","Thick"});
				comboPensWidth[p].Left = 250;
				comboPensWidth[p].Top = top - 2;
				comboPensWidth[p].Width = 70;
				comboPensWidth[p].Text = ((int)Root.PenAttr[p].Width).ToString();
				comboPensWidth[p].TextChanged += comboPensWidth_TextChanged;

				tabPage2.Controls.Add(lbPens[p]);
				tabPage2.Controls.Add(cbPens[p]);
				tabPage2.Controls.Add(pboxPens[p]);
				tabPage2.Controls.Add(comboPensAlpha[p]);
				tabPage2.Controls.Add(comboPensWidth[p]);
			}

			for (int p = 0; p < Root.MaxPenCount; p++)
			{
				int top = p * 25 + 100;
				lbHotkeyPens[p] = new Label();
				lbHotkeyPens[p].Left = 20;
				lbHotkeyPens[p].Width = 40;
				lbHotkeyPens[p].Top = top;
				lbHotkeyPens[p].Text = "Pen " + p.ToString();

				hiPens[p] = new HotkeyInputBox();
				hiPens[p].Hotkey = Root.Hotkey_Pens[p];
				hiPens[p].Left = 70;
				hiPens[p].Width = 150;
				hiPens[p].Top = top;
				hiPens[p].OnHotkeyChanged += hi_OnHotkeyChanged;

				tabPage3.Controls.Add(lbHotkeyPens[p]);
				tabPage3.Controls.Add(hiPens[p]);
			}

			hiGlobal.Hotkey = Root.Hotkey_Global;
			hiEraser.Hotkey = Root.Hotkey_Eraser;
			hiPan.Hotkey = Root.Hotkey_Pan;
			hiInkVisible.Hotkey = Root.Hotkey_InkVisible;
			hiPointer.Hotkey = Root.Hotkey_Pointer;
			hiSnapshot.Hotkey = Root.Hotkey_Snap;
			hiUndo.Hotkey = Root.Hotkey_Undo;
			hiRedo.Hotkey = Root.Hotkey_Redo;
			hiClear.Hotkey = Root.Hotkey_Clear;
		}

		private void comboPensAlpha_TextChanged(object sender, EventArgs e)
		{
			for (int p = 0; p < Root.MaxPenCount; p++)
				if ((ComboBox)sender == comboPensAlpha[p])
				{
					byte o;
					if (byte.TryParse(comboPensAlpha[p].Text, out o) && o >= 0 && o <= 255)
					{
						Root.PenAttr[p].Transparency = (byte)(255 - o);
						comboPensAlpha[p].BackColor = Color.White;
					}
					else
					{
						comboPensAlpha[p].BackColor = Color.IndianRed;
					}
				}
		}

		private void comboPensWidth_TextChanged(object sender, EventArgs e)
		{
			for (int p = 0; p < Root.MaxPenCount; p++)
				if ((ComboBox)sender == comboPensWidth[p])
				{
					int o;
					if (int.TryParse(comboPensWidth[p].Text, out o) && o >= 30 && o <= 3000)
					{
						Root.PenAttr[p].Width = o;
						comboPensWidth[p].BackColor = Color.White;
					}
					else
					{
						comboPensWidth[p].BackColor = Color.IndianRed;
					}
				}
		}

		private void pboxPens_Click(object sender, EventArgs e)
		{
			for (int p = 0; p < Root.MaxPenCount; p++)
				if ((PictureBox)sender == pboxPens[p])
				{
					colorDialog1.Color = Root.PenAttr[p].Color;
					if (colorDialog1.ShowDialog() == DialogResult.OK)
					{
						Root.PenAttr[p].Color = colorDialog1.Color;
						pboxPens[p].BackColor = colorDialog1.Color;
					}
				}
		}

		private void cbPens_CheckedChanged(object sender, EventArgs e)
		{
			for (int p = 0; p < Root.MaxPenCount; p++)
				if ((CheckBox)sender == cbPens[p])
					Root.PenEnabled[p] = cbPens[p].Checked;
		}

		private void FormOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			Root.SetHotkey();

			Root.SaveOptions("pens.ini");
			Root.SaveOptions("config.ini");
			Root.SaveOptions("hotkeys.ini");
		}

		private void cbWidthEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.PenWidthEnabled = cbWidthEnabled.Checked;
			lbNote.ForeColor = Color.Red;
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

		private void cbPanEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.PanEnabled = cbPanEnabled.Checked;
		}

		private void cbInkVisibleEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Root.InkVisibleEnabled = cbInkVisibleEnabled.Checked;
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

		private void tbSnapPath_ModifiedChanged(object sender, EventArgs e)
		{
			Root.SnapshotBasePath = tbSnapPath.Text;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			for (int p = 0; p < Root.MaxPenCount; p++)
			{
				if (comboPensWidth[p].Text.ToString() == "Thin")
				{
					comboPensWidth[p].Text = "30";
				}
				else if (comboPensWidth[p].Text == "Normal")
				{
					comboPensWidth[p].Text = "80";
				}
				else if (comboPensWidth[p].Text == "Thick")
				{
					comboPensWidth[p].Text = "500";
				}

				if (comboPensAlpha[p].Text.ToString() == "Pencil")
				{
					comboPensAlpha[p].Text = "255";
				}
				else if (comboPensAlpha[p].Text == "Highlighter")
				{
					comboPensAlpha[p].Text = "80";
				}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Root.CanvasCursor = comboCanvasCursor.SelectedIndex;
		}

		private void cbAllowDragging_CheckedChanged(object sender, EventArgs e)
		{
			Root.AllowDraggingToolbar = cbAllowDragging.Checked;
		}

		private void cbAllowHotkeyInPointer_CheckedChanged(object sender, EventArgs e)
		{
			Root.AllowHotkeyInPointerMode = cbAllowHotkeyInPointer.Checked;
		}

		private void hi_OnHotkeyChanged(object sender, EventArgs e)
		{
			foreach (Control c in tabPage3.Controls)
			{
				if (c.GetType() != typeof(HotkeyInputBox))
					continue;
				HotkeyInputBox hi = (HotkeyInputBox)c;

				hi.ExternalConflictFlag = false;
				foreach (Control c2 in tabPage3.Controls)
				{
					if (c2.GetType() != typeof(HotkeyInputBox))
						continue;
					if (c == c2)
						continue;
					HotkeyInputBox hi2 = (HotkeyInputBox)c2;

					if (hi.Hotkey.ConflictWith(hi2.Hotkey))
					{
						hi.ExternalConflictFlag = true;
						break;
					}
				}
			}
		}
	}
}
