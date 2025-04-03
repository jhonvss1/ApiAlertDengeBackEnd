using ApiAlertaDengue.Interfaces;
using ApiAlertaDengue.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlertaDengue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DengueController(IDengueService dengueService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAlerts()
    {
        var alerts = await dengueService.GetAlertsAsync();
        if (alerts == null)
        {
            return NoContent();
        }
        return Ok(alerts);
    }

    [HttpGet("{year}/{week}")]
    public async Task<ActionResult> GetAlertByWeek(int year, int week)
    {
            var alert = await dengueService.GetAlertByWeekAsync(week, year);
            if(alert == null) return NotFound("It is necessary to inform year and week " +
                                              "n\"within the range of the last six months");
            return Ok(alert);
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> FetchData()
    {
        var result = await dengueService.FetchAndStoreAlertsAsync();
        return result ? Ok() : BadRequest("Failed to fetch and store data");
    }
}