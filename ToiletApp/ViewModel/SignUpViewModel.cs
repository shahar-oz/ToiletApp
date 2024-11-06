using System.Windows.Input;

namespace ToiletApp.ViewModel;

public class SignUpViewModel : ContentPage
{

    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; OnPropertyChanged(); }
    }

    public string password;
    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
    }

   
    public ICommand IsStoreownerChecked { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }

    
    private bool isManager;
    public bool IsManager
    {
        get { return isManager; }
        set
        {
            isManager = value;
            OnPropertyChanged();
            ((Command)IsStoreownerChecked).ChangeCanExecute();
            ((Command)VisitorSelectedCommand).ChangeCanExecute();
        }
    }

    public SignUpViewModel()
    {

        IsStoreownerChecked = new Command(StoreownerSelected, () => !IsManager);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsManager);
        IsManager = false;

    }

    private async void StoreownerSelected()
    {
        IsManager = true;

    }

    private async void VisitorSelected()
    {
        IsManager = false;
    }

}