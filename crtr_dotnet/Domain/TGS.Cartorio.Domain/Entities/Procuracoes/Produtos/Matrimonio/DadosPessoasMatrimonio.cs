using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio
{
    public class DadosPessoasMatrimonio
    {
        public string Nome { get; set; }
        public string IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string DataNascimento { get; set; }
        public string CamposExtras { get; set; }
    }
}
