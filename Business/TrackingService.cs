using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using HtmlAgilityPack;
using Model.Helpers;
using System.Linq;

namespace Service
{
    public class TrackingService : ITrackingService
    {
        public Produto GetTracking(string codigo)
        {
            var req = new RestRequest();
            var opt = new RestClient("https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm?");
            req.Method = Method.POST;
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("acao", "track");
            req.AddParameter("objetos", codigo);
            req.AddParameter("btnPesq", "Buscar");

            HtmlDocument doc = new HtmlDocument();
            var retorno = opt.Execute(req).Content;
            doc.LoadHtml(retorno);

            Produto prd = new Produto();

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
                mov.Titulo = fullobj.ChildNodes[3].ChildNodes[1].InnerText.ParseBOMChars();
                mov.Descricao = fullobj.ChildNodes[3].ChildNodes[4].InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", " ").Replace("  ", " ").Substring(1).ParseBOMChars();

                prd.Movimentacoes.Add(mov);
            }

            return prd;
        }
    }
}
