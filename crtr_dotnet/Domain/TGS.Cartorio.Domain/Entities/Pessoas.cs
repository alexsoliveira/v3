using System;
using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Pessoas
    {
        public Pessoas()
        {
            Cartorios = new HashSet<Cartorios>();
            PessoasContatos = new HashSet<PessoasContatos>();
            Usuarios = new HashSet<Usuarios>();
            ProcuracoesPartes = new HashSet<ProcuracoesPartes>();
            Solicitacoes = new HashSet<Solicitacoes>();
        }

        public long IdPessoa { get; set; }
        public long Documento { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public bool? FlagAtivo { get; set; }

        public virtual TiposDocumentosPc IdTipoDocumentoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual PessoasFisicas PessoasFisicas { get; set; }
        public virtual PessoasJuridicas PessoasJuridicas { get; set; }
        public virtual ICollection<Cartorios> Cartorios { get; set; }
        public virtual ICollection<PessoasContatos> PessoasContatos { get; set; }
        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
        public virtual ICollection<ProcuracoesPartes> ProcuracoesPartes { get; set; }
    }
}
