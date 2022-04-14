using TGS.Cartorio.Domain.Entities;
using System.Collections.Generic;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IAssinaturaDigitalLogService
    {
        IEnumerable<AssinaturaDigitalLog> ConsultarTodos();        
    }
}
