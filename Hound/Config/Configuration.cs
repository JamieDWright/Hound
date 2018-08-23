using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Hound.Config
{
    public class Configuration
    {
        private const string _CONFIG_API_KEY  = "DataDog_Api_Key";
        private const string _CONFIG_TAG_KEY = "DataDog_Tags";

        public static string GetApiKey()
        {
            try
            {
                var config = GetConfiguration();
                return config.Read(_CONFIG_API_KEY);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static List<string> GetTags()
        {
            try
            {
                var config = GetConfiguration();
                var tags = config.Read(_CONFIG_TAG_KEY);

                return new List<string>(tags.Split(',', ';'));
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        private static IConfigurationReader GetConfiguration()
        {
            if (Type.GetType("Xamarin.Forms.Device") != null)
            {
                return new NetStandardReader();
            }
            else
            {
                var description = RuntimeInformation.FrameworkDescription;
                var platform = description.Substring(0, description.LastIndexOf(' '));

                switch (platform)
                {
                    case ".NET Framework":
                        return new NetFrameworkReader();
                    default:
                        return new NetStandardReader();
                }
            }
        }
    }
}