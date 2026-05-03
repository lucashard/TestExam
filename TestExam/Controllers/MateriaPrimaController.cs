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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateMateriaPrimaCommand command)
    {
        var updated = await _mediator.Send(command with { Id = id });
        return updated ? NoContent() : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? id, [FromQuery] string? nombre, [FromQuery] string? descripcion)
    {
        var result = await _mediator.Send(new GetAllMateriaPrimaQuery(id, nombre, descripcion));
        return Ok(result);
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
