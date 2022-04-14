using System;
using System.Text;


namespace TGS.Cartorio.Application.DTO
{
    public class ProdutosImagemDto
    {
        public int IdProdutoImagem { get; set; }
        public int IdProduto { get; set; }
        public byte[] BlobConteudo { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public string StrBlobConteudo
        {
            get
            {
                return Encoding.UTF8.GetString(this.BlobConteudo, 0, this.BlobConteudo.Length);
            }
        }
    }
}
