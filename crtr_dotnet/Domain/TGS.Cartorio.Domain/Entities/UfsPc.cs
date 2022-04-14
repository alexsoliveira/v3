using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class UfsPc
    {
        public UfsPc()
        {
            
        }

        public int IdUf { get; set; }
        public string Uf { get; set; }
        public string DescricaoUf { get; set; }
    }
}
