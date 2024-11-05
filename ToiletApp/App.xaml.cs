using ToiletApp.ViewModel;
using ToiletApp.Views;

namespace ToiletApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
