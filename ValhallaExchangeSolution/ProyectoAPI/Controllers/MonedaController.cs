using Domain.IServices;
using Domain.Models;
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
    }
}
