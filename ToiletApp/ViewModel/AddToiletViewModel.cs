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

        #region builder
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
            this.photos = new ObservableCollection<string>();
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
             };

            //Add toilet to the server database
            CurrentToiletInfo? t = await this.proxy.AddToilet(information);

            if (t != null)
            {
                int success = 0, fail = 0;
                foreach(string path in this.Photos)
                {
                    t = await proxy.UploadToiletImage(path, t.ToiletId);
                    if ( t != null)
                    {
                        success++;
                    }
                    else
                    { fail++; }
                }

                if (fail > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Toilet was added but {fail} images fail to be uploaded", "ok");
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
