using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class PessoasContatos
    {
        public long IdPessoaContato { get; set; }
        public long IdPessoa { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public bool? FlagAtivo { get; set; }
        public int IdContato { get; set; }

        public virtual Contatos IdContatoNavigation { get; set; }
        public virtual Pessoas IdPessoaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
