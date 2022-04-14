using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	
    public class PerfisService : IPerfisService
    {
		private readonly ISqlRepository<Perfis> _perfisRepositorio;
    }
}
