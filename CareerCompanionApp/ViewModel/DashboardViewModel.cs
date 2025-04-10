using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CareerCompanionApp.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        // Sample dynamic values (you can later pull from Firebase)
        public string Greeting => $"Welcome back!";
        public int ResumeCount => 3; // Replace with actual count
        public int SavedJobsCount => 5; // Replace with actual data
        public string LastQuizScore => "80%"; // Replace with real score

        public ICommand GoToResumeCommand { get; }
        public ICommand GoToJobsCommand { get; }
        public ICommand GoToQuizCommand { get; }
        public ICommand GoToTrackerCommand { get; }

        public DashboardViewModel()
        {
            GoToResumeCommand = new Command(async () => await Shell.Current.GoToAsync("//ResumePage"));
            GoToJobsCommand = new Command(async () => await Shell.Current.GoToAsync("//JobSearchPage"));
            GoToQuizCommand = new Command(async () => await Shell.Current.GoToAsync("//QuizPage"));
            GoToTrackerCommand = new Command(async () => await Shell.Current.GoToAsync("//TrackerPage"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}