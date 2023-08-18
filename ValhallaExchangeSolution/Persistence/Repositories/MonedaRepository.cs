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
    public class MonedaRepository : IMonedaRepository
    {
        private readonly ApplicationDbContext _context;
        public MonedaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Moneda> GetMonedas()
        {
            return _context.Monedas.ToList();
        }

        public void BorrarRegistrosDeMoneda() { 
            
        }

        public void MeterMonedas(List<Moneda> monedas)
        {
            _context.Monedas.AddRange(monedas);
            _context.SaveChanges();
        }


        //puede devolver nulo
        public async Task<Moneda?> ObtenerMonedaPorCodigo(string codigoMoneda)
        {
            return await _context.Monedas.Where(moneda => moneda.Codigo.ToUpper().Equals(codigoMoneda.ToUpper())).FirstOrDefaultAsync();
        }
    }
}
