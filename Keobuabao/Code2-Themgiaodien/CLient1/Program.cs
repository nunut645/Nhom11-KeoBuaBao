using System;
using System.Windows.Forms;

namespace Client1   // 👈 phải trùng với namespace của Form1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());  // chạy form chính
        }
    }
}
