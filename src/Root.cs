using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;
using System.Threading;

namespace gInk
{
	public class Root
	{
		public bool EraserMode = false;
		public bool EraserLock = false;

		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
		public FormCollection FormCollection;
		public FormDisplay FormDisplay;

		public int CurrentPen;

		public Root()
		{
			CurrentPen = 3;
			ReadOptions();

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("Exit", OnExit);

			trayIcon = new NotifyIcon();
			trayIcon.Text = "MyTrayApp";
			trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
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
			FormDisplay.IC.Ink.DeleteStrokes();
			FormDisplay.ClearCanvus();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay();
		}

		public void SelectPen(int pen)
		{
			FormCollection.SelectPen(pen);
		}

		public void ReadOptions()
		{
			if (!File.Exists("config.ini"))
			{
				return;
			}

			FileStream fini = new FileStream("config.ini", FileMode.Open);
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

					switch (sName)
					{
						case "SOMENAME":
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
	}
}

