using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Devgis.MapApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DateTime dtStart = new DateTime(2014, 4,3);
            DateTime dtNown = DateTime.Now;
            TimeSpan tsTime = dtNown - dtStart;
            if (tsTime.Days > 60)
            {
                System.Diagnostics.Process.Start("http://flysoft.taobao.com/");
                throw new Exception("产品已经过期!");
            }
            Application.Run(new MainMap());
        }
    }
}