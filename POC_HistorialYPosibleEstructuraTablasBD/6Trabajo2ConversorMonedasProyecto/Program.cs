namespace _6Trabajo2ConversorMonedasProyecto
{
    public class Program
    {
        private static List<HistorialMonedasPorUsuario> historial;
        public static void Main(string[] args)
        {
            if (historial == null) {
                historial = new List<HistorialMonedasPorUsuario>(); 
            }
            IniciarPrograma();
        }
        private static void IniciarPrograma()
        {
            bool salir = false;
            while (!salir)
            {
                Moneda monedaOrigen, monedaDestino;
                double importe;
                MostrarOpcionConvertirMoneda(out monedaOrigen, out monedaDestino, out importe);

                GuardarEnHistorial(1, monedaOrigen, monedaDestino, importe);

                mostrarHistorial();

                Console.WriteLine("Desea convertir otro importe? (pulse cualquier tecla) / n");
                var opcion = Console.ReadLine();
                if (opcion?.ToUpper() == "N")
                {
                    salir = true;
                }

            }
            Console.WriteLine("Gracias por usar nuestra aplicacion");
        }

        private static void MostrarOpcionConvertirMoneda(out Moneda monedaOrigen, out Moneda monedaDestino, out double importe)
        {
            Console.Clear();
            //CadenaEsTipoCorrecto("1", "number");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Bienvenido al conversor de monedas");
            Console.WriteLine("----------------------------------");

            Console.WriteLine("Elija moneda de origen: ");
            Console.WriteLine("Dolares: 1");
            Console.WriteLine("Euros: 2");
            Console.WriteLine("Libras: 3");
            var strMonedaOrigen = Console.ReadLine();
            while (!ValidarOpcionMonedaCorrecta(strMonedaOrigen))
            {
                Console.WriteLine("Elija una opcion correcta");
                strMonedaOrigen = Console.ReadLine();
            }
            monedaOrigen = ObtenerMonedaPorEntero(int.Parse(strMonedaOrigen));
            Console.WriteLine("Elija moneda de destino: ");
            Console.WriteLine("Dolares: 1");
            Console.WriteLine("Euros: 2");
            Console.WriteLine("Libras: 3");
            var monedaDestinoStr = Console.ReadLine();
            while (!ValidarOpcionMonedaCorrecta(monedaDestinoStr))
            {
                Console.WriteLine("Elija una opcion correcta");
                monedaDestinoStr = Console.ReadLine();
            }
            monedaDestino = ObtenerMonedaPorEntero(int.Parse(monedaDestinoStr));
            Console.WriteLine("Elija el importe: ");
            var importeStr = Console.ReadLine();
            while (!ValidarImporte(importeStr))
            {
                Console.WriteLine("Elija un importe valido");
                importeStr = Console.ReadLine();
            }
            importe = double.Parse(importeStr);
            var resultado = ConversorMonedas.ConvertirMoneda(monedaOrigen, monedaDestino, importe);

            Console.WriteLine($"{importe} {monedaOrigen} equivalen a {resultado} {monedaDestino}");
        }

        private static void mostrarHistorial()
        {
            Console.WriteLine("\n\n------------------------------------------");
            foreach (var registro in historial)
            {
                Console.WriteLine(registro.ToString());
            }
            Console.WriteLine("------------------------------------------");
        }


        //private static void CadenaEsTipoCorrecto(string valor, string tipo)
        //{
        //    switch (valor.ToUpper())
        //    {
        //        case: ""
        //    }
        //}

        //guar

        //dto idUsuario, monedaOrigen, monedaDestino, importe, factorCambio, fecha


        private static void GuardarEnHistorial(int idUsuario, Moneda monedaOrigen, Moneda monedaDestino, double importe)
        {
            var factor = 1.1;
            var fechaActual = DateTime.Now;
            var resultado = ConversorMonedas.ConvertirMoneda(monedaOrigen, monedaDestino, importe);
            var historialMonedas = new HistorialMonedasPorUsuario(idUsuario, monedaOrigen, monedaDestino, importe, factor, fechaActual, resultado);
            historial.Add(historialMonedas);
        }

        private static bool ValidarImporte(string importeStr)
        {
            try { double.Parse(importeStr); return true; } catch { return false; };
        }

        private static bool ValidarOpcionMonedaCorrecta(string opcion)
        {
            if (opcion == "1" || opcion == "2" || opcion == "3")
            {
                return true;
            }
            return false;
        }

        private static Moneda ObtenerMonedaPorEntero(int valor)
        {


            switch (valor)
            {
                case 1:
                    return Moneda.Dolares;
                case 2:
                    return Moneda.Euros;
                default:
                    return Moneda.Libras;
            }
        }
    }
}