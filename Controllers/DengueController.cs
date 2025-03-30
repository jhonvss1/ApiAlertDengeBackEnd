using ApiAlertaDengue.Interfaces;
using ApiAlertaDengue.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlertaDengue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DengueController : ControllerBase
{
    private readonly IDengueService  _dengueService;

    public DengueController(IDengueService dengueService)
    {
        _dengueService = dengueService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DengueAlert>>> GetAlerts()
    {
        return Ok(await _dengueService.GetAlertsAsync());
    }

    [HttpGet("{year}/{week}")]
    public async Task<ActionResult<IEnumerable<DengueAlert>>> GetAlertByWeek(int year, int week)
    {
        var alert = await _dengueService.GetAlertByWeekAsync(week, year);
        if(alert == null) return NotFound();
        return Ok(alert);
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> FetchData()
    {
        var result = await _dengueService.FetchAndStoreAlertsAsync();
        return result ? Ok() : BadRequest("Failed to fetch and store data");
    }

    [HttpGet("teste")]
    public IActionResult Test()
    {
        return Ok("Funcionou!!!");
    }
}