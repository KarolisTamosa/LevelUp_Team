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
using Utils;
using ProyectoAPI.Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
                    return NotFound(new { message = "el usuario no existe" });
                }
                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioModel);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch/*("{idUsuario}")*/]
        public async Task<IActionResult> ActualizarPropiedadesUsuario([FromBody] JsonPatchDocument<UsuarioParaActualizarDTO> patchDoc)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Guid idUsuario = JwtConfigurator.GetTokenUsuario(identity);

                if (patchDoc == null)
                {
                    return BadRequest(new { message = "El documento de parche JSON no puede ser nulo." });
                }

                var usuario = await _usuarioService.GetUsuarioPorID(idUsuario);
                if (usuario == null)
                {
                    return NotFound(new { message = "No existe un usuario con este id" });
                }

                var usuarioToUpdate = _mapper.Map<UsuarioParaActualizarDTO>(usuario);

                // Aplica las operaciones de parche al DTO de actualización a usuarioToUpdate
                patchDoc.ApplyTo(usuarioToUpdate, ModelState);//re puede setear el mismo email

                //ahora usuarioToUpdate se ha actualizado en memoria con el correo
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = ModelState.ToString() });
                }

                if (!(usuarioToUpdate.Email.ToUpper() == usuario.Email.ToUpper()))//si pasa un email distinto
                {
                    //verificar el email pasado no existe
                    bool existeUsuarioConEmailPasado = await _usuarioService.ExisteUsuarioConEmailIndicado(usuarioToUpdate.Email);
                    if (existeUsuarioConEmailPasado)
                    {
                        return BadRequest(new { message = $"El email {usuarioToUpdate.Email} ya esta siendo utilizado" });
                    }
                }

                //TODO: mas validaciones de email y password como el formato email, longitud caracteres, caracteres especiales ....
                bool esEmailValido = UsuarioUtils.EsEmailConFormatoValido(usuarioToUpdate.Email);
                if (!esEmailValido)
                {
                    return BadRequest(new { message = "Correo con formato invalido" });
                }
                bool esPasswordValida = UsuarioUtils.EsPasswordConFormatoValido(usuarioToUpdate.Password, 4, 16);
                if (!esPasswordValida)
                {
                    return BadRequest(new { message = "Contraseña con formato invalido, debe tener ente 4 y 16 caracteres" });
                }

                usuario.Email = usuarioToUpdate.Email;
                usuario.PasswordEncriptado = usuarioToUpdate.Password;

                bool exito = await _usuarioService.ActualizarUsuario(usuario);

                if (!exito)
                {
                    return BadRequest(new { message = "Error al actualizar el usuario" });
                }

                return Ok(new { message = "Uusario actualizado con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
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

                if (string.IsNullOrEmpty(usuarioDTO.Email) || string.IsNullOrEmpty(usuarioDTO.Password) || usuarioDTO.FechaNacimiento == null || usuarioDTO.IdPais == null || usuarioDTO.IdPais == Guid.Empty)
                {
                    return BadRequest(new { message = "Debes completar todos los campos" });
                }

                bool existeUsuarioConEmail = await _usuarioService.ExisteUsuarioConEmailIndicado(usuarioDTO.Email);
                if (existeUsuarioConEmail)
                {
                    return BadRequest(new { message = "El usuario con email " + usuarioDTO.Email + " ya existe!" });
                }
                //VEIFICACIONES DE FORMATO
                bool esEmailValido = UsuarioUtils.EsEmailConFormatoValido(usuarioDTO.Email);
                if (!esEmailValido)
                {
                    return BadRequest(new { message = "Correo con formato invalido" });
                }
                bool esPasswordValida = UsuarioUtils.EsPasswordConFormatoValido(usuarioDTO.Password, 4, 16);
                if (!esPasswordValida)
                {
                    return BadRequest(new { message = "Contraseña con formato invalido, debe tener ente 4 y 16 caracteres" });
                }
                bool esMayorDeEdad = UsuarioUtils.EsEdadPermitida(usuarioDTO.FechaNacimiento);
                if (!esMayorDeEdad)
                {
                    return BadRequest(new { message = "Debes tener mas de 18 años para poder registrarte" });
                }

                var usuarioModel = _mapper.Map<Usuario>(usuarioDTO);
                //TODO: Crear un metodo estatico en Utils para encriptar password y guardarlo en la bbdd encriptada // usuarioModel.PasswordEncriptado = encriptarPasword(usuarioModel.PasswordEncriptado);

                await _usuarioService.CrearUsuario(usuarioModel);
                return Ok(new { message = "Usuario creado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
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
                //var usuarioDevolucionDTO = _mapper.Map<UsuarioDTO>(usuario);
                //return Ok(usuarioDevolucionDTO);//retornaremos despues el token JWT, por ahora el usuarioDTO
                string tokenString = JwtConfigurator.GetToken(usuario, _configuration);
                return Ok(new { token = tokenString });//despues devolveremos el token (con id y todo), pero por ahora devolvemos mensaje
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.GetType() + " - ERROR DE SERVIDOR " });
            }
        }

    }
}
