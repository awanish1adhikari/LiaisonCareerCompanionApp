
using Firebase.Database;
using Firebase.Database.Query;
using CareerCompanionApp.Models;

namespace CareerCompanionApp.Services;

public class FirebaseService
{
    private readonly FirebaseClient _client;

    public FirebaseService()
    {
        _client = new FirebaseClient("https://your-project-id.firebaseio.com/"); // <-- Replace with your Firebase DB URL
    }

    // Save or Update Resume
    public async Task SaveResumeToCloudAsync(Resume resume)
    {
        await _client
            .Child("resumes")
            .Child(resume.Id)
            .PutAsync(resume);
    }

    // Get All Resumes
    public async Task<List<Resume>> GetAllResumesFromCloudAsync()
    {
        var items = await _client
            .Child("resumes")
            .OnceAsync<Resume>();

        return items.Select(i => i.Object).ToList();
    }

    // Delete Resume
    public async Task DeleteResumeAsync(string resumeId)
    {
        await _client
            .Child("resumes")
            .Child(resumeId)
            .DeleteAsync();
    }

    // Placeholder for future: Saved jobs, quiz scores, etc.
    // public async Task SaveJobApplication(...) {}
    // public async Task SaveQuizResult(...) {}
}