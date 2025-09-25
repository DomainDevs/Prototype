using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Poliza.Queries;
using Application.Features.Poliza.Commands.Create;
using Application.Features.Poliza.Commands.Update;
using Application.Features.Poliza.Commands.Delete;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PvHeaderController : ControllerBase
{
    private readonly IMediator _mediator;

    public PvHeaderController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _mediator.Send(new GetAllPvHeaderQuery());
        return Ok(list);
    }

    [HttpGet("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    public async Task<IActionResult> GetById(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso)
    {
        var item = await _mediator.Send(new GetPvHeaderByIdQuery(cod_suc, cod_ramo, nro_pol, nro_endoso));
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PvHeaderRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = dto.ToCommandCreate();
        var result = await _mediator.Send(command);
        // Para PK compuestos se asume que el handler devuelve un objeto con todas las claves
        return CreatedAtAction(nameof(GetById), result, result);
    }

    [HttpPut("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    public async Task<IActionResult> Update(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso, [FromBody] PvHeaderRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = dto.ToUpdateCommand();
        var result = await _mediator.Send(command);
        return result == 0 ? NotFound() : NoContent();
    }

    [HttpDelete("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    public async Task<IActionResult> Delete(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso)
    {
        var deleted = await _mediator.Send(new DeletePvHeaderCommand(cod_suc, cod_ramo, nro_pol, nro_endoso));
        return !deleted ? NotFound() : NoContent();
    }
}