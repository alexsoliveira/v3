using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TGS.Cartorio.Infrastructure.Utility.Others;

namespace TGS.Cartorio.Application.DTO
{
    public class PessoasDto
    {
        [Required(ErrorMessage = "Informe o campo IdSolicitacaoParte.")]
        public long IdSolicitacaoParte { get; set; }
        public string TipoDocumentoDescricao { get; set; }
        [Required(ErrorMessage = "Informe o campo IdTipoDocumento.")]
        public int IdTipoDocumento { get; set; }
        [Required(ErrorMessage = "Informe o campo NumeroDocumento.")]
        public long NumeroDocumento { get; set; }
        public string TipoParteDescricao { get; set; }
        [Required(ErrorMessage = "Informe o campo IdTipoParte.")]
        public int IdTipoParte { get; set; }


        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value.GetValidEmail(); }

        }

    }
}
