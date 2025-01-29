using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToiletApp.Services;
using ToiletApp.Models;
using System.Collections.ObjectModel;
using ToiletApp.Views;

namespace ToiletApp.ViewModel
{
 
    public class SystemViewModel : ViewModelBase
    {
        private ToiletAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        private ObservableCollection<CurrentToiletInfo> toiletsCol;
        public ObservableCollection<CurrentToiletInfo> ToiletsCol { get { return toiletsCol; } set { toiletsCol = value; OnPropertyChanged(); } }

        private List<CurrentToiletInfo> toilets;
        public List<Status> Statuses { get; set; }

        private Status selectedStatus;
        public Status SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                Filter();
                OnPropertyChanged();
            }
        }

        private void Filter()
        {
            ToiletsCol.Clear();
            foreach(var toilet in toilets)
            {
                if (toilet.StatusID == SelectedStatus.Id)
                    ToiletsCol.Add(toilet);
            }
        }
        
        public SystemViewModel(ToiletAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            this.RegisterCommand = new Command(OnSignUp);
            errorMsgStatusToilet = "";
            errorMsg = "";
            ApproveToilet = new Command(ChangeStatusToApprove);
            DeclineToilet = new Command(ChangStatusToDecline);
            toilets = new List<CurrentToiletInfo>();
            Statuses = new List<Status>();
            Statuses.Add(new Status()
            {
                Id = 1,
                Name = "Approved"
            });
            Statuses.Add(new Status()
            {
                Id = 2,
                Name = "Pending"
            });

            Statuses.Add(new Status()
            {
                Id = 3,
                Name = "Declined"
            });
            
            ToiletsCol = new ObservableCollection<CurrentToiletInfo>();
            GetAllApprovedToilets();


        }

        #region GetToiletsByStatus
        public async void GetAllApprovedToilets()
        {

            List<CurrentToiletInfo> list = await this.proxy.GetAllApprovedToilets();
            foreach(CurrentToiletInfo t in list)
                    toilets.Add(t);
            GetAllPendingToilets();
        }

        public async void GetAllPendingToilets()
        {

            List<CurrentToiletInfo> list = await this.proxy.GetAllPendingToilets();
            foreach (CurrentToiletInfo t in list)
                toilets.Add(t);
            GetAllDeclinedToilets();
        }

        public async void GetAllDeclinedToilets()
        {

            List<CurrentToiletInfo> list = await this.proxy.GetAllDeclinedToilets();
            foreach (CurrentToiletInfo t in list)
                toilets.Add(t);
            SelectedStatus = Statuses[1];
        }
        #endregion

        #region Properties
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
        private string errorMsgStatusToilet;
        public string ErrorMsgStatusToilet
        {
            get => errorMsgStatusToilet;
            set
            {
                if (errorMsgStatusToilet != value)
                {
                    errorMsgStatusToilet = value;
                    OnPropertyChanged(nameof(ErrorMsgStatusToilet));
                }
            }
        }
        private string email;
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
                }
            }
        }
        #endregion


        public ICommand ApproveToilet { get; set; }
        public ICommand DeclineToilet { get; set; }
        public ICommand RegisterCommand { get; }

        #region Toilet selected - approve and decline
        private CurrentToiletInfo selectedObject;
        public CurrentToiletInfo SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                if (value != null)
                {
                    // Extract the Id property by from the toilet object
                    int id = value.ToiletId;
                    SelectedToilet = ToiletsCol.Where(r => r.ToiletId == id).FirstOrDefault();
                }
                else
                    SelectedToilet = null;
                OnPropertyChanged();
            }
        }

        private CurrentToiletInfo selectedToilet;
        public CurrentToiletInfo SelectedToilet
        {
            get => selectedToilet;
            set
            {
                if (value != null)
                {
                    selectedToilet = value;
                    OnPropertyChanged();
                }

            }
        }

        public async void ChangeStatusToApprove()
        {
            ErrorMsgStatusToilet = "";
            bool success = await this.proxy.ChangeStatusToApprove(SelectedToilet);
            if (success)
            {
                ErrorMsgStatusToilet = "Status Changed to Approved";
                await Application.Current.MainPage.DisplayAlert("Success!", "Toilet's status changed to approved", "ok");
            }
            else
            {
                ErrorMsgStatusToilet = "Something Went Wrong";
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong. Try again later", "ok");
            }
        }

        public async void ChangStatusToDecline()
        {
            ErrorMsgStatusToilet = "";
            bool success = await this.proxy.ChangStatusToDecline(SelectedToilet);
            if (success)
            {
                ErrorMsgStatusToilet = "Status Changed To Declined";
                await Application.Current.MainPage.DisplayAlert("Success!", "Toilet's status changed to declined", "ok");
            }
            else
            {
                ErrorMsgStatusToilet = "Something went Wrong";

                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong. Try again later", "ok");
            }
               }

        #endregion

        private void OnSignUp()
        {

            ErrorMsg = "";
            Email = "";
            Password = "";

            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUpPageView>());

        }




    }
}
