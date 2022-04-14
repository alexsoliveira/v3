using Newtonsoft.Json.Linq;


namespace TGS.Cartorio.Application.DTO
{
    public class ProdutosModalidadesDto
    {
        public int IdModalidade { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public dynamic ConteudoObj { get {
                if (Conteudo != null)
                    return JObject.Parse(Conteudo);
                
                return null;
            } 
        }
        public byte[] BlobConteudo { get; set; }
        public string StrBlobConteudo { get; set; }
    }
}
