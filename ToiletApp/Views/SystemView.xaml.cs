using Microsoft.Extensions.DependencyInjection;
using ToiletApp.Services;
using ToiletApp.ViewModel;

namespace ToiletApp.Views;

public partial class SystemView : ContentPage
{
	public SystemView(SystemViewModel vn)
	{
        this.BindingContext = vn;
        InitializeComponent();
    }

   
}