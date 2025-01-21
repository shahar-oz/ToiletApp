using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToiletApp.Services;
using ToiletApp.Models;
using System.Collections.ObjectModel;

namespace ToiletApp.ViewModel
{
    public class AddToiletViewModel:ViewModelBase
    {
        private ToiletAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        private UserInfo? currentUser;

        #region Constructor
        public AddToiletViewModel(ToiletAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            AddToiletCommand = new Command(AddToilet);
            AddPhotoCommand = new Command(AddPhoto);
            address = "";
            AddressError = "Invalid Address!";
            price = 0.0;
            accessibility = false;
            //statusID = 2;
            this.photos = new ObservableCollection<string>();
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }
        #endregion

        #region Properties
        private ObservableCollection<string> photos;
        public ObservableCollection<string> Photos
        {
            get { return photos; }
            set { photos = value; OnPropertyChanged(); }
        }

        #region Address
        private bool showAddressError;

        public bool ShowAddressError
        {
            get => showAddressError;
            set
            {
                showAddressError = value;
                OnPropertyChanged("ShowAddressError");
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                ValidateAddress();
                OnPropertyChanged("Address");
            }
        }

        private string addressError;

        public string AddressError
        {
            get => addressError;
            set
            {
                addressError = value;
                OnPropertyChanged("AddressError");
            }
        }

        private void ValidateAddress(bool fullValidation = false)
        {
            this.ShowAddressError = string.IsNullOrEmpty(Address);
        }

        #endregion


        //private int statusID;
        //public int StatusID
        //{
        //    get { return statusID; }
        //    set { statusID = value; OnPropertyChanged(); }
        //}


        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }
        private bool accessibility;
        public bool Accessibility
        {
            get { return accessibility; }
            set { accessibility = value; OnPropertyChanged(); }
        }
        #endregion

        #region buttons
        public ICommand AddToiletCommand { get; set; }
        public ICommand AddPhotoCommand { get; set; }
        #endregion

        private async void AddPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.Photos.Add(result.FullPath);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void AddToilet()
        {
            InServerCall = true;

            CurrentToiletInfo information = new CurrentToiletInfo 
            { 
                ToiletId = 0,
                Tlocation = Address, 
                Price = Price, 
                Accessibility=Accessibility,
                StatusID = 2 //Pending
            };

            //Add toilet to the server database
            CurrentToiletInfo? t = await this.proxy.AddToilet(information);
            InServerCall = false;
            if (t != null)
            {
                int fail = 0;
                foreach(string path in this.Photos)
                {
                    t = await proxy.UploadToiletImage(path, t.ToiletId);

                    if (t == null) ++fail;
                }
                
                if (fail > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Toilet was added but {fail} images fail to be uploaded", "ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", "Toilet was added successfully", "ok");
                    

                }
               if( currentUser.UserType == (int)USER_TYPES.ADMIN)
                {
                    await Shell.Current.GoToAsync("//System");
                }
                if (currentUser.UserType == (int)USER_TYPES.SERVICE_PROVIDER)
                {
                    await Shell.Current.GoToAsync("//ServiceProvider");
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error","Something went wrong, try again later!", "ok");
            }

            
            
            InServerCall = false;

            

        }


    }
}
