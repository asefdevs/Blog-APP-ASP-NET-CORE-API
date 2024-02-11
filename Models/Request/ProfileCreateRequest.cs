namespace newProject.Models
{
    public class ProfileCreateRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
    }
}