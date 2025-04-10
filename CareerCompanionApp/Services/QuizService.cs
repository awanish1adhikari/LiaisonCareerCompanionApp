using System.Net.Http;
using System.Text.Json;
using CareerCompanionApp.Models;

namespace CareerCompanionApp.Services
{
    public class QuizService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://who-wants-to-be-a-millionaire1.p.rapidapi.com/questions";
        private const string RapidApiKey = "62290c46e2msh1f7ab2a79e775d6p129922jsn50d37590218f"; // API Key
        private const string RapidApiHost = "who-wants-to-be-a-millionaire1.p.rapidapi.com";

        public QuizService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", RapidApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", RapidApiHost);
        }

        public async Task<List<QuizQuestion>> GetQuizQuestionsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<QuizQuestion>();

            var json = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(json).RootElement;

            var questions = new List<QuizQuestion>();

            foreach (var item in root.EnumerateArray())
            {
                var questionText = System.Net.WebUtility.HtmlDecode(item.GetProperty("question").GetString());
                var correct = System.Net.WebUtility.HtmlDecode(item.GetProperty("correct_answer").GetString());

                var incorrect = item.GetProperty("incorrect_answers").EnumerateArray()
                    .Select(ans => System.Net.WebUtility.HtmlDecode(ans.GetString()))
                    .ToList();

                var allOptions = new List<string>(incorrect) { correct };
                var shuffled = allOptions.OrderBy(x => new Random().Next()).ToList();
                var correctIndex = shuffled.IndexOf(correct);

                questions.Add(new QuizQuestion
                {
                    Question = questionText,
                    Options = shuffled,
                    CorrectIndex = correctIndex
                });
            }

            return questions;
        }
    }
}
