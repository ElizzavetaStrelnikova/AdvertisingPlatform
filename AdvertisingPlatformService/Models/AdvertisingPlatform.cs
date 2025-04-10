namespace AdvertisingPlatformService.Models
{
    public class Child
    {
        public string path { get; set; }
        public string name { get; set; }
    }

    public class Platform
    {
        public string name { get; set; }
        public string base_path { get; set; }
        public List<Child> children { get; set; }
    }

    public class Root
    {
        public List<Platform> platforms { get; set; }
    }
}


