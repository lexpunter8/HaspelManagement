using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Hosting;

namespace AllinqManagementApi
{
    public class Program
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static async Task Main(string[] args)
        {

            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = CreateWebHostBuilder(args);

            if (isService)
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                string pathToContentRoot = Path.GetDirectoryName(pathToExe);

                builder.UseContentRoot(pathToContentRoot);
            }
            else
            {
                builder.UseContentRoot(AppContext.BaseDirectory);
            }

            IWebHost host = builder.Build();

            _log.Debug("test");
            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseUrls("http://0.0.0.0:5003")
            .UseStartup<Startup>();
    }
}
