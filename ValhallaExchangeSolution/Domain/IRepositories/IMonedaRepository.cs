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
        List<Moneda> GetMonedas();
        void MeterMonedas(List<Moneda> monedas);
        Task<Moneda?> ObtenerMonedaPorCodigo(string codigoMoneda);
    }
}
