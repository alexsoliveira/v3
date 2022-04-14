using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	
    public class AssinaturaDigitalLogService : IAssinaturaDigitalLogService
    {
		private readonly ISqlRepository<AssinaturaDigitalLog> _assinaturaDigitalLogRepository;

        public IEnumerable<AssinaturaDigitalLog> ConsultarTodos()
        {
            throw new System.NotImplementedException();
        }
    }
}
