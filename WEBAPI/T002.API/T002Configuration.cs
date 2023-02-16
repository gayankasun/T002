using Microsoft.Extensions.Configuration;

namespace T002.API.T002.API
{
    public interface IT002Configuration
    {
        IConfiguration ConfigurationRoot { get; }
        string GetValue(string section, string key);
        T GetValue<T>(string section, string key);
        DatabaseSettings DatabaseSettings { get; }
        IConfigurationSection GetSection(string key);
    }
    public class T002Configuration : IT002Configuration
    {
        public IConfiguration ConfigurationRoot { get; private set; }

        public T002Configuration(IConfiguration configuration)
        {
            this.ConfigurationRoot = configuration;
        }

        public DatabaseSettings DatabaseSettings
        {
            get
            {
                string endpointUri = GetValue(ConfigurationSections.Database, "EndpointUri");
                string primaryKey = GetValue(ConfigurationSections.Database, "PrimaryKey");
                string databaseId = GetValue(ConfigurationSections.Database, "DatabaseId");
                string containerId = GetValue(ConfigurationSections.Database, "ContainerId");

                return new DatabaseSettings
                {
                    EndpointUri = endpointUri,
                    PrimaryKey = primaryKey,
                    DatabaseId = databaseId,
                    ContainerId = containerId
                };
            }
        }

        public string GetValue(string section, string key)
        {
            return this.ConfigurationRoot.GetValue<string>(string.Format("{0}:{1}", section, key));
        }

        public T GetValue<T>(string section, string key)
        {
            return this.ConfigurationRoot.GetValue<T>(string.Format("{0}:{1}", section, key));
        }

        public IConfigurationSection GetSection(string key)
        {
            return this.ConfigurationRoot.GetSection(key);
        }
    }

    public static class ConfigurationSections
    {
        public const string SendGridWebHook = "SendGridWebHook";
        public const string Database = "Database";
    }
    public class DatabaseSettings
    {
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }
    }
}
