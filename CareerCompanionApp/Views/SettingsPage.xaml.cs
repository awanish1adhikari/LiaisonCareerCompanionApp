using CareerCompanionApp.ViewModels;

namespace CareerCompanionApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(); // Set BindingContext in the code-behind
        }
    }
}