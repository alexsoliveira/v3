using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Application.AppServices.Interfaces;
using System.Linq;

namespace TGS.Cartorio.Application.Validation
{
    public class TiposPartesPcValidation : AbstractValidator<TiposPartesPc>, ITiposPartesPcValidation
    {
        private readonly ITiposPartesPCAppService _tiposPartesPCAppService;
        public TiposPartesPcValidation(ITiposPartesPCAppService tiposPartesPCAppService)
        {
            _tiposPartesPCAppService = tiposPartesPCAppService;
        }

        public bool ValidarTipoPartes(int IdTipoParte)
        {
            return ValidarTipoPartesAsync(IdTipoParte).Result;
        }

        private async Task<bool> ValidarTipoPartesAsync(int IdTipoParte)
        {
            var _tipoparte = await _tiposPartesPCAppService.BuscarTodos(p => p.IdTipoParte == IdTipoParte);
            
            return (_tipoparte.FirstOrDefault() != null);
        }
    }
}
