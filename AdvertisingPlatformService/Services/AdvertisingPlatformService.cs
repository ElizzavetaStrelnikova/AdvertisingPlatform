using AdvertisingPlatform.Service.Interfaces;
using AdvertisingPlatform.Service.Models;
using System.Text.Json;

namespace AdvertisingPlatform.Service.Services
{
    public class AdvertisingPlatformService : IAdvertisingPlatformService<Platform>
    {
        private readonly ILogger<AdvertisingPlatformService> _logger;
        private DateTime _lastLoadTime;
        private List<Platform> _advertisingPlatforms = new();
        private readonly string _jsonPath;
        private readonly SemaphoreSlim _cacheLock = new(1, 1);

        public AdvertisingPlatformService(IConfiguration config, ILogger<AdvertisingPlatformService> logger)
        {
            _jsonPath = config["Data:DataFilePath"];
            _logger = logger;
            _logger.LogInformation("Service initialized with JSON path: {Path}", _jsonPath);

            _ = InitializeDataAsync();
        }



        /// <summary>
        /// The method gets all the advertising platforms from the file asynchronously.
        /// </summary>
        /// <returns>List of all the advertising platforms.</returns>
        public async Task<IEnumerable<Platform>> GetAllAdvertisingPlatforms()
        {
            try
            {
                await _cacheLock.WaitAsync();
                if (IsCacheStale())
                {
                    await ReloadCacheAsync();
                }
                return new List<Platform>(_advertisingPlatforms); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load advertising platforms");
                return _advertisingPlatforms.Count > 0
                    ? new List<Platform>(_advertisingPlatforms)
                    : Enumerable.Empty<Platform>();
            }
            finally
            {
                _cacheLock.Release();
            }
        }

        /// <summary>
        /// The method gets the advertising platforms in the specific location from the file asynchronously.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List of advertising platforms for a specific location.</returns>
        public async Task<IEnumerable<Platform>> GetAdvertisingPlatformsByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return Enumerable.Empty<Platform>();

            try
            {
                await _cacheLock.WaitAsync();
                if (IsCacheStale())
                {
                    await ReloadCacheAsync();
                }

                return _advertisingPlatforms
                    .Where(p => p.base_path == location ||
                           (p.children?.Any(c => c.path == location) == true))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to filter platforms by location: {location}");
                return Enumerable.Empty<Platform>();
            }
            finally
            {
                _cacheLock.Release();
            }
        }

        #region Private Methods
        /// <summary>
        /// Creates an instance of data at first using a service.
        /// </summary>
        /// <returns>Cached data.</returns>
        private async Task InitializeDataAsync()
        {
            try
            {
                await _cacheLock.WaitAsync();
                await ReloadCacheAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Initial data load failed");
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        /// <summary>
        /// Checks if cached data exists and it is valid.
        /// </summary>
        /// <returns>True/False</returns>
        private bool IsCacheStale()
        {
            return !File.Exists(_jsonPath) ||
                   File.GetLastWriteTimeUtc(_jsonPath) > _lastLoadTime;
        }

        /// <summary>
        /// Reads the data from the json, deserializes it and save it to cache for a sooner use..
        /// </summary>
        /// <returns>Cached data.</returns>
        private async Task ReloadCacheAsync()
        {
            try
            {
                string json = await File.ReadAllTextAsync(_jsonPath);
                var data = JsonSerializer.Deserialize<Root>(json) ?? new Root();

                _advertisingPlatforms.Clear();
                _advertisingPlatforms.AddRange(data.platforms ?? Enumerable.Empty<Platform>());
                _lastLoadTime = DateTime.UtcNow;

                _logger.LogInformation("Cache reloaded. Platforms count: {Count}", _advertisingPlatforms.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cache reload failed");
                throw;
            }
        }
        #endregion
    }
}