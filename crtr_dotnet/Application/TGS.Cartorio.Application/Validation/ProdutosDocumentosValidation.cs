using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation
{
    public class ProdutosDocumentosValidation
       : AbstractValidator<ProdutosDocumentos>
    {
        private readonly IUsuariosValidation _usuario;
        private readonly IProdutosValidation _produto;
        private readonly ITiposDocumentosPcValidation _tipodocumento;

        public ProdutosDocumentosValidation(
            IUsuariosValidation usuario,
            IProdutosValidation produto,
            ITiposDocumentosPcValidation tipodocumento
            )
        {
            _usuario = usuario;
            _produto = produto;
            _tipodocumento = tipodocumento;

            RuleFor(p => p.IdUsuario)
                 .Must(_usuario.ValidarUsuario)
                 .WithMessage("Usuário informado não está cadastrado.");

            RuleFor(p => p.IdProduto)
              .Must(_produto.ValidarProduto)
              .WithMessage("Produto informado não está cadastrado.");

            RuleFor(p => p.IdTipoDocumento)
              .Must(_tipodocumento.ValidarTipoDocumento)
              .WithMessage("Documento informado não está cadastrado.");
        }
    }
}
