using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Configuracoes
    {
        public int IdConfiguracao { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataOperacao { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
