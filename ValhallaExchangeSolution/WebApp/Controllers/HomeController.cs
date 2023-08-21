using Domain.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMonedaService _monedaService;
        private readonly IApiMonedasService _apiMonedasService;

        public HomeController(ILogger<HomeController> logger, IMonedaService monedaService, IApiMonedasService apiMonedasService)
        {
            _logger = logger;
            _monedaService = monedaService;
            _apiMonedasService = apiMonedasService;
        }

        public IActionResult Index()
        {


            var lista = _monedaService.GetMonedas().GetAwaiter().GetResult().ToList();
            if (lista == null || lista.Count == 0)
            {
                var listaApi = _apiMonedasService.ObtenerListaMonedasDeApi();
                _monedaService.MeterMonedas(listaApi).GetAwaiter().GetResult();
                ViewBag.lista = _monedaService.GetMonedas().GetAwaiter().GetResult().ToList();
            }
            else
            {
                ViewBag.lista = lista;
            }

            return View();
        }


        [HttpPost]
        public string RealizarConversion(string importeInput, string codigoMonedaOrigen, string codigoMonedaDestino)

        {

            string resultado = "";
            try
            {

                bool esNumerico = double.TryParse(importeInput, out double importe);

                if (!string.IsNullOrEmpty(codigoMonedaOrigen) && !string.IsNullOrEmpty(codigoMonedaDestino) && esNumerico)
                {
                    Moneda monedaOrigen = _monedaService.ObtenerMonedaPorCodigo(codigoMonedaOrigen).GetAwaiter().GetResult();//getawaiter en vez de await para hacerlo sincrono y se pueda llamar en el html
                    Moneda monedaDestino = _monedaService.ObtenerMonedaPorCodigo(codigoMonedaDestino).GetAwaiter().GetResult();
                    if (monedaOrigen != null && monedaDestino != null && importe >= 0)
                    {
                         resultado = _monedaService.ObtenerResultadoConvertirMoneda(monedaOrigen, monedaDestino, importe).ToString();
                    }
                    else
                    {
                        resultado = "Algun código de moneda no existe o el importe es negativo";
                    }
                }
                else
                {
                    resultado = "Alguno de los valores que has metido es null";
                }
                

                //mensaje de error
            }
            catch (Exception ex)
            {
                resultado = "Error en la conversión de monedas.";
            }

            return resultado;

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}