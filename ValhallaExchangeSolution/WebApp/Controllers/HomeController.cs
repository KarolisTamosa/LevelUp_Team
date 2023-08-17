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

        public HomeController(ILogger<HomeController> logger, IMonedaService monedaService)
        {
            _logger = logger;
            _monedaService = monedaService;
        }
        //
        public IActionResult Index()
        {
            List<Moneda> lista = _monedaService.GetMonedas();
            if (lista.Count == 0)
            {
                var listaMonedas = new List<Moneda> {
                    new Moneda()
                    {
                        Codigo = "EUR",
                        Nombre = "Euro",
                        ValorEnDolares = 1.2,
                        Eliminado = false
                    },
                    new Moneda()
                    {
                        Codigo = "USD",
                        Nombre = "Dolar",
                        ValorEnDolares = 1,
                        Eliminado = false
                    }
                };
                _monedaService.MeterMonedas(listaMonedas);
                ViewBag.lista = _monedaService.GetMonedas();
            }
            else
            {
                ViewBag.lista = lista;
            }
            return View();//chris

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
                    Moneda monedaOrigen = _monedaService.ObtenerMonedaPorCodigo(codigoMonedaOrigen).Result;//getawaiter en vez de await para hacerlo sincrono y se pueda llamar en el html
                    Moneda monedaDestino = _monedaService.ObtenerMonedaPorCodigo(codigoMonedaDestino).Result;
                    if (monedaOrigen != null && monedaDestino != null && importe >= 0)
                    {
                         resultado = _monedaService.ObtenerResultadoConvertirMoneda(monedaOrigen, monedaDestino, importe).ToString();
                    }
                    else
                    {
                        resultado = "Alguna código de moneda no existe o el importe es negativo";
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