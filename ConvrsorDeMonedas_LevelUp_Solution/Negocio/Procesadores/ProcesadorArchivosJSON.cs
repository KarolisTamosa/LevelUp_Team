using Datos;
namespace Negocio
{
    
    public static class ProcesadorArchivoJSON
    {

        public static void CrearJsonConListaDivisa()
        {
            if(!ArchivosJSON.ExisteArchivoFinalJSON())
            {
                ResultadoApiDivisas listaDivisas = ApiDivisas.ImportarMonedasDesdeApi();
                ArchivosJSON.CrearArchivoJsonPorApi(listaDivisas);
            }


        }

        public static void ProcesarArchivoJSON()
        {

            if (ArchivosJSON.ExisteArchivoInboxJSON())
            {
                try
                {
                    ArchivosJSON.ProcesarArchivoJSON();
                }
                catch (Exception ex)
                {
                    Controller.err.NuevoError($"Error general al leer o procesar el archivo JSON: {ex.Message}");
                }
            }
        }

        public static void EliminarDivisa(string nombreDivisaEliminar)
        {
            try
            {
                List<Divisa> divisas = ProcesadorArchivoJSON.CogerDivisasDeJson();


                Divisa divisaEncontrada = divisas.Find(divisa => divisa.Nombre == nombreDivisaEliminar);
                if (divisaEncontrada == null)
                {
                    return;
                }

                divisas.Remove(divisaEncontrada);
                GuardarDivisas(divisas);
            }
            catch (Exception e)
            {

               Controller.err.NuevoError(e.Message);
            }
            
        }

        public static void GuardarDivisas(List<Divisa> divisas)
        {

            ResultadoApiDivisas resultadoApiMonedas = ProcesadorDivisas.CambiarListaDivisaAResultadoApiMonedas(divisas);

            ArchivosJSON.GuardarDivisas(resultadoApiMonedas);
            
        }

        public static List<Divisa> CogerDivisasDeJson()
        {
            ResultadoApiDivisas resultadoApiMonedas = CogerResultadoApiMonedasDeJson();


            return ProcesadorDivisas.CambiarResultadoApiMonedasAListaDivisa(resultadoApiMonedas);
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJson()
        {
            ResultadoApiDivisas a = new();
            try
            {
                a = ArchivosJSON.CogerResultadoApiMonedasDeJson();
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }

            return a;
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJsonInbox()
        {
            ResultadoApiDivisas a = new();
            try
            {
                a = ArchivosJSON.CogerResultadoApiMonedasDeJsonInbox();
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }

            return a;
        }
        public static void ResetearDatosJsonListadoDivisa()
        {
            ArchivosJSON.EliminarRutaFinalJSON();
            ProcesadorArchivoJSON.CrearJsonConListaDivisa();
            ProcesadorArchivoJSON.ProcesarArchivoJSON();
        }

        public static void MeterErrorEnJson(string err)
        {

        }
        public static void MostrarHistorial()
        {

        }
    }
}

