using ToiletApp.ViewModel;

namespace ToiletApp.Views;

public partial class SignUpPageView : ContentPage
{
	public SignUpPageView(SignUpViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}