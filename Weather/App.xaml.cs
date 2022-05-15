using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Weather.ViewModels;

namespace Weather
{

    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost __Host;
        public static IHost Host  // => IHost host = __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        {
            get
            {                
                if (__Host is null)
                    __Host = Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
                return __Host;
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //throw new NotImplementedException();
            //services.AddSingleton<DataService>();
            //services.AddSingleton<CountriesStatisticViewModel>();
            services.AddSingleton<WelcomeWindowModel>();

            //App.Host.Services.GetRequiredService<>
        }

        public static string CurrentDirectory => IsDesignMode
            ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }       
}
