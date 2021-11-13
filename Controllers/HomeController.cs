    using CloudComputingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(string dni)
        {
            Data.Conexion con = new Data.Conexion();
            Task<Datos> dat = con.GetPerson(dni);
            return View(await dat);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(string nomber,string apellido, string dni)
        {
            Data.Conexion con = new Data.Conexion();
            await con.CrearPersona(nomber,apellido,dni);
            return Redirect("Index");
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
