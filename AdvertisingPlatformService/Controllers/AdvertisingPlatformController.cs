using AdvertisingPlatformService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatformService.Controllers;

[ApiController]
[Route("api/platforms")]
public class AdvertisingPlatformController : ControllerBase
{
    private readonly IAdvertisingPlatformService advertisingPlatformService;

    public AdvertisingPlatformController(IAdvertisingPlatformService advertisingPlatformService)
    {
        advertisingPlatformService = advertisingPlatformService;
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
