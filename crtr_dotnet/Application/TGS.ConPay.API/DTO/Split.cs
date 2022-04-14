using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGS.Pagamento.API.DTO
{
    public class Split
    {
        /// <summary>
        /// 
        /// </summary>
        public string partnerDocumentNumber { get; set; }

        /// <summary>
        /// Percentual do participante no split (em %).
        /// </summary>
        public double value { get; set; }
    }
}
