using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool newInstance;
            string guid = ((GuidAttribute)Assembly.GetExecutingAssembly().
            GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();
            string mutexID = String.Format("Global\\{{{0}}}", guid);
            using (Mutex mutex = new Mutex(true, mutexID, out newInstance))
            {
                if (newInstance)
                {
                    Properties.Settings.Default.IsWindows10 = Utility.IsWin10();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    DirectoryInfo tempDir = new DirectoryInfo(Path.GetTempPath() + "FileShare\\");

                    if (tempDir.Exists)
                    {
                        try
                        {
                            tempDir.Clear();
                        }
                        catch (IOException) { }
                    }

                    Application.Run(new FileForm());
                }
                else
                    MessageBox.Show("Er is al een instantie van FileShare actief op deze computer.", "FileShare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    mutex.ReleaseMutex();
                }
                catch (ApplicationException) { }
                catch (ObjectDisposedException) { }
            }
        }
    }
}
