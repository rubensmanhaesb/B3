using B3.API.Models;
using B3.API.Request;
using B3.Application.Dtos;
using B3.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B3.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CalculadoraCdbController(ICalculadoraCdbApplicationService applicationService) : ControllerBase

    {
        private readonly ICalculadoraCdbApplicationService _applicationService = applicationService;


        [ProducesResponseType(typeof(CdbResultDto), 200)]
        [ProducesResponseType(typeof(ErroResponse), 500)]
        [HttpPost]
        public async Task<IActionResult> Calcular([FromBody] CdbRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            CdbResultDto resultado = await _applicationService.CalcularAsync(request.ValorInicial, request.PrazoEmMeses, cancellationToken).ConfigureAwait(false);

            return Ok(resultado);
        }

    }
}
