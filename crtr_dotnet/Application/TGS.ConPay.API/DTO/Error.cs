using System;
using System.Collections.Generic;

namespace TGS.ConPay.API.DTO
{
    internal class ErrorCCResponse
    {
        public string error { get; set; }
        public string message { get; set; }
        public object[] errors { get; set; }
        public DateTime timestamp { get; set; }
        public string status { get; set; }
    }

    public class ErrorBillResponse
    {
        public List<Erro> erro { get; set; }
        public class Erro
        {
            public string titulo { get; set; }
            public string mensagem { get; set; }
            public string status { get; set; }
        }
    }

    


}
