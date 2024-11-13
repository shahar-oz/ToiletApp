using System.Windows.Input;
using ToiletApp.Utils;

namespace ToiletApp.ViewModel;

public class SignUpViewModel : ViewModelBase
{
    //User_Error
    private string userError;
    public string UserError
    {
        get { return userError; }
        set
        {
            userError = value;
            OnPropertyChanged(UserError);
        }
    }

    private string name;
    public string Name
    {
        get { return name; }
        set { 
            name = value;
            OnPropertyChanged(Name);
            if (!Validations.IsValidUserName(Name)){
                UserError = "test 12324325";
                OnPropertyChanged(Name);
            }
     }
    }

    private string password;
    public string Password
    {
        get { return password; }
        set 
        { 
            password = value; 
            OnPropertyChanged(Password); 
        }
    }

    private string email;
    public string Email
    {
        get { return email; }
        set
        {
            email = value;
            OnPropertyChanged(Email);
        }
    }
    //PasswordError
    private string passwordError;
    public string PasswordError
    {
        get { return passwordError; }
        set
        {
            passwordError = value;
            OnPropertyChanged(PasswordError);
        }
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