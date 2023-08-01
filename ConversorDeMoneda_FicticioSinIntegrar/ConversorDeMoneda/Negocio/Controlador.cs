
using Entidades;

namespace ConversorDeMoneda
{
    static class Controlador
    {
        private static int ultimoID = 0;
        private static List<Moneda> monedas = new List<Moneda>();
        public static void CrearMonedas()
        {
            MeterMoneda(1.00, "Euros");
            MeterMoneda(1.12, "Dolares");
            MeterMoneda(0.87, "Libras");
        }
        public static void MeterMoneda(double valor, string tipo)
        {
            monedas.Add(new Moneda(ultimoID, valor, tipo));
            ultimoID+=1;
        }
        public static double ConvertirMoneda(int tipoMonedaOrigen, int tipoMonedaDestino, double dineroAConvertir)
        {
            Moneda moneda1 = BuscarMonedaPorTipo(tipoMonedaOrigen);
            Moneda moneda2 = BuscarMonedaPorTipo(tipoMonedaDestino);

            var factor = (moneda2.Valor / moneda1.Valor);
            return  factor *  dineroAConvertir;

            
        }

        private static Moneda BuscarMonedaPorTipo(int tipoMoneda)
        {
            return monedas.Find(m => m.Id.Equals(tipoMoneda));
        }
        public static List<String> ObtenerToStringDeMonedas()
        {
            return (from moneda in monedas.Distinct()
                    select moneda.ToString()).ToList(); ;

        }


    }
}
