using Domain.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using DTO.Usuario;
using AutoMapper;
using DTO.Historial;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProyectoAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper,
            IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario([FromRoute] Guid idUsuario)
        {
            try
            {
                var usuarioModel = await _usuarioService.GetUsuarioPorID(idUsuario);
                if (usuarioModel == null)
                {
                    return NotFound( new { message = "el usuario no existe"});
                }
                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioModel);
                return Ok(usuarioDTO);
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
                bool existeUsuarioConEmail = await _usuarioService.ExisteUsuarioConEmailIndicado(usuarioDTO.Email);
                if (existeUsuarioConEmail)
                {
                    return BadRequest(new { message = "El usuario con email " + usuarioDTO.Email + " ya existe!" });
                }
                var usuarioModel = _mapper.Map<Usuario>(usuarioDTO);
                //usuarioModel.PasswordEncriptado = encriptarPasword(usuarioModel.PasswordEncriptado);

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
                var usuarioModel = _mapper.Map<Usuario>(usuarioDTO);
                var usuario = await _usuarioService.ValidarUsuarioParaLogueo(usuarioModel);

                if (usuario == null)
                {
                    return BadRequest(new { message = "Usuario o contrasena invalidos" });
                }
                var usuarioDevolucionDTO = _mapper.Map<UsuarioDTO>(usuario);
                return Ok(usuarioDevolucionDTO);//retornaremos despues el token JWT, por ahora el usuarioDTO
            }
            catch (Exception ex)
            {
                return BadRequest(ex + " - ERROR NUESTRO");
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
