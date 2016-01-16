using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Ink;

namespace gInk
{
	public class TestMessageFilter : IMessageFilter
	{
		public Root Root;

		public TestMessageFilter(Root root)
		{
			Root = root;
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == 0x0312)
			{
				//Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
				//int modifier = (int)m.LParam & 0xFFFF;       // The modifier of the hotkey that was pressed.
				//int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

				if (Root.FormCollection == null && Root.FormDisplay == null)
					Root.StartInk();
				else
				{
					Root.FormCollection.RetreatAndExit();
				}
			}
			return false;
		}
	}

	public class Root
	{
		public DrawingAttributes Pen1, Pen2, Pen3;
		public bool Hotkey_Control, Hotkey_Alt, Hotkey_Shift, Hotkey_Win;
		public int Hotkey;

		public bool EraserMode = false;

		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
		public FormCollection FormCollection;
		public FormDisplay FormDisplay;

		public int CurrentPen = 1;  // defaut pen

		public Root()
		{
			SetDefaultPens();
			SetDefaultConfig();
			ReadOptions("pens.ini");
			ReadOptions("config.ini");

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("About", OnAbout);
			trayMenu.MenuItems.Add("Exit", OnExit);

			trayIcon = new NotifyIcon();
			trayIcon.Text = "gInk";
			trayIcon.Icon = new Icon(gInk.Properties.Resources.icon_white, 40, 40);
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			trayIcon.MouseClick += TrayIcon_Click;

			int modifier = 0;
			if (Hotkey_Control) modifier |= 0x2;
			if (Hotkey_Alt) modifier |= 0x1;
			if (Hotkey_Shift) modifier |= 0x4;
			if (Hotkey_Win) modifier |= 0x8;
			if (modifier != 0)
				RegisterHotKey(IntPtr.Zero, 0, modifier, Hotkey);

			TestMessageFilter mf = new TestMessageFilter(this);
			Application.AddMessageFilter(mf);
		}

		private void TrayIcon_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				StartInk();
		}

		public void StartInk()
		{
			FormDisplay = new FormDisplay(this);
			FormCollection = new FormCollection(this);
			SelectPen(CurrentPen);
			FormDisplay.Show();
			FormCollection.Show();
			FormDisplay.DrawButtons(true);
		}
		public void StopInk()
		{
			FormCollection.Close();
			FormDisplay.Close();
			//FormCollection.Dispose();
			//FormDisplay.Dispose();
			FormCollection = null;
			FormDisplay = null;
		}

		public void ClearInk()
		{
			FormCollection.IC.Ink.DeleteStrokes();
			FormDisplay.ClearCanvus();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay();
		}

		public void SelectPen(int pen)
		{
			FormCollection.SelectPen(pen);
		}

		public void SetDefaultPens()
		{
			Pen1 = new DrawingAttributes();
			Pen1.Color = Color.FromArgb(250, 50, 50);
			Pen1.Width = 80;
			Pen1.Transparency = 30;

			Pen2 = new DrawingAttributes();
			Pen2.Color = Color.FromArgb(50, 50, 250);
			Pen2.Width = 80;
			Pen2.Transparency = 30;

			Pen3 = new DrawingAttributes();
			Pen3.Color = Color.FromArgb(240, 240, 0);
			Pen3.Width = 800;
			Pen3.Transparency = 160;
		}

		public void SetDefaultConfig()
		{
			Hotkey_Control = true;
			Hotkey_Alt = true;
			Hotkey_Shift = false;
			Hotkey_Win = false;
			Hotkey = 'G';
		}

		public void ReadOptions(string file)
		{
			if (!File.Exists(file))
			{
				return;
			}

			FileStream fini = new FileStream(file, FileMode.Open);
			StreamReader srini = new StreamReader(fini);
			string sLine = "";
			string sName = "", sPara = "";
			while (sLine != null)
			{
				sLine = srini.ReadLine();
				if
				(
					sLine != null &&
					sLine != "" &&
					sLine.Substring(0, 1) != "-" &&
					sLine.Substring(0, 1) != "%" &&
					sLine.Substring(0, 1) != "'" &&
					sLine.Substring(0, 1) != "/" &&
					sLine.Substring(0, 1) != "!" &&
					sLine.Substring(0, 1) != "[" &&
					sLine.Substring(0, 1) != "#" &&
					sLine.Contains("=") &&
					!sLine.Substring(sLine.IndexOf("=") + 1).Contains("=")
				)
				{
					sName = sLine.Substring(0, sLine.IndexOf("="));
					sName = sName.Trim();
					sName = sName.ToUpper();
					sPara = sLine.Substring(sLine.IndexOf("=") + 1);
					sPara = sPara.Trim();

					int o;
					switch (sName)
					{
						case "PEN1_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen1.Color = Color.FromArgb(int.Parse(sPara), Pen1.Color.G, Pen1.Color.B);
							break;
						case "PEN1_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen1.Color = Color.FromArgb(Pen1.Color.R, int.Parse(sPara), Pen1.Color.B);
							break;
						case "PEN1_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen1.Color = Color.FromArgb(Pen1.Color.R, Pen1.Color.G, int.Parse(sPara));
							break;
						case "PEN2_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen2.Color = Color.FromArgb(int.Parse(sPara), Pen2.Color.G, Pen2.Color.B);
							break;
						case "PEN2_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen2.Color = Color.FromArgb(Pen2.Color.R, int.Parse(sPara), Pen2.Color.B);
							break;
						case "PEN2_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen2.Color = Color.FromArgb(Pen2.Color.R, Pen2.Color.G, int.Parse(sPara));
							break;
						case "PEN3_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen3.Color = Color.FromArgb(int.Parse(sPara), Pen3.Color.G, Pen3.Color.B);
							break;
						case "PEN3_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen3.Color = Color.FromArgb(Pen3.Color.R, int.Parse(sPara), Pen3.Color.B);
							break;
						case "PEN3_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen3.Color = Color.FromArgb(Pen3.Color.R, Pen3.Color.G, int.Parse(sPara));
							break;

						case "PEN1_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen1.Transparency = (byte)(255 - o);
							break;
						case "PEN2_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen2.Transparency = (byte)(255 - o);
							break;
						case "PEN3_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen3.Transparency = (byte)(255 - o);
							break;

						case "PEN1_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								Pen1.Width = o;
							break;
						case "PEN2_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								Pen2.Width = o;
							break;
						case "PEN3_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								Pen3.Width = o;
							break;

						case "HOTKEY":
							sPara = sPara.ToUpper();
							if (sPara.Contains("CONTROL"))
								Hotkey_Control = true;
							else
								Hotkey_Control = false;
							if (sPara.Contains("ALT"))
								Hotkey_Alt = true;
							else
								Hotkey_Alt = false;
							if (sPara.Contains("SHIFT"))
								Hotkey_Shift = true;
							else
								Hotkey_Shift = false;
							if (sPara.Contains("WIN"))
								Hotkey_Win = true;
							else
								Hotkey_Win = false;
							Hotkey = sPara.Substring(sPara.Length - 1).ToCharArray()[0];

							break;
					}
				}
			}
			fini.Close();
		}

		private void OnAbout(object sender, EventArgs e)
		{
			FormAbout FormAbout = new FormAbout();
			FormAbout.Show();
		}

		private void OnExit(object sender, EventArgs e)
		{
			int modifier = 0;
			if (Hotkey_Control) modifier |= 0x2;
			if (Hotkey_Alt) modifier |= 0x1;
			if (Hotkey_Shift) modifier |= 0x4;
			if (Hotkey_Win) modifier |= 0x8;
			if (modifier != 0)
				UnregisterHotKey(IntPtr.Zero, 0);

			trayIcon.Dispose();
			Application.Exit();
		}

		[DllImport("user32.dll")]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);
	}
}

