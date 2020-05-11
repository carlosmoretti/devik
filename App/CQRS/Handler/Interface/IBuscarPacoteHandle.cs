using App.CQRS.Request;
using App.CQRS.Response;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Handler.Interface
{
    public interface IBuscarPacoteHandle<TReq, TRes> : IHandle<TReq, TRes> 
        where TRes : class 
        where TReq : class
    {
        BuscarPacoteResponse Handle(BuscarPacoteRequest req, ITrackingService _service);
    }
}
