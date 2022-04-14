using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class ProdutosVitrineDto
    {
        public ProdutosVitrineDto()
        {
            Produtos = new List<Produtos>();
        }

        public int IdProdutoCategoria { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Produtos> Produtos { get; set; }
    }
}
