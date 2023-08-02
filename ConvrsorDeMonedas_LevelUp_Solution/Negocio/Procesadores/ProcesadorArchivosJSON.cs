using Datos;
using Newtonsoft.Json;
using System.Globalization;
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
                    ResultadoApiMonedas divisas = CogerResultadoApiMonedasDeJsonInbox();


                    Console.WriteLine("Contenido del archivo JSON procesado:");

                    string rutaArchivoBackup = Path.Combine("C:/archivos/backup", $"monedas_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                    File.Copy(rutaArchivoJSON, rutaArchivoBackup);

                    string rutaArchivoProgreso = Path.Combine("C:/archivos/proceso", $"monedas_progreso{DateTime.Now:yyyyMMdd_HHmmss}.json");
                    File.Move(rutaArchivoJSON, rutaArchivoProgreso);

                    string rutaArchivoFinal = Path.Combine("C:/archivos/final", $"monedas_final.json");
                    if (File.Exists(rutaArchivoFinal))
                        File.Delete(rutaArchivoFinal);

                    File.Move(rutaArchivoProgreso, rutaArchivoFinal);

                }
                catch (JsonReaderException e)
                {
                    Controller.err.MyStringProperty = e.Message;


                    string nombreArchivoError = $"monedas{DateTime.Now:yyyyMMdd_HHmmss}.err";
                    string rutaArchivoError = Path.Combine("C:/archivos/inbox", nombreArchivoError);

                    string rutaFinalerrorJSON = Path.Combine("C:/archivos/final", nombreArchivoError);
                    File.Move(rutaArchivoJSON, rutaFinalerrorJSON);
                }
                catch (Exception ex)
                {
                    Controller.err.MyStringProperty = $"Error general al leer o procesar el archivo JSON: {ex.Message}";
                }
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
            Controller.err.NuevoError("asd");
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

            GuardarDivisas(divisas);
        }

        public static void EliminarDivisa()
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
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }
            
        }

        

        public static void GuardarDivisas(List<Divisa> divisas)
        {

            ResultadoApiMonedas resultadoApiMonedas = ProcesadorAPIMonedas.CambiarAJsonApiMonedas(divisas);

            string rutaArchivoFinal = Path.Combine("C:/archivos/final", "monedas_final.json");
            string json = JsonConvert.SerializeObject(resultadoApiMonedas, Formatting.Indented);
            File.WriteAllText(rutaArchivoFinal, json);
            Console.WriteLine("Cambios guardados en el archivo JSON.");
        }

        public static List<Divisa> CogerDivisasDeJson()
        {
            ResultadoApiMonedas resultadoApiMonedas = CogerResultadoApiMonedasDeJson();


            return ProcesadorAPIMonedas.Cambiar(resultadoApiMonedas);
        }
        public static ResultadoApiMonedas CogerResultadoApiMonedasDeJson()
        {
            ResultadoApiMonedas a = new();
            try
            {
                string json = File.ReadAllText(rutaFinalJSON);
                 a = JsonConvert.DeserializeObject<ResultadoApiMonedas>(json);
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }
  
            return a;
        }
        public static ResultadoApiMonedas CogerResultadoApiMonedasDeJsonInbox()
        {
            ResultadoApiMonedas a = new();
            try
            {
                string json = File.ReadAllText(rutaArchivoJSON);
                a = JsonConvert.DeserializeObject<ResultadoApiMonedas>(json);
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }

            return a;
        }

        public static void MeterErrorEnJson(string err)
        {

        }
        public static void MostrarHistorial()
        {

        }
    }
}

