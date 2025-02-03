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
                    fonts.AddFont("Switzal.ttf", "Switzal"); 
                    fonts.AddFont("Balney Demo.otf", "Balney"); 
                    fonts.AddFont("OWLY BARN Personal Use.ttf", "font"); 
                    fonts.AddFont("Kissing Season.ttf", "kiss"); 

                });

        
            #region SingeltonsandTransients
            builder.Services.AddSingleton<ToiletAppWebAPIProxy>();
     


            #region TransientViews

            builder.Services.AddTransient<AddToilet>();
            builder.Services.AddTransient<LoginPageView> ();;
            builder.Services.AddTransient<SelectedToilet>();
            builder.Services.AddTransient<SelectToiletView>();
            builder.Services.AddTransient<ServiceProvider>();
            builder.Services.AddTransient<ShareYourExView>();
            builder.Services.AddTransient<SignUpPageView>();
            builder.Services.AddTransient<SystemView>();
            #endregion

            #region TransientViewModels
            builder.Services.AddTransient<AddToiletViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginPageViewModel> ();
            builder.Services.AddTransient<SelectedToiletViewModel>();
            builder.Services.AddTransient<SelectToiletViewModel>();
            builder.Services.AddTransient<ServiceProviderViewModel>();
            builder.Services.AddTransient<ShareYourExViewModel> ();
            builder.Services.AddTransient<SignUpViewModel> ();
            builder.Services.AddTransient<SystemViewModel> ();
            #endregion


            builder.Services.AddTransient<AppShell>();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
