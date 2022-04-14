using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.Validation
{
    public class PessoasJuridicasValidation : AbstractValidator<PessoasJuridicas> 
    {
        private readonly IPessoasAppService _pessoaservice;
        private readonly IPessoasJuridicasAppService _pessoajuridica;
        private readonly IUsuariosValidation _usuario;

        public PessoasJuridicasValidation(IPessoasAppService pessoaservice,
            IPessoasJuridicasAppService pessoajuridica,
            IUsuariosValidation usuario
            )
        {
            _pessoaservice = pessoaservice;
            _pessoajuridica = pessoajuridica;
            _usuario = usuario;

            RuleFor(p => p.NomeFantasia).NotEmpty().WithMessage("Informe o nome fantasia.");
            RuleFor(p => p.RazaoSocial).NotEmpty().WithMessage("Informe a razão social.");

            RuleFor(p => p.IdGenero)
            .Must(ValidarGenero)
            .WithMessage("Informe o gênero correto.");

            RuleFor(p => p.IdPessoa)
            .Must(ValidarPessoaCadastrada)
            .WithMessage("Pessoa já está cadastrada.")
            .When(x => ValidarTipoPessoa(x.IdPessoa));

            RuleFor(p => p.IdPessoa)
            .Must(ValidarTipoPessoa)
            .WithMessage("Informe o gênero correto.");

            RuleFor(p => p.IdUsuario)
            .Must(_usuario.ValidarUsuario)
            .WithMessage("Usuário informado não está cadastrado.");
        }
        /// <summary>
        /// O gênero não pode ser 1-Feminino / 2-Masculino
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidarGenero(int value)
        {
            return !new List<int> { 1, 2 }.Contains(value);
        }

        /// <summary>
        /// O tipo do documento deve ser 5 - CNPJ
        /// </summary>
        /// <param name="IdPessoa"></param>
        /// <returns></returns>
        private async Task<bool> ValidarTipoPessoaAsync(long IdPessoa)
        {
            return !(new List<int>() { 5 }).Contains((await _pessoaservice.BuscarId(IdPessoa)).IdTipoDocumento);
        }
        private bool ValidarTipoPessoa(long IdPessoa)
        {
            return ValidarTipoPessoaAsync(IdPessoa).Result;
        }

        /// <summary>
        /// Verificar se já existe cadastro para pessoa informada
        /// </summary>
        /// <param name="IdPessoa"></param>
        /// <returns></returns>
        private async Task<bool> ValidarPessoaCadastradaAsync(long IdPessoa)
        {
            var pessoajuridica = await _pessoajuridica.BuscarTodos(x => x.IdPessoa == IdPessoa);

            return (pessoajuridica == null);
        }
        private bool ValidarPessoaCadastrada(long IdPessoa)
        {
            return ValidarPessoaCadastradaAsync(IdPessoa).Result;
        }


    }
}

