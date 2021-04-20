using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using HtmlAgilityPack;
using Model.Helpers;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service
{
    public class TrackingService : ITrackingService
    {
        public async Task<Produto> GetTracking(string codigo)
        {
            var values = new Dictionary<string, string>
            {
                { "acao", "track" },
                { "objetos", codigo },
                { "btnPesq", "Buscar" }
            };

            var body = new FormUrlEncodedContent(values);
            var cli = new HttpClient();

            var resp = await cli.PostAsync("https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm?", body);

            UTF8Encoding utf8 = new UTF8Encoding();

            HtmlDocument doc = new HtmlDocument();
            var retorno = await resp.Content.ReadAsStringAsync();
            doc.LoadHtml(retorno);

            Produto prd = new Produto();
            prd.Codigo = codigo;

            if (!doc.GetElementbyId("DataEntrega").InnerText.IsEmpty())
                prd.Previsao = Convert.ToDateTime(doc.GetElementbyId("DataEntrega").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            if (!doc.GetElementbyId("UltimoEvento").InnerText.IsEmpty())
                prd.UltimaMovimentacao = Convert.ToDateTime(doc.GetElementbyId("UltimoEvento").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            if (!doc.GetElementbyId("EventoPostagem").InnerText.IsEmpty())
                prd.Postagem = Convert.ToDateTime(doc.GetElementbyId("EventoPostagem").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            var tabelas = doc.DocumentNode.Descendants().Where(d => d.HasClass("listEvent") && d.HasClass("sro"));
            foreach (var item in tabelas)
            {
                Movimentacao mov = new Movimentacao();
                var fullobj = item.ChildNodes.Where(d => d.Name == "tr").FirstOrDefault();
                var datas = fullobj.ChildNodes[1].InnerText.Split("\r\n");

                mov.Data = Convert.ToDateTime($"{datas[0]} {datas[1]}");
                mov.Titulo = fullobj.ChildNodes[3].ChildNodes[1].InnerText;
                mov.Descricao = fullobj.ChildNodes[3].ChildNodes[3].InnerText;

                prd.Movimentacoes.Add(mov);
            }

            return prd;
        }
    }
}
