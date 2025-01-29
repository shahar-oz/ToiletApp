using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToiletApp.Models;
using ToiletApp.Views;

namespace ToiletApp.ViewModel
{
    public class AppShellViewModel : ViewModelBase
    {
        private UserInfo? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        public bool IsAdmin
        {
            get
            {
                return this.currentUser.UserType == (int)USER_TYPES.ADMIN;
            }
        }
        public bool IsGeneralUser
        {
            get
            {
                return this.currentUser.UserType == (int)USER_TYPES.GENERAL;
            }
        }
        public bool IsServiceProvider
        {
            get
            {
                return this.currentUser.UserType == (int) USER_TYPES.SERVICE_PROVIDER;
            }
        }
       





        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginPageView>());
        }
    }
}


