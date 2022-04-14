using System;
using System.Collections.Generic;

namespace TGS.ConPay.API.DTO
{
    public class ConsultPaymentResponse
    {
        public string chargeReference { get; set; }
        public string chargeDescription { get; set; }
        public string chargeStatus { get; set; }
        public DateTime chargeCreatedAt { get; set; }
        public DateTime chargePaidAt { get; set; }
        public object chargeCanceledAt { get; set; }
        public string chargeValue { get; set; }
        public string chargeType { get; set; }
        public bool chargeBearerInterest { get; set; }
        public ChargeBearerInterestInfo chargeBearerInterestInfo { get; set; }
        public int chargeInstallments { get; set; }
        public CardInfo cardInfo { get; set; }
        public string chargeMerchantName { get; set; }
        public ChargeClient chargeClient { get; set; }
        public List<ChargeSplit> chargeSplits { get; set; }
        public string chargeNotificationUrl { get; set; }
    }

    public class ChargeBearerInterestInfo
    {
    }

    public class CardInfo
    {
        public string cardHolderName { get; set; }
        public string cardBrand { get; set; }
        public string cardFirstDigits { get; set; }
        public string cardLastDigits { get; set; }
    }    

    public class ShippingAddress
    {
        public string receiverName { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string postalCode { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string federationUnit { get; set; }
    }

    public class ChargeClient
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string documentNumber { get; set; }
        public string dateOfBirth { get; set; }
        public string ddd { get; set; }
        public string phoneNumber { get; set; }
        public Address address { get; set; }
        public ShippingAddress shippingAddress { get; set; }
    }

    public class ChargeSplit
    {
        public string partnerDocumentNumber { get; set; }
        public int value { get; set; }
    }
}
