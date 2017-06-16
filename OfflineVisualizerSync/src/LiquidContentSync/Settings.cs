using Microsoft.Extensions.Configuration;

namespace LiquidContentSync
{
    public class Settings
    {
        public static string LiquidApiAddress => Configuration["api_url"];
        public static string ApiKey => Configuration["api_key"];

        static IConfiguration Configuration => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }
}
