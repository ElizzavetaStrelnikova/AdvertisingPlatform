using AdvertisingPlatformService.Models;

namespace AdvertisingPlatformService.Interfaces
{
    public interface IAdvertisingPlatformService
    {
        Task<IEnumerable<AdvertisingPlatform>> GetAllAdvertisingPlatforms();
        Task<IEnumerable<AdvertisingPlatform>> GetAdvertisingPlatformsByLocation(string location);
    }
}
