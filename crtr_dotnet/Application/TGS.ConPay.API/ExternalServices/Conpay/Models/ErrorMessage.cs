using System.Collections.Generic;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class ErrorMessage
    {
        public List<Error> Erro { get; set; }
    }


    public class Error
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public int Status { get; set; }
    }
}
