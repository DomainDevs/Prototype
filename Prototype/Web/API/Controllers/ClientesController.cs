using Application.DTOs;
using Application.Features.Cliente.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Mappers;
using Application.Features.Cliente.Commands.Delete;

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
        // Obtener todos los clientes
        // ==================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _mediator.Send(new GetAllClientesQuery());
            return Ok(clientes);
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

        // ==================================================
        // Eliminar un cliente
        // ==================================================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteClienteCommand(id));

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
