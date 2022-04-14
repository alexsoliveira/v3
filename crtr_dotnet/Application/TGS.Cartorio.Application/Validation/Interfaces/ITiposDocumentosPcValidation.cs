using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.Validation.Interfaces
{
    public interface ITiposDocumentosPcValidation
    {
        bool ValidarTipoDocumento(int IdTipoDocumento, long documento = 0);
        bool ValidarTipoDocumento(int IdTipoDocumento);
    }
}
