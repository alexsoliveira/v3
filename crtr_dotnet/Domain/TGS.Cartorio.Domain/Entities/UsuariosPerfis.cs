using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class UsuariosPerfis
    {
        public int IdUsuarioPerfil { get; set; }
        public long IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuarioOperacao { get; set; }

        public virtual Perfis IdPerfilNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual Usuarios IdUsuarioOperacaoNavigation { get; set; }
    }
}
