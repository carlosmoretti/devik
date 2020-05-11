using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Movimentacao
    {
        public Movimentacao()
        {
            Data = new DateTime();
        }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
    }
}
