using FluentValidation;
using System.Text.RegularExpressions;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Validation
{
    public class EnderecoValidation : AbstractValidator<EnderecoDto>
    {
        public EnderecoValidation()
        {
            
            RuleFor(n => n.Cep)
             .NotEmpty().WithMessage("Informe um cep");

            RuleFor(n => n.Cep).Length(8)
            .WithMessage("Formato de cep inválido. Verifique a formatação!");
        }       
    }
}
