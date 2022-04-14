using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ILogSistemaAppService
    {
        Task Add(CodLogSistema codLogSistema, object objConteudo, Exception ex = null);
        void AddByJob(CodLogSistema codLogSistema, object objConteudo, Exception ex = null);
    }
}
