using ToiletApp.Models;
using ToiletApp.ViewModel;
using ToiletApp.Views;

namespace ToiletApp
{
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
