using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CareerCompanionApp.Models;
using CareerCompanionApp.Services;

namespace CareerCompanionApp.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly QuizService _quizService;

        public ObservableCollection<QuizQuestion> Questions { get; set; } = new();
        private int _currentIndex = 0;

        public QuizQuestion? CurrentQuestion => Questions.Count > _currentIndex ? Questions[_currentIndex] : null;

        private string _selectedOption;
        public string SelectedOption
        {
            get => _selectedOption;
            set { _selectedOption = value; OnPropertyChanged(); }
        }

        private int _score;

        private int Score
        {
            get => _score;
            set { _score = value; OnPropertyChanged(); }
        }

        public ICommand SubmitAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        public QuizViewModel(string selectedOption)
        {
            _selectedOption = selectedOption;
            _quizService = new QuizService();
            SubmitAnswerCommand = new Command(OnSubmitAnswer);
            NextQuestionCommand = new Command(OnNextQuestion);

            _ = LoadQuestionsFromApiAsync();
        }

        private async Task LoadQuestionsFromApiAsync()
        {
            var fetchedQuestions = await _quizService.GetQuizQuestionsAsync();

            Questions.Clear();
            foreach (var question in fetchedQuestions)
                Questions.Add(question);

            _currentIndex = 0;
            OnPropertyChanged(nameof(CurrentQuestion));
        }

        private void OnSubmitAnswer()
        {
            if (CurrentQuestion == null || SelectedOption == null) return;

            var correctAnswer = CurrentQuestion.Options[CurrentQuestion.CorrectIndex];
            if (SelectedOption == correctAnswer)
                Score++;

            SelectedOption = null;
        }

        private void OnNextQuestion()
        {
            if (_currentIndex < Questions.Count - 1)
            {
                _currentIndex++;
                OnPropertyChanged(nameof(CurrentQuestion));
                SelectedOption = null;
            }
            else
            {
                // TODO: Navigate to results page or show summary
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
