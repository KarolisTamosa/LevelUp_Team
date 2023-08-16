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
            return View();

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