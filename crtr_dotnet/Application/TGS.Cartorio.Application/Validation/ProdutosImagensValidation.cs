using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation
{
    public class ProdutosImagensValidation 
        : AbstractValidator<ProdutosImagens>
    {
        private readonly IUsuariosValidation _usuario;
        private readonly IProdutosValidation _produto;

        public ProdutosImagensValidation(IUsuariosValidation usuario,
            IProdutosValidation produto
            )
        {
            _usuario = usuario;
            _produto = produto;

            RuleFor(p => p.BlobConteudo)
                .NotEmpty()
                .WithMessage("Carregue a imagem do produto.");

            RuleFor(p => p.IdUsuario)
              .Must(_produto.ValidarProduto)
              .WithMessage("Produto informado não está cadastrado.");

            RuleFor(p => p.IdUsuario)
              .Must(_usuario.ValidarUsuario)
              .WithMessage("Usuário informado não está cadastrado.");
        }
    }
}
