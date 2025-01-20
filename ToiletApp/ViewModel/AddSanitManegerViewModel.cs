
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToiletApp.Models;
using ToiletApp.Services;

namespace ToiletApp.ViewModel
{
    public class AddSanitManegerViewModel:ViewModelBase
    {
        private ToiletAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region Constructor
        public AddSanitManegerViewModel(ToiletAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            AddSanitCommand = new Command(AddSanit);
            name = "";
            email = "";
            servicezone = "";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            IsPassword = true;
        }
        #endregion

        #region Properties
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

        #region Name
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;   
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region Servicezone
        private string servicezone;

        public string Servicezone
        {
            get => servicezone;
            set
            {
                servicezone = value;
                OnPropertyChanged("Servicezone");
            }
        }
        #endregion

        #endregion

        #region Buttons
        public ICommand AddSanitCommand { get; set; }
        #endregion

        //Define a method that will be called when the Add button is clicked
        public async void AddSanit()
        {
            ValidateEmail();
            ValidatePassword();

            if (!ShowPasswordError && !ShowEmailError)
            {
                //Create a new SanitMan object with the data from the registration form
                var newSanitman = new SaninManInfo  
                {
                    Servicezone = Servicezone,
                    Username = Name,
                    Pass = Password,
                    Email = Email
                };
               
                //If the registration was successful, navigate to the Log In page
                if (newSanitman != null)
                {
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

    }
}
