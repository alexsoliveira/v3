using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class ProdutosModalidadesPc
    {
        public ProdutosModalidadesPc()
        {
            ProdutosModalidades = new HashSet<ProdutosModalidades>();
        }


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

        public virtual ICollection<ProdutosModalidades> ProdutosModalidades { get; set; }
    }
}
