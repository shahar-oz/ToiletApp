using ToiletApp.Models;
using ToiletApp.ViewModel;
using ToiletApp.Views;

namespace ToiletApp
{
    public enum USER_TYPES
    {
        GENERAL = 1,
        SERVICE_PROVIDER = 2,
        SANITAION = 3,
        ADMIN = 4
    }
    public partial class App : Application
    {
        public UserInfo? LoggedInUser { get; set; }


        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            LoginPageView? v = serviceProvider.GetService<LoginPageView>();
            MainPage = new NavigationPage(v);
            
        }
    }
}
