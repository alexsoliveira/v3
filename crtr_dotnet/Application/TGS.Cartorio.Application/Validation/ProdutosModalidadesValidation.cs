using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation
{
    public class ProdutosModalidadesValidation
        : AbstractValidator<ProdutosModalidades>
    {
        private readonly IUsuariosValidation _usuario;
        private readonly IProdutosValidation _produto;
        private readonly IProdutosModalidadesPcValidation _produtomodalidade;

        
        public ProdutosModalidadesValidation(
            IUsuariosValidation usuario,
            IProdutosValidation produto,
            IProdutosModalidadesPcValidation produtomodalidade
            )
        {
            _usuario = usuario;
            _produto = produto;
            _produtomodalidade = produtomodalidade;

            RuleFor(p => p.IdUsuario)
                 .Must(_usuario.ValidarUsuario)
                 .WithMessage("Usuário informado não está cadastrado.");

            RuleFor(p => p.IdProduto)
              .Must(_produto.ValidarProduto)
              .WithMessage("Produto informado não está cadastrado.");

            RuleFor(p => p.IdProdutoModalidade)
              .Must(_produtomodalidade.ValidarProdutoModalidade)
              .WithMessage("Modalidade do produto informada não está cadastrado.");
        }
    }
}
