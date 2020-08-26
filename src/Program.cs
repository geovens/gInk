using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace gInk
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using(Mutex mutex = new Mutex(false, "Global\\" + appGuid))
   			{
      				if(!mutex.WaitOne(0, false))
      				{
         				return;
				}
				
				Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				new Root();
				Application.Run();
			}
		}

		private static string appGuid = "86280230-c3d2-4da0-8621-e2a2466fc136";
		
		private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
		{
			DialogResult result = DialogResult.Cancel;
			try
			{
				Exception ex = (Exception)t.Exception;

				string errorMsg = "UIThreadException\r\n\r\n";
				errorMsg += "Oops, gInk crashed! Please include the following information if you plan to contact the developers (a copy of the following information is stored in crash.txt in the application folder):\r\n\r\n";
				errorMsg += ex.Message + "\r\n\r\n";
				errorMsg += "Stack Trace:\r\n" + ex.StackTrace + "\r\n\r\n";
				WriteErrorLog(errorMsg);

				errorMsg += "!!! PLEASE PRESS ESC KEY TO EXIT IF YOU FEEL YOUR MOUSE CLICK IS BLOCKED BY SOMETHING";
				ShowErrorDialog("UIThreadException", errorMsg);
			}
			catch
			{
				try
				{
					MessageBox.Show("Fatal Windows Forms Error", "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
				}
				finally
				{
					Application.Exit();
				}
			}

			// Exits the program when the user clicks Abort.
			if (result == DialogResult.Abort)
				Application.Exit();
		}

		private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;

				string errorMsg = "UnhandledException\r\n\r\n";
				errorMsg += "Oops, gInk crashed! Please include the following information if you plan to contact the developers:\r\n\r\n";
				errorMsg += ex.Message + "\r\n\r\n";
				errorMsg += "Stack Trace:\r\n" + ex.StackTrace + "\r\n\r\n";
				WriteErrorLog(errorMsg);

				ShowErrorDialog("UnhandledException", errorMsg);

				if (!EventLog.SourceExists("UnhandledException"))
				{
					EventLog.CreateEventSource("UnhandledException", "Application");
				}
				EventLog myLog = new EventLog();
				myLog.Source = "UnhandledException";
				myLog.WriteEntry(errorMsg);
			}
			catch (Exception exc)
			{
				try
				{
					MessageBox.Show("Fatal Non-UI Error", "Fatal Non-UI Error. Could not write the error to the event log. Reason: " + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				finally
				{
					Application.Exit();
				}
			}
		}

		private static DialogResult ShowErrorDialog(string title, string errormsg)
		{
			return MessageBox.Show(errormsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
		}

		private static void WriteErrorLog(string errormsg)
		{
			try
			{
				FileStream fs = new FileStream("crash.txt", FileMode.Create);
				StreamWriter sw = new StreamWriter(fs);
				sw.Write(errormsg);
				sw.Close();
				fs.Close();
			}
			catch
			{
				FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "crash.txt", FileMode.Create);
				StreamWriter sw = new StreamWriter(fs);
				sw.Write(errormsg);
				sw.Close();
				fs.Close();
			}
		}
	}
}
