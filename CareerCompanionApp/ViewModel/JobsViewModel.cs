using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CareerCompanionApp.Models;
using CareerCompanionApp.Services;

namespace CareerCompanionApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(string email, string password, ICommand loginCommand)
        {
            Email = email;
            Password = password;
            LoginCommand = loginCommand;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        // Call AuthService.LoginAsync()
    }

    public class ResumeViewModel : BaseViewModel
    {
        public ResumeViewModel(ObservableCollection<Resume> resumes, ICommand addResumeCommand, ICommand saveResumeCommand)
        {
            Resumes = resumes;
            AddResumeCommand = addResumeCommand;
            SaveResumeCommand = saveResumeCommand;
        }

        public ObservableCollection<Resume> Resumes { get; set; }
        public ICommand AddResumeCommand { get; set; }
        public ICommand SaveResumeCommand { get; set; }
        // Call FirebaseService.SaveResume()
    }

    public partial class JobSearchViewModel : BaseViewModel
    {
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value; OnPropertyChanged(nameof(SearchQuery)); }
        }

        public ObservableCollection<JobListing> JobResults { get; set; } = new();

        public ICommand SearchJobsCommand { get; }

        private readonly JobApiService _jobService;

        public JobSearchViewModel(string searchQuery)
        {
            _searchQuery = searchQuery;
            _jobService = new JobApiService();
            SearchJobsCommand = new Command(async void () => await SearchJobsAsync());
        }

        private async Task SearchJobsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            var results = await _jobService.GetJobsAsync(SearchQuery);
            JobResults.Clear();

            foreach (var job in results)
            {
                JobResults.Add(job);
            }
        }
    }
}