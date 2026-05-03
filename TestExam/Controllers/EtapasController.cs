using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestExam.Controllers;

[ApiController]
[Route("[controller]")]
public class EtapasController : ControllerBase
{
    private readonly IMediator _mediator;

    public EtapasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllEtapasQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _mediator.Send(new GetEtapasByIdQuery(id));
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEtapasCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateEtapasCommand command)
    {
        var updated = await _mediator.Send(command with { Id = id });
        return updated ? NoContent() : NotFound();
    }
}

