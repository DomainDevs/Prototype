// WebApi/Controllers/ClientesController.cs
using Application.DTOs;
using Application.Features.Clientes.Commands;
using Application.Features.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetClienteByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteRequestDto dto)
        {
            var id = await _mediator.Send(new CreateClienteCommand(dto.Nombre, dto.Apellido, dto.Email));
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
    }
}
