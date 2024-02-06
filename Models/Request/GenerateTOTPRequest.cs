using System.ComponentModel.DataAnnotations;

namespace newProject.Models
{
    public class GenerateTOTPRequest
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }
    }
}