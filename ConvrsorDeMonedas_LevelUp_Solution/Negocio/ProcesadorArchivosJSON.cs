using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace ConvrsorDeMonedas_LevelUp
{
    public class Divisa
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public double ValorEnDolares { get; set; }

        public static string jsonProcesado;
    }
    public class ProcesadorArchivoJSON
    {
        public void ProcesarArchivoJSON()
        {
            string rutaArchivoJSON = Path.Combine("C:/archivos/inbox", "monedas.json");
            string rutaFinalJSON = Path.Combine("C:/archivos/final", "monedas_final.json");

            if (File.Exists(rutaArchivoJSON))
            {
                try
                {
                    string json = File.ReadAllText(rutaArchivoJSON);
                    List<Divisa> divisas = JsonConvert.DeserializeObject<List<Divisa>>(json);

                    Console.WriteLine("Contenido del archivo JSON procesado:");

                    string rutaArchivoBackup = Path.Combine("C:/archivos/backup", $"monedas_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                    File.Copy(rutaArchivoJSON, rutaArchivoBackup);

                    string rutaArchivoProgreso = Path.Combine("C:/archivos/proceso", $"monedas_progreso{DateTime.Now:yyyyMMdd_HHmmss}.json");
                    File.Move(rutaArchivoJSON, rutaArchivoProgreso);

                    string rutaArchivoFinal = Path.Combine("C:/archivos/final", $"monedas_final.json");
                    if (File.Exists(rutaArchivoFinal))
                        File.Delete(rutaArchivoFinal);

                    File.Move(rutaArchivoProgreso, rutaArchivoFinal);

                    MostrarListadoDivisas(divisas);
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║ Error al leer o procesar el archivo JSON. El archivo será movido a la carpeta 'final' como '.err'.║");
                    Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════╝");
                    Console.ReadLine();

                    string nombreArchivoError = $"monedas{DateTime.Now:yyyyMMdd_HHmmss}.err";
                    string rutaArchivoError = Path.Combine("C:/archivos/inbox", nombreArchivoError);

                    string rutaFinalerrorJSON = Path.Combine("C:/archivos/final", nombreArchivoError);
                    File.Move(rutaArchivoJSON, rutaFinalerrorJSON);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error general al leer o procesar el archivo JSON: {ex.Message}");
                }
            }
            else
            {
                if (File.Exists(rutaFinalJSON))
                {
                    string json = File.ReadAllText(rutaFinalJSON);
                    List<Divisa> divisas = JsonConvert.DeserializeObject<List<Divisa>>(json);
                    MostrarListadoDivisas(divisas);
                }
                else
                {
                    Console.WriteLine("╔════════════════════════════════════════════════╗");
                    Console.WriteLine("║ No tienes monedas para mostrar en este momento.║");
                    Console.WriteLine("╚════════════════════════════════════════════════╝");
                    Console.ReadLine();
                }

            }
        }

        static void MostrarListadoDivisas(List<Divisa> divisas)
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
    }
}

