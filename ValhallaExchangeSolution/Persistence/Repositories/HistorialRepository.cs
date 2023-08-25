using Domain.IRepositories;
using Domain.Models;
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
            return await _context.Historial.Where(historial => historial.IdUsuario.Equals(usuarioId) && !historial.Eliminado).OrderByDescending(historial => historial.FechaConversion).Take(numResultados)
    .ToListAsync();
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
    }
}
