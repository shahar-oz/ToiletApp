namespace ToiletApp.Views;

public partial class SelectToiletView : ContentPage
{
	public SelectToiletView(SelectToiletViewModel vn)
	{
        InitializeComponent();
        this.BindingContext = vn;
    }
}