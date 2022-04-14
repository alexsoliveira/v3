using System;

namespace TGS.Cartorio.Domain.Entities.Procuracoes
{
    public partial class ProcuracoesPartesEstados
    {
        public long IdProcuracaoParteEstado { get; set; }
        public long IdProcuracaoParte { get; set; }
        public int IdProcuracaoParteEstadoPc { get; set; }
        public DateTime DataOperacao { get; set; }

        public virtual ProcuracoesPartesEstadosPc IdProcuracaoParteEstadoPcNavigation { get; set; }
        public virtual ProcuracoesPartes IdProcuracaoParteNavigation { get; set; }



        public static ProcuracoesPartesEstados Create(long idProcuracaoParte, int novoEstado)
        {
            try
            {
                return new ProcuracoesPartesEstados
                {
                    DataOperacao = DateTime.Now,
                    IdProcuracaoParte = idProcuracaoParte,
                    IdProcuracaoParteEstadoPc = novoEstado
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
