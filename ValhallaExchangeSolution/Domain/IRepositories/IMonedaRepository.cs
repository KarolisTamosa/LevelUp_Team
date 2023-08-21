using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IMonedaRepository
    {
        Task<IEnumerable<Moneda>> GetMonedas();
        Task MeterMonedas(List<Moneda> monedas);
        Task<Moneda> ObtenerMonedaPorCodigo(string codigoMoneda);
    }
}
