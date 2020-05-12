using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace App.CQRS.Response
{
    public class BuscarPacoteResponse : Response
    {
        public Produto Response { get; set; }
    }
}
