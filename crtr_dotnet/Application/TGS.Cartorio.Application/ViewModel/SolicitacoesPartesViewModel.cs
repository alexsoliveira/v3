using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.ViewModel
{
    public class SolicitacoesPartesViewModel
    {
        public long IdSolicitacaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdPessoa { get; set; }
        public int IdTipoParte { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

        public int IdSolicitacaoParteEstado { get; set; }
        public string Conteudo { get; set; }
        public string EnderecoEntrega { get; set; }
        public string Email { get; set; }
        public virtual ICollection<SolicitacoesDocumentos> SolicitacoesDocumentos { get; set; }
        public virtual Pessoas IdPessoaNavigation { get; set; }
    }
}
