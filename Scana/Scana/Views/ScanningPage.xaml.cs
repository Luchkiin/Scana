using Plugin.Toast;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scana.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanningPage : ContentPage
    {
        public ScanningPage()
        {
            InitializeComponent();
        }

        public void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AutoFocus();
                try
                {
                    if (result != null && !string.IsNullOrEmpty(result.Text))
                    {
                        if (result.Text.StartsWith("http://") || result.Text.StartsWith("https://"))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                {
                    PauseAnalysis();
                    //await DisplayAlert("Scanned result", result.Text, "OK");
                    await Launcher.OpenAsync(new Uri(result.Text));
                    ResumeAnalysis();
                });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                PauseAnalysis();
                                await DisplayAlert("Scanned result", result.Text + "\n" + result.BarcodeFormat, "OK");
                                ResumeAnalysis();
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        /// <summary>
        /// The OnAppearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            scanView.IsScanning = true;
        }

        /// <summary>
        /// The OnDisappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            scanView.IsScanning = false;
        }

        /// <summary>
        /// The BackBtn_Clicked
        /// </summary>
        /// <param name="sender">The sender<see cref="System.Object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private async void BackBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// The FlashBtn_Clicked
        /// </summary>
        /// <param name="sender">The sender<see cref="System.Object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private void FlashBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (scanView != null)
                scanView.ToggleTorch();
            CrossToastPopUp.Current.ShowToastMessage("Flashlight clicked");
        }

        /// <summary>
        /// The closeBtn_Clicked
        /// </summary>
        /// <param name="sender">The sender<see cref="System.Object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private async void closeBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// The AutoFocus
        /// </summary>
        public void AutoFocus()
        {
            if (scanView != null)
                scanView.AutoFocus();
        }

        /// <summary>
        /// The AutoFocus
        /// </summary>
        /// <param name="x">The x<see cref="int"/></param>
        /// <param name="y">The y<see cref="int"/></param>
        public void AutoFocus(int x, int y)
        {
            if (scanView != null)
                scanView.AutoFocus(x, y);
        }

        /// <summary>
        /// The PauseAnalysis
        /// </summary>
        public void PauseAnalysis()
        {
            if (scanView != null)
                scanView.IsAnalyzing = false;
        }

        /// <summary>
        /// The ResumeAnalysis
        /// </summary>
        public void ResumeAnalysis()
        {
            if (scanView != null)
                scanView.IsAnalyzing = true;
        }
    }
}