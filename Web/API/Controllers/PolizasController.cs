// API/Controllers/PolizasController.cs
using Application.UseCase.Emision;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizasController : ControllerBase
    {
        private readonly GetPolizaCompletaUseCase _getPolizaCompletaUseCase;

        public PolizasController(GetPolizaCompletaUseCase getPolizaCompletaUseCase)
        {
            _getPolizaCompletaUseCase = getPolizaCompletaUseCase;
        }

        [HttpGet("{idPv}")]
        public async Task<ActionResult<Polizas?>> Get(int idPv)
        {
            var poliza = await _getPolizaCompletaUseCase.ExecuteAsync(idPv);

            if (poliza == null)
                return NotFound();

            return Ok(poliza);
        }
    }
}
