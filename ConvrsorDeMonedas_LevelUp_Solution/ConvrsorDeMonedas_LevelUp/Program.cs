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
                opcionMenuPrincipal = program.MenuPrincipal();
                if (opcionMenuPrincipal != -1)
                {
                    program.EjecutarValorElegidoMenuPrincipal(opcionMenuPrincipal);

                }
            } while (opcionMenuPrincipal != 5);

        }

        private void EjecutarValorElegidoMenuPrincipal(int opcionMenuPrincipal)
        {
            switch (opcionMenuPrincipal)
            {
                case 1:
                    Conversor();
                    break;
                case 2:
                    MostrarDivisas();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   1. Modificar las divisas                                             ║");
                    Console.WriteLine("║   2. Agregar una nueva divisa                                          ║");
                    Console.WriteLine("║   3. Eliminar una divisa                                               ║");
                    Console.WriteLine("║   4. Resetear listado divisas                                          ║");
                    Console.WriteLine("║   5. Salir                                                             ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                    string opcioninterna = Console.ReadLine();

                    switch (opcioninterna)
                    {
                        case "1":
                            ModificarDivisas();
                            break;
                        case "2":
                            Console.Clear();
                            AgregarDivisa();
                            break;
                        case "3":
                            Console.Clear();
                            EliminarDivisa();
                            break;
                        case "4":
                            Console.Clear();
                            ResetearListadoDivisas();
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                            break;
                    }
                    break;
                case 4:
                    ProcesadorArchivoJSON.MostrarHistorial();
                    Console.WriteLine("Implementación pendiente...");
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                    break;
            }
            Console.WriteLine("Saliendo del programa...");
        }

        private void ResetearListadoDivisas()
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ ¿Estas seguro que quieres resetear todos los datos de las divisas?. Se perderán los cambios(S/N).   ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            bool respuesta = PreguntarSiNo();
            if (respuesta) Controller.ResetearDatosJsonListadoDivisa();
            
        }
        private bool PreguntarSiNo()
        {
            string respuesta = Console.ReadLine().ToUpper();

            return respuesta == "S";
        }
        private int MenuPrincipal()
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
                opcionMenu = PedirNumeroOpcionMenuPrincipal();
            } while (opcionMenu == -1);
            return opcionMenu;

        }

        private int PedirNumeroOpcionMenuPrincipal()
        {
            int result = -1;
            int.TryParse(Console.ReadLine(), out result);

            return this.ComprobarValorPedidoCorrecto(result, 1, 5) ? result : -1;

        }

        private bool ComprobarValorPedidoCorrecto(int AComprobar, int min, int max)
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

                    Console.WriteLine("         ╔════════════════════════════╗      ");
                    Console.WriteLine("         ║   1. Volver atrás          ║      ");
                    Console.WriteLine("         ║   2. Salir                 ║      ");
                    Console.WriteLine("         ╚════════════════════════════╝      ");
                    Console.Write("                  Ingrese una opción:              ");
                    string opcionListado = Console.ReadLine();

                    if (opcionListado == "1")
                    {
                        break;
                    }
                    else if (opcionListado == "2")
                    {
                        Console.WriteLine("Saliendo del programa...");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-2).");
                    }
                }
            }
            else
            {
                SinElementosAMostrar();
                Console.ReadLine();
            }

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

        public void ModificarDivisas()
        {
            string err = null;
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
                Console.WriteLine("El valor ingresado no es válido.");
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

                string nombreEntrada;
                bool comprobarNombre;
                do
                {
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                   Inserte el nombre de la divisa de entrada:           ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                    nombreEntrada = Console.ReadLine();

                    comprobarNombre = conversor.ComprobarNombre(nombreEntrada.ToUpper(), divisas);

                    if (comprobarNombre)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Inserte un nombre de divisa valido");
                    }
                } while (!comprobarNombre);
                comprobarNombre = false;
                string nombreSalida;
                do
                {
                    Console.WriteLine("╔═════════════════════════════════════════════════════╗");
                    Console.WriteLine("║      Inserte el nombre de la divisa de salida:      ║");
                    Console.WriteLine("╚═════════════════════════════════════════════════════╝");
                    nombreSalida = Console.ReadLine();

                    comprobarNombre = conversor.ComprobarNombre(nombreSalida.ToUpper(), divisas);

                    if (comprobarNombre)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Inserte un nombre de divisa valido");
                    }
                } while (!comprobarNombre);
                bool comprobarImporte;
                double importe;
                do
                {
                    Console.WriteLine("╔══════════════════════════════════════════════╗");
                    Console.WriteLine("║      Seleccione el importe a convertir:      ║");
                    Console.WriteLine("╚══════════════════════════════════════════════╝");

                    double.TryParse(Console.ReadLine(), out importe);
                    comprobarImporte = conversor.ComprobarImporte(importe);

                    if (comprobarImporte)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Inserte un importe valido");
                    }
                } while (!comprobarImporte);

                double resultado = conversor.Convertir(nombreEntrada, nombreSalida, importe, divisas, Historial);
                Console.Clear();
                Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("  EL resultado de la conversión es: " + resultado + " " + nombreSalida.ToUpper()+"                             ");
                Console.WriteLine("  La divisa de entrada ha sido: " + nombreEntrada.ToUpper() + "                                                ");
                Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════");

                Console.WriteLine("         ╔════════════════════════════╗      ");
                Console.WriteLine("         ║   1. Volver atrás          ║      ");
                Console.WriteLine("         ║   2. Salir                 ║      ");
                Console.WriteLine("         ╚════════════════════════════╝      ");
                Console.Write("                  Ingrese una opción:              ");
                string opcionListado = Console.ReadLine();

                if (opcionListado == "1")
                {
                    break;
                }
                else if (opcionListado == "2")
                {
                    Console.WriteLine("Saliendo del programa...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-2).");
                }
            }

        }

        public void MostrarHistorial()
        {
            if (Historial.Count == 0)
            {
                Console.WriteLine("No existen registros en el historial");
                return;
            }
            foreach(var registro in Historial)
            {
                Console.WriteLine(registro.ToString());
            }
            Console.ReadLine();
        }
    }
}
