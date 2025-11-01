using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Poliza.Queries;
using Application.Features.Poliza.Commands;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;
using Shared.DTOs;
using Shared.Helpers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PvHeaderController : ControllerBase
{
    private readonly IMediator _mediator;

    public PvHeaderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // =====================================
    // GET: api/PvHeader
    // =====================================
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<IEnumerable<PvHeaderQueryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var list = await _mediator.Send(new PvHeaderGetAllQuery());
        return Ok(ApiResponse.Success(list, "Consulta exitosa"));
    }

    // =====================================
    // GET: api/PvHeader/{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}
    // =====================================
    [HttpGet("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    [ProducesResponseType(typeof(ResponseDTO<IEnumerable<PvHeaderQueryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso)
    {
        var item = await _mediator.Send(new PvHeaderGetByIdQuery(cod_suc, cod_ramo, nro_pol, nro_endoso));
        if (item == null) return NotFound(ApiResponse.Fail<object>("Registro no encontrado"));
        return Ok(ApiResponse.Success(item, "Registro encontrado"));
    }

    // =====================================
    // POST: api/PvHeader
    // =====================================
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] PvHeaderCreateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse.Fail("Error de validación", errors));
        }
        var command = dto.ToCommandCreate();
        var result = await _mediator.Send(command);

        if (result == 0)
        {
            return BadRequest(ApiResponse.Fail<object>("No se pudo insertar el registro"));
        }
        // Para PK compuestos se asume que el handler devuelve un objeto con todas las claves
        return CreatedAtAction(nameof(GetById), result, ApiResponse.Success(result, "Registro creado correctamente"));
    }

    // =====================================
    // PUT: api/PvHeader/{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}
    // =====================================
    [HttpPut("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso, [FromBody] PvHeaderUpdateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse.Fail("Error de validación", errors));
        }

        //Inyección de llaves
        dto.CodSuc = cod_suc;
        dto.CodRamo = cod_ramo;
        dto.NroPol = nro_pol;
        dto.NroEndoso = nro_endoso;

        var command = dto.ToUpdateCommand();
        var result = await _mediator.Send(command);
        if (result == 0)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado para actualización"));
        }
        return Ok(ApiResponse.Success(result, "Registro actualizado correctamente"));
    }

    // =====================================
    // DELETE: api/PvHeader/{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}
    // =====================================
    [HttpDelete("{cod_suc}/{cod_ramo}/{nro_pol}/{nro_endoso}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int cod_suc, int cod_ramo, long nro_pol, int nro_endoso)
    {
        var deleted = await _mediator.Send(new PvHeaderDeleteCommand(cod_suc, cod_ramo, nro_pol, nro_endoso));
        if (!deleted)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado para eliminación"));
        }
        return Ok(ApiResponse.Success<object>(null, "Registro eliminado correctamente"));
    }
}