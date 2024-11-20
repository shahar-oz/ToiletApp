using Microsoft.Extensions.Logging;
using ToiletApp.Views;
using ToiletApp.ViewModel;
using ToiletApp.Services;

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
            builder.Services.AddSingleton<ToiletAppWebAPIProxy>();
            builder.Services.AddTransient<LoginPageView> ();
            builder.Services.AddTransient<LoginPageViewModel> ();
            builder.Services.AddTransient<SignUpViewModel> ();
            builder.Services.AddTransient<SignUpPageView> ();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<SelectToiletView>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
