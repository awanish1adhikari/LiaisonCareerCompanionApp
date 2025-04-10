using System;
using Microsoft.Maui.Controls;

namespace CareerCompanionApp.Views
{
    public partial class JobSearchPage : ContentPage
    {
        public JobSearchPage()
        {
            InitializeComponent();
        }

        private async void OnViewJobClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string url)
            {
                await Launcher.Default.OpenAsync(new Uri(url));
            }
        }
    }
}