using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace TGS.Cartorio.Application.Jobs.Filter
{
    public class AcessoLiberadoFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            //acesso liberado para qualquer requisição
            return true;
        }
    }
}
