using Negocio;
using System.Runtime.CompilerServices;

namespace ConvrsorDeMonedas_LevelUp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new();
            int opcionMenuPrincipal = program.MenuPrincipal();
            Console.WriteLine(opcionMenuPrincipal);
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
                Console.WriteLine("║   1. Conversor de monedas                                              ║");
                Console.WriteLine("║   2. Listado de divisas                                                ║");
                Console.WriteLine("║   3. Editor de divisas                                                 ║");
                Console.WriteLine("║   4. Historial de conversiones                                         ║");
                Console.WriteLine("║   5. Salir                                                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine("                         Powered by LevelUp_Team                          ");
                Console.WriteLine("                                                                          ");
                Console.WriteLine("          Escribe el número de la opción que deseas realizar.             ");
                string opcion = Console.ReadLine();

                return opcionMenu;
            } while (opcionMenu == -1);
        }

        private int PedirNumeroOpcionMenuPrincipal()
        {
            int result = -1;
            int.TryParse(Console.ReadLine(), out result);
            switch (result)
            {
                case 1:

                    ProcesadorArchivoJSON.ProcesarArchivoJSON();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   1. Modificar las divisas                                             ║");
                    Console.WriteLine("║   2. Agregar una nueva divisa                                          ║");
                    Console.WriteLine("║   3. Eliminar una divisa                                               ║");
                    Console.WriteLine("║   5. Salir                                                             ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
                    string opcioninterna = Console.ReadLine();

                    switch (opcioninterna)
                    {
                        case "1":
                            ProcesadorArchivoJSON.ModificarDivisas();
                            break;
                        case "2":
                            Console.Clear();
                            ProcesadorArchivoJSON.AgregarDivisa();
                            break;
                        case "3":
                            Console.Clear();
                            ProcesadorArchivoJSON.EliminarDivisa();
                            break;
                        case "4":
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-4).");
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

            return this.ComprobarValorPedidoCorrecto(result, 1, 5) ? result : -1;

        }

        private bool ComprobarValorPedidoCorrecto(int AComprobar, int min, int max)
        {
            return (AComprobar >= min && AComprobar <= max);
        }
        private void MostrarDivisas()
        {
            Console.WriteLine(ProcesadorAPIMonedas.RecogerListaDivisas());
        }
    }

}