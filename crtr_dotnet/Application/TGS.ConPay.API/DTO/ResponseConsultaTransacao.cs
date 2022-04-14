using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.ConPay.API.DTO;

namespace TGS.Pagamento.API.DTO
{
    public class ResponseConsultaTransacao
    {
        public string chargeReference { get; set; }
        public string chargeDescription { get; set; }
        public string chargeStatus { get; set; }
        public DateTime chargeCreatedAt { get; set; }
        public DateTime chargePaidAt { get; set; }
        public object chargeCanceledAt { get; set; }
        public double chargeValue { get; set; }
        public string chargeType { get; set; }
        public int chargeInstallments { get; set; }
        public bool chargeBearerInterest { get; set; }
        public ChargeBearerInterestInfo chargeBearerInterestInfo { get; set; }
        public CardInfo cardInfo { get; set; }
        public string chargeMerchantName { get; set; }
        public ChargeClient chargeClient { get; set; }
        public string splitType { get; set; }
        public List<object> chargeSplits { get; set; }
        public string chargeNotificationUrl { get; set; }
    }
}
