using Domain.IServices;
using Domain.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Moneda>>> GetMonedas()
        {
            var listaMonedas = _monedaService.GetMonedas();
            return Ok(listaMonedas);
        }
        //localhost:xxxxx/api/Moneda/eur

        [HttpGet("{codigoMoneda}")]
        public async Task<ActionResult<Moneda>> GetMoneda([FromRoute] string codigoMoneda)
        {
            try
            {
                var moneda = await _monedaService.ObtenerMonedaPorCodigo(codigoMoneda.ToString());
                if (moneda == null)
                {
                    return NotFound();
                }
                return Ok(moneda);

            } catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");//400
            }
            
        }
        [Route("ConvertirMoneda")]
        [HttpPost]
        public async Task<ActionResult<double>> Convertir([FromBody] ConversorDTO conversorDTO)
        {
            try
            {
                string codigoMonedaOrigen = conversorDTO.CodigoMonedaOrigen;
                string codigoMonedaDestino = conversorDTO.CodigoMonedaDestino;
                double importe = conversorDTO.Importe;
                //verificar si existe moneda
                var monedaOrigen = await _monedaService.ObtenerMonedaPorCodigo(codigoMonedaOrigen);
                var monedaDestino = await _monedaService.ObtenerMonedaPorCodigo(codigoMonedaDestino);
                if (monedaOrigen == null || monedaDestino == null|| importe < 0)
                {
                    return BadRequest("Alguno de los codigos de las monedas introducidas no son validas O el importe introduciodo es negativo");
                }
                var resultado = _monedaService.ObtenerResultadoConvertirMoneda(monedaOrigen, monedaDestino, importe);
                return Ok(resultado);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
