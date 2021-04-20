using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using RestSharp;
using HtmlAgilityPack;
using RestSharp.Serialization;
using Model;
using Model.Helpers;
using Service;
using Service.Interface;

namespace Correios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RastreioController : ControllerBase
    {
        private ITrackingService _trackingService;
        public RastreioController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpGet("Track/{codigo}")]
        public async Task<object> Track(string codigo)
        {
            return await _trackingService.GetTracking(codigo);
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}