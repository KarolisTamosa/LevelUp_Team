using Datos;

namespace Negocio
{
    public static class ProcesadorDivisas
    {
        public static List<Divisa> CambiarResultadoApiMonedasAListaDivisa(ResultadoApiDivisas resultadoApiMonedas)
        {
            return resultadoApiMonedas.Conversion_Rates.Select(m => new Divisa()
            {
                Nombre = m.Key,
                ValorEnDolares = m.Value
            }).ToList();
        }

        public static ResultadoApiDivisas CambiarListaDivisaAResultadoApiMonedas(List<Divisa> divisas)
        {

            ResultadoApiDivisas resultApiMonedas = ProcesadorArchivoJSON.CogerResultadoApiMonedasDeJson();

            resultApiMonedas.Conversion_Rates = divisas.ToDictionary(d => d.Nombre, d => d.ValorEnDolares);

            return resultApiMonedas;
        }
    }
}
