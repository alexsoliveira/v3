using System.Collections.Generic;

namespace TGS.Pagamento.API.ExternalServices.Conpay.Models
{
    public class SimuladorParcelas
    {
        public SimuladorParcelas() { }
        public SimuladorParcelas(RespostaSimuladorParcela resposta) 
        {
            ValorTotal = resposta.ValorTotal;
            PreencherParcelas(resposta.Parcelas);
        }

        public decimal ValorTotal { get; set; }
        public List<Parcela> Parcelas { get; set; }

        private void PreencherParcelas(RespostaParcelas respostaParcelas)
        {
            Parcelas = new List<Parcela>();

            if (respostaParcelas.Parcela_1 != null)
                Parcelas.Add(SetParcela(1, respostaParcelas.Parcela_1));

            if (respostaParcelas.Parcela_2 != null)
                Parcelas.Add(SetParcela(2, respostaParcelas.Parcela_2));

            if (respostaParcelas.Parcela_3 != null)
                Parcelas.Add(SetParcela(3, respostaParcelas.Parcela_3));

            if (respostaParcelas.Parcela_4 != null)
                Parcelas.Add(SetParcela(4, respostaParcelas.Parcela_4));

            if (respostaParcelas.Parcela_5 != null)
                Parcelas.Add(SetParcela(5, respostaParcelas.Parcela_5));

            if (respostaParcelas.Parcela_6 != null)
                Parcelas.Add(SetParcela(6, respostaParcelas.Parcela_6));

            if (respostaParcelas.Parcela_7 != null)
                Parcelas.Add(SetParcela(7, respostaParcelas.Parcela_7));

            if (respostaParcelas.Parcela_8 != null)
                Parcelas.Add(SetParcela(8, respostaParcelas.Parcela_8));

            if (respostaParcelas.Parcela_9 != null)
                Parcelas.Add(SetParcela(9, respostaParcelas.Parcela_9));

            if (respostaParcelas.Parcela_10 != null)
                Parcelas.Add(SetParcela(10, respostaParcelas.Parcela_10));

            if (respostaParcelas.Parcela_11 != null)
                Parcelas.Add(SetParcela(11, respostaParcelas.Parcela_11));

            if (respostaParcelas.Parcela_12 != null)
                Parcelas.Add(SetParcela(12, respostaParcelas.Parcela_12));

            if (respostaParcelas.Parcela_13 != null)
                Parcelas.Add(SetParcela(13, respostaParcelas.Parcela_13));

            if (respostaParcelas.Parcela_14 != null)
                Parcelas.Add(SetParcela(14, respostaParcelas.Parcela_14));

            if (respostaParcelas.Parcela_15 != null)
                Parcelas.Add(SetParcela(15, respostaParcelas.Parcela_15));

            if (respostaParcelas.Parcela_16 != null)
                Parcelas.Add(SetParcela(16, respostaParcelas.Parcela_16));

            if (respostaParcelas.Parcela_17 != null)
                Parcelas.Add(SetParcela(17, respostaParcelas.Parcela_17));

            if (respostaParcelas.Parcela_18 != null)
                Parcelas.Add(SetParcela(18, respostaParcelas.Parcela_18));

            if (respostaParcelas.Parcela_19 != null)
                Parcelas.Add(SetParcela(19, respostaParcelas.Parcela_19));

            if (respostaParcelas.Parcela_20 != null)
                Parcelas.Add(SetParcela(20, respostaParcelas.Parcela_20));

            if (respostaParcelas.Parcela_21 != null)
                Parcelas.Add(SetParcela(21, respostaParcelas.Parcela_21));

            if (respostaParcelas.Parcela_22 != null)
                Parcelas.Add(SetParcela(22, respostaParcelas.Parcela_22));

            if (respostaParcelas.Parcela_23 != null)
                Parcelas.Add(SetParcela(23, respostaParcelas.Parcela_23));

            if (respostaParcelas.Parcela_24 != null)
                Parcelas.Add(SetParcela(24, respostaParcelas.Parcela_24));

        }

        private Parcela SetParcela(int numero, ParcelaDados parcelaDados)
        {
            return new Parcela
            {
                Numero = numero,
                ValorParcela = parcelaDados.ValorParcela,
                Juros = parcelaDados.Juros
            };
        }
    }
}
