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
    public class PessoasFisicasValidation : AbstractValidator<PessoasFisicas>
    {
        private readonly IPessoasAppService _pessoaservice;
        private readonly IPessoasFisicasAppService _pessoafisica;
        private readonly IUsuariosValidation _usuario;

        public PessoasFisicasValidation(
            IPessoasFisicasAppService pessoafisica,
            IPessoasAppService pessoaservice,
            IUsuariosValidation usuario
            )
        {
            _pessoafisica = pessoafisica;
            _pessoaservice = pessoaservice;
            _usuario = usuario;

            RuleFor(p => p.NomePessoa).NotEmpty().WithMessage("Informe o nome da pessoa.");

            RuleFor(p => p.IdGenero)
                .Must(ValidarGenero)
                .WithMessage("Informe o gênero correto.");

            RuleFor(p => p.IdPessoa)
            .Must(ValidarTipoPessoa)
            .WithMessage("O tipo do documento informado para a pessoa está incorreto.");

            RuleFor(p => p.IdPessoa)
                .Must(ValidarPessoaCadastrada)
                .WithMessage("Pessoa já está cadastrada.")
                .When(x => ValidarTipoPessoa(x.IdPessoa));

            RuleFor(p => p.IdUsuario)
            .Must(_usuario.ValidarUsuario)
            .WithMessage("Usuário informado não está cadastrado.");

        }
        /// <summary>
        /// Generos válidos
        /// 1 -	Feminino
        /// 2 -	Masculino
        /// 6 -	Outros
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidarGenero(int value)
        {
            return new List<int> { 1, 2, 6 }.Contains(value);
        }

        /// <summary>
        /// Verificar se já existe cadastro para pessoa informada
        /// </summary>
        /// <param name="IdPessoa"></param>
        /// <returns></returns>
        private async Task<bool> ValidarPessoaCadastradaAsync(long IdPessoa)
        {
            var pessoafisica = await _pessoafisica.BuscarTodos(x => x.IdPessoa == IdPessoa);

            return (pessoafisica == null);
        }
        private bool ValidarPessoaCadastrada(long IdPessoa)
        {
            return ValidarPessoaCadastradaAsync(IdPessoa).Result;
        }

        /// <summary>
        /// O tipo do documento deve ser 2 - CPF
        /// </summary>
        /// <param name="IdPessoa"></param>
        /// <returns></returns>
        private async Task<bool> ValidarTipoPessoaAsync(long IdPessoa)
        {
            return (new List<int>() { 2 }).Contains((await _pessoaservice.BuscarId(IdPessoa)).IdTipoDocumento);
        }
        private bool ValidarTipoPessoa(long IdPessoa)
        {
            return ValidarTipoPessoaAsync(IdPessoa).Result;
        }
    }
}