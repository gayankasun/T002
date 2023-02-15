using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using T002.API;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
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
                   }
                   else
                   {
                       var secretClient = new SecretClient(
                           vault,
                           new DefaultAzureCredential(new DefaultAzureCredentialOptions
                           {
                               ManagedIdentityClientId = vaultMIClientId,

                           }));
                   }
               }
           })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}