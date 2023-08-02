﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{

    public static class ArchivosJSON
    {
        private static string rutaArchivoJSON = Path.Combine(@"C:\archivos\inbox", "monedas.json");

        public static void CrearArchivoJsonPorApi(ResultadoApiMonedas resultadoApi)
        {
            string json = JsonConvert.SerializeObject(resultadoApi, Formatting.Indented);
            if (!File.Exists(rutaArchivoJSON))
            {
                File.Create(rutaArchivoJSON);//.Close();
            }
            File.WriteAllText(rutaArchivoJSON, json);
        }
    }
}
