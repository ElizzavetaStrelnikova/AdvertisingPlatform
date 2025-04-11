using AdvertisingPlatform.Service.Controllers;
using AdvertisingPlatform.Service.Interfaces;
using AdvertisingPlatform.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdvertisingPlatform.Service.Tests
{
    public class AdvertisingPlatformControllerTests
    {
        private readonly Mock<IAdvertisingPlatformService<Platform>> _mockService = new();
        private readonly AdvertisingPlatformController _controller;

        public AdvertisingPlatformControllerTests()
        {
            _controller = new AdvertisingPlatformController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAdvertisingPlatforms_ReturnsOkResult()
        {
            var testPlatforms = new List<Platform> { new(), new() };
            _mockService.Setup(x => x.GetAllAdvertisingPlatforms())
                       .ReturnsAsync(testPlatforms);

            var result = await _controller.GetAllAdvertisingPlatforms();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPlatforms = Assert.IsAssignableFrom<IEnumerable<Platform>>(okResult.Value);
            Assert.Equal(2, returnedPlatforms.Count());
        }

        [Fact]
        public async Task GetAdvertisingPlatformsByLocation_ReturnsFilteredResults()
        {
            var testPlatforms = new List<Platform> { new() { base_path = "/test" } };
            _mockService.Setup(x => x.GetAdvertisingPlatformsByLocation(It.IsAny<string>()))
                       .ReturnsAsync(testPlatforms);

            var result = await _controller.GetAdvertisingPlatformsByLocation("/test");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPlatforms = Assert.IsAssignableFrom<IEnumerable<Platform>>(okResult.Value);
            Assert.Single(returnedPlatforms);
        }

        [Fact]
        public async Task GetAdvertisingPlatformsByLocation_ReturnsEmptyForNoMatch()
        {
            _mockService.Setup(x => x.GetAdvertisingPlatformsByLocation(It.IsAny<string>()))
                       .ReturnsAsync(Enumerable.Empty<Platform>());

            var result = await _controller.GetAdvertisingPlatformsByLocation("/nonexistent");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Empty((IEnumerable<Platform>)okResult.Value);
        }
    }
}