using Microsoft.AspNetCore.Mvc;

namespace ExcelRobot.Controllers;

[ApiController]
[Route("[controller]")]
public class ImportController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}