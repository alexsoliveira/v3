using System;
using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Solicitacoes
    {
        public Solicitacoes()
        {
            AssinaturaDigitalLog = new HashSet<AssinaturaDigitalLog>();
            SolicitacoesNotificacoes = new HashSet<SolicitacoesNotificacoes>();
            ProcuracoesPartes = new HashSet<ProcuracoesPartes>();
            SolicitacoesEstados = new HashSet<SolicitacoesEstados>();
            Matrimonios = new HashSet<Matrimonios>();
            SolicitacoesTaxas = new HashSet<SolicitacoesTaxas>();
        }

        public long IdSolicitacao { get; set; }
        public int IdProduto { get; set; }
        public int IdSolicitacaoEstado { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public int? IdCartorio { get; set; }
        public string CamposPagamento { get; set; }
        public decimal? ValorFrete { get; set; }
        public int? IdTipoFrete { get; set; }
        public string Conteudo { get; set; }
        public long? IdPessoa { get; set; }

        public virtual Cartorios IdCartorioNavigation { get; set; }
        public virtual Produtos IdProdutoNavigation { get; set; }
        public virtual SolicitacoesEstadosPc IdSolicitacaoEstadoNavigation { get; set; }
        public virtual TiposFretesPc IdTipoFreteNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual Pessoas IdPessoaSolicitanteNavigation { get; set; }
        public virtual ICollection<AssinaturaDigitalLog> AssinaturaDigitalLog { get; set; }
        public virtual ICollection<SolicitacoesEstados> SolicitacoesEstados { get; set; }
        public virtual ICollection<Matrimonios> Matrimonios { get; set; }
        public virtual ICollection<SolicitacoesNotificacoes> SolicitacoesNotificacoes { get; set; }
        public virtual ICollection<ProcuracoesPartes> ProcuracoesPartes { get; set; }
        public virtual ICollection<SolicitacoesTaxas> SolicitacoesTaxas { get; set; }
    }


}
