using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestExam.Controllers;

[ApiController]
[Route("[controller]")]
public class OperarioEtapaController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperarioEtapaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllOperarioEtapaQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _mediator.Send(new GetOperarioEtapaByIdQuery(id));
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOperarioEtapaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateOperarioEtapaCommand command)
    {
        var updated = await _mediator.Send(command with { Id = id });
        return updated ? NoContent() : NotFound();
    }
}

