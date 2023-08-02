using Datos;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace Negocio
{
    
    public static class ProcesadorArchivoJSON
    {
        private static string  rutaArchivoJSON = Path.Combine("C:/archivos/inbox", "monedas.json");
        private static string rutaFinalJSON = Path.Combine("C:/archivos/final", "monedas_final.json");

        public static void CrearJsonConListaDivisa()
        {
            if(!File.Exists(rutaFinalJSON))
            {
                ResultadoApiMonedas listaDivisas = Monedas.ImportarMonedasDesdeApi();
                ArchivosJSON.CrearArchivoJsonPorApi(listaDivisas);
            }


        }
        public static void ProcesarArchivoJSON()
        {
            if (File.Exists(rutaArchivoJSON))
            {
                try
                {
                    List<Divisa> divisas = CogerDivisasDeJson();


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

        public static void ModificarDivisas()
        {

            try
            {
                List<Divisa> divisas = CogerDivisasDeJson();

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

                Divisa divisaEncontrada = divisas.Find(divisa => divisa.Nombre == nombreDivisaModificar);
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
                GuardarDivisas(divisas);
            }
            catch (DirectoryNotFoundException)
            {
                Controller.CrearCarpetas();
            }
            catch (FileNotFoundException)
            {
                ProcesarArchivoJSON();
                ModificarDivisas();
            }



        }

        public static void AgregarDivisa()
        {
            List<Divisa> divisas = CogerDivisasDeJson();

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

            divisas.Add(new Divisa { Nombre = nuevoNombre, Codigo = nuevoCodigo, ValorEnDolares = (decimal)nuevoValor });
            Console.WriteLine("Divisa agregada exitosamente.");

            GuardarDivisas(divisas);
        }

        public static void EliminarDivisa()
        {
            List<Divisa> divisas = CogerDivisasDeJson();
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

            Divisa divisaEncontrada = divisas.Find(divisa => divisa.Nombre == nombreDivisaEliminar);
            if (divisaEncontrada == null)
            {
                Console.WriteLine("No se encontró la divisa.");
                return;
            }

            divisas.Remove(divisaEncontrada);
            Console.WriteLine("Divisa eliminada exitosamente.");

            GuardarDivisas(divisas);
        }

        

        private static void GuardarDivisas(List<Divisa> divisas)
        {
            string rutaArchivoFinal = Path.Combine("C:/archivos/final", "monedas_final.json");
            string json = JsonConvert.SerializeObject(divisas, Formatting.Indented);
            File.WriteAllText(rutaArchivoFinal, json);
            Console.WriteLine("Cambios guardados en el archivo JSON.");
        }

        public static void MostrarListadoDivisas(List<Divisa> divisas)
        {
            
        }

        public static List<Divisa> CogerDivisasDeJson()
        {
            string json = File.ReadAllText(rutaArchivoJSON);
            ResultadoApiMonedas resultadoApiMonedas = JsonConvert.DeserializeObject<ResultadoApiMonedas>(json);
            return ProcesadorAPIMonedas.Cambiar(resultadoApiMonedas);
        }
        public static void MostrarHistorial()
        {

        }
    }
}

