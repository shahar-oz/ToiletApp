namespace ToiletApp.ViewModel;

public class LoginPageViewModel : ViewModelBase
{
    private string username;
    public string UserName
    {
        get => username;
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged(nameof(UserName));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string email;
    public string Email
    {
        get => password;
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged(nameof(Email));
               
            }
        }
    }
    public LoginPageViewModel()
	{
	  
	}
}