using Domain.IRepositories;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        private readonly ApplicationDbContext _context;
        public HistorialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId, int numResultados)
        {
            if (usuarioId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(usuarioId));
            }
            return await _context.Historial.Where(historial => historial.IdUsuario.Equals(usuarioId) && !historial.Eliminado).OrderByDescending(historial => historial.FechaConversion).Take(numResultados).ToListAsync();
        }
        public async Task<IEnumerable<Historial>> GetHistorialPorUsuarioConProcedimientoAlmacenado(Guid usuarioId, int numResultados)
        {
            var usuarioIdParam = new SqlParameter("@UsuarioId", usuarioId);
            var numResultadosParam = new SqlParameter("@NumResultados", numResultados);

            return await _context.Historial.FromSqlRaw($"EXEC GetHistorialFromUsuarios @UsuarioId, @NumResultados", usuarioIdParam, numResultadosParam).ToListAsync();
        }

        public async Task GuardarRegistroDeHistorial(Historial historial)
        {
            if (historial == null)
            {
                throw new ArgumentNullException(nameof(historial));
            }
            _context.Add(historial);
            await _context.SaveChangesAsync();
        }

        public async Task BorrarRegistroDeHistorial(Historial historial)
        {
            if (historial == null)
            {
                throw new ArgumentNullException(nameof(historial));
            }
            historial.Eliminado = true;
            _context.Entry(historial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Historial> GetHistorialById(Guid idHistorial)
        {
            if (idHistorial == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(idHistorial));
            }
            return await _context.Historial.Where(historial => historial.IdHistorial.Equals(idHistorial) && !historial.Eliminado).FirstOrDefaultAsync();
        }

        public async Task<Historial> GetHistorialByIdHistorialEIdUsuario(Guid idHistorial, Guid idUsuario)
        {
            if (idHistorial == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(idHistorial));
            }
            if (idUsuario == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(idUsuario));
            }
            return await _context.Historial.Where(historial => historial.IdHistorial.Equals(idHistorial) && historial.IdUsuario.Equals(idUsuario) && !historial.Eliminado).FirstOrDefaultAsync();
        }
    }
}
