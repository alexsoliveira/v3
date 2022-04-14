using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class PessoasEnderecos
    {
        public long IdPessoaEndereco { get; set; }
        public long IdPessoa { get; set; }
        public int IdEndereco { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public bool? FlagAtivo { get; set; }

        public virtual Enderecos IdEnderecoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
