using OtpNet;
using System;

namespace newProject.Services
{
    public class TOTP
    {
        public byte[] GenerateRandomKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20);

            var base32String = Base32Encoding.ToString(key);
            var base32Bytes = Base32Encoding.ToBytes(base32String);
            return base32Bytes;
        }

        public string GenerateTotpCode(byte[] secretKey)
        {
            var totp = new OtpNet.Totp(secretKey, step: 10, totpSize: 6);
            var totpCode = totp.ComputeTotp(DateTime.UtcNow);
            return totpCode;
        }

        public bool VerifyTotpCode(string totpCode, byte[] secretKey)
        {
            var totp = new OtpNet.Totp(secretKey, step: 10, totpSize: 6);
            return totp.VerifyTotp(totpCode, out long timeStepMatched, new VerificationWindow(2, 2));
        }
    }
}
