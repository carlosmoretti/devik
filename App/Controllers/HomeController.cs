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
using App.Contexto;
using System.Runtime.CompilerServices;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BuscarPacoteHandle _handler;
        private ITrackingService _tracking;
        private AdicionarEmailHandle _adicionarEmailHandle;
        private DatabaseContext _db;

        public HomeController(ILogger<HomeController> logger, BuscarPacoteHandle handler, ITrackingService tracking, AdicionarEmailHandle adicionarEmailHandle, DatabaseContext db)
        {
            _logger = logger;
            _handler = handler;
            _tracking = tracking;
            _adicionarEmailHandle = adicionarEmailHandle;
            _db = db;
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
            return View(new Models.VisualizarPacoteViewModel()
            {
                BuscarPacoteResponse = _handler.Handle(req, _tracking),
                AdicionarEmailRequest = new AdicionarEmailRequest() { CodigoPacote = req.NumeroPacote }
            });
        }

        [HttpPost]
        public IActionResult CadastrarEmail(VisualizarPacoteViewModel req)
        {
            return View("Detail", new Models.VisualizarPacoteViewModel()
            {
                BuscarPacoteResponse = _handler.Handle(new BuscarPacoteRequest() { NumeroPacote = req.AdicionarEmailRequest.CodigoPacote }, _tracking),
                AdicionarEmailRequest = new AdicionarEmailRequest() { CodigoPacote = req.AdicionarEmailRequest.CodigoPacote },
                AdicionarEmailResponse = _adicionarEmailHandle.HandleContext(req.AdicionarEmailRequest, _db)
            });
        }
    }
}
