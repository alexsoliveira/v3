namespace TGS.ConPay.API.DTO
{
    public class ResponsePayCreditCard
    {
        public bool Success { get; set; }
        public int StatusCodeServico { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
