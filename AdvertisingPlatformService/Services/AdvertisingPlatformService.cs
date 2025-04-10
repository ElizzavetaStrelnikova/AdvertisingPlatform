using AdvertisingPlatformService.Extensions;
using AdvertisingPlatformService.Interfaces;
using AdvertisingPlatformService.Models;
using System.Text.Json;

namespace AdvertisingPlatformService.Services
{
    public class AdvertisingPlatformService : IAdvertisingPlatformService
    {
        private static readonly List<Platform> advertisingPlatforms = [];
        private static readonly string pathToDataFile = "Data:DataFilePath";

        /// <summary>
        /// Gets a json file and deserializes it.
        /// </summary>
        /// <returns>Completed task of a deserialized json.</returns>
        Task<IEnumerable<Root>> IAdvertisingPlatformService.GetAllData()
        {
            var json = JsonDeserializer.GetPathToJson(pathToDataFile);
            var data = JsonDeserializer.DeserializeJson<IEnumerable<Root>>(json);
            return Task.FromResult(data);
        }

        /// <summary>
        /// The method gets all the advertising platforms from the file.
        /// </summary>
        /// <returns>List of all the advertising platforms.</returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<IEnumerable<Platform>> IAdvertisingPlatformService.GetAllAdvertisingPlatforms()
        {
            
        }

        /// <summary>
        /// The method gets the advertising platforms in the specific location from the file.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List of advertising platforms for a specific location.</returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<IEnumerable<Platform>> IAdvertisingPlatformService.GetAdvertisingPlatformsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
