using System.ComponentModel;

namespace TGS.Cartorio.Application.Enumerables
{
    public enum StatusBoleto
    {
        [Description("Boleto criado mas não registrado")]
        A,
        [Description("Boleto registrado")]
        R,
        [Description("Boleto pago")]
        P,
        [Description("Boleto vencido")]
        V,
        [Description("Boleto baixado")]
        B
    }
}

//RETORNO DO SERVIÇO DA CONPAY
//Status Descriação
//A	Boleto criado mas não registrado
//R	Boleto registrado
//P	Boleto pago
//V	Boleto vencido
//B	Boleto baixado
