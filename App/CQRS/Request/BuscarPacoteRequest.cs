using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Request
{
    public class BuscarPacoteRequest
    {
        [DisplayName("Código de Rastreio")]
        public string NumeroPacote { get; set; }
    }
}
