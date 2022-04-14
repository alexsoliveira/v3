using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.DTO
{
    public class ProdutosDto
    {
        public int IdProduto { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public string Campos { get; set; }
        public bool? FlagAtivo { get; set; }
        public string SubTitulo { get; set; }
        public Usuarios IdUsuarioNavigation { get; set; }
        public ICollection<ProdutosDocumentos> ProdutosDocumentos { get; set; }
        public ICollection<ProdutosImagens> ProdutosImagens { get; set; }
        public ICollection<ProdutosModalidadesDto> ProdutosModalidades { get; set; }
        public ICollection<Solicitacoes> Solicitacoes { get; set; }
    }
}
