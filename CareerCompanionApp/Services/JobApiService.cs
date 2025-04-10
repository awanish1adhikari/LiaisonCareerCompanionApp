using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using CareerCompanionApp.Models;

namespace CareerCompanionApp.Services
{
    public class JobApiService
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey = "x-rapidapi-key: 62290c46e2msh1f7ab2a79e775d6p129922jsn50d37590218f"; //  API Key
        private const string ApiHost = "jsearch.p.rapidapi.com";

        public JobApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", ApiHost);
        }

        public async Task<List<JobListing>> GetJobsAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://jsearch.p.rapidapi.com/search?query={Uri.EscapeDataString(query)}&page=1&num_pages=1");

                if (!response.IsSuccessStatusCode)
                    return new List<JobListing>();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var jobs = new List<JobListing>();
                var dataArray = doc.RootElement.GetProperty("data");

                foreach (var item in dataArray.EnumerateArray())
                {
                    jobs.Add(new JobListing
                    {
                        Title = item.GetProperty("job_title").GetString(),
                        Company = item.GetProperty("employer_name").GetString(),
                        Location = item.GetProperty("job_city").GetString(),
                        Description = item.GetProperty("job_description").GetString(),
                        Url = item.GetProperty("job_apply_link").GetString()
                    });
                }

                return jobs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching jobs: " + ex.Message);
                return new List<JobListing>();
            }
        }
    }
}
