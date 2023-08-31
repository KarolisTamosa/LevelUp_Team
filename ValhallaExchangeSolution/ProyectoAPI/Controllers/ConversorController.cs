using Domain.IServices;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/conversor")]
    [ApiController]
    public class ConversorController : ControllerBase
    {
        private readonly IMonedaService _monedaService;
        public ConversorController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<double>> Convertir([FromBody] ConversorDTO conversorDTO)
        {
            try
            {
                string codigoMonedaOrigen = conversorDTO.CodigoMonedaOrigen;
                string codigoMonedaDestino = conversorDTO.CodigoMonedaDestino;
                double importe = conversorDTO.Importe;
                
                var monedaOrigen = await _monedaService.ObtenerMonedaPorCodigo(codigoMonedaOrigen);
                var monedaDestino = await _monedaService.ObtenerMonedaPorCodigo(codigoMonedaDestino);
                if (monedaOrigen == null || monedaDestino == null)
                {
                    return BadRequest(new { message = "Alguno de los codigos de las monedas introducidas no son validos" });
                }
                if (importe < 0)
                {
                    return BadRequest(new { message = "El importe debe ser positivo" });
                }

                var resultado = _monedaService.ObtenerResultadoConvertirMoneda(monedaOrigen, monedaDestino, importe);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }
    }
}
