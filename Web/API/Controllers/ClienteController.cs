using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Cliente.Queries;
using Application.Features.Cliente.Commands;
using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;
using Shared.DTOs;
using Shared.Helpers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClienteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // =====================================
    // GET: api/Cliente
    // =====================================
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<IEnumerable<ClienteQueryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var list = await _mediator.Send(new ClienteGetAllQuery());
        return Ok(ApiResponse.Success(list, "Consulta exitosa"));
    }

    // =====================================
    // GET: api/Cliente/{id}
    // =====================================
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<IEnumerable<ClienteQueryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _mediator.Send(new ClienteGetByIdQuery(id));
        if (item == null) return NotFound(ApiResponse.Fail<object>("Registro no encontrado"));
        return Ok(ApiResponse.Success(item, "Registro encontrado"));
    }

    // =====================================
    // POST: api/Cliente
    // =====================================
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ClienteCreateRequestDto dto)
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
        return CreatedAtAction(nameof(GetById), new { id = result }, ApiResponse.Success(result, "Registro creado correctamente"));
    }

    // =====================================
    // PUT: api/Cliente/{id}
    // =====================================
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteUpdateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse.Fail("Error de validación", errors));
        }

        //Inyección de llaves
        dto.Id = id;

        var command = dto.ToUpdateCommand();
        var result = await _mediator.Send(command);
        if (result == 0)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado para actualización"));
        }
        return Ok(ApiResponse.Success(result, "Registro actualizado correctamente"));
    }

    // =====================================
    // DELETE: api/Cliente/{id}
    // =====================================
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new ClienteDeleteCommand(id));
        if (!deleted)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado para eliminación"));
        }
        return Ok(ApiResponse.Success<object>(null, "Registro eliminado correctamente"));
    }
}