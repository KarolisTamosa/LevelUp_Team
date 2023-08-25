using Domain.IServices;
using Domain.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/Moneda")]
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
            try
            {
                IEnumerable<Moneda> listaMonedas = await _monedaService.GetMonedas();
                return Ok(listaMonedas.ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");//400
            }
        }
    }
}
