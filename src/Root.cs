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
		public const int MaxPenCount = 10;

		// options
		public bool[] PenEnabled = new bool[MaxPenCount];
		public bool EraserEnabled = true;
		public bool PointerEnabled = true;
		public bool PenWidthEnabled = false;
		public bool SnapEnabled = true;
		public bool CloseOnSnap = true;
		public bool UndoEnabled = true;
		public bool ClearEnabled = true;
		public bool PanEnabled = true;
		public DrawingAttributes[] PenAttr = new DrawingAttributes[MaxPenCount];
		public bool Hotkey_Control, Hotkey_Alt, Hotkey_Shift, Hotkey_Win;
		public int Hotkey = 0;
		public bool AutoScroll;
		public bool WhiteTrayIcon;
		public string SnapshotBasePath;
		public int CanvasCursor = 0;

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
		public bool UponSubPanelUpdate = false;

		public bool PanMode = false;

		public Ink[] UndoStrokes;
		//public Ink UponUndoStrokes;
		public int UndoP;
		public int UndoDepth, RedoDepth;

		public NotifyIcon trayIcon;
		public ContextMenu trayMenu;
		public FormCollection FormCollection;
		public FormDisplay FormDisplay;
		public FormButtonHitter FormButtonHitter;
		public FormOptions FormOptions;

		public int CurrentPen = 1;  // defaut pen
		public int GlobalPenWidth = 80;
		public bool gpPenWidthVisible = false;

		public Root()
		{
			SetDefaultPens();
			SetDefaultConfig();
			ReadOptions("pens.ini");
			ReadOptions("config.ini");

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("About...", OnAbout);
			trayMenu.MenuItems.Add("Options...", OnOptions);
			trayMenu.MenuItems.Add("-");
			trayMenu.MenuItems.Add("Exit", OnExit);

			Size size = SystemInformation.SmallIconSize;
			trayIcon = new NotifyIcon();
			trayIcon.Text = "gInk";
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			trayIcon.MouseClick += TrayIcon_Click;
			trayIcon.BalloonTipText = "Snapshot saved. Click here to browse snapshots.";
			trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
			SetTrayIconColor();

			SetHotkey();

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
			if (CurrentPen < 0)
				CurrentPen = 0;
			if (!PenEnabled[CurrentPen])
			{
				CurrentPen = 0;
				while (CurrentPen < MaxPenCount && !PenEnabled[CurrentPen])
					CurrentPen++;
				if (CurrentPen == MaxPenCount)
					CurrentPen = -2;
			}
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

		public void Pan(int x, int y)
		{
			FormCollection.IC.Ink.Strokes.Move(x, y);

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
			gpPenWidthVisible = false;
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

			FormButtonHitter.Hide();
		}

		public void SelectPen(int pen)
		{
			FormCollection.SelectPen(pen);
		}

		public void SetDefaultPens()
		{
			PenEnabled[0] = true;
			PenAttr[0] = new DrawingAttributes();
			PenAttr[0].Color = Color.FromArgb(220, 95, 60);
			PenAttr[0].Width = 80;
			PenAttr[0].Transparency = 5;

			PenEnabled[1] = true;
			PenAttr[1] = new DrawingAttributes();
			PenAttr[1].Color = Color.FromArgb(30, 110, 200);
			PenAttr[1].Width = 80;
			PenAttr[1].Transparency = 5;

			PenEnabled[2] = true;
			PenAttr[2] = new DrawingAttributes();
			PenAttr[2].Color = Color.FromArgb(235, 180, 55);
			PenAttr[2].Width = 80;
			PenAttr[2].Transparency = 5;

			PenEnabled[3] = true;
			PenAttr[3] = new DrawingAttributes();
			PenAttr[3].Color = Color.FromArgb(120, 175, 70);
			PenAttr[3].Width = 80;
			PenAttr[3].Transparency = 5;

			PenEnabled[4] = true;
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

		public void SetTrayIconColor()
		{
			if (WhiteTrayIcon)
			{
				if (File.Exists("icon_white.ico"))
					trayIcon.Icon = new Icon("icon_white.ico");
				else
					trayIcon.Icon = global::gInk.Properties.Resources.icon_white;
			}
			else
			{
				if (File.Exists("icon_red.ico"))
					trayIcon.Icon = new Icon("icon_red.ico");
				else
					trayIcon.Icon = global::gInk.Properties.Resources.icon_red;
			}


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

					if (sName.StartsWith("PEN"))
					{
						int penid = 0;
						if (int.TryParse(sName.Substring(3, 1), out penid))
						{
							if (sName.EndsWith("_ENABLED"))
							{
								if (sPara.ToUpper() == "TRUE" || sPara == "1" || sPara.ToUpper() == "ON")
									PenEnabled[penid] = true;
								else if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
									PenEnabled[penid] = false;
							}

							int penc = 0;
							if (int.TryParse(sPara, out penc))
							{
								if (sName.EndsWith("_RED") && penc >= 0 && penc <= 255)
								{
									PenAttr[penid].Color = Color.FromArgb(penc, PenAttr[penid].Color.G, PenAttr[penid].Color.B);
								}
								else if (sName.EndsWith("_GREEN") && penc >= 0 && penc <= 255)
								{
									PenAttr[penid].Color = Color.FromArgb(PenAttr[penid].Color.R, penc, PenAttr[penid].Color.B);
								}
								else if (sName.EndsWith("_BLUE") && penc >= 0 && penc <= 255)
								{
									PenAttr[penid].Color = Color.FromArgb(PenAttr[penid].Color.R, PenAttr[penid].Color.G, penc);
								}
								else if (sName.EndsWith("_ALPHA") && penc >= 0 && penc <= 255)
								{
									PenAttr[penid].Transparency = (byte)(255 - penc);
								}
								else if (sName.EndsWith("_WIDTH") && penc >= 30 && penc <= 3000)
								{
									PenAttr[penid].Width = penc;
								}
							}
						}

					}

					switch (sName)
					{
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
							if (Hotkey_Control || Hotkey_Alt || Hotkey_Shift || Hotkey_Win)
								Hotkey = sPara.Substring(sPara.Length - 1).ToCharArray()[0];
							else
								Hotkey = 0;
							break;
						case "WHITE_TRAY_ICON":
							if (sPara.ToUpper() == "TRUE" || sPara == "1" || sPara.ToUpper() == "ON")
								WhiteTrayIcon = true;
							else
								WhiteTrayIcon = false;
							break;
						case "SNAPSHOT_PATH":
							SnapshotBasePath = sPara;
							if (!SnapshotBasePath.EndsWith("/") && !SnapshotBasePath.EndsWith("\\"))
								SnapshotBasePath += "/";
							break;
						case "ERASER_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								EraserEnabled = false;
							break;
						case "POINTER_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								PointerEnabled = false;
							break;
						case "PEN_WIDTH_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								PenWidthEnabled = false;
							else if (sPara.ToUpper() == "TRUE" || sPara == "1" || sPara.ToUpper() == "ON")
								PenWidthEnabled = true;
							break;
						case "SNAPSHOT_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								SnapEnabled = false;
							break;
						case "CLOSE_ON_SNAP":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								CloseOnSnap = false;
							break;
						case "UNDO_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								UndoEnabled = false;
							break;
						case "CLEAR_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								ClearEnabled = false;
							break;
						case "PAN_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								PanEnabled = false;
							break;
						case "CANVAS_CURSOR":
							if (sPara == "0")
								CanvasCursor = 0;
							else if (sPara == "1")
								CanvasCursor = 1;
							break;
					}
				}
			}
			fini.Close();
		}

		public void SaveOptions(string file)
		{
			if (!File.Exists(file))
			{
				return;
			}

			FileStream fini = new FileStream(file, FileMode.Open);
			StreamReader srini = new StreamReader(fini);
			string sLine = "";
			string sNameO = "";
			string sName = "", sPara = "";

			List<string> writelines = new List<string>();

			while (sLine != null)
			{
				sPara = "";
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
					sNameO = sLine.Substring(0, sLine.IndexOf("="));
					sName = sNameO.Trim().ToUpper();

					if (sName.StartsWith("PEN"))
					{
						int penid = 0;
						if (int.TryParse(sName.Substring(3, 1), out penid) && penid >= 0 && penid < MaxPenCount)
						{
							if (sName.EndsWith("_ENABLED"))
							{
								if (PenEnabled[penid])
									sPara = "True";
								else
									sPara = "False";
							}
							else if (sName.EndsWith("_RED"))
							{
								sPara = PenAttr[penid].Color.R.ToString();
							}
							else if (sName.EndsWith("_GREEN"))
							{
								sPara = PenAttr[penid].Color.G.ToString();
							}
							else if (sName.EndsWith("_BLUE"))
							{
								sPara = PenAttr[penid].Color.B.ToString();
							}
							else if (sName.EndsWith("_ALPHA"))
							{
								sPara = (255 - PenAttr[penid].Transparency).ToString();
							}
							else if (sName.EndsWith("_WIDTH"))
							{
								sPara = ((int)PenAttr[penid].Width).ToString();
							}
						}

					}

					switch (sName)
					{
						case "HOTKEY":
							/*
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
							*/
							if (Hotkey_Control)
								sPara += "Control + ";
							if (Hotkey_Alt)
								sPara += "Alt + ";
							if (Hotkey_Shift)
								sPara += "Shift + ";
							if (Hotkey_Win)
								sPara += "Win + ";
							if (sPara == "" || Hotkey == 0)
								sPara = "None";
							else
								sPara += (char)Hotkey;

							break;
						case "WHITE_TRAY_ICON":
							if (WhiteTrayIcon)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "SNAPSHOT_PATH":
							sPara = SnapshotBasePath;
							break;
						case "ERASER_ICON":
							if (EraserEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "POINTER_ICON":
							if (PointerEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "PEN_WIDTH_ICON":
							if (PenWidthEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "SNAPSHOT_ICON":
							if (SnapEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "CLOSE_ON_SNAP":
							if (CloseOnSnap)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "UNDO_ICON":
							if (UndoEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "CLEAR_ICON":
							if (ClearEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "PAN_ICON":
							if (PanEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "CANVAS_CURSOR":
							sPara = CanvasCursor.ToString();
							break;
					}
				}
				if (sPara != "")
					writelines.Add(sNameO + "= " + sPara);
				else if (sLine != null)
					writelines.Add(sLine);
			}
			fini.Close();

			FileStream frini = new FileStream(file, FileMode.Create);
			StreamWriter swini = new StreamWriter(frini);
			swini.AutoFlush = true;
			foreach (string line in writelines)
				swini.WriteLine(line);
			frini.Close();
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
			ReadOptions("pens.ini");
			ReadOptions("config.ini");
			FormOptions = new FormOptions(this);
			FormOptions.Show();
		}

		public void SetHotkey()
		{
			int modifier = 0;
			if (Hotkey_Control) modifier |= 0x2;
			if (Hotkey_Alt) modifier |= 0x1;
			if (Hotkey_Shift) modifier |= 0x4;
			if (Hotkey_Win) modifier |= 0x8;
			if (modifier != 0)
				RegisterHotKey(IntPtr.Zero, 0, modifier, Hotkey);
		}

		public void UnsetHotkey()
		{
			int modifier = 0;
			if (Hotkey_Control) modifier |= 0x2;
			if (Hotkey_Alt) modifier |= 0x1;
			if (Hotkey_Shift) modifier |= 0x4;
			if (Hotkey_Win) modifier |= 0x8;
			if (modifier != 0)
				UnregisterHotKey(IntPtr.Zero, 0);
		}

		private void OnExit(object sender, EventArgs e)
		{
			UnsetHotkey();

			trayIcon.Dispose();
			Application.Exit();
		}

		[DllImport("user32.dll")]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);
	}
}

