namespace newProject.Models
{
    public class GenerateTOTPResponse
    {
        public byte[] secretKey { get; set; }
        public string? totpCode { get; set; }

        public GenerateTOTPResponse(byte[] secretKey, string totpCode)
        {
            this.secretKey = secretKey;
            this.totpCode = totpCode;
        }

    }
}