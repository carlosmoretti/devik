using App.Contexto;
using App.CQRS.Request;
using App.CQRS.Response;
using Microsoft.EntityFrameworkCore;
using Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Handler.Interface
{
    public interface IAdicionarEmailHandle : IHandle<AdicionarEmailRequest, AdicionarEmailResponse>
    {
        AdicionarEmailResponse HandleContext(AdicionarEmailRequest req, DatabaseContext db);
    }
}
