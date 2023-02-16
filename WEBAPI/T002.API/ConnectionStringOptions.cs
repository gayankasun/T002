using System;

namespace T002.API.T002.API
{
    public class ConnectionStringOptions
    {
        public Uri ServiceEndpoint { get; set; }
        public string AuthKey { get; set; }

        public void Deconstruct(out Uri serviceEndpoint, out string authKey)
        {
            serviceEndpoint = ServiceEndpoint;
            authKey = AuthKey;
        }
    }
}
