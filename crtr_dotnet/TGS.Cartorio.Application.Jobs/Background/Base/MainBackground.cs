using TGS.Cartorio.Application.AppServices.Interfaces;

namespace TGS.Cartorio.Application.Jobs.Background.Base
{
    public abstract class MainBackground
    {
        private readonly IContaAppService _contaAppService;
        public MainBackground(IContaAppService contaAppService)
        {
            _contaAppService = contaAppService;
        }
    }
}
