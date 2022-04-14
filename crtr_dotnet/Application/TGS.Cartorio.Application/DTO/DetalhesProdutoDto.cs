using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class DetalhesProdutoDto
    {
        public int IdProduto { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Campos { get; set; }
        public bool? FlagAtivo { get; set; }
        public ICollection<ProdutosImagemDto> ProdutosImagens { get; set; }
        public ICollection<ProdutosModalidadesDto> ProdutosModalidades { get; set; }
        public string TituloCategoriaProduto { get; set; }
    }
}
