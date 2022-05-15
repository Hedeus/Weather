using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Weather
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args) => 
            Host.CreateDefaultBuilder(Args)
                .UseContentRoot(App.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) => cfg
                    .SetBasePath(App.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                .ConfigureServices(App.ConfigureServices);
        //{ 
            //var host_builder = Host.CreateDefaultBuilder(Args);
            //host_builder.UseContentRoot(App.CurrentDirectory);
            //host_builder.ConfigureAppConfiguration((host, cfg) =>
            //{
            //    cfg.SetBasePath(App.CurrentDirectory);
            //    cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //});
            //return host_builder;
        //}
    }
}
