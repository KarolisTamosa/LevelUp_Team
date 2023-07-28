using System;
using System.Collections.Generic;

namespace ConvrsorDeMonedas_LevelUp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            

        }
        private void MenuPrincipal()
        {
            bool salir = false;
            List<Divisa> divisas = new List<Divisa>();

            while (!salir)
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
                ProcesadorArchivoJSON procesador = new ProcesadorArchivoJSON();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Implementación pendiente...");
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Leyendo y procesando el archivo JSON...");

                        procesador.ProcesarArchivoJSON();
                        break;
                    case "3":
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
                                procesador.ModificarDivisas();
                                break;
                            case "2":
                                Console.Clear();
                                procesador.AgregarDivisa();
                                break;
                            case "3":
                                Console.Clear();
                                procesador.EliminarDivisa();
                                break;
                            case "4":
                                salir = true;
                                break;
                            default:
                                Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-4).");
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Implementación pendiente...");
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida (1-5).");
                        break;
                }
            }
            Console.WriteLine("Saliendo del programa...");
        }
        private void MostrarDivisas()
        {
            procesador.MostrarListadoDivisas(procesadorAPI.RecogerMonedasDesdeApi());
        }
    }
}