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
            ViewBag.lista = listaMonedas;
            return View();//chris

        }

        [HttpPost]
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

        private decimal RealizarConversion(int importe, string monedaOrigen, string monedaDestino)
        {
            // Aquí implementa la lógica de conversión real
            // y devuelve el resultado calculado
            return importe * 2; // Esto es solo un ejemplo
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