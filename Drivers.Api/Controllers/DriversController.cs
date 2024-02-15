using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/controller]")]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;
    private readonly DriverServices _driverService;

    public DriversController(
        ILogger<DriversController> logger,
        DriverServices driverServices)
    {
        _logger = logger;
        _driverService=driverServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverService.GetAsync();
        return Ok(drivers);
    }
}
