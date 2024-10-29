using ToiletApp.ViewModel;

namespace ToiletApp.Views;

public partial class LoginPageView : ContentPage
{
	public LoginPageView(LoginPageViewModel vn)
	{
		this.BindingContext = vn;
		InitializeComponent();
	}
}