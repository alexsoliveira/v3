using AutoMapper;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Factories
{
    
    public class OutorgadoNuloFactory : OutorgadoFactory 
    {
        public OutorgadoNuloFactory()
            : base(null, null)
        { }

        protected override OutorgadoBase _strategy => null;

        public override bool IsElegible(Outorgados outorgado)
        {
            return true;
        }
    }
}
