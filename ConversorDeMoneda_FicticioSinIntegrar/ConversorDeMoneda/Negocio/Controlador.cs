
using ConversorDeMoneda.Negocio;
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
            ultimoID += 1;
        }
        public static double ConvertirMoneda(int tipoMonedaOrigen, int tipoMonedaDestino, double dineroAConvertir)
        {
            Moneda moneda1 = BuscarMonedaPorTipo(tipoMonedaOrigen);
            Moneda moneda2 = BuscarMonedaPorTipo(tipoMonedaDestino);

            var factor = (moneda2.Valor / moneda1.Valor);

            //guardar en historial
            return factor * dineroAConvertir;


        }

        public static double ObtenerFactor(Divisa moneda1, Divisa moneda2)
        {
            return moneda2.ValorEnDolares / moneda1.ValorEnDolares;
        }
        public static void GuardarEnHistorial(Divisa moneda1, Divisa moneda2, double importe, double resultado)
        {
            //ruta historial
            //c:
            //string rutaHistorial = Path.Combine("C:/archivos/historial", "historial.json");
            //string carpetaHistorial = @"C:/archivos/inbox";

            //string rutaHistorial = "historial.json";
            var registro = new HistorialMonedasPorUsuario()
            {
                IdUsuario = 1,
                MonedaOrigen = moneda1,
                MonedaDestino = moneda2,
                Importe = importe,
                FactorCambio = ObtenerFactor(moneda1, moneda2),
                FechaConversion = DateTime.Now,
                Resultado = resultado
            };
            Program.Historial.Add(registro);
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
