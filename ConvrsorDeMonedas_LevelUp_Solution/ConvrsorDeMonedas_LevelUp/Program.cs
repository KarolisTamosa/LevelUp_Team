namespace Presentacion
{
    public class Divisa
    {

        static void Main(string[] args)
        {

            bool salir = false;

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

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Implementación pendiente...");
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Leyendo y procesando el archivo JSON...");

                        ProcesadorArchivoJSON procesador = new ProcesadorArchivoJSON();
                        procesador.ProcesarArchivoJSON();
                        break;
                    case "3":
                        Console.WriteLine("Implementación pendiente...");
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
    }
}