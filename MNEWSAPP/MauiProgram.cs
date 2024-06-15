using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;


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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("mnewsfontfamily.ttf", "mnewsicons");
                    fonts.AddFont("serifprobold.ttf", "mnewslogo");
                    fonts.AddFont("POPPINS-BOLD.TTF", "headlinefont");

                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
