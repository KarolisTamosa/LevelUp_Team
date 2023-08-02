using ConversorDeMoneda.Negocio;
using Entidades;

namespace ConversorDeMoneda
{
    public class Program
    {
        public static List<HistorialMonedasPorUsuario> Historial;
        static void Main(string[] args)
        {
            {
                VerificarHistorialVacio();
                Console.WriteLine("                                                                          ");
                Console.WriteLine("                                                                          ");
                Console.WriteLine("░█████╗░░█████╗░███╗░░██╗██╗░░░██╗███████╗██████╗░░██████╗░█████╗░██████╗░");
                Console.WriteLine("██╔══██╗██╔══██╗████╗░██║██║░░░██║██╔════╝██╔══██╗██╔════╝██╔══██╗██╔══██╗");
                Console.WriteLine("██║░░╚═╝██║░░██║██╔██╗██║╚██╗░██╔╝█████╗░░██████╔╝╚█████╗░██║░░██║██████╔╝");
                Console.WriteLine("██║░░██╗██║░░██║██║╚████║░╚████╔╝░██╔══╝░░██╔══██╗░╚═══██╗██║░░██║██╔══██╗");
                Console.WriteLine("╚█████╔╝╚█████╔╝██║░╚███║░░╚██╔╝░░███████╗██║░░██║██████╔╝╚█████╔╝██║░░██║");
                Console.WriteLine("░╚════╝░░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═════╝░░╚════╝░╚═╝░░╚═╝");
                Console.WriteLine("                                                                          ");
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║   1. Conversor de monedas                                              ║");
                Console.WriteLine("║   2. Listado de divisas                                                ║");
                Console.WriteLine("║   3. Editor de divisas                                                 ║");
                Console.WriteLine("║   4. Historial de conversiones                                         ║");
                Console.WriteLine("║   5. Salir                                                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine("                         Powered by LevelUp_Team                          ");
                Console.WriteLine("                                                                          ");
                Console.WriteLine("          Escribe el número de la opción que deseas realizar.             ");
                Console.ReadLine();
            }
            Program programa = new Program();

            Controlador.CrearMonedas();

            int monedaOrigen = programa.PedirTipoMoneda("origen");
            int monedaDestino = programa.PedirTipoMoneda("destino");
            double dineroAConvertir = programa.PedirDineroAConvertir();

            Console.WriteLine(Controlador.ConvertirMoneda(monedaOrigen, monedaDestino, dineroAConvertir));

            MostrarHistorial();
         }

        private static void MostrarHistorial()
        {
            foreach(var registro in Historial) { 
                Console.WriteLine(registro.ToString());
            }
        }

        private static void VerificarHistorialVacio()
        {
            if (Historial == null)
            {
                Historial = new List<HistorialMonedasPorUsuario>();
            }
        }

        private double PedirDineroAConvertir()
        {
            bool dineroCorrecto = false;
            double dineroAConvertir = 0;

            do
            {
                Console.Write("Ingresa el dinero a convertir: ");
                if(double.TryParse(Console.ReadLine(), out dineroAConvertir))
                {
                    if (dineroAConvertir >= 0)
                    {
                        dineroCorrecto = true;

                    }
                }

            } while (!dineroCorrecto);
            return dineroAConvertir;
        }

        private int PedirTipoMoneda(string tipoMoneda)
        {
            bool respuestaCorrecta = false;
            int moneda = 0;

            string comentario = "Elige el tipo de moneda " + tipoMoneda;
            List<string> toStringMonedas = Controlador.ObtenerToStringDeMonedas();
            foreach (var toStringMoneda in toStringMonedas)
            {
                comentario += " "+toStringMoneda;
            }
            comentario+=": ";
            do
            {
                Console.Write(comentario);
                if (int.TryParse(Console.ReadLine(), out moneda) )
                {
                    if (moneda >= 0 && moneda <= toStringMonedas.Count)
                    {
                        respuestaCorrecta = true;

                    }
                }

            } while (!respuestaCorrecta);
            return moneda;
        }
    }
}