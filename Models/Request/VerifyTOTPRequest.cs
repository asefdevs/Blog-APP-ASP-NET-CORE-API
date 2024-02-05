namespace newProject.Models
{
    public class VerifyTotpRequest
    {
        public byte[] secretKey { get; set; }
        public string? totpCode { get; set; }
    }
}