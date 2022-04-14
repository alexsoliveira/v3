using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Validation
{
    public class PessoasValidation : AbstractValidator<Pessoas>, IPessoasValidation
    {
        private readonly IUsuariosValidation _usuario;
        private readonly IPessoasAppService _pessoas;

        public PessoasValidation(IUsuariosValidation usuario,
                                 IPessoasAppService pessoas
            )
        {
            _usuario = usuario;
            _pessoas = pessoas;

            RuleFor(x => x.Documento.ToString())
                .Length(11)
                .When(x => x.IdTipoDocumento == 2) /*CPF*/
                .WithMessage("O documento informado é inválido.");

            RuleFor(x => x.Documento.ToString())
                .Length(14)
                .When(x => x.IdTipoDocumento == 5) /*CNPJ*/
                .WithMessage("O documento informado é inválido.");

            RuleFor(p => p.IdUsuario)
            .Must(_usuario.ValidarUsuario)
            .WithMessage("Usuário informado não está cadastrado.");
        }

        public bool ValidarPessoa(long IdPessoa)
        {
            return ValidarPessoaAsync(IdPessoa).Result;
        }

        private async Task<bool> ValidarPessoaAsync(long IdPessoa)
        {
            var pessoa = await _pessoas.BuscarId(IdPessoa);
            return (pessoa != null);
        }
    }
}
