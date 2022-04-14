using System;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class PessoasFisicas
    {
        public long IdPessoaFisica { get; set; }
        public long IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string NomeSocial { get; set; }
        public int IdGenero { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string Conteudo { get; set; }

        public virtual GenerosPc IdGeneroNavigation { get; set; }
        public virtual Pessoas IdPessoaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
