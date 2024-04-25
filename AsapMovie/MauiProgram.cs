using AsapMovie.Methods_and_Models;
using AsapMovie.Pages;
using Microsoft.Extensions.Logging;

namespace AsapMovie ;

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
                
            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddTransient<InitialPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }