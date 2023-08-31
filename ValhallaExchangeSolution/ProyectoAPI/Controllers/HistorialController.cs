using AutoMapper;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using DTO.Historial;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Seguridad;
using System.Security.Claims;

namespace ProyectoAPI.Controllers
{
    [Route("api/usuarios/{usuarioId}/historial")]
    [ApiController]
    public class HistorialController : ControllerBase
    {

        private readonly IHistorialService _historialService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMonedaService _monedaService;
        private readonly IMapper _mapper;

        //private readonly IUsuarioService _usuarioService;
        public HistorialController(IHistorialService historialService, IUsuarioService usuarioService, IMonedaService monedaService, IMapper mapper)
        {
            _historialService = historialService;
            _usuarioService = usuarioService;
            _monedaService = monedaService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]//PRIVADO
        public async Task<ActionResult<IEnumerable<HistorialGetDTO>>> GetHistorialPorUsuario([FromRoute] Guid usuarioId, [FromQuery] int res = 10)
        {
            try
            {
                //Obtener id del token de la cabecera de la peticion
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Guid idUsuario = JwtConfigurator.GetTokenUsuario(identity);

                if (idUsuario != usuarioId)
                {
                    return Unauthorized(new { message = "No tienes permiso para acceder al historial" });
                }

                var usuario = await _usuarioService.GetUsuarioPorID(usuarioId);
                if (usuario == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }

                var historialPorUsuarioFromRepo = await _historialService.GetHistorialPorUsuarioConProcedimientoAlmacenado(usuarioId, res);

                if (historialPorUsuarioFromRepo == null || historialPorUsuarioFromRepo.Count() == 0)
                {
                    return NotFound(new { message = "No existen registros de historial de este usuario" });
                }
                IEnumerable<HistorialGetDTO> p = _mapper.Map<IEnumerable<HistorialGetDTO>>(historialPorUsuarioFromRepo);
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }

        }

        [HttpGet("{historialId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<HistorialGetDTO>> GetUnHistorialPorUsuario([FromRoute] Guid usuarioId, [FromRoute] Guid historialId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Guid idUsuario = JwtConfigurator.GetTokenUsuario(identity);

                if (idUsuario != usuarioId)
                {
                    return Unauthorized(new { message = "No tienes permiso para acceder al historial" });
                }


                var usuario = await _usuarioService.GetUsuarioPorID(usuarioId);
                if (usuario == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }

                //TODO: crear metodo repositorio que reciba idhistorial e idUsuario y que devuelva registro
                var unHistorialPorUsuarioFromRepo = await _historialService.GetHistorialById(historialId);

                if (unHistorialPorUsuarioFromRepo == null)
                {
                    return NotFound(new { message = "No existen registros de historial de este historial" });
                }

                return Ok(_mapper.Map<HistorialGetDTO>(unHistorialPorUsuarioFromRepo));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GuardarHistorial([FromRoute] Guid usuarioId, [FromBody] HistorialForCreationDTO historialDTO)
        {
            //guardar historial
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Guid idUsuario = JwtConfigurator.GetTokenUsuario(identity);

                if (idUsuario != usuarioId)
                {
                    return Unauthorized(new { message = "No tienes permiso para guardar el historial" });
                }

                var usuario = await _usuarioService.GetUsuarioPorID(usuarioId);
                if (usuario == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }

                var monedaOrigen = await _monedaService.ObtenerMonedaPorCodigo(historialDTO.CodigoMonedaOrigen);
                var monedaDestino = await _monedaService.ObtenerMonedaPorCodigo(historialDTO.CodigoMonedaDestino);
                if (monedaOrigen == null || monedaDestino == null)
                {
                    return BadRequest(new { message = "Codigos de monedas invalidos" });
                }

                var historialModel = new Historial()
                {
                    IdUsuario = usuarioId,
                    IdMonedaOrigen = monedaOrigen.IdMoneda,
                    IdMonedaDestino = monedaDestino.IdMoneda,
                    FactorCambio = _monedaService.ObtenerFactorCambioDeDosMonedas(monedaOrigen, monedaDestino),//llamar a metodo de _monedaService que retorne el factor dado 2 monedas
                    Importe = historialDTO.Importe,
                    FechaConversion = DateTime.Now,
                    ResultadoConversion = historialDTO.ResultadoConversion,
                    Eliminado = false
                };

                await _historialService.GuardarRegistroDeHistorial(historialModel);
                return Ok(new { message = "Registro de Historial guardado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }

        [HttpDelete("{idHistorial}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> BorrarRegistroHistorialDeUsuario([FromRoute] Guid usuarioId, [FromRoute] Guid idHistorial)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Guid idUsuario = JwtConfigurator.GetTokenUsuario(identity);

                if (idUsuario != usuarioId)
                {
                    return Unauthorized(new { message = "No tienes permiso para borrar el registro del historial" });
                }

                var usuario = await _usuarioService.GetUsuarioPorID(usuarioId);
                if (usuario == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }
                var historial = await _historialService.GetHistorialByIdHistorialEIdUsuario(idHistorial, usuarioId);
                if (historial == null)
                {
                    return NotFound(new { message = "No existe un registro del historial con este id" });
                }
                await _historialService.BorrarRegistroDeHistorial(historial);
                return Ok(new { message = "Registro de historial eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }
    }
}
