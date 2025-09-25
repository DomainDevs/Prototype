using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Cliente.Queries;
using Application.Features.Cliente.Commands.Create;
using Application.Features.Cliente.Commands.Update;
using Application.Features.Cliente.Commands.Delete;
using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClienteController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _mediator.Send(new GetAllClientesQuery());
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _mediator.Send(new GetClienteByIdQuery(id));
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = dto.ToCommandCreate();
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = dto.ToUpdateCommand();
        var result = await _mediator.Send(command);
        return result == 0 ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteClienteCommand(id));
        return !deleted ? NotFound() : NoContent();
    }
}