using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISolicitacoesEstadosAppService
    {
        Task<List<SolicitacoesEstados>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(int pagina = 0);

        Task Incluir(SolicitacoesEstados solicitacoesestados);
        Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao);
    }
}
