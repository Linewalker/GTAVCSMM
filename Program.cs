using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GTAVCSMM
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "GTA5TrainerCS", out createdNew))

            if (createdNew)
            {
                   Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Overlay());
                    
             }
        }

    }
}
