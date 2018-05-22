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
				else
					Root.Dock();
			}
			return false;
		}
	}

	public class Root
	{
		// options
		public DrawingAttributes[] PenAttr = new DrawingAttributes[10];
		public bool Hotkey_Control, Hotkey_Alt, Hotkey_Shift, Hotkey_Win;
		public int Hotkey;
		public bool AutoScroll;
		public bool WhiteTrayIcon;
		public string SnapshotBasePath;

		public bool EraserMode = false;
		public bool Docked = false;
		public bool PointerMode = false;
		public bool FingerInAction = false;  // true when mouse down, either drawing or snapping or whatever
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

		public int PenCount = 10;
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

			if (UndoStrokes == null)
			{
				UndoStrokes = new Ink[8];
				UndoStrokes[0] = FormCollection.IC.Ink.Clone();
				UndoDepth = 0;
				UndoP = 0;
			}

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
			PenAttr[0] = new DrawingAttributes();
			PenAttr[0].Color = Color.FromArgb(220, 95, 60);
			PenAttr[0].Width = 80;
			PenAttr[0].Transparency = 5;

			PenAttr[1] = new DrawingAttributes();
			PenAttr[1].Color = Color.FromArgb(30, 110, 200);
			PenAttr[1].Width = 80;
			PenAttr[1].Transparency = 5;

			PenAttr[2] = new DrawingAttributes();
			PenAttr[2].Color = Color.FromArgb(235, 180, 55);
			PenAttr[2].Width = 80;
			PenAttr[2].Transparency = 5;

			PenAttr[3] = new DrawingAttributes();
			PenAttr[3].Color = Color.FromArgb(120, 175, 70);
			PenAttr[3].Width = 80;
			PenAttr[3].Transparency = 5;

			PenAttr[4] = new DrawingAttributes();
			PenAttr[4].Color = Color.FromArgb(145, 70, 160);
			PenAttr[4].Width = 500;
			PenAttr[4].Transparency = 200;

			PenAttr[5] = new DrawingAttributes();
			PenAttr[5].Color = Color.FromArgb(220, 95, 60);
			PenAttr[5].Width = 80;
			PenAttr[5].Transparency = 5;

			PenAttr[6] = new DrawingAttributes();
			PenAttr[6].Color = Color.FromArgb(30, 110, 200);
			PenAttr[6].Width = 80;
			PenAttr[6].Transparency = 5;

			PenAttr[7] = new DrawingAttributes();
			PenAttr[7].Color = Color.FromArgb(235, 180, 55);
			PenAttr[7].Width = 80;
			PenAttr[7].Transparency = 5;

			PenAttr[8] = new DrawingAttributes();
			PenAttr[8].Color = Color.FromArgb(120, 175, 70);
			PenAttr[8].Width = 80;
			PenAttr[8].Transparency = 5;

			PenAttr[9] = new DrawingAttributes();
			PenAttr[9].Color = Color.FromArgb(145, 70, 160);
			PenAttr[9].Width = 500;
			PenAttr[9].Transparency = 200;
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
								PenAttr[0].Color = Color.FromArgb(int.Parse(sPara), PenAttr[0].Color.G, PenAttr[0].Color.B);
							break;
						case "PEN1_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[0].Color = Color.FromArgb(PenAttr[0].Color.R, int.Parse(sPara), PenAttr[0].Color.B);
							break;
						case "PEN1_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[0].Color = Color.FromArgb(PenAttr[0].Color.R, PenAttr[0].Color.G, int.Parse(sPara));
							break;
						case "PEN2_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[1].Color = Color.FromArgb(int.Parse(sPara), PenAttr[1].Color.G, PenAttr[1].Color.B);
							break;
						case "PEN2_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[1].Color = Color.FromArgb(PenAttr[1].Color.R, int.Parse(sPara), PenAttr[1].Color.B);
							break;
						case "PEN2_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[1].Color = Color.FromArgb(PenAttr[1].Color.R, PenAttr[1].Color.G, int.Parse(sPara));
							break;
						case "PEN3_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[2].Color = Color.FromArgb(int.Parse(sPara), PenAttr[2].Color.G, PenAttr[2].Color.B);
							break;
						case "PEN3_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[2].Color = Color.FromArgb(PenAttr[2].Color.R, int.Parse(sPara), PenAttr[2].Color.B);
							break;
						case "PEN3_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[2].Color = Color.FromArgb(PenAttr[2].Color.R, PenAttr[2].Color.G, int.Parse(sPara));
							break;
						case "PEN4_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[3].Color = Color.FromArgb(int.Parse(sPara), PenAttr[3].Color.G, PenAttr[3].Color.B);
							break;
						case "PEN4_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[3].Color = Color.FromArgb(PenAttr[3].Color.R, int.Parse(sPara), PenAttr[3].Color.B);
							break;
						case "PEN4_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[3].Color = Color.FromArgb(PenAttr[3].Color.R, PenAttr[3].Color.G, int.Parse(sPara));
							break;
						case "PEN5_RED":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[4].Color = Color.FromArgb(int.Parse(sPara), PenAttr[4].Color.G, PenAttr[4].Color.B);
							break;
						case "PEN5_GREEN":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[4].Color = Color.FromArgb(PenAttr[4].Color.R, int.Parse(sPara), PenAttr[4].Color.B);
							break;
						case "PEN5_BLUE":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[4].Color = Color.FromArgb(PenAttr[4].Color.R, PenAttr[4].Color.G, int.Parse(sPara));
							break;

						case "PEN1_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[0].Transparency = (byte)(255 - o);
							break;
						case "PEN2_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[1].Transparency = (byte)(255 - o);
							break;
						case "PEN3_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[2].Transparency = (byte)(255 - o);
							break;
						case "PEN4_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[3].Transparency = (byte)(255 - o);
							break;
						case "PEN5_ALPHA":
							if (int.TryParse(sPara, out o) && o >= 0 && o <= 255)
								PenAttr[4].Transparency = (byte)(255 - o);
							break;

						case "PEN1_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								PenAttr[0].Width = o;
							break;
						case "PEN2_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								PenAttr[1].Width = o;
							break;
						case "PEN3_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								PenAttr[2].Width = o;
							break;
						case "PEN4_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								PenAttr[3].Width = o;
							break;
						case "PEN5_WIDTH":
							if (int.TryParse(sPara, out o) && o > 30 && o <= 3000)
								PenAttr[4].Width = o;
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

