using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exusiai
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ComputerData.Get();
            HostConfiguration config = new HostConfiguration();
            config.RewriteLocalhost = true;
            var host = new NancyHost(config,new Uri("http://localhost:5150"));
            host.Start();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
