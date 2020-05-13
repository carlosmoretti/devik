using System;
using System.Collections.Generic;
using System.Linq;
using Model.Persistence;

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
        public string Codigo { get; set; }
        public List<Email> Emails { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
    }
}
