using Domain.IRepositories;
using Domain.IServices;
using DTO.Historial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {

        private readonly IHistorialService _monedaService;
        //private readonly IUsuarioService _usuarioService;
        public HistorialController(IHistorialService monedaService/*, IUsuarioService usuarioService*/)
        {
            _monedaService = monedaService;
            //_usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialGetDTO>>> GetHistorialPorUsuario(Guid usuarioId)
        {
            //if (!await _monedaService.AuthorExistsAsync(usuarioId))
            //{
            //    return NotFound();
            //}

            //var coursesForAuthorFromRepo = await _courseLibraryRepository.GetCoursesAsync(authorId);
            //return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
            return null;
        }
    }
}
