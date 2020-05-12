using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Produto
    {
        public Produto()
        {
            this.Movimentacoes = new List<Movimentacao>();
        }
        public DateTime? Postagem { get; set; }
        public DateTime? UltimaMovimentacao { get; set; }
        public DateTime? Previsao { get; set; }
        public string Location { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
    }
}
