using AdvertisingPlatformService.Models;

namespace AdvertisingPlatformService.Interfaces
{
    public interface IAdvertisingPlatformService
    {
        Task<IEnumerable<Root>> GetAllData();
        Task<IEnumerable<Platform>> GetAllAdvertisingPlatforms();
        Task<IEnumerable<Platform>> GetAdvertisingPlatformsByLocation(string location);
    }
}
