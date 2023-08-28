using AutoMapper;
using Domain.IServices;
using Domain.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/monedas")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _monedaService;
        private readonly IMapper _mapper;


        public MonedaController(IMonedaService monedaService, IMapper mapper)
        {
            _monedaService = monedaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MonedaDTO>>> GetMonedas()
        {
            try
            {
                IEnumerable<Moneda> listaMonedas = await _monedaService.GetMonedas();
                return Ok(_mapper.Map<IEnumerable<MonedaDTO>>(listaMonedas));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //localhost:xxxxx/api/Moneda/eur

        [HttpGet("{codigoMoneda}")]
        public async Task<ActionResult<MonedaDTO>> GetMoneda([FromRoute] string codigoMoneda)
        {
            try
            {
                var moneda = await _monedaService.ObtenerMonedaPorCodigo(codigoMoneda.ToString());
                if (moneda == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<MonedaDTO>(moneda));

            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");//400
            }
        }
    }
}
