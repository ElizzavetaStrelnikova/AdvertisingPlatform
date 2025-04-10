using AdvertisingPlatformService.Models;
using System.Text.Json;

namespace AdvertisingPlatformService.Extensions
{
    public static class JsonDeserializer
    {
        private static IConfigurationRoot config;

        public static string GetPathToJson(string dataFilePath)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string jsonFilePath = config[dataFilePath];
            string fullPath = Path.GetFullPath(jsonFilePath);

            return fullPath;
        }

        public static T DeserializeJson<T>(string fullPathToJson) where T : class
        {
            string jsonFile = File.ReadAllText(fullPathToJson);

            var myObject = JsonSerializer.Deserialize<T>(jsonFile);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            var moduleData = JsonSerializer.Deserialize<T>(jsonFile, options);

            return moduleData;
        }
    }
}
