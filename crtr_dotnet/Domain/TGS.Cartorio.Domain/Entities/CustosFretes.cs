using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public class CustosFretes
    {
        [NotMapped]
        public int IdTipoFrete { get; set; }
        [NotMapped]
        public long Cep { get; set; }
        [NotMapped]
        public Decimal Valor { get; set; }
        [NotMapped]
        public string Prazo { get; set; }
    }
}
