using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MonedaService : IMonedaService
    {
        private readonly IMonedaRepository _monedaRepository;
        private readonly IApiMonedasService _apiMonedasService;
        public MonedaService(IMonedaRepository monedaRepository, IApiMonedasService apiMonedasService)
        {
            _monedaRepository = monedaRepository;
            _apiMonedasService = apiMonedasService;
        }
        public async Task<IEnumerable<Moneda>> GetMonedas()
        {
            return await _monedaRepository.GetMonedas();
        }

        public async Task MeterMonedas(List<Moneda> monedas)
        {
            await _monedaRepository.MeterMonedas(monedas);
        }

        public async Task<Moneda> ObtenerMonedaPorCodigo(string codigoMoneda)
        {
            return await _monedaRepository.ObtenerMonedaPorCodigo(codigoMoneda);
        }

        //pasar 2 monedas + importe y devolver resultado
        public double ObtenerResultadoConvertirMoneda(Moneda monedaOrigen, Moneda monedaDestino, double importe)
        {
            double factor = (monedaDestino.ValorEnDolares / monedaOrigen.ValorEnDolares);
            return importe * factor;
        }

        public async Task ActualizarMonedasDeApiPorDia()
        {
            //recoger monedas de bd

            //recoger monedas de api

            //
        }

        public double ObtenerFactorCambioDeDosMonedas(Moneda monedaOrigen, Moneda monedaDestino)
        {
            if (monedaOrigen == null || monedaDestino == null)
            {
                throw new ArgumentNullException(nameof(monedaOrigen));//lanzar excepcion y la controla el controlador de la api
            }

            return (monedaDestino.ValorEnDolares / monedaOrigen.ValorEnDolares);
        }
    }
}
