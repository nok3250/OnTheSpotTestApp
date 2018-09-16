using OnTheSpotTestApp.FbOauth.Entities;
using System;

namespace OnTheSpotTestApp.FbOauth
{
    public interface IFacebookAuthenticationDelegate
    {
        void OnAuthenticationCompleted(FacebookOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}
