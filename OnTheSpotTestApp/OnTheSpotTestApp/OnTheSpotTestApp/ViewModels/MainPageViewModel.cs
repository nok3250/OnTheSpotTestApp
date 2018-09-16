using OnTheSpotTestApp.FbOauth;
using OnTheSpotTestApp.FbOauth.Entities;
using OnTheSpotTestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using OnTheSpotTestApp.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace OnTheSpotTestApp.ViewModels
{
    public class MainPageViewModel : IFacebookAuthenticationDelegate
    {
        private readonly FacebookAuthenticator _auth;
        private readonly Page _page;
        public ICommand LoginCommand { set; get; }

        public MainPageViewModel(Page page)
        {
            _page = page;
            _auth = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, this);

            LoginCommand = new Command(Authenticate);
        }

        public void Authenticate()
        {
            var authenticator = _auth.GetAuthenticator();

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        public async void OnAuthenticationCompleted(FacebookOAuthToken token)
        {
            var facebookService = new FacebookService();
            var email = await facebookService.GetEmailAsync(token.AccessToken);
            var pictureUrl = await facebookService.GetPictureAsync(token.AccessToken);

            var account = new Account(email, new Dictionary<string, string> { { "picture", pictureUrl } });

            var accountStore = AccountStore.Create();

            var accountStored = accountStore.FindAccountsForService(Configuration.AppName).FirstOrDefault();
            if (accountStored != null)
            {
                accountStored = account;
                accountStore.Save(accountStored, Configuration.AppName);
            }
            else
            {
                accountStore.Save(account, Configuration.AppName);
            }

            await Application.Current.MainPage.Navigation.PushAsync(new PicturePage());
        }

        public async void OnAuthenticationFailed(string message, Exception exception)
        {
            await _page.DisplayAlert(message, exception?.ToString(), "OK");
        }

        public async void OnAuthenticationCanceled()
        {
            await _page.DisplayAlert("Authentication canceled", "You didn't completed the authentication process", "OK");
        }
    }
}
