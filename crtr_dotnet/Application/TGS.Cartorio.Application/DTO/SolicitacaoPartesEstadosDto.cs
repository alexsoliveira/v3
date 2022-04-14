using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoPartesEstadosDto
    {        
        public long IdSolicitacaoParte { get; set; }
        public long IdSolicitacaoParteEstado { get; set; }
        public int IdTipoParte { get; set; }
        public int IdTipoDocumento { get; set; }
        public DateTime DataOperacao { get; set; }
        public string Conteudo { get; set; }
        public PessoasFisicasDto PessoaFisica { get; set; }
        public PessoasJuridicasDto PessoaJuridica { get; set; }

    }
}
