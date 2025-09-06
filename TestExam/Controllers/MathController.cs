using Microsoft.AspNetCore.Mvc;

namespace TestExam.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    [HttpPost("sum")]
    public IActionResult Sum([FromBody] SumRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request body is required");
        }

        var result = request.Number1 + request.Number2;
        return Ok(new SumResponse { Result = result });
    }

    [HttpGet("sum")]
    public IActionResult Sum([FromQuery] double number1, [FromQuery] double number2)
    {
        var result = number1 + number2;
        return Ok(new SumResponse { Result = result });
    }
}

public class SumRequest
{
    public double Number1 { get; set; }
    public double Number2 { get; set; }
}

public class SumResponse
{
    public double Result { get; set; }
}