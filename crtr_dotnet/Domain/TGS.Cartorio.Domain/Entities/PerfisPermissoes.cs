using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class PerfisPermissoes
    {
        public int IdPerfilPermissao { get; set; }
        public int IdPerfil { get; set; }
        public string Permissoes { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

        public virtual Perfis IdPerfilNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
