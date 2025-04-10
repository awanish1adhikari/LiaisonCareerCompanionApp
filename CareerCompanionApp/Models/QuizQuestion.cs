namespace CareerCompanionApp.Models
{
    public class QuizQuestion
    {
        public QuizQuestion(string question, List<string> options, int correctIndex)
        {
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
        }

        public QuizQuestion()
        {
            throw new NotImplementedException();
        }

        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
    }
}