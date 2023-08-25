using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class MonedaRepository : IMonedaRepository
    {
        private readonly ApplicationDbContext _context;
        public MonedaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Moneda>> GetMonedas()
        {
            return await _context.Monedas.ToListAsync();
        }

        public void BorrarRegistrosDeMoneda() { 
            
        }

        public async Task MeterMonedas(List<Moneda> monedas)
        {
            _context.Monedas.AddRange(monedas);
            await _context.SaveChangesAsync();
        }

        public async Task<Moneda> ObtenerMonedaPorCodigo(string codigoMoneda)
        {
            if (string.IsNullOrEmpty(codigoMoneda))
            {
                throw new ArgumentNullException(nameof(codigoMoneda));
            }
            return await _context.Monedas.Where(moneda => moneda.Codigo.ToUpper().Equals(codigoMoneda.ToUpper())).FirstOrDefaultAsync();
        }


    }
}
