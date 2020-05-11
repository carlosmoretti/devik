using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Handler.Interface
{
    public interface IHandle<TReq, TRes> where TReq : class
        where TRes : class
    {
        TRes Handle(TReq req);
    }
}
