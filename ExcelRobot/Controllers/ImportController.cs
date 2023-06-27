using ExcelRobot.BL.Commands.ImportDataToExcel;
using Microsoft.AspNetCore.Mvc;

namespace ExcelRobot.Controllers;

[ApiController]
[Route("[controller]")]
public class ImportController : ControllerBase
{
    [HttpPost]
    public IActionResult Index(ImportDataToExcelCommand command)
    {
        var response = new ImportDataToExcelCommandHandler().Handle(command);
        return Ok(response);
    }
}