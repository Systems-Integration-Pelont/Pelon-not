using Microsoft.AspNetCore.Mvc;

namespace bank_atm.controllers;

[ApiController]
[Route("[controller]")]
public class AtmController: ControllerBase
{
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        return Ok(new { Service = "TargetService", Status = "Running" });
    }
}