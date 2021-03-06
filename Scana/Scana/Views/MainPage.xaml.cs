﻿using Scana.Views;
using System.ComponentModel;
using Xamarin.Forms;

namespace Scana
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void scanButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ScanningPage(), true);
        }

        private async void aboutIcon_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), true);
        }
    }
}