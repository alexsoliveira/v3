using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Application.DTO
{
    public class ProcuracoesPartesDto
    {
        public long IdProcuracaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdPessoa { get; set; }
        public int IdTipoProcuracaoParte { get; set; }
        public int IdProcuracaoParteEstado { get; set; }
        public string ConteudoPessoasFisicas { get; set; }
        public string ConteudoPessoasContatos { get; set; }
        public string JsonConteudo { get; set; }
        public long Documento { get; set; }
        public int IdTipoDocumento { get; set; }
        public string EnderecoEntrega { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
    }
}
