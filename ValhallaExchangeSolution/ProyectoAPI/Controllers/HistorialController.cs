using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<>>> GetCoursesForAuthor(Guid authorId)
        //{
        //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
        //    {
        //        return NotFound();
        //    }

        //    var coursesForAuthorFromRepo = await _courseLibraryRepository.GetCoursesAsync(authorId);
        //    return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
        //}
    }
}
