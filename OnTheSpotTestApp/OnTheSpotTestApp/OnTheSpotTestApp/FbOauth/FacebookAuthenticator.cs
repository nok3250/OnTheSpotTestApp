using System;
using OnTheSpotTestApp.FbOauth.Entities;
using Xamarin.Auth;

namespace OnTheSpotTestApp.FbOauth
{
    public class FacebookAuthenticator
    {
        private OAuth2Authenticator FbAuthenticator { get; }
        private readonly IFacebookAuthenticationDelegate _authenticationDelegate;

        private const string AuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        private const string RedirectUrl = "https://www.facebook.com/connect/login_success.html";
        private const bool IsUsingNativeUi = false;

        public FacebookAuthenticator(string clientId, string scope, IFacebookAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            FbAuthenticator = new OAuth2Authenticator(clientId, scope,
                new Uri(AuthorizeUrl),
                new Uri(RedirectUrl),
                null, IsUsingNativeUi);

            FbAuthenticator.Completed += OnAuthenticationCompleted;
            FbAuthenticator.Error += OnAuthenticationFailed;
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return FbAuthenticator;
        }

        public void OnPageLoading(Uri uri)
        {
            FbAuthenticator.OnPageLoading(uri);
        }

        private void OnAuthenticationCompleted(object sender,
            AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = new FacebookOAuthToken
                {
                    AccessToken = e.Account.Properties["access_token"]
                };
                _authenticationDelegate.OnAuthenticationCompleted(token);
            }
            else
            {
                _authenticationDelegate.OnAuthenticationCanceled();
            }
        }

        private void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            _authenticationDelegate.OnAuthenticationFailed(e.Message, e.Exception);
        }
    }
}
