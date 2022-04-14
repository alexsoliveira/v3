using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class SolicitacoesEstados
    {
        public long IdSolicitacaoEstado { get; set; }
        public long IdSolicitacao { get; set; }
        public int IdEstado { get; set; }
        public DateTime DataOperacao { get; set; }

        public virtual SolicitacoesEstadosPc IdEstadoNavigation { get; set; }
        public virtual Solicitacoes IdSolicitacaoNavigation { get; set; }

        public static SolicitacoesEstados Create(long idSolicitacao, int novoEstado)
        {
            try
            {
                return new SolicitacoesEstados
                {
                    DataOperacao = DateTime.Now,
                    IdEstado = novoEstado,
                    IdSolicitacao = idSolicitacao
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
