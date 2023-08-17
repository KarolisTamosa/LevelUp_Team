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
            ViewBag.resultado = 0;
            ViewBag.lista = listaMonedas;
            return View();//chris

        }

        
        //public ActionResult Convertir()
        //{
        //    // Aquí puedes realizar la lógica de conversión y otras acciones
        //    // utilizando los parámetros MonedaOrigen, MonedaDestino e Importe

        //    //// Por ejemplo:
        //    //decimal importe = decimal.Parse(Importe);
        //    //decimal resultado = RealizarConversion(importe, MonedaOrigen, MonedaDestino);

        //    //ViewBag.Resultado = resultado;

        //    return View("Index"); // Redirigir de nuevo a la vista
        //}
        [HttpPost]
        public IActionResult RealizarConversion(int importe, string monedaOrigen, string monedaDestino)
        {
           ViewBag.resultado = importe * 2;
            return Json(new { ViewBag.resultado });
        }

        private async Task Provisional()
        {
            try
            {
                var input1 = "";
                var input2 = "";
                var importeInput = "";

                bool esNumerico = double.TryParse(importeInput, out double importe);

                if (!string.IsNullOrEmpty(input1) && !string.IsNullOrEmpty(input2) && esNumerico)
                {
                    Moneda monedaOrigen = _monedaService.ObtenerMonedaPorCodigo(input1).Result;//getawaiter en vez de await para hacerlo sincrono y se pueda llamar en el html
                    Moneda monedaDestino = _monedaService.ObtenerMonedaPorCodigo(input2).Result;
                    if (monedaOrigen != null && monedaDestino != null && importe >= 0)
                    {
                        double resultado = _monedaService.ObtenerResultadoConvertirMoneda(monedaOrigen, monedaDestino, importe);
                    }
                }

                //mensaje de error
            }
            catch (Exception ex)
            {

            }
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