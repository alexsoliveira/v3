using FluentValidation;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Infrastructure.Utility.Validators;

namespace TGS.Cartorio.Application.Validation
{
    public class ContaValidation : AbstractValidator<UsuarioRegistro>
    {
        private readonly IUsuariosAppService _usuario;

        public ContaValidation(IUsuariosAppService usuario)
        {
            _usuario = usuario;

            RuleFor(n => n.Nome)
             .NotEmpty().WithMessage("Informe um nome");

            RuleFor(n => n.Documento)
             .NotEmpty().WithMessage("Informe um Documento");
             //.Must(CPFValidation.isValid).WithMessage("CPF informado é inválido");

            RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Informe um e-mail")
            .EmailAddress().WithMessage("E-mail inválido")
            .Must(ValidarEmail).WithMessage("E-mail informado já está cadastrado.");

            RuleFor(s => s.Senha)
                .NotEmpty().WithMessage("Informe a senha")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A senha de conter no máximo 100 caracteres")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial");

            RuleFor(s => s.SenhaConfirmacao)
               .NotEmpty().WithMessage("É necessário confirmar a senha")               
               .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$")
               .WithMessage("A confirmação de senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial");

            RuleFor(s => s.SenhaConfirmacao).Equal(s => s.Senha)
                .WithMessage("As senhas não conferem");
        }

        private async Task<bool> _ValidarEmail(string Email)
        {
            var usuario = await _usuario.BuscarTodos(x => x.Email == Email);
            return (usuario.Count == 0);
        }
        public bool ValidarEmail(string Email)
        {
            return _ValidarEmail(Email).Result;
        }        
    }
}
