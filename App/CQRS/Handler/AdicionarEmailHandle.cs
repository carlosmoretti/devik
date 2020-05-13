using App.Contexto;
using App.CQRS.Handler.Interface;
using App.CQRS.Request;
using App.CQRS.Response;
using Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CQRS.Handler
{
    public class AdicionarEmailHandle : IAdicionarEmailHandle
    {
        public AdicionarEmailResponse Handle(AdicionarEmailRequest req)
        {
            throw new NotImplementedException();
        }

        public AdicionarEmailResponse HandleContext(AdicionarEmailRequest req, DatabaseContext db)
        {

            if (String.IsNullOrEmpty(req.Email) || !req.Email.Contains("@"))
                return new AdicionarEmailResponse()
                {
                    Info = new Models.InfoUsuarioViewModel()
                    {
                        Color = "danger",
                        Message = "Este e-mail não é válido! Insira um correto para prosseguir."
                    }
                };

            if (!db.Emails.ToList().Any(d => d.Endereco == req.Email && d.CodigoPacote == req.CodigoPacote))
            {
                db.Emails.Add(new Email()
                {
                    CodigoPacote = req.CodigoPacote,
                    Endereco = req.Email
                });
                db.SaveChanges();
            }
            else
                return new AdicionarEmailResponse()
                {
                    Info = new Models.InfoUsuarioViewModel()
                    {
                        Color = "danger",
                        Message = "Este e-mail já está associado para este pacote."
                    }
                };

            return new AdicionarEmailResponse()
            {
                Info = new Models.InfoUsuarioViewModel()
                {
                    Color = "success",
                    Message = "O e-mail foi associado a este pacote."
                }
            };
        }
    }
}
