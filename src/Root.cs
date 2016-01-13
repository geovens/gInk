using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace gInk
{
	public class Root
	{
		public FormCollection Form1;
		public FormDisplay Form2;

		public Root()
		{
		}

		public void Run()
		{
			Start();

			Form1 = new FormCollection(this);
			Application.Run(Form1);
		}

		public void Start()
		{
			ReadOptions();
			Form2 = new FormDisplay(this);
			Form2.Show();
		}
		public void Stop()
		{
			Form2.Close();
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
			IPAddress ip = new IPAddress(new byte[4]);
			int port = 0;
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
						case "T900ADDRESS":
							break;
					}
				}
			}
			fini.Close();
		}
	}
}

