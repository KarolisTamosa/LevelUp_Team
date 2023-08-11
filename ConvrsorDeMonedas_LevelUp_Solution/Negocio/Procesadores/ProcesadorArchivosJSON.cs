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
                if (listaDivisas.Result == "False")
                {
                    Controller.err.NuevoError(listaDivisas.Documentation+" || Lo sentimos, pero no puede utilizar la aplicación. Contacte con su administrador");
                }
                else
                {
                    ArchivosJSON.CrearArchivoJsonPorApi(listaDivisas);
                }


            }


        }

        public static void ProcesarArchivoJSON()
        {

            if (ArchivosJSON.ExisteArchivoInboxJSON())
            {
                string? error;
                ArchivosJSON.ProcesarArchivoJSON(out error);

                if (error is not null) Controller.err.NuevoError(error);
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

            ArchivosJSON.GuardarDivisasFinal(resultadoApiMonedas);
            
        }

        public static List<Divisa> CogerDivisasDeJson()
        {
            ResultadoApiDivisas resultadoApiMonedas = CogerResultadoApiMonedasDeJson();


            return ProcesadorDivisas.CambiarResultadoApiMonedasAListaDivisa(resultadoApiMonedas);
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJson()
        {
            ResultadoApiDivisas resultadoApi = new();
            try
            {
                resultadoApi = ArchivosJSON.CogerResultadoApiMonedasDeJson();
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }

            return resultadoApi;
        }
        public static ResultadoApiDivisas CogerResultadoApiMonedasDeJsonInbox()
        {
            ResultadoApiDivisas resultadoApi = new();
            try
            {
                resultadoApi = ArchivosJSON.CogerResultadoApiMonedasDeJsonInbox();
            }
            catch (Exception e)
            {

                Controller.err.MyStringProperty = e.Message;
            }

            return resultadoApi;
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

