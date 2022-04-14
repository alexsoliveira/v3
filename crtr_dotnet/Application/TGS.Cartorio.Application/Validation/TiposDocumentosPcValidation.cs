using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation
{
    public class TiposDocumentosPcValidation : AbstractValidator<TiposDocumentosPc>, ITiposDocumentosPcValidation
    {
        private readonly ITiposDocumentosPcAppService _tiposDocumentosPcAppService;
        
        public TiposDocumentosPcValidation(ITiposDocumentosPcAppService tiposDocumentosPcAppService)
        {
            _tiposDocumentosPcAppService = tiposDocumentosPcAppService;
        }


        public bool ValidarTipoDocumento(int IdTipoDocumento)
        {
            if (!ValidarTipoDocumentoAsync(IdTipoDocumento).Result) return false;
         
            return true;
        }

        public bool ValidarTipoDocumento(int IdTipoDocumento, long documento = 0)
        {
            if (!ValidarTipoDocumentoAsync(IdTipoDocumento).Result) return false;

            if (documento > 0)
            {
                if ((IdTipoDocumento == (int)Domain.Enumerables.ETiposDocumentosPC.CPF) && documento.ToString().Length > 11) return false;
            }

            return true;
        }

        public async Task<bool> ValidarTipoDocumentoAsync(int IdTipoDocumento)
        {
            var tipodocumento = await _tiposDocumentosPcAppService.BuscarId(IdTipoDocumento);

            return (tipodocumento != null);
        }
    }
}
