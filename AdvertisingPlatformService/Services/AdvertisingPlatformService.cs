using AdvertisingPlatformService.Extensions;
using AdvertisingPlatformService.Interfaces;
using AdvertisingPlatformService.Models;

namespace AdvertisingPlatformService.Services
{
    public class AdvertisingPlatformService : IAdvertisingPlatformService
    {
        private static readonly List<Platform> advertisingPlatforms = [];
        private static readonly string pathToDataFile = "Data:DataFilePath";
        private readonly IAdvertisingPlatformService advertisingPlatformService;

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
        /// The method gets all the advertising platforms from the file asynchronously.
        /// </summary>
        /// <returns>List of all the advertising platforms.</returns>
        async Task<IEnumerable<Platform>> IAdvertisingPlatformService.GetAllAdvertisingPlatforms()
        {
            var data = await advertisingPlatformService.GetAllData();

            var platforms = data
                .SelectMany(x => x.platforms);

            advertisingPlatforms.Clear();
            advertisingPlatforms.AddRange(platforms);

            return advertisingPlatforms; 
        }

        /// <summary>
        /// The method gets the advertising platforms in the specific location from the file asynchronously.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List of advertising platforms for a specific location.</returns>
        async Task<IEnumerable<Platform>> IAdvertisingPlatformService.GetAdvertisingPlatformsByLocation(string location)
        {
            var data = await advertisingPlatformService.GetAllData();

            var matchingPlatforms = data
                .SelectMany(root => root.platforms)
                .Where(platform => platform.base_path == location ||
                       (platform.children != null &&
                        platform.children.Any(child => child.path == location)))
                .Distinct(); 

            advertisingPlatforms.Clear();
            advertisingPlatforms.AddRange(matchingPlatforms);

            return advertisingPlatforms;
        }
    }
}
