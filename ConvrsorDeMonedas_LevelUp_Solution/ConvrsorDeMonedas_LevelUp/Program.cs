using Negocio;
using System.ComponentModel;

namespace Presentacion
{
    public class Program
    {
        public static int ANCHO_PANTALLA = 68;
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
            Mensajes.EscribirUnaLista(new List<string>
            {
                "1. Modificar las divisas",
                "2. Agregar una nueva divisa",
                "3. Eliminar una divisa",
                "4. Resetear listado divisas",
                "5. Salir"
            }, 3, ANCHO_PANTALLA);
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
            Mensajes.EscribirMensaje("¿Estas seguro que quieres resetear todos los datos de las divisas?. Se perderán los cambios(S/N).");

            bool respuesta = PreguntarSiNo();
            Console.Clear();

            if (respuesta)
            {
                ProcesadorArchivoJSON.ResetearDatosJsonListadoDivisa();
                Mensajes.EscribirMensaje("Datos reseteados correctamente.");
            }
            else
            {
                Mensajes.EscribirMensaje("No se han reseteado los datos.");
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
                Mensajes.EscribirUnaLista(new List<string>
                {
                    "1. Conversor de divsas",
                    "2. Listado de divisas",
                    "3. Editor de divisas",
                    "4. Historial de conversiones",
                    "5. Salir"
                }, 3, ANCHO_PANTALLA);
                Console.WriteLine("                         Powered by LevelUp_Team                          ");
                Console.WriteLine();
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
        private static void MostrarDivisas()
        {
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
            if (divisas is not null)
            {
                while (true)
                {
                    Mensajes.MostrarListadoDivisas(divisas);
                    MenuVolverSalir();
                }
            }
            else
            {
                SinElementosAMostrar();
                Console.ReadLine();
            }

        }

        private static void SinElementosAMostrar()
        {
            Mensajes.EscribirMensajeAlerta("No tienes monedas para mostrar en este momento.");
            Console.ReadLine();
        }

        private static void MyClass_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MyStringProperty")
            {
                Console.Clear();
                Mensajes.EscribirMensajeError("Ha habido un error al intentar realizar la consulta. Puedes verlo en la carpeta 'final' como '.err'");
                Mensajes.EscribirMensajeError("(error)Nº" + Controller.err.RecogerMensajeError());
                Console.ReadLine();
                Main(null);
            }

        }

        

        public static void ModificarDivisas()
        {
            try
            {
                List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
                MostrarDivisas();
                Mensajes.EscribirMensaje("Escribe el nombre de la divisa que deseas modificar: ");
                string nombreDivisaModificar = Console.ReadLine();

                Divisa divisaEncontrada = divisas.Find(divisa => divisa.Nombre.ToUpper() == nombreDivisaModificar.ToUpper());
                if (divisaEncontrada == null)
                {
                    Console.WriteLine("No se encontró la divisa.");
                    return;
                }
                Console.Clear();
                Mensajes.EscribirMensaje("Escribe el nuevo nombre: ");

                string nuevoNombre = Console.ReadLine();
                divisaEncontrada.Nombre = nuevoNombre;
                Mensajes.EscribirMensajeAlerta("Nombre modificado exitosamente.");
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

            Mensajes.EscribirMensaje("Escribe el nombre de la nueva divisa que quieres añadir: ");

            string nuevoNombre = Console.ReadLine();
            Mensajes.EscribirMensaje("Escribe el código de la nueva divisa que quieres añadir: ");

            string nuevoCodigo = Console.ReadLine();

            Mensajes.EscribirMensaje("Escribe el código de la nueva divisa que quieres añadir: ");

            double nuevoValor;
            if (!double.TryParse(Console.ReadLine(), out nuevoValor))
            {
                Mensajes.EscribirMensajeAlerta("El valor ingresado no es válido.");
                return;
            }

            divisas.Add(new Divisa { Nombre = nuevoNombre, ValorEnDolares = (decimal)nuevoValor });
            Console.WriteLine("Divisa agregada exitosamente.");

            ProcesadorArchivoJSON.GuardarDivisas(divisas);
        }
        private static void EliminarDivisa()
        {
            MostrarDivisas();
            string nombreDivisaEliminar = Console.ReadLine();

            ProcesadorArchivoJSON.EliminarDivisa(nombreDivisaEliminar);
        }


        public static void Conversor()
        {
            Conversor conversor = new Conversor();
            List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();
            while (true)
            {
                Mensajes.MostrarListadoDivisas(divisas);
                string nombreDivisaOrigen = string.Empty;
                string nombreDivisaDestino = string.Empty;
                bool esCodigoMonedaValido = false;
                MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaOrigen, out esCodigoMonedaValido,true);

                esCodigoMonedaValido = false;
                MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaDestino, out esCodigoMonedaValido, false);

                bool esImporteValido = false;
                double importe;
                do
                {
                    Mensajes.EscribirMensaje("Seleccione el importe a convertir: ");

                    bool valorEsNumerico = double.TryParse(Console.ReadLine(), out importe);
                    if (!valorEsNumerico)
                    {
                        Mensajes.EscribirMensajeConColor("Inserte un importe valido", ConsoleColor.Yellow);
                        continue;
                    }
                    esImporteValido = conversor.ComprobarImporte(importe);

                    if (!esImporteValido)
                    {
                        Mensajes.EscribirMensajeConColor("Inserte un importe valido", ConsoleColor.Yellow);
                    }

                } while (!esImporteValido);

                double resultado = conversor.Convertir(nombreDivisaOrigen, nombreDivisaDestino, importe, divisas, Historial);
                Console.Clear();
                Mensajes.EscribirUnaLista(new List<string>
                {
                    "EL resultado de la conversión es: " + resultado + " " + nombreDivisaDestino.ToUpper(),
                    "La divisa de entrada ha sido: " + nombreDivisaOrigen.ToUpper()
                }, 3, ANCHO_PANTALLA);

                MenuVolverSalir();
            }

        }

        

        private static void MostrarInputCodigoEntrSalMientrasSeaInvalido(Conversor conversor, List<Divisa> divisas, out string nombreDivisaOrigen, out bool esCodigoMonedaValido, bool esDeEntrada)
        {
            do
            {
                if (esDeEntrada)
                {
                    Mensajes.EscribirMensaje("Inserte el nombre de la divisa de entrada: ");

                }
                else
                {
                    Mensajes.EscribirMensaje("Inserte el nombre de la divisa de entrada: ");

                }

                nombreDivisaOrigen = Console.ReadLine() ?? "";

                esCodigoMonedaValido = conversor.ComprobarNombre(nombreDivisaOrigen.ToUpper(), divisas);

                if (!esCodigoMonedaValido)
                {
                    Mensajes.EscribirMensajeConColor("Inserte un nombre de divisa valido", ConsoleColor.Yellow);
                }
            } while (!esCodigoMonedaValido);
        }

        public void MostrarHistorial()
        {
            Console.Clear();
            if (Historial.Count == 0)
            {
                Mensajes.EscribirMensajeConColor("No existen registros en el historial", ConsoleColor.Yellow);
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
            Mensajes.EscribirUnaLista(new List<string>
            {
                "1. Volver atrás",
                "2. Salir",
            }, 9, ANCHO_PANTALLA - 9);
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
                    Mensajes.EscribirMensajeConColor("Opción inválida. Por favor, ingrese una opción válida (1-2).", ConsoleColor.Yellow);
                    break;
            }
        }
    }
}
