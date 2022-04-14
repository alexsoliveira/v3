using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.ViewModel
{
    public class SolicitacoesAtosCartoriaisViewModel
    {
        public long IdSolicitacoaAtoCartoral { get; set; }
        public long IdSolicitacao { get; set; }
        public int IdAtoCartorial { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
