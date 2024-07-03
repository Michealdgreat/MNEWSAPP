using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;
using MNEWSAPP.MVVM.Views;
using Xe.AcrylicView;
using CustomShellMaui;
using MNEWSAPP.MVVM.Messages;

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
                    fonts.AddFont("fontello.ttf", "mnewsIcons");


                });

            //DI
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<GetNews>();
            builder.Services.AddSingleton<ApiKeyService>();
            builder.Services.AddSingleton<IndexPage>();
            builder.Services.AddSingleton<SetApiKeyView>();
            builder.Services.AddSingleton<ExploreView>();
            builder.Services.AddSingleton<SearchView>();
            builder.Services.AddSingleton<SettingsView>();
            builder.Services.AddSingleton<FontSizeViewModel>();
            builder.Services.AddSingleton<FontSizeChangedMessage>();
            builder.Services.AddSingleton<ArticleDetailsViewModel>();
            builder.Services.AddSingleton<ChangeFontSizeView>();

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
