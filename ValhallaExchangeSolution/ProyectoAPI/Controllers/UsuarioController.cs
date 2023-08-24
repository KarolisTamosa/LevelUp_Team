using Domain.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using DTO.Usuario;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
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

        [HttpPatch("{idUsuario}")]//DEPRECATED
        public async Task<ActionResult> ActualizarPropiedadesUsuario([FromRoute] Guid idUsuario,
            JsonPatchDocument<UsuarioParaActualizarDTO> usuarioPatchDocument)
        {
            try
            {
                var usuarioModel = await _usuarioService.GetUsuarioPorID(idUsuario);
                if (usuarioModel == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }

                var usuarioToPatch = new UsuarioParaActualizarDTO()
                {
                    Email = usuarioModel.Email,
                    PasswordEncriptado = usuarioModel.PasswordEncriptado
                };

                usuarioPatchDocument.ApplyTo(usuarioToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

                if (!TryValidateModel(usuarioToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                usuarioModel.Email = usuarioToPatch.Email;
                usuarioModel.PasswordEncriptado = usuarioToPatch.PasswordEncriptado;

                //creamos metodo para actualizar
                await _usuarioService.ActualizarUsuario(usuarioModel);



                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");//400
            }
        }

        //CREAR USUARIO
        [Route("registrarUsuario")]
        [HttpPost]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioForCreationDTO usuarioDTO)
        {
            try
                {
                if (usuarioDTO == null)
                {
                    return BadRequest("Formato incorrecto");//400
                }
                //validar nombre de usuario
                //var validateExistence = await _usuarioService.ExisteUsuarioConEmail(usuarioDTO.Email);
                bool existeUsuarioConEmail = await _usuarioService.ExisteUsuarioConEmailIndicado(usuarioDTO.Email);
                if (existeUsuarioConEmail)
                {
                    return BadRequest(new { message = "El usuario con email " + usuarioDTO.Email + " ya existe!" });
                }
                //salvar usuario
                var usuarioModel = new Usuario()
                {
                    Email = usuarioDTO.Email,
                    PasswordEncriptado = usuarioDTO.Password,
                    FechaNacimiento = usuarioDTO.FechaNacimiento,
                    IdPais = usuarioDTO.IdPais
                };
                await _usuarioService.CrearUsuario(usuarioModel);
                return Ok(new { message = "Usuario creado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");
            }
        }


        //CREAR USUARIO
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> LoginUsuario([FromBody] UsuarioForLoginDTO usuarioDTO)
        {
            try
            {
                var usuarioModel = new Usuario()
                {
                    Email = usuarioDTO.Email,
                    PasswordEncriptado = usuarioDTO.Password//llamar a util que lo encripte
                };
                var usuario = await _usuarioService.ValidarUsuarioParaLogueo(usuarioModel);

                if (usuario == null)
                {
                    return BadRequest(new { message = "Usuario o contrasena invalidos" });
                }
                var usuarioDevolucionDTO = new UsuarioDTO()
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    IdPais = usuario.IdPais,
                    Edad = 5
                };
                return Ok(usuarioDevolucionDTO);//retornaremos despues el token JWT, por ahora el usuarioDTO
            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");
            }
        }
    }
}
