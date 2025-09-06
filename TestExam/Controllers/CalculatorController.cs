using Microsoft.AspNetCore.Mvc;

namespace TestExam.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum")]
    public IActionResult Sum(double a, double b)
    {
        var result = a + b;
        _logger.LogInformation($"Calculating sum: {a} + {b} = {result}");
        return Ok(new { a, b, result });
    }
}