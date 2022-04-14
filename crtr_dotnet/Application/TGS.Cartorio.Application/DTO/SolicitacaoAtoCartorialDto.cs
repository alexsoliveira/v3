using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoAtoCartorialDto
    {        
        public int IdSolicitacao { get; set; }
        public int IdAtoCartorial { get; set; }
        public long IdUsuario { get; set; }
        public bool IsChecked { get; set; }
    }
}
