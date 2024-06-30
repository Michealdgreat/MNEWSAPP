using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;
using MNEWSAPP.MVVM.Views;
using Xe.AcrylicView;
using CustomShellMaui;

namespace MNEWSAPP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();


            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseAcrylicView()
                .UseCustomShellMaui()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("mnewsfontfamily.ttf", "mnewsicons");
                    fonts.AddFont("serifprobold.ttf", "mnewslogo");
                    fonts.AddFont("MONTSERRAT-BOLD.TTF", "mnewsbold");
                    fonts.AddFont("MONTSERRAT-MEDIUM.TTF", "mnewsmedium");
                    fonts.AddFont("POPPINS-BOLD.TTF", "poppinsbold");
                    fonts.AddFont("POPPINS-BLACK.TTF", "poppinsblack");
                    fonts.AddFont("POPPINS-SEMIBOLD.TTF", "poppinssemibold");




                });

         
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddSingleton<GetNews>();
            builder.Services.AddTransient<ApiKeyService>();
            builder.Services.AddTransient<IndexPage>();
            builder.Services.AddTransient<IndexPage>();
            builder.Services.AddSingleton<SetApiKeyView>();
            builder.Services.AddTransient<ExploreView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Create the service provider
            var app = builder.Build();

            // Pass the service provider to the App constructor
            return app;
        }
    }
}
