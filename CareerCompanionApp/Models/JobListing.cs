namespace CareerCompanionApp.Models
{
    public class JobListing
    {
        public JobListing(string? title, string company, string location, string description, string url)
        {
            Title = title;
            Company = company;
            Location = location;
            Description = description;
            Url = url;
        }

        public JobListing()
        {
            throw new NotImplementedException();
        }

        public string? Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}