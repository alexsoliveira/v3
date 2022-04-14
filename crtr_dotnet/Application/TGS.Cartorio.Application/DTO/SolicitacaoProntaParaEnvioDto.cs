using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoProntaParaEnvioDto
    {
        public Solicitacoes solicitacoes { get; set; }
        public List<ProcuracoesPartesDto> procuracoesPartes { get; set; }
        public MatrimoniosDto matrimonios { get; set; }
        public IEnumerable<MatrimoniosDocumentos> matrimoniosDocumentos { get; set; }
        public string InformacoesImportantes { get; set; }
    }
}
