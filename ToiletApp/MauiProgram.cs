using Microsoft.Extensions.Logging;
using ToiletApp.Views;
using ToiletApp.ViewModel;

namespace ToiletApp
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
            builder.Services.AddSingleton<LoginPageView> ();
            builder.Services.AddSingleton<LoginPageViewModel> ();
            builder.Services.AddSingleton<SignUpViewModel> ();
            builder.Services.AddSingleton<SignUpPageView> ();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
