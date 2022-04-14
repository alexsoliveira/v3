//using AutoMapper;
//using Newtonsoft.Json;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using TGS.Cartorio.Application.AppServices.Interfaces;
//using TGS.Cartorio.Application.DTO.Products.Matrimonio;
//using TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio;
//using TGS.Cartorio.Domain.Interfaces.Services;

//namespace TGS.Cartorio.Application.AppServices
//{
//    public class MatrimoniosDocumentosAppService : IMatrimoniosDocumentosAppService
//    {
//        private readonly IMatrimoniosDocumentosService _matrimoniosDocumentosService;
//        private readonly IMapper _mapper;

//        public MatrimoniosDocumentosAppService(IMatrimoniosDocumentosService matrimoniosDocumentosService,
//                                               IMapper mapper)
//        {
//            _matrimoniosDocumentosService = matrimoniosDocumentosService;
//            _mapper = mapper;
//        }

//        public async Task Incluir(DadosMatrimonioDto matrimoniosDto)
//        {
//            try
//            {
//                if (matrimoniosDto == null)
//                    throw new Exception("Objeto MatrimoniosDto está nulo!");

//                var matrimonios = _mapper.Map<DadosMatrimonio>(matrimoniosDto);
//                await _matrimoniosService.Incluir(matrimonios);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task Atualizar(DadosMatrimonioDto matrimoniosDto)
//        {
//            try
//            {
//                if (matrimoniosDto == null)
//                    throw new Exception("Objeto MatrimoniosDto está nulo!");

//                var matrimonios = _mapper.Map<DadosMatrimonio>(matrimoniosDto);
//                await _matrimoniosService.Atualizar(matrimonios);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<string> BuscarPorSolicitacao(long idSolicitacao)
//        {
//            try
//            {
//                var matrimonio = await _matrimoniosService.BuscarPorSolicitacao(idSolicitacao);
//                if (matrimonio == null)
//                    return null;

//                var dadosMatrimonioDto = JsonConvert.DeserializeObject<DadosMatrimonioDto>(matrimonio.CamposJson);

//                var docs = await _matrimoniosDocumentosService.BuscarPorSolicitacao(idSolicitacao);
//                if (docs != null
//                    && docs.Count > 0)
//                    dadosMatrimonioDto.DadosContracaoMatrimonio.DocumentoProclamas = $"data:application/pdf;base64," +
//                                                                          $"{Convert.ToBase64String(docs.FirstOrDefault().BlobConteudo)}";


//                return JsonConvert.SerializeObject(dadosMatrimonioDto);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
