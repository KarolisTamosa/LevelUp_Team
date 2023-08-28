using AutoMapper;
using Domain.IServices;
using Domain.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace ProyectoAPI.Controllers
{
    [Route("api/paises")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisservice;
        private readonly IMapper _mapper;



        public PaisController(IPaisService paisService, IMapper mapper)
        {
            _paisservice = paisService;
            _mapper = mapper;
            _mapper=mapper;
        }



        [HttpGet("{Idpais}")]
        public async Task<ActionResult<PaisesDTO>> GetPais(Guid IdPais)
        {
            try
            {
                Pais pais = await _paisservice.GetPaisPorId(IdPais);
                return Ok(_mapper.Map<PaisesDTO>(pais));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisesDTO>>> GetPaises()
        {
            try
            {
                IEnumerable<Pais> listaPaises = await _paisservice.GetPaises();
                return Ok(_mapper.Map<IEnumerable<PaisesDTO>>(_mapper.Map<IEnumerable<PaisesDTO>>(listaPaises)));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
