using AdvertisingPlatformService.Interfaces;
using AdvertisingPlatformService.Models;

namespace AdvertisingPlatformService.Services
{
    public class AdvertisingPlatformService : IAdvertisingPlatformService
    {
        private static readonly List<AdvertisingPlatform> advertisingPlatforms = [];
        /// <summary>
        /// The method gets all the advertising platforms from the file.
        /// </summary>
        /// <returns>List of all the advertising platforms.</returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<IEnumerable<AdvertisingPlatform>> IAdvertisingPlatformService.GetAllAdvertisingPlatforms()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method gets the advertising platforms in the specific location from the file.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List of advertising platforms for a specific location.</returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<IEnumerable<AdvertisingPlatform>> IAdvertisingPlatformService.GetAdvertisingPlatformsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
