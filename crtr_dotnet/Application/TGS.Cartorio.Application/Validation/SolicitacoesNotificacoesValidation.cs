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
    public class SolicitacoesNotificacoesValidation : AbstractValidator<SolicitacoesNotificacoes>
    {
        private readonly ISolicitacoesValidation _solicitacoesvalidation;
        private readonly IUsuariosValidation _usuariosValidation;

        public SolicitacoesNotificacoesValidation(ISolicitacoesValidation solicitacoesvalidation,
            IUsuariosValidation usuariosValidation
            )
        {
            _solicitacoesvalidation = solicitacoesvalidation;
            _usuariosValidation = usuariosValidation;

            RuleFor(p => p.Titulo).NotEmpty().WithMessage("Informe o título.");
            RuleFor(p => p.Conteudo).NotEmpty().WithMessage("Informe o conteúdo.");
            RuleFor(p => p.IdSolicitacao).Must(_solicitacoesvalidation.ValidarSolicitacao).WithMessage("Número da solicitação inválido.");
            RuleFor(p => p.IdUsuario).Must(_usuariosValidation.ValidarUsuario).WithMessage("Usuário inválido.");
        }

       



    }
}
