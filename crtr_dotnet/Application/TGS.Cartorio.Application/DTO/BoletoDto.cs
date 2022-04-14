using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Application.DTO
{
    public class BoletoDto
    {
        public BoletoDto(long idSolicitacao, decimal valorCobrar, string[] emails, PagadorBoletoDto pagador)
        {
            DataVencimento = DateTime.Now.ToString("ddMMyyyy");
            Mensagem = $"Referente a Solicitação {idSolicitacao}";
            UrlNotificacao = "";
            Valor = valorCobrar;
            Emails = emails;
            Pagador = pagador;
        }

        public string DataVencimento { get; set; }
        public string Mensagem { get; set; }
        public string UrlNotificacao { get; set; }
        public decimal Valor { get; set; }
        public PagadorBoletoDto Pagador { get; set; }
        public IList<string> Emails { get; set; }
    }
}
