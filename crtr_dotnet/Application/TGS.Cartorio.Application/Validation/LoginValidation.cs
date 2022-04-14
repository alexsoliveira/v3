using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.ViewModel.Identity;

namespace TGS.Cartorio.Application.Validation
{
    public class LoginValidation : AbstractValidator<UsuarioLogin>
    {
        private readonly IUsuariosAppService _usuario;

        public LoginValidation(IUsuariosAppService usuario)
        {
            _usuario = usuario;

            RuleFor(e => e.Email)
                        .NotEmpty().WithMessage("Informe um e-mail")
                        .EmailAddress().WithMessage("E-mail inválido")
                        .Must(ValidarEmail).WithMessage("Usuário e/ou senha inválidos");

            RuleFor(s => s.Senha)
                .NotEmpty().WithMessage("Informe a senha")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A senha de conter no máximo 100 caracteres")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial");
        }

        private async Task<bool> _ValidarEmail(string Email)
        {
            var usuario = await _usuario.BuscarTodos(x => x.Email == Email);
            return (usuario.Count > 0);
        }
        public bool ValidarEmail(string Email)
        {
            return _ValidarEmail(Email).Result;
        }
    }
}
