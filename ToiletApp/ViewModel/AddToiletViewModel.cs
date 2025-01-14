using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToiletApp.Services;
using ToiletApp.Models;

namespace ToiletApp.ViewModel
{
    public class AddToiletViewModel:ViewModelBase
    {
        //#region Address
        //private bool showAddressError;

        //public bool ShowAddressError
        //{
        //    get => showAddressError;
        //    set
        //    {
        //        showAddressError = value;
        //        OnPropertyChanged("ShowAddressError");
        //    }
        //}

        //private string address;

        //public string Address
        //{
        //    get => address;
        //    set
        //    {
        //        address = value;
        //        ValidateAddress();
        //        OnPropertyChanged("Address");
        //    }
        //}

        //private string addressError;

        //public string AddressError
        //{
        //    get => addressError;
        //    set
        //    {
        //        addressError = value;
        //        OnPropertyChanged("AddressError");
        //    }
        //}

        //private async Task<AddressComponents> ValidateAddress(bool fullValidation = false)
        //{
        //    AddressComponents components = null;
        //    this.ShowAddressError = string.IsNullOrEmpty(Address);
        //    if (fullValidation && !this.ShowAddressError)
        //    {
        //        //Check if the address is valid using the GeocodingService
        //        components = await GeocodingService.GetAddressComponentsAsync(Address);
        //        if (components == null)
        //        {
        //            AddressError = "Address is not valid";
        //            ShowAddressError = true;
        //        }
        //        else
        //        {
        //            AddressError = "";
        //            ShowAddressError = false;
        //        }
        //    }
        //    else
        //    {
        //        AddressError = "Address is required";
        //    }
        //    return components;
        //}

        //#endregion

        private ToiletAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AddToiletViewModel(ToiletAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            AddToiletCommand = new Command(AddToilet);
        }
        #endregion

        #region Properties
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
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
        #endregion


        private async void AddToilet()
        {
            InServerCall = true;

            UserInfo? u = ((App)Application.Current).LoggedInUser;
            //call the server to add the information

            //add the recipe as pending

            //how to add user id 
            CurrentToiletInfo information = new CurrentToiletInfo { Tlocation = Address, Price = Price, Accessibility=Accessibility, Photos=null, Rate=null, Review=null};
            bool worked = await this.proxy.AddToilet(information);
            InServerCall = false;

            if (!worked)
            {
                ErrorMsg = "Something Went Wrong";
            }
            else
            {
                ErrorMsg = "All Good";
            }

        }


    }
}
