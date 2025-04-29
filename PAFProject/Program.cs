using System;
using System.Threading;
using System.Windows.Forms;

namespace PAFProject
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "PAFProject_SingleInstance");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Main());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Application is already running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
