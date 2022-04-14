using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using System.Linq;
using System.Text.Json;
using TGS.Cartorio.Application.DTO;
using System;

namespace TGS.Cartorio.Application.AppServices
{
    public class EnderecosAppService : IEnderecosAppService
    {
        private readonly IEnderecosService _enderecoService;
        private readonly ApiCorreios _apiCorreios;

        public EnderecosAppService(IEnderecosService enderecoService, ApiCorreios apiCorreios)
        {
            _enderecoService = enderecoService;
            _apiCorreios = apiCorreios;
        }

        public async Task Incluir(Enderecos endereco)
        {
            await _enderecoService.Incluir(endereco);
        }
        public async Task Atualizar(Enderecos endereco)
        {
            await _enderecoService.Atualizar(endereco);
        }
        public async Task Apagar(int IdEndereco)
        {
            await _enderecoService.Apagar(IdEndereco);
        }

        public async Task<Enderecos> Buscar(Enderecos endereco)
        {
            return await _enderecoService.Buscar(endereco);
        }

        public async Task<Retorno<EnderecoDto>> Buscar(string cep)
        {
            try
            {
                return await _apiCorreios.Get<EnderecoDto>($"api/Correios/ConsultarEndereco/{cep}", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Enderecos> BuscarId(int id)
        {
            return await _enderecoService.BuscarId(id);
        }

        public async Task<List<Enderecos>> BuscarTodos(int pagina)
        {
            return await _enderecoService.BuscarTodos(pagina);
        }

        public async Task<List<Enderecos>> BuscarTodosPorUsuario(int IdUsuario)
        {
            return await _enderecoService.BuscarTodosPorUsuario(IdUsuario);
        }

        public async Task<List<Enderecos>> BuscarTodosComNoLock(int pagina)
        {
            return await _enderecoService.BuscarTodosComNoLock(pagina);
        }

       
    }
}
