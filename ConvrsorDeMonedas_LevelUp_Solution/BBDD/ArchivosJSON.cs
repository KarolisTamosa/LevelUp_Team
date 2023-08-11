using Newtonsoft.Json;

namespace Datos
{

    public static class ArchivosJSON
    {
        private static string RUTA_ARCHIVO_JSON = Path.Combine(@"C:\archivos\inbox", "monedas.json");
        private static string RUTA_FINAL_JSON = Path.Combine("C:/archivos/final", "monedas_final.json");


        public static void CrearArchivoJsonPorApi(ResultadoApiDivisas resultadoApi)
        {
            GuardarDivisas(resultadoApi, false);
        }

        public static void MirarSiCarpetasEstanCreadasYCrearlas()
        {
            if (!Directory.Exists(@"C:\archivos"))
            {
                Directory.CreateDirectory(@"C:\archivos");
                Directory.CreateDirectory(@"C:\archivos\inbox");
                Directory.CreateDirectory(@"C:\archivos\proceso");
                Directory.CreateDirectory(@"C:\archivos\backup");
                Directory.CreateDirectory(@"C:\archivos\final");
            }
            else
            {
                if (!Directory.Exists(@"C:\archivos\inbox"))
                {
                    Directory.CreateDirectory(@"C:\archivos\inbox");
                }
                if (!Directory.Exists(@"C:\archivos\proceso"))
                {
                    Directory.CreateDirectory(@"C:\archivos\proceso");
                }
                if (!Directory.Exists(@"C:\archivos\backup"))
                {
                    Directory.CreateDirectory(@"C:\archivos\backup");
                }
                if (!Directory.Exists(@"C:\archivos\final"))
                {
                    Directory.CreateDirectory(@"C:\archivos\final");
                }
            }
        }

        public static void EliminarRutaFinalJSON()
        {
            if (File.Exists(RUTA_FINAL_JSON))
            {
                File.Delete(RUTA_FINAL_JSON);
            }

        }

        public static void ProcesarArchivoJSON(out string? error)
        {
            error = null;

            try
            {
                string rutaArchivoBackup = Path.Combine("C:/archivos/backup", $"monedas_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                File.Copy(RUTA_ARCHIVO_JSON, rutaArchivoBackup);

                string rutaArchivoProgreso = Path.Combine("C:/archivos/proceso", $"monedas_progreso{DateTime.Now:yyyyMMdd_HHmmss}.json");
                File.Move(RUTA_ARCHIVO_JSON, rutaArchivoProgreso);

                string rutaArchivoFinal = Path.Combine("C:/archivos/final", $"monedas_final.json");
                if (File.Exists(rutaArchivoFinal))
                    File.Delete(rutaArchivoFinal);

                File.Move(rutaArchivoProgreso, rutaArchivoFinal);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            
          
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJson()
        {
                string json = File.ReadAllText(RUTA_FINAL_JSON);
                return JsonConvert.DeserializeObject<ResultadoApiDivisas>(json);
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJsonInbox()
        {

                string json = File.ReadAllText(RUTA_ARCHIVO_JSON);
                return JsonConvert.DeserializeObject<ResultadoApiDivisas>(json);
        }

        public static void CrearArchivoErrDeInbox()
        {
            string nombreArchivoError = $"monedas{DateTime.Now:yyyyMMdd_HHmmss}.err";
            string rutaArchivoError = Path.Combine("C:/archivos/inbox", nombreArchivoError);

            string rutaFinalerrorJSON = Path.Combine("C:/archivos/final", nombreArchivoError);
            File.Move(RUTA_ARCHIVO_JSON, rutaFinalerrorJSON);
        }

        public static bool ExisteArchivoFinalJSON()
        {
            return File.Exists(RUTA_FINAL_JSON);
        }
        public static bool ExisteArchivoInboxJSON()
        {
            return File.Exists(RUTA_ARCHIVO_JSON);
        }

        public static void GuardarDivisasFinal(ResultadoApiDivisas resultadoApiMonedas)
        {
            GuardarDivisas(resultadoApiMonedas, true);
        }
        public static void GuardarDivisas(ResultadoApiDivisas resultadoApiMonedas,bool esFinal)
        {
            try
            {
                string json = JsonConvert.SerializeObject(resultadoApiMonedas, Formatting.Indented);
                if (esFinal)
                {
                    File.WriteAllText(RUTA_FINAL_JSON, json);
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(RUTA_ARCHIVO_JSON))
                    {
                        sw.Write(json);
                    }
                }
            }
            catch (Exception ex)
            {

                //error = 
            }
            
        }
    }
}
