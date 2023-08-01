
using Datos;

namespace Negocio
{
    public static class ProcesadorAPIMonedas
    {
        private static List<Divisa> listaDivisa = new List<Divisa>();
        private static List<Divisa> ImportarDivisasDesdeApi()
        {
            ResultadoApiMonedas resultadoApiMonedas = Monedas.ImportarMonedasDesdeApi();

            return resultadoApiMonedas.Conversion_Rates.Select(m => new Divisa()
            {
                Nombre = m.Key,
                ValorEnDolares = m.Value
            }).ToList();
        }

        public static List<Divisa> RecogerListaDivisas()
        {
            return listaDivisa;
        }

    }
}
