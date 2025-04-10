using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CareerCompanionApp.Models;
using CareerCompanionApp.Services;
using System.Linq;

namespace CareerCompanionApp.ViewModels
{
    public class ResumeViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseService _firebaseService;

        private Resume _resume = new();
        public Resume Resume
        {
            get => _resume;
            set
            {
                _resume = value;
                OnPropertyChanged();
            }
        }

        private Resume _selectedResume;
        public Resume SelectedResume
        {
            get => _selectedResume;
            set
            {
                _selectedResume = value;
                if (value != null)
                    LoadResumeForEdit(value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Resume> Resumes { get; set; } = new();

        public ICommand SaveResumeCommand { get; }
        public ICommand DeleteResumeCommand { get; }

        public ResumeViewModel()
        {
            _firebaseService = new FirebaseService(); // Ideally use dependency injection

            SaveResumeCommand = new Command(async () => await OnSaveResumeAsync());
            DeleteResumeCommand = new Command<Resume>(async (resume) => await OnDeleteResumeAsync(resume));

            _ = LoadResumesAsync();
        }

        private async Task OnSaveResumeAsync()
        {
            if (string.IsNullOrWhiteSpace(Resume.FullName))
                return;

            try
            {
                await _firebaseService.SaveResumeToCloudAsync(Resume);

                // Remove old version if editing
                var existing = Resumes.FirstOrDefault(r => r.Id == Resume.Id);
                if (existing != null)
                    Resumes.Remove(existing);

                Resumes.Add(new Resume
                {
                    Id = Resume.Id,
                    FullName = Resume.FullName,
                    Education = Resume.Education,
                    Experience = Resume.Experience,
                    Skills = Resume.Skills
                });

                Resume = new Resume(); // Reset form
                OnPropertyChanged(nameof(Resume));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save failed: " + ex.Message);
            }
        }

        private async Task OnDeleteResumeAsync(Resume resume)
        {
            if (resume == null) return;

            try
            {
                await _firebaseService.DeleteResumeAsync(resume.Id);
                Resumes.Remove(resume);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
            }
        }

        private void LoadResumeForEdit(Resume resume)
        {
            Resume = new Resume
            {
                Id = resume.Id,
                FullName = resume.FullName,
                Education = resume.Education,
                Experience = resume.Experience,
                Skills = resume.Skills
            };

            OnPropertyChanged(nameof(Resume));
        }

        private async Task LoadResumesAsync()
        {
            try
            {
                var cloudResumes = await _firebaseService.GetAllResumesFromCloudAsync();
                if (cloudResumes != null)
                {
                    foreach (var resume in cloudResumes)
                        Resumes.Add(resume);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loading resumes failed: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
