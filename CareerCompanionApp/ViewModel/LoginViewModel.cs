using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CareerCompanionApp.Services;
using CareerCompanionApp.ViewModel;
using Xamarin.Forms;

namespace CareerCompanionApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService; // Instance of AuthService for login/signup functionality
        private string _email;
        private string _password;
        private bool _isLoggedIn;
        private string _errorMessage;
        private bool _isSignUpMode; // To toggle between Login and SignUp view

        // Constructor
        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new Command(async () => await LoginAsync());
            SignUpCommand = new Command(async () => await SignUpAsync());
            SwitchToSignUpCommand = new Command(SwitchToSignUpMode);
            SwitchToLoginCommand = new Command(SwitchToLoginMode);
        }

        // Properties for email, password, and login state
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);

                void SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
                {
                    if (!EqualityComparer<T>.Default.Equals(field, value))
                    {
                        field = value;
                        OnPropertyChanged(propertyName);
                    }
                }
            }
        }

        public bool IsSignUpMode
        {
            get => _isSignUpMode;
            set => SetProperty(ref _isSignUpMode, value);
        }

        private void SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        // Commands to handle login and sign-up actions
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand SwitchToSignUpCommand { get; }
        public ICommand SwitchToLoginCommand { get; }

        // Method to handle user login
        private async Task LoginAsync()
        {
            try
            {
                // Clear previous error messages
                ErrorMessage = string.Empty;

                // Validate email and password
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please provide both email and password.";
                    return;
                }

                // Call the AuthService to sign in
                var authLink = await _authService.SignInAsync(Email, Password);

                // Check if login was successful
                if (authLink != null)
                {
                    IsLoggedIn = true;  // Set login status to true
                    // Optionally, navigate to another page (e.g., DashboardPage)
                    await Shell.Current.GoToAsync("//DashboardPage");
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                // Handle login errors
                ErrorMessage = $"Login failed: {ex.Message}";
            }
        }

        // Method to handle user sign-up
        private async Task SignUpAsync()
        {
            try
            {
                // Clear previous error messages
                ErrorMessage = string.Empty;

                // Validate email and password
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please provide both email and password.";
                    return;
                }

                // Call the AuthService to sign up the user
                var authLink = await _authService.SignUpAsync(Email, Password);

                // Check if sign-up was successful
                if (authLink != null)
                {
                    IsLoggedIn = true;  // Set login status to true
                    // Optionally, navigate to another page (e.g., DashboardPage)
                    await Shell.Current.GoToAsync("//DashboardPage");
                }
                else
                {
                    ErrorMessage = "Sign-up failed. Try again.";
                }
            }
            catch (Exception ex)
            {
                // Handle sign-up errors
                ErrorMessage = $"Sign-up failed: {ex.Message}";
            }
        }

        // Method to switch to sign-up mode
        private void SwitchToSignUpMode()
        {
            IsSignUpMode = true;
        }

        // Method to switch to login mode
        private void SwitchToLoginMode()
        {
            IsSignUpMode = false;
        }
    }
}
