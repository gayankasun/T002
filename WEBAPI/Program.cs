using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
     CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    IConfigurationRoot builtConfig = config.Build();
                    string vaultURL = builtConfig["KeyVault:URL"];
                    if (!string.IsNullOrEmpty(vaultURL))
                    {
                        Uri vault = new Uri(vaultURL);
                        string vaultMIClientId = builtConfig["KeyVault:MIClientId"];
                        string vaultPrefix = builtConfig["KeyVault:Prefix"];

                        if (string.IsNullOrEmpty(vaultMIClientId))
                        {
                            string vaultTenatId = builtConfig["KeyVault:TenantId"];
                            var secretClient = new SecretClient(
                                vault,
                                new DefaultAzureCredential(new DefaultAzureCredentialOptions
                                {
                                    VisualStudioTenantId = vaultTenatId
                                }));
                            config.AddAzureKeyVault(secretClient, new KeyVaultPrefixManager(vaultPrefix));
                        }
                        else
                        {
                            var secretClient = new SecretClient(
                                vault,
                                new DefaultAzureCredential(new DefaultAzureCredentialOptions
                                {
                                    ManagedIdentityClientId = vaultMIClientId,

                                }));
                            config.AddAzureKeyVault(secretClient, new KeyVaultPrefixManager(vaultPrefix));
                        }
                    }
                });
    }
}
