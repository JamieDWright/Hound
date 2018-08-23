using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Hound.Config
{
    public class NetStandardReader : IConfigurationReader
    {
        public string Read(string key)
        {
            string configPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            string secondaryConfigPath = Path.Combine(Environment.CurrentDirectory, "houndsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: true)
                .AddJsonFile(secondaryConfigPath, optional:true)
                .Build();

            var apiKey = configuration[key];

            return string.IsNullOrWhiteSpace(apiKey) ? string.Empty : apiKey;
        }
    }
}
