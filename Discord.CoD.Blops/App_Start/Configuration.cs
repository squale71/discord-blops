using Microsoft.Extensions.Configuration;
using System.IO;

namespace Discord.CoD.Blops.App_Start
{
    public class Configuration
    {
        private static Configuration _instance;

        private IConfigurationRoot _config;

        private Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

#if DEBUG
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
#endif

            _config = builder.Build();
        }

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }
                return _instance;
            }
        }


        public string Get(string key)
        {
            return _config[$"{key}"];
        }

        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }
    }
}
