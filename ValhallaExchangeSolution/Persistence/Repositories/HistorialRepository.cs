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
            return await _context.Historial.Where(historial => historial.IdUsuario.Equals(usuarioId) && !historial.Eliminado).ToListAsync();
        }
    }
}
