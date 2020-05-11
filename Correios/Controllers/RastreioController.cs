using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using RestSharp;
using HtmlAgilityPack;
using Correios.Model;
using RestSharp.Serialization;
using Correios.Helpers;

namespace Correios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RastreioController : ControllerBase
    {
        [HttpGet("Track/{codigo}")]
        public object Track(string codigo)
        {
            var req = new RestRequest();
            var opt = new RestClient("https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm?");

            req.Method = Method.POST;
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
            req.AddHeader("Accept", "application/json");
            req.AddParameter("acao", "track");
            req.AddParameter("objetos", codigo);
            req.AddParameter("btnPesq", "Buscar");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(opt.Execute(req).Content);

            Produto prd = new Produto();

            if(!doc.GetElementbyId("DataEntrega").InnerText.IsEmpty())
                prd.Previsao = Convert.ToDateTime(doc.GetElementbyId("DataEntrega").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            if(!doc.GetElementbyId("UltimoEvento").InnerText.IsEmpty())
                prd.UltimaMovimentacao = Convert.ToDateTime(doc.GetElementbyId("UltimoEvento").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            if (!doc.GetElementbyId("EventoPostagem").InnerText.IsEmpty())
                prd.Postagem = Convert.ToDateTime(doc.GetElementbyId("EventoPostagem").Descendants().ToList()[4].InnerText.Replace("\r\n", "").Replace("\t", ""));

            var tabelas = doc.DocumentNode.Descendants().Where(d=>d.HasClass("listEvent") && d.HasClass("sro"));
            foreach(var item in tabelas)
            {
                Movimentacao mov = new Movimentacao();
                var fullobj = item.ChildNodes.Where(d=>d.Name == "tr").FirstOrDefault();
                var datas = fullobj.ChildNodes[1].InnerText.Split("\r\n");

                mov.Data = Convert.ToDateTime($"{datas[0]} {datas[1]}");
                mov.Titulo = fullobj.ChildNodes[3].ChildNodes[1].InnerText;
                mov.Descricao = fullobj.ChildNodes[3].ChildNodes[4].InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", " ").Replace("  ", " ").Substring(1);

                prd.Movimentacoes.Add(mov);
            }

            return prd;
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}