using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class PessoasFisicasDto : PessoasDto
    {
        [Required(ErrorMessage = "Informe o campo NomePessoa.")]
        public string NomePessoa { get; set; }
        public string NomeSocial { get; set; }

        //public ICollection<SolicitacaoDocumentoDto> SolicitacoesDocumentos { get; set; }
    }
}