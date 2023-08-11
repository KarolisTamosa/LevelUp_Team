

using Negocio;

namespace Presentacion
{
    internal static class Mensajes
    {
        private static int ESPACIO_IZQUIERDA_MENSAJE_DEFECTO = 3;
        public static void EscribirUnaLista(System.Collections.Generic.List<string> list, List<string> listaMensaje)
        {
            int anchoMensaje = listaMensaje.Max().Length + 2;

            EscribirUnaLista(listaMensaje, ESPACIO_IZQUIERDA_MENSAJE_DEFECTO, anchoMensaje);
        }
        public static void EscribirUnaLista(List<string> listaMensaje, int espacioIzquierda, int anchoMensaje)
        {

            PintarDetalleArriba(espacioIzquierda, anchoMensaje);

            foreach (var item in listaMensaje)
            {
                Console.WriteLine();
                PintarMensaje(item, anchoMensaje, espacioIzquierda, anchoMensaje);
            }
            Console.WriteLine();
            PintarDetalleAbajo(espacioIzquierda, anchoMensaje);
            Console.WriteLine();

        }


        public static void EscribirMensajeConColor(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            EscribirMensaje(mensaje);
            Console.ResetColor();
        }
        public static void EscribirMensajeConColor(string mensaje, ConsoleColor color, int espacioIzquierda, int anchoMensaje)
        {
            Console.ForegroundColor = color;
            EscribirMensaje(mensaje, espacioIzquierda, anchoMensaje);
            Console.ResetColor();
        }
        public static void EscribirMensajeError(string mensaje)
        {
            EscribirMensajeConColor(mensaje, ConsoleColor.Red);
        }
        public static void EscribirMensajeError(string mensaje, int espacioIzquierda, int anchoMensaje)
        {
            EscribirMensajeConColor(mensaje, ConsoleColor.Red, espacioIzquierda, anchoMensaje);
        }
        public static void EscribirMensajeAlerta(string mensaje)
        {
            EscribirMensajeConColor(mensaje, ConsoleColor.Yellow);
        }
        public static void EscribirMensajeAlerta(string mensaje, int espacioIzquierda, int anchoMensaje)
        {
            EscribirMensajeConColor(mensaje, ConsoleColor.Yellow, espacioIzquierda, anchoMensaje);
        }
        
        public static void EscribirMensaje(string mensaje, int espacioIzquierda, int anchoMensaje)
        {
            PintarDetalleArriba(espacioIzquierda, anchoMensaje);
            Console.WriteLine();
            PintarMensaje(mensaje, anchoMensaje, espacioIzquierda, anchoMensaje);
            Console.WriteLine();
            PintarDetalleAbajo(espacioIzquierda, anchoMensaje);
            Console.WriteLine();

        }
        public static void EscribirMensaje(string mensaje)
        {
            int anchoMensaje = mensaje.Length >= 100 ? 100 : mensaje.Length + 2;
            EscribirMensaje(mensaje, ESPACIO_IZQUIERDA_MENSAJE_DEFECTO, anchoMensaje);
        }
        public static void MostrarListadoDivisas(List<Divisa> divisas)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          Listado de Divisas               ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");
            Console.WriteLine("║   Nombre            │ Valor en Dólares    ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");

            divisas.ForEach(divisa => Console.WriteLine($"║   {divisa.Nombre,-18}│ {divisa.ValorEnDolares,15:N4}     ║"));

            Console.WriteLine("╚═══════════════════════════════════════════╝");
        }

        private static void PintarMensaje(string mensaje, int anchoMensaje, int espacioIzquierda, int anchoMensajeMaximo)
        {
            int faltaAncho = 0;
            int anchoMensajeSobra = mensaje.Length;
            Console.Write("║");
            PintarEspacios(espacioIzquierda);

            for (int i = 0; i < anchoMensajeSobra; i++)
            {
                if (i >= anchoMensajeMaximo -2)
                {
                    Console.WriteLine("   ║");
                    Console.Write("║");
                    PintarEspacios(espacioIzquierda);
                    i = 0;
                    anchoMensajeSobra = anchoMensajeSobra - anchoMensajeMaximo;
                }
                Console.Write(mensaje[i]);
                faltaAncho = anchoMensaje - (i+1);
            }
            for (int i = 0; i < faltaAncho - 2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("   ║");
        }

        private static void PintarDetalleArriba(int espacioIzquierda, int numAncho)
        {
            Console.Write("╔");
            PintarBarrasLaterales(espacioIzquierda, numAncho);
            Console.Write("╗");
        }
        private static void PintarDetalleAbajo(int espacioIzquierda, int numAncho)
        {
            Console.Write("╚");
            PintarBarrasLaterales(espacioIzquierda, numAncho);
            Console.Write("╝");
        }
        private static void PintarBarrasLaterales(int espacioIzquierda, int numAncho)
        {
            for (int i = 0; i < numAncho + espacioIzquierda + 1; i++)
            {
                Console.Write("═");
            }
        }
        private static void PintarEspacios(int numEspacios)
        {
            for (int i = 0; i < numEspacios; i++)
            {
                Console.Write(" ");
            }
        }
    }
}
