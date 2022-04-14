using System.Collections.Generic;

namespace TGS.ConPay.API.DTO
{
    public class Billet
    {
        public Billet()
        {
            pagador = new Pagador();                 
        }
        public string dataVencimento { get; set; }
        public double valor { get; set; }
        public string mensagem { get; set; }
        public Pagador pagador { get; set; }     
    }
}
