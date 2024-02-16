using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{


    private readonly ILogger<DriversController> _logger;
    private readonly DriverServices _driverServices;

    public DriversController(ILogger<DriversController> logger, DriverServices driverServices)
    {
        _logger = logger;
        _driverServices = driverServices;

    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverServices.GetAsync();
        return Ok(drivers);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriverByID(string id)
    {
        return Ok(await _driverServices.GetDriverById(id));
    }
    [HttpPost]
    public async Task<IActionResult> InsertNewDriver([FromBody] Driver drive)
    {
        if (drive == null)
            return BadRequest();

        if (drive.Name == string.Empty) ModelState.AddModelError("Name", "El Driver no debe estar vacío");
        await _driverServices.InsertDriver(drive);
        return Created("Created", true);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarDriver([FromBody] Driver driver, string id)
    {
        if (driver == null)
            return BadRequest();

        if (driver.Name == string.Empty) ModelState.AddModelError("Name", "El Driver no debe estar vacío");
        driver.Id = id;
        await _driverServices.UpdateDriver(driver);
        return Created("Created", true);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(string id)
    {
        await _driverServices.DeleteDriver(id);
        return NoContent();
    }
}
