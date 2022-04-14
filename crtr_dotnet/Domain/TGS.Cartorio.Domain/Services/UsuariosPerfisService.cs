using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
		
    public class UsuariosPerfisService : IUsuariosPerfisService
    {
		    private readonly ISqlRepository<UsuariosPerfis> _usuariosPerfisRepositorio;
    }
}
