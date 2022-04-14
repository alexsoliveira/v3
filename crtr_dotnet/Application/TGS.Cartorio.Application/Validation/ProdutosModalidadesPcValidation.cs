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
    public class ProdutosModalidadesPcValidation
        : AbstractValidator<ProdutosModalidadesPc>, IProdutosModalidadesPcValidation
    {
        private readonly IProdutosModalidadesPcAppService _produtosModalidadesPcAppService;
        public ProdutosModalidadesPcValidation(IProdutosModalidadesPcAppService produtosModalidadesPcAppService)
        {
            _produtosModalidadesPcAppService = produtosModalidadesPcAppService;
        }

        public bool ValidarProdutoModalidade(int IdProdutoModalidade)
        {
            return ValidarProdutoModalidadeAsync(IdProdutoModalidade).Result;
        }

        private async Task<bool> ValidarProdutoModalidadeAsync(int IdProdutoModalidade)
        {
            var produtomodalidade = await _produtosModalidadesPcAppService.BuscarId(IdProdutoModalidade);

            return (produtomodalidade != null);
        }
    }
}
