using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Request
{
    public class AdicionarEmailRequest
    {
        public string CodigoPacote { get; set; }
        public string Email { get; set; }
    }
}
