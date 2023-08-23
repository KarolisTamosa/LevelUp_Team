using Domain.IServices;
using Domain.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace ProyectoAPI.Controllers
{
    [Route("api/pais")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisservice;



        public PaisController(IPaisService paisService)
        {
            _paisservice = paisService;
        }



        [HttpGet("{Idpais}")]
        public async Task<ActionResult<Pais>> GetPais(Guid IdPais)
        {
            try
            {
                Pais listaPaises = await _paisservice.GetPaisPorId(IdPais);
                return Ok(listaPaises);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
