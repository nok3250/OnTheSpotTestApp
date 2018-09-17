using OnTheSpotTestApp.FbOauth;
using OnTheSpotTestApp.FbOauth.Entities;
using OnTheSpotTestApp.Services;
using OnTheSpotTestApp.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OnTheSpotTestApp.ViewModels
{
    public class MainPageViewModel : IFacebookAuthenticationDelegate, INotifyPropertyChanged
    {
        private readonly FacebookAuthenticator _auth;
        private readonly Page _page;
        public ICommand LoginCommand { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _loginButtonIsVisible;
        public bool LoginButtonIsVisible
        {
            get => _loginButtonIsVisible;
            set => SetProperty(ref _loginButtonIsVisible, value);
        }

        public MainPageViewModel(Page page)
        {
            _page = page;
            _auth = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, this);
            LoginButtonIsVisible = true;

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
            LoginButtonIsVisible = false;

            var facebookService = new FacebookService();
            var pictureUrl = await facebookService.GetPictureAsync(token.AccessToken);

            await Application.Current.MainPage.Navigation.PushAsync(new PicturePage(pictureUrl));
        }

        public async void OnAuthenticationFailed(string message, Exception exception)
        {
            await _page.DisplayAlert(message, exception?.ToString(), "OK");
        }

        public async void OnAuthenticationCanceled()
        {
            await _page.DisplayAlert("Authentication canceled", "You didn't completed the authentication process", "OK");
        }

        #region OnPropertyChanged
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
