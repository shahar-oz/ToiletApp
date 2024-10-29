using ToiletApp.ViewModel;
using ToiletApp.Views;

namespace ToiletApp
{
    public partial class App : Application
    {
        public App(LoginPageViewModel vm)
        {
            InitializeComponent();

            MainPage = new LoginPageView(vm);
        }
    }
}
