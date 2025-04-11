using AdvertisingPlatform.Service.Interfaces;
using AdvertisingPlatform.Service.Models;
using System.Text.Json;

namespace AdvertisingPlatform.Service.Services
{
    public class AdvertisingPlatformService : IAdvertisingPlatformService<Platform>
    {
        private readonly ILogger<AdvertisingPlatformService> _logger;
        private readonly List<Platform> _advertisingPlatforms = new();
        private readonly string _jsonPath;

        public AdvertisingPlatformService(IConfiguration config, ILogger<AdvertisingPlatformService> logger)
        {
            _jsonPath = config["Data:DataFilePath"];
            _logger = logger;

            _logger.LogInformation("Service initialized with JSON path: {Path}", _jsonPath);
        }

        /// <summary>
        /// Gets a json file and deserializes it.
        /// </summary>
        /// <returns>Completed task of a deserialized json.</returns>
        public async Task<TData> GetAllData<TData>(string path) where TData : class
        {
            string json = await File.ReadAllTextAsync(Path.Combine(path, _jsonPath));
            return JsonSerializer.Deserialize<TData>(json)
                   ?? throw new InvalidOperationException("Deserialization failed");
        }

        /// <summary>
        /// The method gets all the advertising platforms from the file asynchronously.
        /// </summary>
        /// <returns>List of all the advertising platforms.</returns>
        public async Task<IEnumerable<Platform>> GetAllAdvertisingPlatforms()
        {
            _logger.LogInformation("Fetching all advertising platforms");

            try
            {
                var data = await GetAllData<Root>(_jsonPath);

                _logger.LogDebug("Retrieved {Count} root items", data?.platforms?.Count ?? 0);

                _advertisingPlatforms.Clear();
                _advertisingPlatforms.AddRange(data.platforms);

                _logger.LogInformation("Successfully loaded {Count} platforms", _advertisingPlatforms.Count);

                return _advertisingPlatforms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load advertising platforms");
                throw;
            }
        }

        /// <summary>
        /// The method gets the advertising platforms in the specific location from the file asynchronously.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List of advertising platforms for a specific location.</returns>
        public async Task<IEnumerable<Platform>> GetAdvertisingPlatformsByLocation(string location)
        {
            var data = await GetAllData<Root>(_jsonPath);
            if (data?.platforms == null) return Enumerable.Empty<Platform>();

            var matchingPlatforms = data.platforms
                .Where(p => p.base_path == location ||
                       (p.children?.Any(c => c.path == location) == true))
                .ToList();

            _advertisingPlatforms.Clear();
            _advertisingPlatforms.AddRange(matchingPlatforms);
            return _advertisingPlatforms;
        }
    }
}
