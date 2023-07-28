using Negocio;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProcesadorAPIMonedas
    {
        public List<Divisa> RecogerMonedasDesdeApi()
        {
            ResultadoApiMonedas resultadoApiMonedas = Monedas.ImportarMonedasDesdeApi();

            return resultadoApiMonedas.Conversion_Rates.Select(m => new Divisa()
            {
                Nombre = m.Key,
                ValorEnDolares = m.Value
            }).ToList();
        } 
    }
}
