using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Perfis
    {
        public Perfis()
        {
            PerfisPermissoes = new HashSet<PerfisPermissoes>();
            UsuariosPerfis = new HashSet<UsuariosPerfis>();
        }

        public int IdPerfil { get; set; }
        public string NomePerfil { get; set; }
        public DateTime DataOperacao { get; set; }
        public bool? FlagAtivo { get; set; }
        public long IdUsuarioOperacao { get; set; }

        public virtual Usuarios IdUsuarioOperacaoNavigation { get; set; }
        public virtual ICollection<PerfisPermissoes> PerfisPermissoes { get; set; }
        public virtual ICollection<UsuariosPerfis> UsuariosPerfis { get; set; }
    }
}
