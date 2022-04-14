using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio
{
    public class DadosMatrimonio
    {
        public long IdSolicitacao { get; set; }
        public long IdUsuario { get; set; }
        public DadosContracaoMatrimonio DadosContracaoMatrimonio { get; set; }
        public DadosRequerente DadosRequerente { get; set; }
        public DadosNoivos DadosNoivos { get; set; }
        public List<Testemunha> Testemunhas { get; set; }
    }
}
