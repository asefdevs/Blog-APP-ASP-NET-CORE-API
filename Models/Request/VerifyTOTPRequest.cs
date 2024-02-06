using System.ComponentModel.DataAnnotations;
namespace newProject.Models
{
    public class VerifyTotpRequest
    {
        [Required]
        public string? email { get; set; }
        [Required]
        public byte[] secretKey { get; set; }
        [Required]
        public string? totpCode { get; set; }


    }
}