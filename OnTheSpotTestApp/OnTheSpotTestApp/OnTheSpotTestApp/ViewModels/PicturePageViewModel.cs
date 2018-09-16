using System;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

namespace OnTheSpotTestApp.ViewModels
{
    internal class PicturePageViewModel
    {
        private readonly Account _userAccount;

        public PicturePageViewModel()
        {
            var accountStored = AccountStore.Create().FindAccountsForService(Configuration.AppName).FirstOrDefault();
            if (accountStored != null)
            {
                _userAccount = accountStored;
            }
            else
            {
                throw new AccountStoreException("Stored account is missing");
            }
        }

        public ImageSource ProfileImageSource => new UriImageSource { Uri = new Uri(_userAccount.Properties["picture"]) };
    }
}
