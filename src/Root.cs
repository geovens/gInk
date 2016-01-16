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
	public class Root
	{
		public DrawingAttributes Pen1, Pen2, Pen3;

		public bool EraserMode = false;

		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
		public FormCollection FormCollection;
		public FormDisplay FormDisplay;

		public int CurrentPen = 1;  // defaut pen

		public Root()
		{
			SetDefaultPens();
			ReadOptions("pens.ini");
			ReadOptions("config.ini");

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("Exit", OnExit);

			trayIcon = new NotifyIcon();
			trayIcon.Text = "MyTrayApp";
			trayIcon.Icon = new Icon(gInk.Properties.Resources.icon_white, 40, 40);
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			trayIcon.Click += TrayIcon_Click;
			
		}

		private void TrayIcon_Click(object sender, EventArgs e)
		{
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

					}
				}
			}
			fini.Close();
		}

		private void OnExit(object sender, EventArgs e)
		{
			trayIcon.Dispose();
			Application.Exit();
		}

		[DllImport("user32.dll")]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);
	}
}

