using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class ProcuracoesPartes
    {
        public ProcuracoesPartes()
        {
            MatrimoniosDocumentos = new HashSet<MatrimoniosDocumentos>();
            ProcuracoesPartesEstadosNavigation = new HashSet<ProcuracoesPartesEstados>();
        }
        public long IdProcuracaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdPessoa { get; set; }
        public int IdTipoProcuracaoParte { get; set; }
        public int IdProcuracaoParteEstado { get; set; }
        public string JsonConteudo { get; set; }
        public string EnderecoEntrega { get; set; }
        public string Email { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

        public virtual Solicitacoes SolicitacoesNavigation { get; set; }
        public virtual Pessoas PessoasNavigation { get; set; }
        public virtual TiposProcuracoesPartesPc TiposProcuracoesPartesNavigation { get; set; }
        public virtual ProcuracoesPartesEstadosPc ProcuracoesPartesEstadosPcNavigation { get; set; }
        public virtual ICollection<MatrimoniosDocumentos> MatrimoniosDocumentos { get; set; }
        public virtual ICollection<ProcuracoesPartesEstados> ProcuracoesPartesEstadosNavigation { get; set; }
        public virtual Usuarios UsuarioNavigation { get; set; }
    }
}
