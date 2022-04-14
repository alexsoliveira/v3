using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoParteDto
    {
        public long IdSolicitacaoParte { get; set; }
        public int IdTipoParte { get; set; }
        public int IdTipoDocumento { get; set; }
        public long NumeroDocumento { get; set; }
        public string NomePessoa { get; set; }
        public string NomeSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }                         
        public string Email { get; set; }
        public long IdSolicitacaoParteEstado { get; set; }
        public SolicitacaoDocumentoDto SolicitacoesDocumentos { get; set; }
    }
}
