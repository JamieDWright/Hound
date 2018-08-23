using System.Configuration;

namespace Hound.Config
{
    public class NetFrameworkReader : IConfigurationReader
    {
        public string Read(string key)
        {
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key]))
            {
                return ConfigurationManager.AppSettings[key];
            }

            return string.Empty;
        }
    }
}
