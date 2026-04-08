using System.Security.Cryptography;
using System.Text;

namespace Automation.Infrastructure.Security
{
    public class CredentialService
    {
        private readonly string _path = @"C:\AutomaFiscal\credentials.dat";

        public void Save(string key, string value)
        {
            var encrypted = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(value),
                null,
                DataProtectionScope.CurrentUser);

            File.AppendAllText(_path, $"{key}|{Convert.ToBase64String(encrypted)}{Environment.NewLine}");
        }

        public string? Get(string key)
        {
            if (!File.Exists(_path))
                return null;

            var lines = File.ReadAllLines(_path);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts[0] == key)
                {
                    var decrypted = ProtectedData.Unprotect(
                        Convert.FromBase64String(parts[1]),
                        null,
                        DataProtectionScope.CurrentUser);

                    return Encoding.UTF8.GetString(decrypted);
                }
            }

            return null;
        }
    }
}
