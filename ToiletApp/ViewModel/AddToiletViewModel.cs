using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
