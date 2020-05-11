using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using Microsoft.AspNetCore.Hosting.Server;
using App.CQRS.Handler;
using App.CQRS.Response;
using App.CQRS.Request;
using Service.Interface;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BuscarPacoteHandle _handler;
        private ITrackingService _tracking;

        public HomeController(ILogger<HomeController> logger, BuscarPacoteHandle handler, ITrackingService tracking)
        {
            _logger = logger;
            _handler = handler;
            _tracking = tracking;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Detail(BuscarPacoteRequest req)
        {
            return View(_handler.Handle(req, _tracking));
        }
    }
}
