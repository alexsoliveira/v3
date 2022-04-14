using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class GenerosPc
    {
        public GenerosPc()
        {
            PessoasFisicas = new HashSet<PessoasFisicas>();
            PessoasJuridicas = new HashSet<PessoasJuridicas>();
        }

        public int IdGenero { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<PessoasFisicas> PessoasFisicas { get; set; }
        public virtual ICollection<PessoasJuridicas> PessoasJuridicas { get; set; }
    }
}
