namespace AdvertisingPlatform.Service.Models
{
    public class PlatformChild
    {
        public string path { get; set; }
        public string name { get; set; }
    }

    public class Platform
    {
        public string name { get; set; }
        public string base_path { get; set; }
        public List<PlatformChild> children { get; set; }
    }

    public class Root
    {
        public List<Platform> platforms { get; set; }
    }
}


