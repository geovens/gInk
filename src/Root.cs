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
				else if (Root.Docked)
					Root.UnDock();
			}
			return false;
		}
	}

	public class Root
	{
		// options
		public DrawingAttributes Pen1, Pen2, Pen3, Pen4, Pen5;
		public bool Hotkey_Control, Hotkey_Alt, Hotkey_Shift, Hotkey_Win;
		public int Hotkey;
		public bool AutoScroll;
		public bool WhiteTrayIcon;
		public string SnapshotBasePath;

		public bool EraserMode = false;
		public bool Docked = false;
		public bool PointerMode = false;
		public int Snapping = 0;  // <=0: not snapping, 1: waiting finger, 2:dragging
		public int SnappingX = -1, SnappingY = -1;
		public Rectangle SnappingRect;
		public int UponButtonsUpdate = 0;
		public bool UponTakingSnap = false;
        public bool UponBalloonSnap = false;

		public Ink[] UndoStrokes;
		//public Ink UponUndoStrokes;
		public int UndoP;
		public int UndoDepth, RedoDepth;

		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
		public FormCollection FormCollection;
		public FormDisplay FormDisplay;
		public FormButtonHitter FormButtonHitter;

		public int CurrentPen = 1;  // defaut pen

		public Root()
		{
			SetDefaultPens();
			SetDefaultConfig();
			ReadOptions("pens.ini");
			ReadOptions("config.ini");

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("About", OnAbout);
			trayMenu.MenuItems.Add("Pen Configurations", OnPenSetting);
			trayMenu.MenuItems.Add("Options", OnOptions);
			trayMenu.MenuItems.Add("-");
			trayMenu.MenuItems.Add("Exit", OnExit);

            Size size = SystemInformation.SmallIconSize;
            trayIcon = new NotifyIcon();
			trayIcon.Text = "gInk";
			if (WhiteTrayIcon)
				trayIcon.Icon = new Icon("icon_white.ico");
			else
				trayIcon.Icon = new Icon("icon_red.ico");
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			trayIcon.MouseClick += TrayIcon_Click;
            trayIcon.BalloonTipText = "Snapshot saved. Click here to browse snapshots.";
            trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;


            int modifier = 0;
			if (Hotkey_Control) modifier |= 0x2;
			if (Hotkey_Alt) modifier |= 0x1;
			if (Hotkey_Shift) modifier |= 0x4;
			if (Hotkey_Win) modifier |= 0x8;
			if (modifier != 0)
				RegisterHotKey(IntPtr.Zero, 0, modifier, Hotkey);

			TestMessageFilter mf = new TestMessageFilter(this);
			Application.AddMessageFilter(mf);

			FormCollection = null;
			FormDisplay = null;
		}

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            string snapbasepath = SnapshotBasePath;
            snapbasepath = Environment.ExpandEnvironmentVariables(snapbasepath);
            System.Diagnostics.Process.Start(snapbasepath);
        }

        private void TrayIcon_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (FormDisplay == null && FormCollection == null)
				{
					ReadOptions("pens.ini");
					ReadOptions("config.ini");
					StartInk();
				}
				else if (Docked)
					UnDock();
			}
		}

		public void StartInk()
		{
			if (FormDisplay != null || FormCollection != null)
				return;

			//Docked = false;
			FormDisplay = new FormDisplay(this);
			FormCollection = new FormCollection(this);
			FormButtonHitter = new FormButtonHitter(this);
			if (CurrentPen <= 0)
				CurrentPen = 1;
			SelectPen(CurrentPen);
			FormDisplay.Show();
			FormCollection.Show();
			FormDisplay.DrawButtons(true);

			UndoStrokes = new Ink[8];
			UndoStrokes[0] = FormCollection.IC.Ink.Clone();
			UndoDepth = 0;
			UndoP = 0;

			//UponUndoStrokes = FormCollection.IC.Ink.Clone();
		}
		public void StopInk()
		{
			FormCollection.Close();
			FormDisplay.Close();
			FormButtonHitter.Close();
			//FormCollection.Dispose();
			//FormDisplay.Dispose();
			GC.Collect();
			FormCollection = null;
			FormDisplay = null;

            if (UponBalloonSnap)
            {
                ShowBalloonSnapshot();
                UponBalloonSnap = false;
            }
		}

		public void ClearInk()
		{
			FormCollection.IC.Ink.DeleteStrokes();
			FormDisplay.ClearCanvus();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay(true);
		}

        public void ShowBalloonSnapshot()
        {
            Console.WriteLine(SnapshotBasePath);
            trayIcon.ShowBalloonTip(3000);
        }

		public void UndoInk()
		{
			if (UndoDepth <= 0)
				return;

			UndoP--;
			if (UndoP < 0)
				UndoP = UndoStrokes.GetLength(0) - 1;
			UndoDepth--;
			RedoDepth++;
			FormCollection.IC.Ink.DeleteStrokes();
			if (UndoStrokes[UndoP].Strokes.Count > 0)
				FormCollection.IC.Ink.AddStrokesAtRectangle(UndoStrokes[UndoP].Strokes, UndoStrokes[UndoP].Strokes.GetBoundingBox());
			
			FormDisplay.ClearCanvus();
			FormDisplay.DrawStrokes();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay(true);
		}

		public void RedoInk()
		{
			if (RedoDepth <= 0)
				return;

			UndoDepth++;
			RedoDepth--;
			UndoP++;
			if (UndoP >= UndoStrokes.GetLength(0))
				UndoP = 0;
			FormCollection.IC.Ink.DeleteStrokes();
			if (UndoStrokes[UndoP].Strokes.Count > 0)
				FormCollection.IC.Ink.AddStrokesAtRectangle(UndoStrokes[UndoP].Strokes, UndoStrokes[UndoP].Strokes.GetBoundingBox());

			FormDisplay.ClearCanvus();
			FormDisplay.DrawStrokes();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay(true);
		}

		public void Dock()
		{
			if (FormDisplay == null || FormCollection == null)
				return;

			Docked = true;
			FormCollection.btDock.Image = FormCollection.image_dockback;
			UponButtonsUpdate |= 0x2;
		}

		public void UnDock()
		{
			if (FormDisplay == null || FormCollection == null)
				return;

			Docked = false;
			FormCollection.btDock.Image = FormCollection.image_dock;
			UponButtonsUpdate |= 0x2;
		}

		public void Pointer()
		{
			if (PointerMode == true)
				return;

			PointerMode = true;
			FormCollection.ToThrough();
			FormButtonHitter.Show();
		}

		public void UnPointer()
		{
			if (PointerMode == false)
				return;

			PointerMode = false;
			FormCollection.ToUnThrough();
			FormCollection.ToTopMost();
			FormCollection.Cursor = FormCollection.cursorred;
			FormCollection.IC.Cursor = FormCollection.cursorred;
			FormButtonHitter.Hide();
		}

		public void SelectPen(int pen)
		{
			FormCollection.SelectPen(pen);
		}

		public void SetDefaultPens()
		{
			Pen1 = new DrawingAttributes();
			Pen1.Color = Color.FromArgb(220, 95, 60);
			Pen1.Width = 80;
			Pen1.Transparency = 5;

			Pen2 = new DrawingAttributes();
			Pen2.Color = Color.FromArgb(30, 110, 200);
			Pen2.Width = 80;
			Pen2.Transparency = 5;

			Pen3 = new DrawingAttributes();
			Pen3.Color = Color.FromArgb(235, 180, 55);
			Pen3.Width = 80;
			Pen3.Transparency = 5;

			Pen4 = new DrawingAttributes();
			Pen4.Color = Color.FromArgb(120, 175, 70);
			Pen4.Width = 80;
			Pen4.Transparency = 5;

			Pen5 = new DrawingAttributes();
			Pen5.Color = Color.FromArgb(145, 70, 160);
			Pen5.Width = 500;
			Pen5.Transparency = 200;
		}

		public void SetDefaultConfig()
		{
			Hotkey_Control = true;
			Hotkey_Alt = true;
			Hotkey_Shift = false;
			Hotkey_Win = false;
			Hotkey = 'G';

			AutoScroll = false;
			WhiteTrayIcon = false;
			SnapshotBasePath = "%USERPROFILE%/Pictures/gInk/";
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
						case "PEN4_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen4.Color = Color.FromArgb(int.Parse(sPara), Pen4.Color.G, Pen4.Color.B);
							break;
						case "PEN4_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen4.Color = Color.FromArgb(Pen4.Color.R, int.Parse(sPara), Pen4.Color.B);
							break;
						case "PEN4_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen4.Color = Color.FromArgb(Pen4.Color.R, Pen4.Color.G, int.Parse(sPara));
							break;
						case "PEN5_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen5.Color = Color.FromArgb(int.Parse(sPara), Pen5.Color.G, Pen5.Color.B);
							break;
						case "PEN5_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen5.Color = Color.FromArgb(Pen5.Color.R, int.Parse(sPara), Pen5.Color.B);
							break;
						case "PEN5_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen5.Color = Color.FromArgb(Pen5.Color.R, Pen5.Color.G, int.Parse(sPara));
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
						case "PEN4_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen4.Transparency = (byte)(255 - o);
							break;
						case "PEN5_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								Pen5.Transparency = (byte)(255 - o);
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
						case "PEN4_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								Pen4.Width = o;
							break;
						case "PEN5_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								Pen5.Width = o;
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
							if (sPara.Length > 0)
								Hotkey = sPara.Substring(sPara.Length - 1).ToCharArray()[0];
							break;
						case "ENABLEAUTOSCROLL":
							sPara = sPara.ToUpper();
							if (sPara.Contains("TRUE"))
								AutoScroll = true;
							else
								AutoScroll = false;
							break;
						case "WHITETRAYICON":
							sPara = sPara.ToUpper();
							if (sPara.Contains("TRUE"))
								WhiteTrayIcon = true;
							else
								WhiteTrayIcon = false;
							break;
						case "SNAPSHOTPATH":
							SnapshotBasePath = sPara;
							if (!SnapshotBasePath.EndsWith("/") && !SnapshotBasePath.EndsWith("\\"))
								SnapshotBasePath += "/";
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

		private void OnPenSetting(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("notepad.exe", "pens.ini");
		}

		private void OnOptions(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("notepad.exe", "config.ini");
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

