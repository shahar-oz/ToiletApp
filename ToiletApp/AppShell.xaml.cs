using ToiletApp.ViewModel;
using ToiletApp.Views;

namespace ToiletApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            Routing.RegisterRoute("SignUp", typeof(SignUpPageView));
            Routing.RegisterRoute("Login", typeof(LoginPageView));
            Routing.RegisterRoute("SelectToiletView", typeof(SelectToiletView));

        }

      
    }
}
