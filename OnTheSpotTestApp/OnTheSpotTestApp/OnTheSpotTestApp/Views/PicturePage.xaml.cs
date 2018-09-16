using OnTheSpotTestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnTheSpotTestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicturePage : ContentPage
    {
        public PicturePage()
        {
            InitializeComponent();

            BindingContext = new PicturePageViewModel();
        }
    }
}