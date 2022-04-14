using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGS.Cartorio.Application.DTO.Pagamento
{
    public class CartaoCreditoDto
    {
        /// <summary>
        /// Quantidade de parcelas da transação.
        /// </summary>
        [JsonProperty("installments")]
        [Required(ErrorMessage = "Informe o número de parcelas!")]
        public int NumeroParcelas { get; set; }

        /// <summary>
        /// Valor da parcela selecionada
        /// </summary>
        [JsonProperty("value")]
        [Required(ErrorMessage = "Não foi possível obter o valor total desta solicitação.")]
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Dados do cartão do comprador.
        /// </summary>
        [JsonProperty("card")]
        [Required(ErrorMessage = "Informe corretamente os dados do seu cartão de crédito!")]
        public CartaoDto Cartao { get; set; }

        /// <summary>
        /// Dados do comprador.
        /// </summary>
        [JsonProperty("client")]
        public DadosClienteCartaoCreditoDto Cliente { get; set; }
    }


    public class CartaoDto
    {
        public void FormatarCartao()
        {
            if (!string.IsNullOrEmpty(NumeroCartao))
                NumeroCartao = NumeroCartao.Replace(" ", "");
        }

        public void RemoverDadosPorSeguranca()
        {
            string novoDado = "Dados Removidos Por Segurança";
            
            FormatarCartaoParaUltimosQuatroDigitos();
            
            MesExpiracao = novoDado;
            AnoExpiracao = novoDado;
            CodigoSeguranca = novoDado;
            DonoCartao = null;
        }

        private void FormatarCartaoParaUltimosQuatroDigitos()
        {
            try
            {
                if (NumeroCartao.Length == 16)
                    NumeroCartao = $"XXXX XXXX XXXX {NumeroCartao.Substring(12, 4)}";
                else
                    NumeroCartao = "Dados Removidos Por Segurança";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Número do cartão.
        /// </summary>
        [JsonProperty("number")]
        [Required(ErrorMessage = "Informe o número do seu cartão de crédito corretamente!")]
        public string NumeroCartao { get; set; }

        /// <summary>
        /// Mês de expiração do cartão.
        /// </summary>
        [JsonProperty("exp_month")]
        [Required(ErrorMessage = "Informe o mês de expiração do seu cartão de crédito!")]
        public string MesExpiracao { get; set; }

        /// <summary>
        /// Ano de expiração do cartão.
        /// </summary>
        [JsonProperty("exp_year")]
        [Required(ErrorMessage = "Informe o ano de expiração do seu cartão de crédito!")]
        public string AnoExpiracao { get; set; }

        /// <summary>
        /// Código de segurança do cartão.
        /// </summary>
        [JsonProperty("security_code")]
        [Required(ErrorMessage = "Informe o código de segurança do seu cartão de crédito!")]
        public string CodigoSeguranca { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("holder")]
        [Required(ErrorMessage = "Informe o seu nome como está no cartão de crédito!")]
        public DonoCartaoDto DonoCartao { get; set; }
    }

    public class DonoCartaoDto
    {
        [JsonProperty("name")]
        [Required(ErrorMessage = "Informe o nome como está no seu cartão de crédito!")]
        public string Nome { get; set; }
    }

    public class DadosClienteCartaoCreditoDto
    {
        public void RemoverDadosPorSeguranca()
        {
            string novoDado = "Dados Removidos Por Segurança";
            NomeCompleto = novoDado;
            Documento = novoDado;
        }

        /// <summary>
        /// Nome completo do comprador.
        /// </summary>
        [JsonProperty("fullName")]
        public string NomeCompleto { get; set; }

        /// <summary>
        /// E-mail do comprador.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// CPF ou CNPJ do comprador.
        /// </summary>
        [JsonProperty("documentNumber")]
        public string Documento { get; set; }

        /// <summary>
        /// DDD telefone comprador.
        /// </summary>
        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        /// <summary>
        /// Número do telefone do comprador.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string Telefone { get; set; }

        /// <summary>
        /// Endereço do comprador
        /// </summary>
        [JsonProperty("address")]
        public EnderecoCartaoCreditoDto Endereco { get; set; }
    }

    public class EnderecoCartaoCreditoDto
    {
        /// <summary>
        /// Nome da rua do comprador.
        /// </summary>
        [JsonProperty("street")]
        public string Rua { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        [JsonProperty("number")]
        public string Numero { get; set; }

        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        [JsonProperty("complement")]
        public string Complemento { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        [JsonProperty("postalCode")]
        public string CEP { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [JsonProperty("district")]
        public string Bairro { get; set; }

        /// <summary>
        /// Cidade.
        /// </summary>
        [JsonProperty("city")]
        public string Cidade { get; set; }

        /// <summary>
        /// UF.
        /// </summary>
        [JsonProperty("federationUnit")]
        public string UF { get; set; }
    }

    public class RespostaPagamentoCartaoCreditoDto
    {
        [JsonProperty("reference")]
        public string Referencia { get; set; }
        [JsonProperty("value")]
        public float Value { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        public bool Sucesso { get; set; }
        [JsonProperty("success")]
        public bool StatusCodeSucesso { get; set; }
        [JsonProperty("statusCodeServico")]
        public int StatusCodeServicoPagamento { get; set; }
        [JsonProperty("message")]
        public string MensagemErro { get; set; }
        [JsonProperty("bearerInterest")]
        public bool BearerInterest { get; set; }
    }

    public class RespostaConsultaBoleto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public bool Sucesso { get; set; }
        [JsonProperty("message")]
        public string MensagemErro { get; set; }
    }

    #region RespostaConsultaTransacao


    public class RespostaConsultaTransacaoDto
    {
        public bool Sucesso { get; set; }
        [JsonProperty("message")]
        public string MensagemErro { get; set; }
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

    public class Address
    {
        /// <summary>
        /// Nome da rua do comprador.
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        public string complement { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public string postalCode { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// Cidade.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// UF.
        /// </summary>
        public string federationUnit { get; set; }
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

    #endregion
}
