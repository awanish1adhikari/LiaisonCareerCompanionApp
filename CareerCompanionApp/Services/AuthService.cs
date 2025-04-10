using System;
using System.Threading.Tasks;
using Firebase.Auth; 

namespace CareerCompanionApp.Services
{
    public class AuthService
    {
        private readonly string _apiKey = "YOUR_FIREBASE_API_KEY"; // Replace with your Firebase API Key or your Auth API Key

        // Method for user login
        public async Task<string> SignInAsync(string email, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));

                // Sign in the user using Firebase authentication
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                if (auth != null)
                {
                    // Return the Firebase user token if successful
                    return auth.FirebaseToken;
                }
                else
                {
                    throw new Exception("Authentication failed");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions such as incorrect credentials
                throw new Exception($"Login failed: {ex.Message}");
            }
        }

        // Method for user sign-up
        public async Task<string> SignUpAsync(string email, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));

                // Create a new user with email and password using Firebase Authentication
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                if (auth != null)
                {
                    // Return the Firebase user token if sign-up is successful
                    return auth.FirebaseToken;
                }
                else
                {
                    throw new Exception("Sign-up failed");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions such as email already in use or weak passwords
                throw new Exception($"Sign-up failed: {ex.Message}");
            }
        }

        // Method to log the user out
        public async Task LogoutAsync()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));

                // Log out the current user
                await authProvider.SignOutAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions during logout
                throw new Exception($"Logout failed: {ex.Message}");
            }
        }

        // Optional: Method to check if the user is currently logged in
        public async Task<bool> Is
