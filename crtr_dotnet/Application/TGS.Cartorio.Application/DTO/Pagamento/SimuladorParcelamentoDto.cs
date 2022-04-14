using System;
using System.Collections.Generic;
using System.Linq;

namespace TGS.Cartorio.Application.DTO.Pagamento
{
    public class SimuladorParcelamentoDto
    {
        public decimal ValorTotal { get; set; }
        public List<ParcelaDto> Parcelas { get; set; }
        public bool Sucesso { get; set; }
        public string MensagemErro { get; set; }

        public void ProcessarParcelas()
        {
            try
            {
                if (Parcelas != null)
                {
                    foreach (var parcela in Parcelas)
                    {
                        if (parcela.ValorParcela > 0 && parcela.Numero > 0)
                        {
                            Parcelas.First(p => p == parcela).ValorTotal = parcela.ValorParcela;
                            Parcelas.First(p => p == parcela).ValorParcela = decimal.Round(parcela.ValorParcela / parcela.Numero, 2);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ParcelaDto
    {
        public int Numero { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Juros { get; set; }
    }
}
