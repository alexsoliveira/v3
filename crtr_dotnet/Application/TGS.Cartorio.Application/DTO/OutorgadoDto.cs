using System;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.DTO
{
    public class OutorgadoDto
    {
        public long IdProcuracaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdUsuario { get; set; }
        public long? IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public long Documento { get; set; }
        public string EnderecoEntrega { get; set; }
        public int IdTipoProcuracaoParte { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdProcuracaoParteEstado { get; set; }
        public string JsonConteudo { get; set; }
        public DateTime DataOperacao { get; set; }
    }
}
