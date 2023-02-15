using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace WEBAPI
{
    public class KeyVaultPrefixManager : KeyVaultSecretManager
    {
        private readonly string prefix;

        public KeyVaultPrefixManager(string prefix)
        {
            this.prefix = $"{prefix}-";
        }
        public override bool Load(SecretProperties secret)
        {
            return secret.Name.StartsWith(prefix);
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name
            .Substring(prefix.Length)
            .Replace("--", ConfigurationPath.KeyDelimiter);
        }
    }
}
