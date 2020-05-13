using App.CQRS.Request;
using App.CQRS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class VisualizarPacoteViewModel
    {
        public VisualizarPacoteViewModel()
        {
            BuscarPacoteResponse = new BuscarPacoteResponse();
            AdicionarEmailResponse = new AdicionarEmailResponse();
            AdicionarEmailRequest = new AdicionarEmailRequest();
        }
        public BuscarPacoteResponse BuscarPacoteResponse { get; set; }
        public AdicionarEmailRequest AdicionarEmailRequest { get; set; }
        public AdicionarEmailResponse AdicionarEmailResponse { get; set; }
    }
}
