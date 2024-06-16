using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;

namespace MNEWSAPP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Load the configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            builder.Configuration.AddConfiguration(configuration);

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("mnewsfontfamily.ttf", "mnewsicons");
                    fonts.AddFont("serifprobold.ttf", "mnewslogo");
                    fonts.AddFont("MONTSERRAT-BOLD.TTF", "mnewsbold");
                });

            // Register services
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<GetNews>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

