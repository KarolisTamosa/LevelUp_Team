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
    public class PaisRepository : IPaisRepository
    {
        private readonly ApplicationDbContext _context;
        public PaisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Pais>> GetPais()
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<Pais>> GetPaisPorUsuario(Guid IdPais)
        {
            throw new NotImplementedException();
        }

        public Task MeterPaises(List<Pais> Paises)
        {
            throw new NotImplementedException();
        }

        public async Task<Pais> ObtenerPaisPorId(Guid IdPais)
        {
            return await _context.Paises.Where(pais => pais.IdPais == IdPais).FirstOrDefaultAsync();
        }
    }
}
