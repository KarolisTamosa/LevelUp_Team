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
        List<Moneda> GetMonedas();
        void MeterMonedas(List<Moneda> monedas);
    }
}
