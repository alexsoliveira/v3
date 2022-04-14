using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation.Interfaces
{
    public interface IContaValidation
    {
        ContaValidation ValidarConta(UsuarioRegistro conta);
    }
}
