using Domain.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController:ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<Usuario>> GetUsuario([FromRoute] Guid idUsuario)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioPorID(idUsuario);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");//400
            }

        }

    }
}
