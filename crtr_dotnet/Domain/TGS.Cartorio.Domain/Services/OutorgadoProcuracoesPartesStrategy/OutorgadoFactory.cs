using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy
{
    public abstract class OutorgadoFactory
    {
        protected readonly IMapper _mapper;
        protected readonly IProcuracoesPartesSqlRepository _procuracoesPartesRepository;
        protected abstract OutorgadoBase _strategy { get; }
        public OutorgadoFactory Proximo { get; set; }
        public OutorgadoFactory(IMapper mapper,
                                IProcuracoesPartesSqlRepository procuracoesPartesRepository)
        {
            _mapper = mapper;
            _procuracoesPartesRepository = procuracoesPartesRepository;
        }
        
        public async Task Run(Outorgados outorgado)
        {
            try
            {
                if (_strategy == null)
                    return;

                else if (IsElegible(outorgado))
                {
                    await _strategy.Create(outorgado);
                    return;
                }

                await Proximo.Run(outorgado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public abstract bool IsElegible(Outorgados outorgado);
        private Exception CreateException(Outorgados outorgado)
        {
            var exceptionMessage = "Não foi possível criar Outorgado!";

            if (outorgado != null)
                exceptionMessage += $"   {JsonConvert.SerializeObject(outorgado)}";
                
            return new Exception(exceptionMessage);
        }
    }
}
