using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Validation.Interfaces;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Validation
{
    public class ProdutosValidation : AbstractValidator<Produtos> , IProdutosValidation
    {
        private readonly IUsuariosValidation _usuario;
        private readonly IProdutosAppService _produtoAppService;

        public ProdutosValidation(IUsuariosValidation usuario
            , IProdutosAppService produtoAppService
            )
        {
            _usuario = usuario;
            _produtoAppService = produtoAppService;

            RuleFor(x=> x.Titulo)
                .NotEmpty()
                .WithMessage("Informe o título do produto");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Informe a descrição do produto");

            RuleFor(x => x.SubTitulo)
                .NotEmpty()
                .WithMessage("Informe o subtítulo do produto");

            RuleFor(p => p.IdUsuario)
                 .Must(_usuario.ValidarUsuario)
                 .WithMessage("Usuário informado não está cadastrado.");
        }

        public bool ValidarProduto(int IdProduto)
        {
            return ValidarProdutoAsync(IdProduto).Result;
        }

        private async Task<bool> ValidarProdutoAsync(int IdProduto)
        {
            var produto = await _produtoAppService.BuscarId(IdProduto);
            return (produto != null);
        }

        public bool ValidarProduto(long IdProduto)
        {
            return ValidarProdutoAsync(IdProduto).Result;
        }

        private async Task<bool> ValidarProdutoAsync(long IdProduto)
        {
            ProdutosDto produtoDto = null;

            if (int.TryParse(IdProduto.ToString(), out int idProduto))
                produtoDto = await _produtoAppService.BuscarId(idProduto);

            return produtoDto != null;
        }




    }



}
