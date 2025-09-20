using Application.DTOs;
using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Mappers;

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

        // ==================================================
        // Obtener un cliente por Id
        // ==================================================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _mediator.Send(new GetClienteByIdQuery(id));

            if (cliente is null)
                return NotFound();

            return Ok(cliente);
        }

        // ==================================================
        // Crear un nuevo cliente
        // ==================================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = dto.ToCommandCreate(); //dto -> command
            var newId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
        }

        // ==================================================
        // Actualizar un cliente existente
        // ==================================================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = dto.ToUpdateCommand(); //dto -> command
            var updatedId = await _mediator.Send(command);

            if (updatedId == 0)
                return NotFound();

            return NoContent();
        }
    }
}
