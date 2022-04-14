using System;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public class Outorgados
    {
        public long IdProcuracaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long? IdPessoa { get; set; }
        public int IdTipoDocumento { get; set; }
        public long Documento { get; set; }
        public int IdTipoProcuracaoParte { get; set; }
        public int IdProcuracaoParteEstado { get; set; }
        public string JsonConteudo { get; set; }
        public string EnderecoEntrega { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
    }
}
