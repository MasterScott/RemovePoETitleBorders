using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace RemovePoETitleBorders
{
    class Program
    {
        //Sets window attributes
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Gets window attributes
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //assorted constants needed
        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000; //child window
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;


        static void Main(string[] args)
        {
            Process[] Procs = Process.GetProcesses();
            foreach (Process proc in Procs)
            {
                if (proc.ProcessName.StartsWith("PathOfExile"))
                {
                    IntPtr pFoundWindow = proc.MainWindowHandle;
                    int style = GetWindowLong(pFoundWindow, GWL_STYLE);
                    SetWindowLong(pFoundWindow, GWL_STYLE, (style & ~WS_CAPTION));

                    ShowWindow(proc.MainWindowHandle, SW_MINIMIZE);
                    ShowWindow(proc.MainWindowHandle, SW_MAXIMIZE);
                }
            }
        }
    }
}
