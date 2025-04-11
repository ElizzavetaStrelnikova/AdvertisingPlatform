namespace AdvertisingPlatform.Service.Interfaces
{
    public interface IAdvertisingPlatformService<T> where T : class
    {
        Task<TData> GetAllData<TData>(string path) where TData : class;
        Task<IEnumerable<T>> GetAllAdvertisingPlatforms();
        Task<IEnumerable<T>> GetAdvertisingPlatformsByLocation(string location);
    }
}
