using ToiletApp.ViewModel;

namespace ToiletApp.Views;

public partial class AddToilet : ContentPage
{
    public AddToilet(AddToiletViewModel vn)
    {
        InitializeComponent();
        this.BindingContext = vn;
    }

  
}