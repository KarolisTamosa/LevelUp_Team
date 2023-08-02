using Datos;
using Newtonsoft.Json;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Negocio
{
    
    public static class ProcesadorArchivoJSON
    {

        public static void CrearJsonConListaDivisa()
        {
            if(!ArchivosJSON.ExisteArchivoFinalJSON())
            {
                ResultadoApiMonedas listaDivisas = Monedas.ImportarMonedasDesdeApi();
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
                catch (JsonReaderException e)
                {
                    Controller.err.MyStringProperty = e.Message;

                    ArchivosJSON.CrearArchivoErrDeInbox();
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

            ResultadoApiMonedas resultadoApiMonedas = ProcesadorAPIMonedas.CambiarAJsonApiMonedas(divisas);

            ArchivosJSON.GuardarDivisas(resultadoApiMonedas);
            
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
                a = ArchivosJSON.CogerResultadoApiMonedasDeJson();
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
                a = ArchivosJSON.CogerResultadoApiMonedasDeJsonInbox();
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

