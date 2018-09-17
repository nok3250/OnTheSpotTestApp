using System;
using Xamarin.Forms;

namespace OnTheSpotTestApp.ViewModels
{
    internal class PicturePageViewModel
    {
        private readonly string _pictureUrl;

        public PicturePageViewModel(string pictureUrl)
        {
            _pictureUrl = pictureUrl;
        }

        public ImageSource ProfileImageSource => new UriImageSource { Uri = new Uri(_pictureUrl) };
    }
}
