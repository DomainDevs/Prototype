using Microsoft.AspNetCore.Mvc;
using Application.Features.Localidad.DTOs;
using Application.Features.Localidad.Mappers;
using Application.Features.Localidad.Services;
using Shared.DTOs;
using Shared.Helpers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly IDepartamentoService _service;

    public DepartamentoController(IDepartamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<IEnumerable<object>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        var response = ApiResponse.Success(items, "Consulta exitosa");
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado"));
        }

        var response = ApiResponse.Success(item, "Registro encontrado");
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] DepartamentoCreateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(ApiResponse.Fail("Error de validación", errors));
        }

        var id = await _service.CreateAsync(dto);
        var response = ApiResponse.Success(id, "Registro creado correctamente");
        return CreatedAtAction(nameof(GetById), new { id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] DepartamentoUpdateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(ApiResponse.Fail("Error de validación", errors));
        }

        dto.Id = id;

        await _service.UpdateAsync(dto);
        var response = ApiResponse.Success<object>(null, "Registro actualizado correctamente");
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(ApiResponse.Fail<object>("Registro no encontrado para eliminación"));
        }

        var response = ApiResponse.Success<object>(null, "Registro eliminado correctamente");
        return Ok(response);
    }
}