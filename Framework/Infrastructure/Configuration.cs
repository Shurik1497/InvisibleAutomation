using Microsoft.Extensions.Configuration;
using System;

namespace Framework.Infrastructure
{
    public class Configuration
    {
        private Configuration(string baseUrl, string hubUrl, string email, string password, int explicitWait)
        {
            BaseUrl = baseUrl;
            HubUrl = hubUrl;
            Email = email;
            Password = password;
            ExplicitWait = explicitWait;
        }

        public string BaseUrl { get; private set; }
        public string HubUrl { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int ExplicitWait { get; private set; }

        public static Configuration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true);

            IConfigurationRoot configuration = builder.Build();

            var hubUrl = configuration["HubUrl"];
            var baseUrl = configuration["BaseUrl"];
            var email = configuration["Email"];
            var password = configuration["Password"];
            var explicitWait = int.Parse(configuration["ExplicitWait"]);

            return new Configuration(baseUrl, hubUrl, email, password, explicitWait);
        }
    }
}
