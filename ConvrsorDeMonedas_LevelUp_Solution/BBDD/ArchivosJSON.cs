using Newtonsoft.Json;

namespace Datos
{

    public static class ArchivosJSON
    {
        private static string rutaArchivoJSON = Path.Combine(@"C:\archivos\inbox", "monedas.json");
        private static string rutaFinalJSON = Path.Combine("C:/archivos/final", "monedas_final.json");


        public static void CrearArchivoJsonPorApi(ResultadoApiDivisas resultadoApi)
        {
            string json = JsonConvert.SerializeObject(resultadoApi, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(rutaArchivoJSON)) 
            {
                sw.Write(json);
            }
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
            if (File.Exists(rutaFinalJSON))
            {
                File.Delete(rutaFinalJSON);
            }

        }

        public static void ProcesarArchivoJSON()
        {

            ResultadoApiDivisas divisas = CogerResultadoApiMonedasDeJsonInbox();

            string rutaArchivoBackup = Path.Combine("C:/archivos/backup", $"monedas_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            File.Copy(rutaArchivoJSON, rutaArchivoBackup);

            string rutaArchivoProgreso = Path.Combine("C:/archivos/proceso", $"monedas_progreso{DateTime.Now:yyyyMMdd_HHmmss}.json");
            File.Move(rutaArchivoJSON, rutaArchivoProgreso);

            string rutaArchivoFinal = Path.Combine("C:/archivos/final", $"monedas_final.json");
            if (File.Exists(rutaArchivoFinal))
                File.Delete(rutaArchivoFinal);

            File.Move(rutaArchivoProgreso, rutaArchivoFinal);
          
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJson()
        {
                string json = File.ReadAllText(rutaFinalJSON);
                return JsonConvert.DeserializeObject<ResultadoApiDivisas>(json);
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJsonInbox()
        {

                string json = File.ReadAllText(rutaArchivoJSON);
                return JsonConvert.DeserializeObject<ResultadoApiDivisas>(json);
        }

        public static void CrearArchivoErrDeInbox()
        {
            string nombreArchivoError = $"monedas{DateTime.Now:yyyyMMdd_HHmmss}.err";
            string rutaArchivoError = Path.Combine("C:/archivos/inbox", nombreArchivoError);

            string rutaFinalerrorJSON = Path.Combine("C:/archivos/final", nombreArchivoError);
            File.Move(rutaArchivoJSON, rutaFinalerrorJSON);
        }

        public static bool ExisteArchivoFinalJSON()
        {
            return File.Exists(rutaFinalJSON);
        }
        public static bool ExisteArchivoInboxJSON()
        {
            return File.Exists(rutaArchivoJSON);
        }

        public static void GuardarDivisas(ResultadoApiDivisas resultadoApiMonedas)
        {
            string rutaArchivoFinal = Path.Combine("C:/archivos/final", "monedas_final.json");
            string json = JsonConvert.SerializeObject(resultadoApiMonedas, Formatting.Indented);
            File.WriteAllText(rutaArchivoFinal, json);
        }
    }
}
