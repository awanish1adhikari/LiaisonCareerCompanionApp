namespace CareerCompanionApp.Models
{
    public class Resume
    {
        public Resume(string fullName, string education, string experience, string skills)
        {
            FullName = fullName;
            Education = education;
            Experience = experience;
            Skills = skills;
        }

        public Resume()
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
    }
}