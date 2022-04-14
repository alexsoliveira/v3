using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class PessoasJuridicas
    {
        public long IdPessoaJuridica { get; set; }
        public long IdPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int IdGenero { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

        public virtual GenerosPc IdGeneroNavigation { get; set; }
        public virtual Pessoas IdPessoaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
