using FluentValidation;
using Microsoft.Extensions.WebEncoders.Testing;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.Validation
{
    public class UsuariosValidation : AbstractValidator<Usuarios> , IUsuariosValidation
    {
        private readonly IUsuariosAppService _usuario;

        public UsuariosValidation(IUsuariosAppService usuario)
        {
            _usuario = usuario;

            RuleFor(x => x.Email)                
                .NotEmpty()
                .WithMessage("Informe o e-mail.");

            RuleFor(x => x.NomeUsuario)
                .NotEmpty()
                .WithMessage("Informe o nome de usuário.");

            RuleFor(x => x.Email)
                .Must(ValidarEmail)
                .WithMessage("E-mail informado já está cadastrado.");
        }
      
        private async Task<bool> _ValidarEmail(string Email)
        {
            var usuario = await _usuario.BuscarTodos(x => x.Email == Email);
            return (usuario == null);
        }
        public bool ValidarEmail(string Email)
        {
            return _ValidarEmail(Email).Result;
        }
        private async Task<bool> ValidarUsuarioAsync(long IdUsuario)
        {
            var usuario = await _usuario.BuscarId(IdUsuario);
            return (usuario != null);
        }
        public bool ValidarUsuario(long IdUsuario)
        {
            return ValidarUsuarioAsync(IdUsuario).Result;
        }
    }
}
