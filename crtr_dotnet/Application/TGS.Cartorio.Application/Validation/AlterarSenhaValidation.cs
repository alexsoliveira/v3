using FluentValidation;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.ViewModel.Identity;

namespace TGS.Cartorio.Application.Validation
{
    public class AlterarSenhaValidation : AbstractValidator<UsuarioAlterarSenha>
    {
        public AlterarSenhaValidation()
        {
            RuleFor(n => n.UserId)
             .NotEmpty().WithMessage("Informe um usuário id");

            RuleFor(s => s.SenhaAtual)
                .NotEmpty().WithMessage("Informe a senha")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A senha de conter no máximo 100 caracteres")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial");

            RuleFor(s => s.NovaSenha)
               .NotEmpty().WithMessage("É necessário confirmar a senha")               
               .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$")
               .WithMessage("A confirmação de senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial");           
        }      
    }
}
