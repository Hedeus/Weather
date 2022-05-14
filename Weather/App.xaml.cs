using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Weather.ViewModels;

namespace Weather
{

    public partial class App : Application
    {
        

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //services.AddSingleton<DataService>();
            //services.AddSingleton<CountriesStatisticViewModel>();
            services.AddSingleton<WelcomeWindowModel>();
        }

    }
}
