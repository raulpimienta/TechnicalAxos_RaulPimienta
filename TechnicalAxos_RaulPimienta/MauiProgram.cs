using Microsoft.Extensions.Logging;
using TechnicalAxos_RaulPimienta.Services;
using TechnicalAxos_RaulPimienta.ViewModels;
using TechnicalAxos_RaulPimienta.Views;

namespace TechnicalAxos_RaulPimienta
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            return builder.Build();
        }
    }
}
