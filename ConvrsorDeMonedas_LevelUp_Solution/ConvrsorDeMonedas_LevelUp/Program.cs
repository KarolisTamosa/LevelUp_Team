using Negocio;
using System.ComponentModel;

namespace Presentacion
{
    public class Program
    {
        public static List<HistorialMonedasPorUsuario> Historial;
        public Program()
        {
            VerificarHistorialVacio();
            Controller.err.PropertyChanged += MyClass_PropertyChanged;
            Controller.CrearCarpetas();
            ProcesadorArchivoJSON.CrearJsonConListaDivisa();
            ProcesadorArchivoJSON.ProcesarArchivoJSON();

        }

        private void VerificarHistorialVacio()
        {
            if (Historial == null)
            {
                Historial = new List<HistorialMonedasPorUsuario>();
            }
        }

        static void Main(string[] args)
        {
            Program program = new();
            int opcionMenuPrincipal;
            do
            {
                opcionMenuPrincipal = MenuPrincipal();
                if (opcionMenuPrincipal != -1)
                {
                    program.EjecutarValorElegidoMenuPrincipal((AccionesMenuPrincipal)opcionMenuPrincipal);

                }
            } while (opcionMenuPrincipal != 5);

        }

        private void EjecutarValorElegidoMenuPrincipal(AccionesMenuPrincipal opcionMenuPrincipal)
        {
            switch (opcionMenuPrincipal)
            {
                case AccionesMenuPrincipal.ConvertirMoneda:
                    Conversor();
                    break;
                case AccionesMenuPrincipal.MostrarListadoDivisas:
                    MostrarDivisas();
                    break;
                case AccionesMenuPrincipal.EditorDeListadoDivisas:
                    MostrarMenuEditorListadoDivisas();
                    break;
                case AccionesMenuPrincipal.MostrarHistorialConversiones:
                    MostrarHistorial();
                    break;
                case AccionesMenuPrincipal.Salir:
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                    break;
            }
            Console.WriteLine("Saliendo del programa...");
        }

        private void MostrarMenuEditorListadoDivisas()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   1. Modificar las divisas                                             ║");
            Console.WriteLine("║   2. Agregar una nueva divisa                                          ║");
            Console.WriteLine("║   3. Eliminar una divisa                                               ║");
            Console.WriteLine("║   4. Resetear listado divisas                                          ║");
            Console.WriteLine("║   5. Salir                                                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
            AccionesMenuEditarDivisas opcionMenuEditarDivisas = PedirNumeroOpcionMenuEditarDivisasl(5);

            switch (opcionMenuEditarDivisas)
            {
                case AccionesMenuEditarDivisas.ModificarUnaDivisa:
                    ModificarDivisas();
                    break;
                case AccionesMenuEditarDivisas.AgregarNuevaDivisa:
                    Console.Clear();
                    AgregarDivisa();
                    break;
                case AccionesMenuEditarDivisas.EliminarUnaDivisa:
                    Console.Clear();
                    EliminarDivisa();
                    break;
                case AccionesMenuEditarDivisas.ResetearTodoListadoDivisas:
                    ResetearListadoDivisas();
                    break;
                case AccionesMenuEditarDivisas.Salir:
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                    break;
            }
        }
        private void ResetearListadoDivisas()
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ ¿Estas seguro que quieres resetear todos los datos de las divisas?. Se perderán los cambios(S/N).   ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            bool respuesta = PreguntarSiNo();
            Console.Clear();

            if (respuesta)
            {
                ProcesadorArchivoJSON.ResetearDatosJsonListadoDivisa();
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║ Datos reseteados correctamente.   ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine("╔══════════════════════════════════╗");
                Console.WriteLine("║ No se han reseteado los datos.   ║");
                Console.WriteLine("╚══════════════════════════════════╝");
            }
            Console.ReadLine();
            
        }
        private bool PreguntarSiNo()
        {
            string respuesta = Console.ReadLine().ToUpper();

            return respuesta == "S";
        }
        private static int MenuPrincipal()
        {
            int opcionMenu = 0;

            do
            {
                Console.Clear();
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
                Console.WriteLine("║   1. Conversor de divsas                                               ║");
                Console.WriteLine("║   2. Listado de divisas                                                ║");
                Console.WriteLine("║   3. Editor de divisas                                                 ║");
                Console.WriteLine("║   4. Historial de conversiones                                         ║");
                Console.WriteLine("║   5. Salir                                                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine("                         Powered by LevelUp_Team                          ");
                Console.WriteLine("                                                                          ");
                Console.WriteLine("          Escribe el número de la opción que deseas realizar.             ");
                opcionMenu = PedirNumeroOpcionMenus(5);
            } while (opcionMenu == -1);
            return opcionMenu;

        }

        private static int PedirNumeroOpcionMenus(int ultimoNumero)
        {
            int result = -1;
            int.TryParse(Console.ReadLine(), out result);

            return ComprobarValorPedidoCorrecto(result, 1, ultimoNumero) ? result : -1;

        }

        private AccionesMenuEditarDivisas PedirNumeroOpcionMenuEditarDivisasl(int ultimoNumero)
        {
            bool opcionPedidaCorrecto = false;
            int result = -1;
            do
            {
                opcionPedidaCorrecto = int.TryParse(Console.ReadLine(), out result);
                opcionPedidaCorrecto = ComprobarValorPedidoCorrecto(result, 1, ultimoNumero);

            } while (!opcionPedidaCorrecto);

            return (AccionesMenuEditarDivisas)result;

        }

        private static bool ComprobarValorPedidoCorrecto(int AComprobar, int min, int max)
        {
            return (AComprobar >= min && AComprobar <= max);
        }
        private void MostrarDivisas()
        {
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
            if (divisas is not null)
            {
                while (true)
                {
                    MostrarListadoDivisas(divisas);
                    MenuVolverSalir();
                }
            }
            else
            {
                SinElementosAMostrar();
                Console.ReadLine();
            }

        }

        private static void MostrarListadoDivisas(List<Divisa> divisas)
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

        private void SinElementosAMostrar()
        {
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║ No tienes monedas para mostrar en este momento.║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.ReadLine();
        }

        private static void MyClass_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MyStringProperty")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║ Ha habido un error al intentar realizar la consulta. Puedes verlo en la carpeta 'final' como '.err'.║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                Console.ReadLine();
                Console.ResetColor();
                Main(null);
            }

        }


        private static void EscribirMensajeError(string mensaje)
        {
            MostrarMensajeConColor(mensaje, ConsoleColor.Red);
        }
        private static void EscribirMensajeAlerta(string mensaje)
        {
            MostrarMensajeConColor(mensaje, ConsoleColor.Yellow);
        }
        private static void MostrarMensajeConColor(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            EscribirMensaje(mensaje);
            Console.ResetColor();
        }
        private static void EscribirMensaje(string mensaje)
        {
            int anchoMensaje = mensaje.Length >= 100 ? 100 : mensaje.Length + 2;
            PintarDetalleArriba(anchoMensaje);
            Console.WriteLine();
            PintarMensaje(mensaje, anchoMensaje, 100);
            Console.WriteLine();
            PintarDetalleAbajo(anchoMensaje);
        }
        private static void EscribirMensajeError(string mensaje, int anchoMensaje)
        {
            MostrarMensajeConColor(mensaje, ConsoleColor.Red, anchoMensaje);
        }
        private static void EscribirMensajeAlerta(string mensaje, int anchoMensaje)
        {
            MostrarMensajeConColor(mensaje, ConsoleColor.Yellow, anchoMensaje);
        }
        private static void MostrarMensajeConColor(string mensaje, ConsoleColor color, int anchoMensaje)
        {
            Console.ForegroundColor = color;
            EscribirMensaje(mensaje, anchoMensaje);
            Console.ResetColor();
        }
        private static void EscribirMensaje(string mensaje, int anchoMensaje)
        {
            PintarDetalleArriba(anchoMensaje);
            Console.WriteLine();
            PintarMensaje(mensaje, anchoMensaje, anchoMensaje);
            Console.WriteLine();
            PintarDetalleAbajo(anchoMensaje);
        }

        private static void PintarMensaje(string mensaje, int anchoMensaje, int anchoMensajeMaximo)
        {
            int faltaAncho = 0;
            int anchoMensajeSobra = mensaje.Length;
            Console.Write("║ ");

            for (int i = 0; i < anchoMensajeSobra; i++)
            {
                if(i > 97)
                {
                    Console.WriteLine(" ║");
                    Console.Write("║ ");
                    i = 0;
                    anchoMensajeSobra = mensaje.Length - anchoMensajeMaximo;
                }
                Console.Write(mensaje[i]);
                faltaAncho = anchoMensaje - (i+1);
            }
            for (int i = 0; i < faltaAncho - 2; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ║");
        }

        private static void PintarDetalleArriba(int numAncho)
        {
            Console.Write("╔");
            for (int i = 0; i < numAncho; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
        }
        private static void PintarDetalleAbajo(int numAncho)
        {
            Console.Write("╚");
            for (int i = 0; i < numAncho; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
        }

        public void ModificarDivisas()
        {
            try
            {
                List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();

                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║          Listado de Divisas               ║");
                Console.WriteLine("╠═══════════════════════════════════════════╣");
                Console.WriteLine("║   Nombre            │ Valor en Dólares    ║");
                Console.WriteLine("╠═══════════════════════════════════════════╣");

                foreach (Divisa divisa in divisas)
                {
                    Console.WriteLine($"║   {divisa.Nombre,-18}│ {divisa.ValorEnDolares,15:N4}     ║");
                }

                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║ Escribe el nombre de la divisa que deseas modificar║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                string nombreDivisaModificar = Console.ReadLine();

                Divisa divisaEncontrada = divisas.Find(divisa => divisa.Nombre.ToUpper() == nombreDivisaModificar.ToUpper());
                if (divisaEncontrada == null)
                {
                    Console.WriteLine("No se encontró la divisa.");
                    return;
                }
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║ Escribe el nuevo nombre. ║");
                Console.WriteLine("╚══════════════════════════╝");
                string nuevoNombre = Console.ReadLine();
                divisaEncontrada.Nombre = nuevoNombre;
                Console.WriteLine("Nombre modificado exitosamente.");
                ProcesadorArchivoJSON.GuardarDivisas(divisas);
            }
            catch (DirectoryNotFoundException)
            {
                Controller.CrearCarpetas();
            }
            catch (FileNotFoundException)
            {

                ProcesadorArchivoJSON.ProcesarArchivoJSON();
            }
        }

        public static void AgregarDivisa()
        {
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();

            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Escribe el nombre de la nueva divisa que quieres añadir║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            string nuevoNombre = Console.ReadLine();

            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Escribe el código de la nueva divisa que quieres añadir║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            string nuevoCodigo = Console.ReadLine();

            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║ Escribe el valor en dólares de la nueva divisa║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            double nuevoValor;
            if (!double.TryParse(Console.ReadLine(), out nuevoValor))
            {
                MostrarMensajeConColor("El valor ingresado no es válido.", ConsoleColor.Yellow);
                return;
            }

            divisas.Add(new Divisa { Nombre = nuevoNombre, ValorEnDolares = (decimal)nuevoValor });
            Console.WriteLine("Divisa agregada exitosamente.");

            ProcesadorArchivoJSON.GuardarDivisas(divisas);
        }
        private static void EliminarDivisa()
        {
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          Listado de Divisas               ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");
            Console.WriteLine("║   Nombre            │ Valor en Dólares    ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");

            foreach (Divisa divisa in divisas)
            {
                Console.WriteLine($"║   {divisa.Nombre,-18}│ {divisa.ValorEnDolares,15:N4}     ║");
            }

            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine("Escribe el nombre de la divisa que deseas eliminar:");
            string nombreDivisaEliminar = Console.ReadLine();

            ProcesadorArchivoJSON.EliminarDivisa(nombreDivisaEliminar);
        }


        public static void Conversor()
        {
            Conversor conversor = new Conversor();
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
            while (true)
            {
                MostrarListadoDivisas(divisas);
                //MenuVolverSalir();
                //if(Console.ReadKey()== ConsoleKey.Enter)
                //{

                //}
                string nombreDivisaOrigen = string.Empty;
                string nombreDivisaDestino = string.Empty;
                bool esCodigoMonedaValido = false;
                MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaOrigen, out esCodigoMonedaValido,true);
                //MenuVolverSalir();
                esCodigoMonedaValido = false;
                MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaDestino, out esCodigoMonedaValido, false);
                //MenuVolverSalir();

                bool esImporteValido = false;
                double importe;
                do
                {
                    Console.WriteLine("╔══════════════════════════════════════════════╗");
                    Console.WriteLine("║      Seleccione el importe a convertir:      ║");
                    Console.WriteLine("╚══════════════════════════════════════════════╝");

                    bool valorEsNumerico = double.TryParse(Console.ReadLine(), out importe);
                    if (!valorEsNumerico)
                    {
                        MostrarMensajeConColor("Inserte un importe valido", ConsoleColor.Yellow);
                        continue;
                    }
                    esImporteValido = conversor.ComprobarImporte(importe);

                    if (!esImporteValido)
                    {
                        MostrarMensajeConColor("Inserte un importe valido", ConsoleColor.Yellow);
                    }

                } while (!esImporteValido);

                double resultado = conversor.Convertir(nombreDivisaOrigen, nombreDivisaDestino, importe, divisas, Historial);
                Console.Clear();
                Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("  EL resultado de la conversión es: " + resultado + " " + nombreDivisaDestino.ToUpper() + "                             ");
                Console.WriteLine("  La divisa de entrada ha sido: " + nombreDivisaOrigen.ToUpper() + "                                                ");
                Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");

                MenuVolverSalir();
            }

        }

        

        private static void MostrarInputCodigoEntrSalMientrasSeaInvalido(Conversor conversor, List<Divisa> divisas, out string nombreDivisaOrigen, out bool esCodigoMonedaValido, bool esDeEntrada)
        {
            do
            {
                if (esDeEntrada)
                {
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                   Inserte el nombre de la divisa de entrada:           ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                }
                else
                {
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                   Inserte el nombre de la divisa de salida:            ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                }
                
                nombreDivisaOrigen = Console.ReadLine() ?? "";

                esCodigoMonedaValido = conversor.ComprobarNombre(nombreDivisaOrigen.ToUpper(), divisas);

                if (!esCodigoMonedaValido)
                {
                    MostrarMensajeConColor("Inserte un nombre de divisa valido", ConsoleColor.Yellow);
                }
            } while (!esCodigoMonedaValido);
        }

        public void MostrarHistorial()
        {
            Console.Clear();
            if (Historial.Count == 0)
            {
                MostrarMensajeConColor("No existen registros en el historial", ConsoleColor.Yellow);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("Historial ═══════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");
            foreach (var registro in Historial)
            {
                Console.WriteLine(registro.ToString());
                Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");
            }

            MenuVolverSalir();
        }
        private static void MenuVolverSalir()
        {
            Console.WriteLine("         ╔════════════════════════════╗      ");
            Console.WriteLine("         ║   1. Volver atrás          ║      ");
            Console.WriteLine("         ║   2. Salir                 ║      ");
            Console.WriteLine("         ╚════════════════════════════╝      ");
            Console.Write("                  Ingrese una opción:              ");
            string mensaje = Console.ReadLine();
            AccionesMenuVolverSalir opcionListado = (AccionesMenuVolverSalir)int.Parse(mensaje);


            switch (opcionListado)
            {
                case AccionesMenuVolverSalir.VolverAtras:
                    MenuPrincipal();
                    break;
                case AccionesMenuVolverSalir.Salir:
                    Console.WriteLine("Saliendo del programa...");
                    Environment.Exit(0);
                    break;
                default:
                    MostrarMensajeConColor("Opción inválida. Por favor, ingrese una opción válida (1-2).", ConsoleColor.Yellow);
                    break;
            }
        }
    }
}
