using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        private readonly ApplicationDbContext _context;
        public HistorialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(usuarioId));
            }
            return await _context.Historial.Where(historial => historial.IdUsuario.Equals(usuarioId) && !historial.Eliminado).ToListAsync();
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
