using Negocio;
using System;
using System.Collections.Generic;
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
                    Environment.Exit(0);
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
                "5. Volver"
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
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                    Thread.Sleep(2000);
                    MostrarMenuEditorListadoDivisas();
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
                Mensajes.EscribirMensajeConColor("Datos reseteados correctamente.", ConsoleColor.Green);
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

        //retorna -1 si la opcion no es correcta, pasar el valor despues de hacer Console.ReadLine()
        private int EsOpcionCorrecta(string? valor, int valorMinimo, int valorMaximo)
        {
            int opcionNumerica;
            bool esValorEntero = int.TryParse(valor, out opcionNumerica);
            if (!esValorEntero)
            {
                return -1;
            }
            bool esValorEnteroDentroDeRango = ComprobarValorPedidoCorrecto(opcionNumerica, valorMinimo, valorMaximo);
            if (!esValorEnteroDentroDeRango)
            {
                return -1;
            }
            return opcionNumerica;
        }

        private static int PedirNumeroOpcionMenus(int ultimoNumero)
        {
            int result = -1;
            int.TryParse(Console.ReadLine(), out result);

            return ComprobarValorPedidoCorrecto(result, 1, ultimoNumero) ? result : -1;

        }

        private AccionesMenuEditarDivisas PedirNumeroOpcionMenuEditarDivisasl(int ultimoNumero)
        {
            int result;
            bool opcionPedidaCorrecto = int.TryParse(Console.ReadLine(), out result);
            if (!opcionPedidaCorrecto)
            {
                return (AccionesMenuEditarDivisas)result;
            }
            opcionPedidaCorrecto = ComprobarValorPedidoCorrecto(result, 1, ultimoNumero);
            if (!opcionPedidaCorrecto)
            {
                return (AccionesMenuEditarDivisas)result;
            }
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

                Mensajes.MostrarListadoDivisas(divisas);
                MenuVolverSalir();
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
                Mensajes.MostrarListadoDivisas(divisas);

              
                Divisa divisaEncontrada = null;
                do
                {
                    Mensajes.EscribirMensaje("Escribe el nombre de la divisa que deseas modificar(-1(volver atras): ");
                    string nombreDivisaModificar = Console.ReadLine();
                    if (nombreDivisaModificar == "-1") Main(null);

                    divisaEncontrada = divisas.Find(divisa => divisa.Nombre.ToUpper() == nombreDivisaModificar.ToUpper());

                    if (divisaEncontrada == null)
                    {
                        Mensajes.EscribirMensajeAlerta("No se encontró la divisa.");
                    }


                } while (divisaEncontrada == null);
                Console.Clear();
                Mensajes.EscribirMensaje("Escribe el nuevo nombre(-1(volver atras): ");

                string nuevoNombre = Console.ReadLine();
                if (nuevoNombre == "-1") Main(null);

                divisaEncontrada.Nombre = nuevoNombre;

                ProcesadorArchivoJSON.GuardarDivisas(divisas);

                Console.Clear();
                Mensajes.EscribirMensajeConColor("Nombre modificado exitosamente.", ConsoleColor.Green);
                Console.ReadLine();
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

            string nuevoNombre = "";
            bool nombreRepetido = false;
            do
            {
                Mensajes.EscribirMensaje("Escribe el nombre de la nueva divisa que quieres añadir(-1(volver atras)): ");

                nuevoNombre = Console.ReadLine();

                nombreRepetido = new Conversor().ComprobarNombre(nuevoNombre.ToUpper(), divisas);

                if (nombreRepetido)
                {
                    Mensajes.EscribirMensajeAlerta("El nombre "+nuevoNombre+" ya esta en uso.");
                }
                if (nuevoNombre == "-1") Main(null);

            } while (nombreRepetido);


            
            double valor = 0;
            bool resultBueno = false;
            do
            {
                Mensajes.EscribirMensaje("Escribe el valor de la nueva divisa que quieres añadir(-1(volver atras)): ");

                resultBueno = double.TryParse(Console.ReadLine(), out valor);

                if (!resultBueno)
                {
                    Mensajes.EscribirMensajeAlerta("Parámetros no válidos");
                }
                if (valor == -1) Main(null);

            } while (!resultBueno);


            divisas.Add(new Divisa { Nombre = nuevoNombre, ValorEnDolares = (decimal)valor });
            
            ProcesadorArchivoJSON.GuardarDivisas(divisas);
            Console.Clear();
            Mensajes.EscribirMensajeConColor("Divisa agregada exitosamente.", ConsoleColor.Green);
            Console.ReadLine();
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

            Mensajes.MostrarListadoDivisas(divisas);
            string nombreDivisaOrigen = string.Empty;
            string nombreDivisaDestino = string.Empty;
            bool esCodigoMonedaValido = false;
            MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaOrigen, out esCodigoMonedaValido, true);

            esCodigoMonedaValido = false;
            MostrarInputCodigoEntrSalMientrasSeaInvalido(conversor, divisas, out nombreDivisaDestino, out esCodigoMonedaValido, false);

            bool esImporteValido = false;
            double importe;
            do
            {
                Mensajes.EscribirMensaje("Seleccione el importe a convertir(-1(volver atras)): ");

                bool valorEsNumerico = double.TryParse(Console.ReadLine(), out importe);
                if (!valorEsNumerico)
                {
                    Mensajes.EscribirMensajeConColor("Inserte un importe valido", ConsoleColor.Yellow);
                    continue;
                }
                if (importe == -1) Main(null);
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



        private static void MostrarInputCodigoEntrSalMientrasSeaInvalido(Conversor conversor, List<Divisa> divisas, out string nombreDivisaOrigen, out bool esCodigoMonedaValido, bool esDeEntrada)
        {
            do
            {
                if (esDeEntrada)
                {
                    Mensajes.EscribirMensaje("Inserte el nombre de la divisa de entrada(-1(volver atras)): ");

                }
                else
                {
                    Mensajes.EscribirMensaje("Inserte el nombre de la divisa de salida(-1(volver atras)): ");

                }

                nombreDivisaOrigen = Console.ReadLine() ?? "";
                if (nombreDivisaOrigen == "-1") Main(null);

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
            int opcion = -1;
            do
            {
                Console.Write("                  Ingrese una opción:              ");
                opcion = PedirNumeroOpcionMenus(2);

                if (opcion == -1)
                {
                    Mensajes.EscribirMensajeAlerta("Parámetros no válidos");
                }
            } while (opcion == -1);


            AccionesMenuVolverSalir opcionListado = (AccionesMenuVolverSalir)opcion;

            switch (opcionListado)
            {
                case AccionesMenuVolverSalir.VolverAtras:
                    break;
                case AccionesMenuVolverSalir.Salir:
                    Console.WriteLine("Saliendo del programa...");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
