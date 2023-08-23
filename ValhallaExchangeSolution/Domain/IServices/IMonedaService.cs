using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IMonedaService
    {
        Task<IEnumerable<Moneda>> GetMonedas();
        Task MeterMonedas(List<Moneda> monedas);
        Task<Moneda> ObtenerMonedaPorCodigo(string codigoMoneda);
        double ObtenerResultadoConvertirMoneda(Moneda monedaOrigen, Moneda monedaDestino, double importe);
        double ObtenerFactorCambioDeDosMonedas(Moneda monedaOrigen, Moneda monedaDestino);
    }
}
