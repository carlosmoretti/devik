using App.CQRS.Request;
using App.CQRS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Service;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using App.CQRS.Handler.Interface;

namespace App.CQRS.Handler
{
    public class BuscarPacoteHandle : IBuscarPacoteHandle<BuscarPacoteRequest, BuscarPacoteResponse>
    {
        public BuscarPacoteResponse Handle(BuscarPacoteRequest req, ITrackingService _service)
        {
            var res = new BuscarPacoteResponse();
            if (String.IsNullOrEmpty(req.NumeroPacote))
                return new BuscarPacoteResponse()
                {
                    Info = new Models.InfoUsuarioViewModel()
                    {
                        Color = "danger",
                        Message = "O código de rastreio não pode ser vazio."
                    }
                };

            return new BuscarPacoteResponse()
            {
                Response = _service.GetTracking(req.NumeroPacote)
            };
        }

        public BuscarPacoteResponse Handle(BuscarPacoteRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
