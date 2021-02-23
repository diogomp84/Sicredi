using System.IO;
using AutoSicredi.Base;
using Microsoft.Extensions.Configuration;

namespace AutoSicredi.Config
{
    public class ConfigApiReader
    {
        public static void SetFrameworkSettings()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configurationRoot = builder.Build();

            ApiSettings.Name = configurationRoot.GetSection("apiTestSettings").Get<ApiTestSettings>().Name;
            ApiSettings.AUT = configurationRoot.GetSection("apiTestSettings").Get<ApiTestSettings>().AUT;
            ApiSettings.ContentType = configurationRoot.GetSection("apiTestSettings").Get<ApiTestSettings>().ContentType;
        }
    }
}
