using System.Text;


namespace TGS.Cartorio.Application.DTO
{
    public class ProdutosModalidadesPcDto
    {
        public int IdModalidade { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public byte[] BlobConteudo { get; set; }


        public string StrBlobConteudo
        {
            get
            {
                return Encoding.UTF8.GetString(this.BlobConteudo, 0, this.BlobConteudo.Length);
            }

        }
    }
}
