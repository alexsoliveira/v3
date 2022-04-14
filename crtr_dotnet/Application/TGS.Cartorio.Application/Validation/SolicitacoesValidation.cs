using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Enumerables;

namespace TGS.Cartorio.Application.Validation
{
    public class SolicitacoesValidation : AbstractValidator<SolicitacoesDto>, ISolicitacoesValidation
    {
        private readonly IUsuariosValidation _usuariovalidation;
        private readonly IProdutosValidation _produtovalidation;
        private readonly ISolicitacoesAppService _solicitacoesappservice;

        public SolicitacoesValidation(IUsuariosValidation usuariovalidation,
                                      IProdutosValidation produtovalidation,
                                      ISolicitacoesAppService solicitacoesappservice)
        {
            _usuariovalidation = usuariovalidation;
            _produtovalidation = produtovalidation;
            _solicitacoesappservice = solicitacoesappservice;

            RuleFor(p => p.IdProduto)
               .Must(_produtovalidation.ValidarProduto)
               .WithMessage("Produto informado não está cadastrado.");

            RuleFor(p => p.IdUsuario)
                .Must(_usuariovalidation.ValidarUsuario)
                .WithMessage("Usuário informado não está cadastrado.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Informe o e-mail.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.NumeroDocumento.ToString())
                .NotEmpty().WithMessage("Informe o documento - CPF.")
                .Must(x => x.Length > 0 && x.Length <= 11)
                .When(x => x.IdTipoDocumento == (int)ETiposDocumentosPC.CPF)
                .WithMessage("O CPF informado é inválido.");

            RuleFor(x => x.NomePessoa)
                .NotEmpty()
                .When(x => x.IdTipoDocumento == (int)ETiposDocumentosPC.CPF)
                .WithMessage("Informe o nome.");

            RuleFor(x => x.NumeroDocumento.ToString())
                .NotEmpty().WithMessage("Informe o documento - CNPJ.")
                .Must(x => x.Length > 0 && x.Length <= 14)
                .When(x => x.IdTipoDocumento == (int)ETiposDocumentosPC.CNPJ)
                .WithMessage("O CNPJ informado é inválido.");

            RuleFor(x => x)
                .NotEmpty().WithMessage("Informe o genero.")
                .Custom(ValidarGenero);

            RuleFor(x => x.RazaoSocial)
                .NotEmpty()
                .When(x => x.IdTipoDocumento == (int)ETiposDocumentosPC.CNPJ)
                .WithMessage("Informe a razão social.");

            RuleFor(x => x.NomeFantasia)
                .NotEmpty()
                .When(x => x.IdTipoDocumento == (int)ETiposDocumentosPC.CNPJ)
                .WithMessage("Informe o nome fantasia.");
        }

        private void ValidarGenero(SolicitacoesDto solicitacoes, CustomContext context)
        {

            if (solicitacoes.IdTipoDocumento == (int)ETiposDocumentosPC.CPF)
            {
                if (solicitacoes.IdGenero != (int)EGenerosPC.Feminino
                 && solicitacoes.IdGenero != (int)EGenerosPC.Outros
                 && solicitacoes.IdGenero != (int)EGenerosPC.Masculino)
                {
                    context.AddFailure("Erro na configuração do genero - PF.");
                }
            }

            if (solicitacoes.IdTipoDocumento == (int)ETiposDocumentosPC.CNPJ)
            {
                if (solicitacoes.IdGenero != (int)EGenerosPC.ONG
                 && solicitacoes.IdGenero != (int)EGenerosPC.Outros
                 && solicitacoes.IdGenero != (int)EGenerosPC.Privado
                 && solicitacoes.IdGenero != (int)EGenerosPC.Publico
                 )
                {
                    context.AddFailure("Erro na configuração do genero - PJ.");
                }
            }
        }

        public bool ValidarSolicitacao(long IdSolicitacao)
        {
            return ValidarSolicitacaoAsync(IdSolicitacao).Result;
        }
        public async Task<bool> ValidarSolicitacaoAsync(long IdSolicitacao)
        {
            var solicitacao = await _solicitacoesappservice.BuscarTodosComNoLock(p => p.IdSolicitacao == IdSolicitacao);
            return (solicitacao.Count() > 0);
        }
    }
}
