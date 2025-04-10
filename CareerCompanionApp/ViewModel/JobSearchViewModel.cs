using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CareerCompanionApp.Models;
using CareerCompanionApp.Services;

namespace CareerCompanionApp.ViewModels
{
    public class JobSearchViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<JobListing> JobResults { get; set; } = new();

        public ICommand SearchJobsCommand { get; }

        private readonly JobApiService _jobService;

        public JobSearchViewModel()
        {
            _jobService = new JobApiService();
            SearchJobsCommand = new Command(async () => await SearchJobsAsync());
        }

        private async Task SearchJobsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
                return;

            try
            {
                var results = await _jobService.GetJobsAsync(SearchQuery);
                JobResults.Clear();

                foreach (var job in results)
                {
                    JobResults.Add(job);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching jobs: " + ex.Message);
                // Optionally show alert or toast
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}