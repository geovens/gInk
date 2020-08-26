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
				else if (Root.PointerMode)
				{
					//Root.UnPointer();
					Root.SelectPen(Root.LastPen);
				}
				else
				{
					//Root.Pointer();
					Root.SelectPen(-2);
				}
			}
			return false;
		}
	}

	public class Root
	{
		public Local Local = new Local();
		public const int MaxPenCount = 10;

		// options
		public bool[] PenEnabled = new bool[MaxPenCount];
		public bool EraserEnabled = true;
		public bool PointerEnabled = true;
		public bool PenWidthEnabled = false;
		public bool SnapEnabled = true;
		public bool UndoEnabled = true;
		public bool ClearEnabled = true;
		public bool PanEnabled = true;
		public bool InkVisibleEnabled = true;
		public DrawingAttributes[] PenAttr = new DrawingAttributes[MaxPenCount];
		public bool AutoScroll;
		public bool WhiteTrayIcon;
		public string SnapshotBasePath;
		public int CanvasCursor = 0;
		public bool AllowDraggingToolbar = true;
		public bool AllowHotkeyInPointerMode = true;
		public int gpButtonsLeft, gpButtonsTop;

		// advanced options
		public string CloseOnSnap = "blankonly";
		public bool AlwaysHideToolbar = false;
		public float ToolbarHeight = 0.06f;

		// hotkey options
		public Hotkey Hotkey_Global = new Hotkey();
		public Hotkey[] Hotkey_Pens = new Hotkey[10];
		public Hotkey Hotkey_Eraser = new Hotkey();
		public Hotkey Hotkey_InkVisible = new Hotkey();
		public Hotkey Hotkey_Pointer = new Hotkey();
		public Hotkey Hotkey_Pan = new Hotkey();
		public Hotkey Hotkey_Undo = new Hotkey();
		public Hotkey Hotkey_Redo = new Hotkey();
		public Hotkey Hotkey_Snap = new Hotkey();
		public Hotkey Hotkey_Clear = new Hotkey();

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
		public bool UponAllDrawingUpdate = false;
		public bool MouseMovedUnderSnapshotDragging = false; // used to pause re-drawing when mouse is not moving during dragging to take a screenshot

		public bool PanMode = false;
		public bool InkVisible = true;

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
		public int LastPen = 1;
		public int GlobalPenWidth = 80;
		public bool gpPenWidthVisible = false;
		public string SnapshotFileFullPath = ""; // used to record the last snapshot file name, to select it when the balloon is clicked

		public Root()
		{
			for (int p = 0; p < MaxPenCount; p++)
				Hotkey_Pens[p] = new Hotkey();

			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add(Local.MenuEntryAbout + "...", OnAbout);
			trayMenu.MenuItems.Add(Local.MenuEntryOptions + "...", OnOptions);
			trayMenu.MenuItems.Add("-");
			trayMenu.MenuItems.Add(Local.MenuEntryExit, OnExit);

			SetDefaultPens();
			SetDefaultConfig();
			ReadOptions("pens.ini");
			ReadOptions("config.ini");
			ReadOptions("hotkeys.ini");			

			Size size = SystemInformation.SmallIconSize;
			trayIcon = new NotifyIcon();
			trayIcon.Text = "gInk";
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			trayIcon.MouseClick += TrayIcon_Click;
			trayIcon.BalloonTipText = Local.NotificationSnapshot;
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
			//string snapbasepath = SnapshotBasePath;
			//snapbasepath = Environment.ExpandEnvironmentVariables(snapbasepath);
			//System.Diagnostics.Process.Start(snapbasepath);
			string fullpath = System.IO.Path.GetFullPath(SnapshotFileFullPath);
			System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", fullpath));
		}

		private void TrayIcon_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (FormDisplay == null && FormCollection == null)
				{
					ReadOptions("pens.ini");
					ReadOptions("config.ini");
					ReadOptions("hotkeys.ini");
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
			SetInkVisible(true);
			FormCollection.ButtonsEntering = 1;
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
			if (x == 0 && y == 0)
				return;

			FormCollection.IC.Ink.Strokes.Move(x, y);

			FormDisplay.ClearCanvus();
			FormDisplay.DrawStrokes();
			FormDisplay.DrawButtons(true);
			FormDisplay.UpdateFormDisplay(true);
		}

		public void SetInkVisible(bool visible)
		{
			InkVisible = visible;
			if (visible)
				FormCollection.btInkVisible.Image = FormCollection.image_visible;
			else
				FormCollection.btInkVisible.Image = FormCollection.image_visible_not;

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
			FormCollection.ButtonsEntering = -1;
			UponButtonsUpdate |= 0x2;
		}

		public void UnDock()
		{
			if (FormDisplay == null || FormCollection == null)
				return;

			Docked = false;
			FormCollection.btDock.Image = FormCollection.image_dock;
			FormCollection.ButtonsEntering = 1;
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
			FormCollection.Activate();

			FormButtonHitter.Hide();
		}

		public void SelectPen(int pen)
		{
			FormCollection.SelectPen(pen);
		}

		public void SetDefaultPens()
		{
			PenEnabled[0] = false;
			PenAttr[0] = new DrawingAttributes();
			PenAttr[0].Color = Color.FromArgb(80, 80, 80);
			PenAttr[0].Width = 80;
			PenAttr[0].Transparency = 0;

			PenEnabled[1] = true;
			PenAttr[1] = new DrawingAttributes();
			PenAttr[1].Color = Color.FromArgb(225, 60, 60);
			PenAttr[1].Width = 80;
			PenAttr[1].Transparency = 0;

			PenEnabled[2] = true;
			PenAttr[2] = new DrawingAttributes();
			PenAttr[2].Color = Color.FromArgb(30, 110, 200);
			PenAttr[2].Width = 80;
			PenAttr[2].Transparency = 0;

			PenEnabled[3] = true;
			PenAttr[3] = new DrawingAttributes();
			PenAttr[3].Color = Color.FromArgb(235, 180, 55);
			PenAttr[3].Width = 80;
			PenAttr[3].Transparency = 0;

			PenEnabled[4] = true;
			PenAttr[4] = new DrawingAttributes();
			PenAttr[4].Color = Color.FromArgb(120, 175, 70);
			PenAttr[4].Width = 80;
			PenAttr[4].Transparency = 0;

			PenEnabled[5] = true;
			PenAttr[5] = new DrawingAttributes();
			PenAttr[5].Color = Color.FromArgb(235, 125, 15);
			PenAttr[5].Width = 500;
			PenAttr[5].Transparency = 175;

			PenAttr[6] = new DrawingAttributes();
			PenAttr[6].Color = Color.FromArgb(230, 230, 230);
			PenAttr[6].Width = 80;
			PenAttr[6].Transparency = 0;

			PenAttr[7] = new DrawingAttributes();
			PenAttr[7].Color = Color.FromArgb(250, 140, 200);
			PenAttr[7].Width = 80;
			PenAttr[7].Transparency = 0;

			PenAttr[8] = new DrawingAttributes();
			PenAttr[8].Color = Color.FromArgb(25, 180, 175);
			PenAttr[8].Width = 80;
			PenAttr[8].Transparency = 0;

			PenAttr[9] = new DrawingAttributes();
			PenAttr[9].Color = Color.FromArgb(145, 70, 160);
			PenAttr[9].Width = 500;
			PenAttr[9].Transparency = 175;
		}

		public void SetDefaultConfig()
		{
			Hotkey_Global.Control = true;
			Hotkey_Global.Alt = true;
			Hotkey_Global.Shift = false;
			Hotkey_Global.Win = false;
			Hotkey_Global.Key = 'G';

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
			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			if (!File.Exists(file))
				file = AppDomain.CurrentDomain.BaseDirectory + file;
			if (!File.Exists(file))
				return;


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

							if (sName.EndsWith("_HOTKEY"))
							{
								Hotkey_Pens[penid].Parse(sPara);
							}
						}

					}

					int tempi = 0;
					float tempf = 0;
					switch (sName)
					{
						case "LANGUAGE_FILE":
							ChangeLanguage(sPara);
							break;
						case "HOTKEY_GLOBAL":
							Hotkey_Global.Parse(sPara);
							break;
						case "HOTKEY_ERASER":
							Hotkey_Eraser.Parse(sPara);
							break;
						case "HOTKEY_INKVISIBLE":
							Hotkey_InkVisible.Parse(sPara);
							break;
						case "HOTKEY_POINTER":
							Hotkey_Pointer.Parse(sPara);
							break;
						case "HOTKEY_PAN":
							Hotkey_Pan.Parse(sPara);
							break;
						case "HOTKEY_UNDO":
							Hotkey_Undo.Parse(sPara);
							break;
						case "HOTKEY_REDO":
							Hotkey_Redo.Parse(sPara);
							break;
						case "HOTKEY_SNAPSHOT":
							Hotkey_Snap.Parse(sPara);
							break;
						case "HOTKEY_CLEAR":
							Hotkey_Clear.Parse(sPara);
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
								CloseOnSnap = "false";
							else if (sPara.ToUpper() == "TRUE" || sPara == "1" || sPara.ToUpper() == "ON")
								CloseOnSnap = "true";
							else if (sPara.ToUpper() == "BLANKONLY")
								CloseOnSnap = "blankonly";
							break;
						case "ALWAYS_HIDE_TOOLBAR":
							if (sPara.ToUpper() == "TRUE" || sPara == "1" || sPara.ToUpper() == "ON")
								AlwaysHideToolbar = true;
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
						case "INKVISIBLE_ICON":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								InkVisibleEnabled = false;
							break;
						case "ALLOW_DRAGGING_TOOLBAR":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								AllowDraggingToolbar = false;
							break;
						case "ALLOW_HOTKEY_IN_POINTER_MODE":
							if (sPara.ToUpper() == "FALSE" || sPara == "0" || sPara.ToUpper() == "OFF")
								AllowHotkeyInPointerMode = false;
							break;
						case "TOOLBAR_LEFT":
							if (int.TryParse(sPara, out tempi))
								gpButtonsLeft = tempi;
							break;
						case "TOOLBAR_TOP":
							if (int.TryParse(sPara, out tempi))
								gpButtonsTop = tempi;
							break;
						case "TOOLBAR_HEIGHT":
							if (float.TryParse(sPara, out tempf))
								ToolbarHeight = tempf;
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
				file = AppDomain.CurrentDomain.BaseDirectory + file;
			if (!File.Exists(file))
				return;

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
							else if (sName.EndsWith("_HOTKEY"))
							{
								sPara = Hotkey_Pens[penid].ToString();
							}
						}

					}

					switch (sName)
					{
						case "LANGUAGE_FILE":
							sPara = Local.CurrentLanguageFile;
							break;
						case "HOTKEY_GLOBAL":
							sPara = Hotkey_Global.ToString();
							break;
						case "HOTKEY_ERASER":
							sPara = Hotkey_Eraser.ToString();
							break;
						case "HOTKEY_INKVISIBLE":
							sPara = Hotkey_InkVisible.ToString();
							break;
						case "HOTKEY_POINTER":
							sPara = Hotkey_Pointer.ToString();
							break;
						case "HOTKEY_PAN":
							sPara = Hotkey_Pan.ToString();
							break;
						case "HOTKEY_UNDO":
							sPara = Hotkey_Undo.ToString();
							break;
						case "HOTKEY_REDO":
							sPara = Hotkey_Redo.ToString();
							break;
						case "HOTKEY_SNAPSHOT":
							sPara = Hotkey_Snap.ToString();
							break;
						case "HOTKEY_CLEAR":
							sPara = Hotkey_Clear.ToString();
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
							if (CloseOnSnap == "true")
								sPara = "True";
							else if (CloseOnSnap == "false")
								sPara = "False";
							else
								sPara = "BlankOnly";
							break;
						case "ALWAYS_HIDE_TOOLBAR":
							if (AlwaysHideToolbar)
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
						case "INKVISIBLE_ICON":
							if (PanEnabled)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "ALLOW_DRAGGING_TOOLBAR":
							if (AllowDraggingToolbar)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "ALLOW_HOTKEY_IN_POINTER_MODE":
							if (AllowHotkeyInPointerMode)
								sPara = "True";
							else
								sPara = "False";
							break;
						case "TOOLBAR_LEFT":
							sPara = gpButtonsLeft.ToString();
							break;
						case "TOOLBAR_TOP":
							sPara = gpButtonsTop.ToString();
							break;
						case "TOOLBAR_HEIGHT":
							sPara = ToolbarHeight.ToString();
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
		/*
		private void OnPenSetting(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("notepad.exe", "pens.ini");
		}
		*/
		private void OnOptions(object sender, EventArgs e)
		{
			if (FormOptions != null)
				return;
			if (FormDisplay != null || FormCollection != null)
				return;

			ReadOptions("pens.ini");
			ReadOptions("config.ini");
			ReadOptions("hotkeys.ini");
			FormOptions = new FormOptions(this);
			FormOptions.Show();
		}

		public void SetHotkey()
		{
			int modifier = 0;
			if (Hotkey_Global.Control) modifier |= 0x2;
			if (Hotkey_Global.Alt) modifier |= 0x1;
			if (Hotkey_Global.Shift) modifier |= 0x4;
			if (Hotkey_Global.Win) modifier |= 0x8;
			if (modifier != 0)
				RegisterHotKey(IntPtr.Zero, 0, modifier, Hotkey_Global.Key);
		}

		public void UnsetHotkey()
		{
			int modifier = 0;
			if (Hotkey_Global.Control) modifier |= 0x2;
			if (Hotkey_Global.Alt) modifier |= 0x1;
			if (Hotkey_Global.Shift) modifier |= 0x4;
			if (Hotkey_Global.Win) modifier |= 0x8;
			if (modifier != 0)
				UnregisterHotKey(IntPtr.Zero, 0);
		}

		public void ChangeLanguage(string filename)
		{
			Local.LoadLocalFile(filename);

			trayMenu.MenuItems.Clear();
			trayMenu.MenuItems.Add(Local.MenuEntryAbout + "...", OnAbout);
			trayMenu.MenuItems.Add(Local.MenuEntryOptions + "...", OnOptions);
			trayMenu.MenuItems.Add("-");
			trayMenu.MenuItems.Add(Local.MenuEntryExit, OnExit);
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

