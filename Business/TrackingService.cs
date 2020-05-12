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

            req.OnBeforeDeserialization = resp => {
                if (resp.RawBytes.Length >= 3 && resp.RawBytes[0] == 0xEF && resp.RawBytes[1] == 0xBB && resp.RawBytes[2] == 0xBF)
                {
                    // Copy the data but with the UTF-8 BOM removed.
                    var newData = new byte[resp.RawBytes.Length - 3];
                    Buffer.BlockCopy(resp.RawBytes, 3, newData, 0, newData.Length);
                    resp.RawBytes = newData;

                    // Force re-conversion to string on next access
                    resp.Content = null;
                }
            };



            var opt = new RestClient("https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm?");

            opt.Encoding = Encoding.UTF8;

            req.Method = Method.POST;
            //req.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            opt.Encoding = Encoding.GetEncoding("ISO-8859-1");
            req.AddParameter("Accept", "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddParameter("Accept-Encoding", "gzip, deflate, br");
            req.AddParameter("Location", "../resultado.cfm");
            req.AddParameter("Content-Language", "pt-BR");
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
