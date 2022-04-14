using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class PessoasJuridicasDto : PessoasDto
    {
        [Required(ErrorMessage = "Informe o campo NomeFantasia.")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Informe o campo RazaoSocial.")]
        public string RazaoSocial { get; set; }
        //public ICollection<SolicitacaoDocumentoDto> SolicitacoesDocumentos { get; set; }
    }
}
