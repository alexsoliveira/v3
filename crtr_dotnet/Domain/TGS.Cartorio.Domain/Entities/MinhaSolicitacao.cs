using Newtonsoft.Json;
using System;
using System.Linq;

namespace TGS.Cartorio.Domain.Entities
{
    public class MinhaSolicitacao
    {
        public string CamposPagamento { get; set; }
        public Pessoas PessoaSolicitante { get; set; }
        public string EnderecoPagador { get; set; }
        public Solicitacoes Solicitacao { get; set; }


        public ContatosConteudo GetContatosConteudo()
        {
            try
            {
                if (PessoaSolicitante != null
                    && PessoaSolicitante.PessoasContatos != null
                    && PessoaSolicitante.PessoasContatos.Any()
                    && PessoaSolicitante.PessoasContatos.First().IdContatoNavigation != null)
                {
                    var contato = PessoaSolicitante.PessoasContatos.First().IdContatoNavigation;
                    return JsonConvert.DeserializeObject<ContatosConteudo>(contato.Conteudo);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
