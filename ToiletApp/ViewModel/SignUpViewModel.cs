using CoreFoundation;
using System.Windows.Input;
using ToiletApp.Utils;

namespace ToiletApp.ViewModel;

public class SignUpViewModel : ViewModelBase
{
    //    //User_Error
    //    private string userError;
    //    public string UserError
    //    {
    //        get { return userError; }
    //        set
    //        {
    //            userError = value;
    //            OnPropertyChanged(UserError);
    //        }
    //    }

    //    private string name;
    //    public string Name
    //    {
    //        get { return name; }
    //        set { 
    //            name = value;
    //            OnPropertyChanged(Name);
    //            if (!Validations.IsValidUserName(Name)){
    //                UserError = "test 12324325";
    //                OnPropertyChanged(Name);
    //            }
    //     }
    //    }

    //    private string password;
    //    public string Password
    //    {
    //        get { return password; }
    //        set 
    //        { 
    //            password = value; 
    //            OnPropertyChanged(Password); 
    //        }
    //    }

    //    private string email;
    //    public string Email
    //    {
    //        get { return email; }
    //        set
    //        {
    //            email = value;
    //            OnPropertyChanged(Email);
    //        }
    //    }
    //    //PasswordError
    //    private string passwordError;
    //    public string PasswordError
    //    {
    //        get { return passwordError; }
    //        set
    //        {
    //            passwordError = value;
    //            OnPropertyChanged(PasswordError);
    //        }
    //    }


    //    public ICommand IsStoreownerChecked { get; set; }
    //    public ICommand VisitorSelectedCommand { get; set; }


    //    private bool isManager;
    //    public bool IsManager
    //    {
    //        get { return isManager; }
    //        set
    //        {
    //            isManager = value;
    //            OnPropertyChanged();
    //            ((Command)IsStoreownerChecked).ChangeCanExecute();
    //            ((Command)VisitorSelectedCommand).ChangeCanExecute();
    //        }
    //    }

    //    public SignUpViewModel()
    //    {

    //        IsStoreownerChecked = new Command(StoreownerSelected, () => !IsManager);
    //        VisitorSelectedCommand = new Command(VisitorSelected, () => IsManager);
    //        IsManager = false;

    //    }

    //    private async void StoreownerSelected()
    //    {
    //        IsManager = true;

    //    }

    //    private async void VisitorSelected()
    //    {
    //        IsManager = false;
    //    }

    #region Name
    private bool showNameError;

    public bool ShowNameError
    {
        get => showNameError;
        set
        {
            showNameError = value;
            OnPropertyChanged("ShowNameError");
        }
    }

    private string name;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            ValidateName();
            OnPropertyChanged("Name");
        }
    }

    private string nameError;

    public string NameError
    {
        get => nameError;
        set
        {
            nameError = value;
            OnPropertyChanged("NameError");
        }
    }

    private void ValidateName()
    {
        this.ShowNameError = string.IsNullOrEmpty(Name);
    }
    #endregion

    #region Email
    private bool showEmailError;

    public bool ShowEmailError
    {
        get => showEmailError;
        set
        {
            showEmailError = value;
            OnPropertyChanged("ShowEmailError");
        }
    }

    private string email;

    public string Email
    {
        get => email;
        set
        {
            email = value;
            ValidateEmail();
            OnPropertyChanged("Email");
        }
    }

    private string emailError;

    public string EmailError
    {
        get => emailError;
        set
        {
            emailError = value;
            OnPropertyChanged("EmailError");
        }
    }

    private void ValidateEmail()
    {
        this.ShowEmailError = string.IsNullOrEmpty(Email);
        if (!ShowEmailError)
        {
            //check if email is in the correct format using regular expression
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = "Email is not valid";
                ShowEmailError = true;
            }
            else
            {
                EmailError = "";
                ShowEmailError = false;
            }
        }
        else
        {
            EmailError = "Email is required";
        }
    }
    #endregion
    #region Password
    private bool showPasswordError;

    public bool ShowPasswordError
    {
        get => showPasswordError;
        set
        {
            showPasswordError = value;
            OnPropertyChanged("ShowPasswordError");
        }
    }

    private string password;

    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged("Password");
        }
    }

    private string passwordError;

    public string PasswordError
    {
        get => passwordError;
        set
        {
            passwordError = value;
            OnPropertyChanged("PasswordError");
        }
    }

    private void ValidatePassword()
    {
        //Password must include characters and numbers and be longer than 4 characters
        if (string.IsNullOrEmpty(password) ||
            password.Length < 4 ||
            !password.Any(char.IsDigit) ||
            !password.Any(char.IsLetter))
        {
            this.ShowPasswordError = true;
        }
        else
            this.ShowPasswordError = false;
    }

    //This property will indicate if the password entry is a password
    private bool isPassword = true;
    public bool IsPassword
    {
        get => isPassword;
        set
        {
            isPassword = value;
            OnPropertyChanged("IsPassword");
        }
    }
    //This command will trigger on pressing the password eye icon
    public Command ShowPasswordCommand { get; }
    //This method will be called when the password eye icon is pressed
    public void OnShowPassword()
    {
        //Toggle the password visibility
        IsPassword = !IsPassword;
    }
    #endregion

    #region Photo

    private string photoURL;

    public string PhotoURL
    {
        get => photoURL;
        set
        {
            photoURL = value;
            OnPropertyChanged("PhotoURL");
        }
    }

    private string localPhotoPath;

    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged("LocalPhotoPath");
        }
    }
    public async void OnRegister()
    {
        ValidateName();
        ValidateLastName();
        ValidateEmail();
        ValidatePassword();

        if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError)
        {
            //Create a new AppUser object with the data from the registration form
            var newUser = new AppUser
            {
                UserName = Name,
                UserLastName = LastName,
                UserEmail = Email,
                UserPassword = Password,
                IsManager = false
            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            newUser = await proxy.Register(newUser);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (newUser != null)
            {
                //UPload profile imae if needed
                if (!string.IsNullOrEmpty(LocalPhotoPath))
                {
                    await proxy.LoginAsync(new LoginInfo { Email = newUser.UserEmail, Password = newUser.UserPassword });
                    AppUser? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                    if (updatedUser == null)
                    {
                        InServerCall = false;
                        await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                    }
                }
                InServerCall = false;

                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the registration failed, display an error message
                string errorMsg = "Registration failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
            }
        }
    }

    //Define a method that will be called upon pressing the cancel button
    public void OnCancel()
    {
        //Navigate back to the login page
        ((App)(Application.Current)).MainPage.Navigation.PopAsync();
    }
}

