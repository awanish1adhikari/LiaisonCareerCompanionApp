using CareerCompanionApp.ViewModel;

namespace CareerCompanionApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool _isSoundEnabled;
        private bool _areNotificationsEnabled;
        private bool _isDarkModeEnabled;

        public SettingsViewModel()
        {
            // Optionally load settings from local storage or settings service
            LoadSettings();
        }

        public bool IsSoundEnabled
        {
            get => _isSoundEnabled;
            set => SetProperty(ref _isSoundEnabled, value);
        }

        private void SetProperty(ref bool isSoundEnabled, bool value)
        {
            throw new NotImplementedException();
        }

        public bool AreNotificationsEnabled
        {
            get => _areNotificationsEnabled;
            set => SetProperty(ref _areNotificationsEnabled, value);
        }

        public bool IsDarkModeEnabled
        {
            get => _isDarkModeEnabled;
            set => SetProperty(ref _isDarkModeEnabled, value);
        }

        // Optionally load settings from a persistent storage or service
        private void LoadSettings()
        {
            // Example: Load settings from a service or local storage
            // _isSoundEnabled = settingsService.GetSoundSetting();
            // _areNotificationsEnabled = settingsService.GetNotificationsSetting();
            // _isDarkModeEnabled = settingsService.GetDarkModeSetting();
        }
    }
}