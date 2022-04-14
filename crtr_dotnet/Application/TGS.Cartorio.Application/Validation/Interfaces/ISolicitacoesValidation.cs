using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.Validation.Interfaces
{
    public interface ISolicitacoesValidation
    {
        bool ValidarSolicitacao(long IdSolicitacao);
    }
}
