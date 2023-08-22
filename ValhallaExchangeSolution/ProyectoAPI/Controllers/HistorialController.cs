using AutoMapper;
using Domain.IRepositories;
using Domain.IServices;
using DTO.Historial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/usuarios/{usuarioId}/historial")]
    [ApiController]
    public class HistorialController : ControllerBase
    {

        private readonly IHistorialService _monedaService;
        private readonly IMapper _mapper;

        //private readonly IUsuarioService _usuarioService;
        public HistorialController(IHistorialService monedaService/*, IUsuarioService usuarioService*/)
        {
            _monedaService = monedaService;
            //_usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialGetDTO>>> GetHistorialPorUsuario([FromRoute] Guid usuarioId)
        {
            //if (!await _monedaService.AuthorExistsAsync(usuarioId))
            //{
            //    return NotFound(new { message = "No existen registros de este usuario"});
            //}

            var historialPorUsuarioFromRepo = await _monedaService.GetHistorialPorUsuario(usuarioId);
            
            if (historialPorUsuarioFromRepo == null)
            {
                return NotFound( new { message = "No existen registros de historial de este usuario"});
            }

            return Ok(_mapper.Map<IEnumerable<HistorialGetDTO>>(historialPorUsuarioFromRepo));
        }
    }
}
