using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface ISolicitacoesEstadosService
    {
        Task<List<SolicitacoesEstados>> BuscarTodos(int pagina);
        Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(int pagina);
        Task Incluir(SolicitacoesEstados solicitacoesestados);
        Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao);
        Task<SolicitacoesEstados> BuscarId(long idSolicitacoesEstados);
    }
}
