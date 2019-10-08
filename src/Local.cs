using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace gInk
{
	public class Local
	{
		Dictionary<string, string> Languages = new Dictionary<string, string>();

		public string CurrentLanguageFile;

		public string[] ButtonNamePen = new string[10];

		public string ButtonNamePenwidth = "Pen width";
		public string ButtonNameErasor = "Eraser";
		public string ButtonNamePan = "Pan";
		public string ButtonNameMousePointer = "Mouse pointer";
		public string ButtonNameInkVisible = "Ink visible";
		public string ButtonNameSnapshot = "Snapshot";
		public string ButtonNameUndo = "Undo";
		public string ButtonNameRedo = "Redo";
		public string ButtonNameClear = "Clear";
		public string ButtonNameExit = "Exit drawing";
		public string ButtonNameDock = "Dock";

		public string MenuEntryExit = "Exit";
		public string MenuEntryOptions = "Options";
		public string MenuEntryAbout = "About";

		public string OptionsTabGeneral = "General";
		public string OptionsTabPens = "Pens";
		public string OptionsTabHotkeys = "Hotkeys";

		public string OptionsGeneralCanvascursor = "Canvus cursor";
		public string OptionsGeneralCanvascursorArrow = "Arrow";
		public string OptionsGeneralCanvascursorPentip = "Pen tip";
		public string OptionsGeneralSnapshotsavepath = "Snapshot save path";
		public string OptionsGeneralWhitetrayicon = "Use white tray icon";
		public string OptionsGeneralAllowdragging = "Allow dragging toolbar";
		public string OptionsGeneralNotePenwidth = "Note: pen width panel overides each individual pen width settings";

		public string OptionsPensShow = "Show";
		public string OptionsPensColor = "Color";
		public string OptionsPensAlpha = "Alpha";
		public string OptionsPensWidth = "Width";
		public string OptionsPensPencil = "Pencil";
		public string OptionsPensHighlighter = "Highlighter";
		public string OptionsPensThin = "Thin";
		public string OptionsPensNormal = "Normal";
		public string OptionsPensThick = "Thick";

		public string OptionsHotkeysglobal = "Global hotkey (start drawing, switch between mouse pointer and drawing)";
		public string OptionsHotkeysEnableinpointer = "Enable all following hotkeys in mouse pointer mode (may cause a mess)";

		public string NotificationSnapshot = "Snapshot saved. Click here to browse snapshots.";

		public Local()
		{
			ButtonNamePen[0] = "Pen 0";
			ButtonNamePen[1] = "Pen 1";
			ButtonNamePen[2] = "Pen 2";
			ButtonNamePen[3] = "Pen 3";
			ButtonNamePen[4] = "Pen 4";
			ButtonNamePen[5] = "Pen 5";
			ButtonNamePen[6] = "Pen 6";
			ButtonNamePen[7] = "Pen 7";
			ButtonNamePen[8] = "Pen 8";
			ButtonNamePen[9] = "Pen 9";

			LoadLocalList();
		}

		public void LoadLocalList()
		{
			DirectoryInfo d = new DirectoryInfo("./lang/");
			FileInfo[] Files = d.GetFiles("*.txt");
			foreach (FileInfo file in Files)
			{
				string filename = file.Name;

				FileStream fini = new FileStream("./lang/" + filename, FileMode.Open);
				StreamReader srini = new StreamReader(fini);
				string sLine;
				do
				{
					sLine = srini.ReadLine();
				}
				while (!sLine.StartsWith("LanguageName"));
				string sPara = sLine.Substring(sLine.IndexOf("=") + 1);
				sPara = sPara.Trim();
				sPara = sPara.Trim('\"');
				string languagename = sPara;

				Languages.Add(filename.Substring(0, filename.Length - 4), sPara);

				fini.Close();
			}
		}

		public List<string> GetLanguagenames()
		{
			List<string> names = new List<string>();
			foreach (KeyValuePair<string, string> pair in Languages)
				names.Add(pair.Value);

			return names;
		}

		public string GetFilenameByLanguagename(string languagename)
		{
			foreach (KeyValuePair<string, string> pair in Languages)
				if (pair.Value == languagename)
					return pair.Key;

			return "";
		}

		public string GetLanguagenameByFilename(string filename)
		{
			foreach (KeyValuePair<string, string> pair in Languages)
				if (pair.Key == filename)
					return pair.Value;

			return "";
		}

		public void LoadLocalFile(string loname)
		{
			string filename = "./lang/" + loname + ".txt";

			if (!File.Exists(filename))
			{
				return;
			}

			FileStream fini = new FileStream(filename, FileMode.Open);
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
					sPara = sLine.Substring(sLine.IndexOf("=") + 1);
					sPara = sPara.Trim();
					sPara = sPara.Trim('\"');

					if (sName.StartsWith("ButtonNamePen"))
					{
						int penid = 0;
						if (int.TryParse(sName.Substring(13, 1), out penid))
						{
							ButtonNamePen[penid] = sPara;
						}
					}

					System.Reflection.FieldInfo fi = typeof(Local).GetField(sName);
					if (fi != null)
						fi.SetValue(this, sPara);
				}
			}
			fini.Close();

			CurrentLanguageFile = loname;
		}
	}
}
