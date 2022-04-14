using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class MatrimoniosAppService : IMatrimoniosAppService
    {
        private readonly IMatrimoniosService _matrimoniosService;
        private readonly IMatrimoniosDocumentosService _matrimoniosDocumentosService;
        private readonly IMapper _mapper;
        private readonly ILogSistemaAppService _logSistemaAppService;
        public MatrimoniosAppService(
            IMatrimoniosService matrimoniosService,
            IMapper mapper,
            IMatrimoniosDocumentosService matrimoniosDocumentosService,
            ILogSistemaAppService logSistemaAppService)
        {
            _matrimoniosService = matrimoniosService;
            _mapper = mapper;
            _matrimoniosDocumentosService = matrimoniosDocumentosService;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task<long> Incluir(DadosMatrimonioDto matrimoniosDto)
        {
            try
            {
                if (matrimoniosDto == null)
                    throw new Exception("Objeto MatrimoniosDto está nulo!");

                var matrimonios = _mapper.Map<DadosMatrimonio>(matrimoniosDto);
                return await _matrimoniosService.Incluir(matrimonios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> Atualizar(DadosMatrimonioDto matrimoniosDto)
        {
            try
            {
                if (matrimoniosDto == null)
                    throw new Exception("Objeto MatrimoniosDto está nulo!");

                var matrimonios = _mapper.Map<DadosMatrimonio>(matrimoniosDto);
                return await _matrimoniosService.Atualizar(matrimonios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> BuscarPorSolicitacao(long idSolicitacao)
        {
            Matrimonios matrimonio = null;
            try
            {
                matrimonio = await _matrimoniosService.BuscarPorSolicitacao(idSolicitacao);
                if (matrimonio == null)
                    return null;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_MatrimoniosAppService_BuscarPorSolicitacao,
                    new
                    {
                        IdSolicitacao = idSolicitacao
                    }, ex);

                throw;
            }

            DadosMatrimonioDto dadosMatrimonioDto = null;
            try
            {
                dadosMatrimonioDto = JsonConvert.DeserializeObject<DadosMatrimonioDto>(matrimonio.CamposJson);
                if (dadosMatrimonioDto == null)
                    throw new Exception("Ocorreu um erro ao tratar os dados do matrimônio!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_MatrimoniosAppService_DesserializarDadosMatrimonios,
                    new
                    {
                        IdSolicitacao = idSolicitacao,
                        Matrimonios = matrimonio
                    }, ex);

                throw;
            }


            ICollection<MatrimoniosDocumentos> docs = null;
            try
            {
                docs = await _matrimoniosDocumentosService.BuscarPorSolicitacao(idSolicitacao);

                if (docs != null
                    && docs.Count > 0
                    && docs.Any(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.Proclamas))
                    dadosMatrimonioDto.DadosContracaoMatrimonio.DocumentoProclamas = $"data:application/pdf;base64," +
                                                    $"{Convert.ToBase64String(docs.First(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.Proclamas).BlobConteudo)}";
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_MatrimoniosAppService_BuscarPorSolicitacao_BuscarMatrimoniosDocumentos,
                    new
                    {
                        IdSolicitacao = idSolicitacao,
                        Matrimonios = matrimonio
                    }, ex);

                throw;
            }

            return JsonConvert.SerializeObject(dadosMatrimonioDto);
        }
    }
}
