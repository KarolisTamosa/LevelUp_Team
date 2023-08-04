using Datos;

namespace Negocio
{
    public static class Controller
    {
        public static ErrorChange err = new ErrorChange();
        public static void CrearCarpetas()
        {
            ArchivosJSON.MirarSiCarpetasEstanCreadasYCrearlas();
        }

        
    }
}
