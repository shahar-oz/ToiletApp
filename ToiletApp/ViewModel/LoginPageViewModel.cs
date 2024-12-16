using Microsoft.Win32;
using System.Windows.Input;
using ToiletApp.Models;
using ToiletApp.Services;
using ToiletApp.Views;

namespace ToiletApp.ViewModel;

public class LoginPageViewModel : ViewModelBase
{
    //   private string username;
    //   public string UserName
    //   {
    //       get => username;
    //       set
    //       {
    //           if (username != value)
    //           {
    //               username = value;
    //               OnPropertyChanged(nameof(UserName));
    //               // can add more logic here like email validation etc.
    //               // can add error message property and set it here
    //           }
    //       }
    //   }
    //   private string password;
    //   public string Password
    //   {
    //       get => password;
    //       set
    //       {
    //           if (password != value)
    //           {
    //               password = value;
    //               OnPropertyChanged(nameof(Password));
    //               // can add more logic here like email validation etc.
    //               // can add error message property and set it here
    //           }
    //       }
    //   }
    //   private string email;
    //   public string Email
    //   {
    //       get => password;
    //       set
    //       {
    //           if (email != value)
    //           {
    //               email = value;
    //               OnPropertyChanged(nameof(Email));

    //           }
    //       }
    //   }
    //   public LoginPageViewModel()
    //{

    //}
    private ToiletAppWebAPIProxy proxy;
    private IServiceProvider serviceProvider;
    public LoginPageViewModel(ToiletAppWebAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.proxy = proxy;
        this.serviceProvider = serviceProvider;
        this.LoginCommand = new Command(OnLogin);
        this.RegisterCommand = new Command(OnSignUp);

    }

    private string email;
    private string password;

    public string Email
    {
        get => email;
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }


    private string errorMsg;
    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }
    private bool rememberMe;
    public bool RememberMe
    {
        get => rememberMe;
        set
        {
            if (rememberMe != value)
            {
                rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
            }
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    private async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        LoginInfo loginInfo = new LoginInfo { Email = Email, Password = Password };
        UserInfo u = await this.proxy.LoginAsync(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ErrorMsg = "Invalid email or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            AppShell shell = serviceProvider.GetService<AppShell>();

            ((App)Application.Current).MainPage = shell;

            
            Shell.Current.FlyoutIsPresented = false;
            //Shell.Current.GoToAsync("SelectToiletView");

        }
    }
    private void OnSignUp()
    {

        ErrorMsg = "";
        Email = "";
        Password = "";

        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUpPageView>());

    }

    
}