using System.Collections.Generic;

namespace TGS.Cartorio.Application.DTO.Products.Matrimonio
{
    public class DadosMatrimonioDto
    {
        public long IdMatrimonio { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdUsuario { get; set; }
        public DadosContracaoMatrimonioDto DadosContracaoMatrimonio { get; set; }
        public DadosRequerenteDto DadosRequerente { get; set; }
        public DadosNoivosDto DadosNoivos { get; set; }
        public List<TestemunhaDto> Testemunhas { get; set; }
        public bool AlteracaoDaSolicitacao { get; set; }
    }
}
