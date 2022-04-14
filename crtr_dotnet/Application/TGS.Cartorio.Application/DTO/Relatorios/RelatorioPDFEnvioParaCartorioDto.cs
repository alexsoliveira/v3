using TGS.Cartorio.Application.DTO.Products.Matrimonio;

namespace TGS.Cartorio.Application.DTO.Relatorios
{
    public class RelatorioPDFEnvioParaCartorioDto
    {
        public OutorgantesDto outorgantes { get; set; }
        public OutorgadoDto outorgados { get; set; }
        public DadosMatrimonioDto matrimonio { get; set; }
        public string InformacoesImportantes { get; set; }
    }    
}
