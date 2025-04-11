using AdvertisingPlatform.Service.Interfaces;
using AdvertisingPlatform.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatform.Service.Controllers;

[ApiController]
[Route("api/platforms")]
public class AdvertisingPlatformController : ControllerBase
{
    private readonly IAdvertisingPlatformService<Platform> advertisingPlatformService;
    public AdvertisingPlatformController(IAdvertisingPlatformService<Platform> advertisingPlatformService)
    {
        this.advertisingPlatformService = advertisingPlatformService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdvertisingPlatforms()
    {
        var platforms = await advertisingPlatformService.GetAllAdvertisingPlatforms();
        return Ok(platforms);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAdvertisingPlatformsByLocation([FromQuery] string location)
    {
        var platforms = await advertisingPlatformService.GetAdvertisingPlatformsByLocation(location);
        return Ok(platforms);
    }
}
