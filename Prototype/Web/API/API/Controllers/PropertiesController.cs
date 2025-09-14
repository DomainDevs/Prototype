using Application.DTOs;
using Application.Features.Properties.Queries;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly GetPropertyQueryHandler _queryHandler;

    public PropertiesController(GetPropertyQueryHandler queryHandler) // Inyectamos el handler de la capa de Aplicación
    {
        _queryHandler = queryHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PropertyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
    {
        var query = new GetAllPropertiesQuery();
        var result = await _queryHandler.Handle(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        // Aquí podrías mapear errores de la capa de aplicación a códigos HTTP más específicos
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PropertyDto>> GetById(int id)
    {
        var query = new GetPropertyQuery(id);
        var result = await _queryHandler.Handle(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        // Si el handler devuelve un fallo para GetById, asumimos que no se encontró
        return NotFound(result.ErrorMessage);
    }
}