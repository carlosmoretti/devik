using App.CQRS.Request;
using App.CQRS.Response;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Handler.Interface
{
    public interface IBuscarPacoteHandle : IHandle<BuscarPacoteRequest, BuscarPacoteResponse>
    {
        BuscarPacoteResponse Handle(BuscarPacoteRequest req, ITrackingService _service);
    }
}
