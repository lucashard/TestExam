using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestExam.Controllers;

[ApiController]
[Route("[controller]")]
public class MateriaPrimaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MateriaPrimaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMateriaPrimaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var materiaPrima = await _mediator.Send(new GetMateriaPrimaByIdQuery(id));
        if (materiaPrima == null)
            return NotFound();
        return Ok(materiaPrima);
    }
}
