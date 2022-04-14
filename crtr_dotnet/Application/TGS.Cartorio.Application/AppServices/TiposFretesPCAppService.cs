using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices
{
    public class TiposFretesPCAppService : ITiposFretesPCAppService
    {
        private readonly ITiposFretesPCService _tiposFretesPCService;
        private readonly ApiCorreios _apiCorreios;

        public TiposFretesPCAppService(ITiposFretesPCService tiposFretesPCService, ApiCorreios apiCorreios)
        {
            _tiposFretesPCService = tiposFretesPCService;
            _apiCorreios = apiCorreios;
        }

        public async Task<List<TiposFretesPc>> BuscarTodos(int pagina)
        {
            return await _tiposFretesPCService.BuscarTodos(pagina);
        }

        public async Task<List<TiposFretesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposFretesPCService.BuscarTodosComNoLock(pagina);
        }

        //public async Task<object> BuscarCustos(string cep)
        //{
        //    try
        //    {
        //        dynamic dados = new ExpandoObject();

        //        //API correios 
        //        var endereco = await _apiCorreios.Get<EnderecoDto>($"api/Correios/ConsultarEndereco/{cep}", false);

        //        var fretes = await _tiposFretesPCService.BuscarCustos(Convert.ToInt64(cep));

        //        dados.Endereco = endereco.Sucesso ? endereco.ObjRetorno : null;
        //        dados.TiposFretesPc = fretes;

        //        return dados;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}