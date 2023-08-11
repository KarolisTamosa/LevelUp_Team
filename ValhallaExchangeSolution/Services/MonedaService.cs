using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
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
        public MonedaService(IMonedaRepository monedaRepository)
        {
            _monedaRepository = monedaRepository;
        }
        public List<Moneda> GetMonedas()
        {
            return _monedaRepository.GetMonedas();
        }

        public void MeterMonedas(List<Moneda> monedas)
        {
            _monedaRepository.MeterMonedas(monedas);
        }
    }
}
