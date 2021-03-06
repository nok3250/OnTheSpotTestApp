﻿using OnTheSpotTestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnTheSpotTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(this);
        }
    }
}
