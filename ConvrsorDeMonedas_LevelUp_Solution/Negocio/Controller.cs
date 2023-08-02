using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Controller
    {
        public static ErrorChange err = new ErrorChange();
        public static void CrearCarpetas()
        {
            ArchivosJSON.MirarSiCarpetasEstanCreadasYCrearlas();
        }

        public static void ResetearDatosJsonListadoDivisa()
        {
            ArchivosJSON.EliminarRutaFinalJSON();
            ProcesadorArchivoJSON.CrearJsonConListaDivisa();
            ProcesadorArchivoJSON.ProcesarArchivoJSON();
        }
    }
}
