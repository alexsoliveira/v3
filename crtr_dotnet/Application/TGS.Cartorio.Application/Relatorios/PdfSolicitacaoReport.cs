using AutoMapper;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.DTO.Relatorios;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.Relatorios.Base;
using TGS.Cartorio.Application.Relatorios.Extensions;
using TGS.Cartorio.Application.Relatorios.Interfaces;
using TGS.Cartorio.Application.ViewModel;

namespace TGS.Cartorio.Application.Relatorios
{
    public class PdfSolicitacaoReport : BasePdfReport, IPdfSolicitacaoReport
    {
        private SolicitacaoProntaParaEnvioDto _solicitacao { get; set; }
        private IMapper _mapper { get; set; }
        private ValidadorEnvioEmailSolicitacaoCartorioDto _validador { get; set; }
        public PdfSolicitacaoReport(IMapper mapper)
            : base("PROCURAÇÃO PARA CONTRAIR MATRIMÔNIO")
        {
            _mapper = mapper;
        }

        public void SetDadosSolicitacao(SolicitacaoProntaParaEnvioDto relatorioPDFEnvioParaCartorioDto)
        {
            try
            {
                _solicitacao = relatorioPDFEnvioParaCartorioDto;
                var outorgante = GetOutorgante();
                base.SetData(
                    _solicitacao.solicitacoes.IdSolicitacao,
                    outorgante.Nome, 
                    outorgante.Documento.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ValidadorEnvioEmailSolicitacaoCartorioDto GerarReport(string razaoSocialCartorio, string emailCartorio)
        {
            try
            {
                byte[] docProclamas = _solicitacao.matrimoniosDocumentos.FirstOrDefault(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.Proclamas)?.BlobConteudo;
                
                byte[] docAssinado = null;
                var outorgante = GetOutorgante();
                if (outorgante.IdTipoDocumento == (int)TiposDocumentos.CPF)
                    docAssinado = _solicitacao.matrimoniosDocumentos.FirstOrDefault(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.RG)?.BlobAssinaturaDigital;
                else
                    docAssinado = _solicitacao.matrimoniosDocumentos.FirstOrDefault(x => x.IdTipoDocumento == (int)TiposDocumentosMatrimonio.RNE)?.BlobAssinaturaDigital;

                byte[] docCartorio = base.GerarReport(docProclamas, docAssinado);

                var zipBytes = CreateZipFileOfManyPdf(new Dictionary<string, byte[]> {
                    { $"Solicitacao_{_solicitacao.solicitacoes.IdSolicitacao}", docCartorio },
                    { $"Proclamas_Solicitacao_{_solicitacao.solicitacoes.IdSolicitacao}", docProclamas },
                    { $"DocumentoAssinado_Solicitacao_{_solicitacao.solicitacoes.IdSolicitacao}", docAssinado }
                });

                return new ValidadorEnvioEmailSolicitacaoCartorioDto(
                    _idSolicitacao,
                    zipBytes,
                    outorgante.Nome,
                    outorgante.Email,
                    razaoSocialCartorio,
                    emailCartorio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void CreateBody()
        {
            try
            {
                CreateOutorgante();
                CreateOutorgado();
                CreateMatrimonio();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CreateOutorgante()
        {
            try
            {
                var outorgante = GetOutorgante();
                var table = CreatePdfPTable(3);

                table.AddTitle("dados do outorgante",
                               corFont: BaseColor.WHITE,
                               backgroundColor: CartorioColor.BACKGROUND_COLOR_TITULOS_DIVERSOS,
                               borderColor: BaseColor.BLACK);

                var tipoDocumento = (TiposDocumentos)outorgante.IdTipoDocumento;
                
                var conteudoPessoasFisicas = new ConteudoPessoasFisicasDto();
                if (!string.IsNullOrEmpty(outorgante.ConteudoPessoasFisicas))
                    conteudoPessoasFisicas = JsonConvert.DeserializeObject<ConteudoPessoasFisicasDto>(outorgante.ConteudoPessoasFisicas);

                string rg = tipoDocumento == TiposDocumentos.CPF ? conteudoPessoasFisicas.RG : "";
                table.NewCell($"{tipoDocumento}: {outorgante.Documento}      RG: {rg}", 
                    corFont: BaseColor.BLACK, 
                    borderless: true,
                    colspan: 3);

                table.AddSpace();

                table.NewCell($"Nome Completo: {outorgante.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"E-mail: {outorgante.Email}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);                

                table.AddSpace();

                string dataNascimento = "";
                if (conteudoPessoasFisicas.DataNascimento.HasValue)
                    dataNascimento = conteudoPessoasFisicas.DataNascimento.Value.ToString("dd/MM/yyyy");

                table.NewCell($"Data de Nascimento: {dataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                table.NewCell($"Profissão: {conteudoPessoasFisicas.Profissao}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                table.NewCell($"Estado Civil: {conteudoPessoasFisicas.EstadoCivil}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                
                table.CompleteRow();

                var conteudoContatos = new ContatoViewModel();
                if (!string.IsNullOrEmpty(outorgante.ConteudoPessoasContatos))
                    conteudoContatos = JsonConvert.DeserializeObject<ContatoViewModel>(outorgante.ConteudoPessoasContatos);

                table.NewCell($"Nacionalidade: {conteudoPessoasFisicas.Nacionalidade}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                table.NewCell($"Fone Celular: {conteudoContatos.Celular}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                table.NewCell($"Fone Alternativo: {conteudoContatos.Fixo}",
                    corFont: BaseColor.BLACK,
                    borderless: true);
                //table.NewCell(" ", borderless: true);
                table.CompleteRow();

                table.AddSpace();

                table.NewCell($"Endereço",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                EnderecosDto enderecoDto = null;
                if (!string.IsNullOrEmpty(outorgante.EnderecoEntrega))
                    enderecoDto = JsonConvert.DeserializeObject<EnderecosDto>(outorgante.EnderecoEntrega);

                string strEndereco = "";
                if (enderecoDto != null && enderecoDto.Conteudo != null)
                {
                    strEndereco = $"{enderecoDto.Conteudo.Logradouro}, " +
                           $"{enderecoDto.Conteudo.Numero}";
                    if (!string.IsNullOrEmpty(enderecoDto.Conteudo.Complemento))
                        strEndereco += $", {enderecoDto.Conteudo.Complemento} - ";
                    else
                        strEndereco += " - ";

                    strEndereco += $"{enderecoDto.Conteudo.Cep} - " +
                           $"{enderecoDto.Conteudo.Bairro} - " +
                           $"{enderecoDto.Conteudo.Localidade} - " +
                           $"{enderecoDto.Conteudo.Uf}";
                }

                table.NewCell(strEndereco,
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                AddPdfPTableToDocument(table);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CreateOutorgado()
        {
            try
            {
                var outorgado = GetOutorgado();
                var table = CreatePdfPTable(3);

                table.AddTitle("dados do outorgado",
                               corFont: BaseColor.WHITE,
                               backgroundColor: CartorioColor.BACKGROUND_COLOR_TITULOS_DIVERSOS,
                               borderColor: BaseColor.BLACK);

                var tipoDocumento = (TiposDocumentos)outorgado.IdTipoDocumento;
                
                table.NewCell($"{tipoDocumento}: {outorgado.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSpace();

                table.NewCell($"Nome Completo: {outorgado.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"E-mail: {outorgado.Email}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);                

                table.AddSpace();

                AddPdfPTableToDocument(table);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CreateMatrimonio()
        {
            try
            {
                var matrimonio = GetMatrimonio();
                string InformacoesImportantes = _solicitacao.InformacoesImportantes != null? _solicitacao.InformacoesImportantes : "";
                var table = CreatePdfPTable(3);
                var combo = new ComboDto();
                string documento = "";
                string situacao = "";
                var parte = new ComboDto();                

                table.AddTitle("dados para matrimonio",
                               corFont: BaseColor.WHITE,
                               backgroundColor: CartorioColor.BACKGROUND_COLOR_TITULOS_DIVERSOS,
                               borderColor: BaseColor.BLACK);

                table.AddSubTitle("informações importantes",
                               corFont: CartorioColor.GREEN_CARTORIO);

                table.NewCell($"{InformacoesImportantes}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSubTitle("dados do requerente",
                               corFont: CartorioColor.GREEN_CARTORIO);


                table.NewCell($"Nome: {matrimonio.DadosRequerente.Requerente.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosRequerente.Requerente.IdTipoDocumento);
                
                documento = combo.Texto == "" ? "Documento" : combo.Texto;                 

                table.NewCell($"{documento}: {matrimonio.DadosRequerente.Requerente.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosRequerente.Requerente.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSubTitle("dados da mãe",
                               corFont: CartorioColor.GREEN_CARTORIO);

                table.NewCell($"Nome: {matrimonio.DadosRequerente.MaeRequerente.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosRequerente.MaeRequerente.IdTipoDocumento);

                documento = combo.Texto == "" ? "Documento" : combo.Texto;

                table.NewCell($"{documento}: {matrimonio.DadosRequerente.MaeRequerente.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosRequerente.MaeRequerente.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosRequerente.MaeRequerente.Situacao);

                situacao = combo.Texto == "" ? "" : combo.Texto;

                table.NewCell($"Situação: {situacao}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);                

                table.AddSubTitle("dados do pai",
                               corFont: CartorioColor.GREEN_CARTORIO);

                table.NewCell($"Nome: {matrimonio.DadosRequerente.PaiRequerente.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosRequerente.PaiRequerente.IdTipoDocumento);

                documento = combo.Texto == "" ? "Documento" : combo.Texto;

                table.NewCell($"{documento}: {matrimonio.DadosRequerente.PaiRequerente.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosRequerente.PaiRequerente.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosRequerente.PaiRequerente.Situacao);

                situacao = combo.Texto == "" ? "" : combo.Texto;

                table.NewCell($"Situação: {situacao}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSubTitle("dados da testemunha",
                               corFont: CartorioColor.GREEN_CARTORIO);

                foreach (var testemunha in matrimonio.Testemunhas)
                {
                    parte = JsonConvert.DeserializeObject<ComboDto>(testemunha.Parte);
                    if (parte.Valor == 0)
                    {                       
                        table.NewCell($"Nome: {testemunha.Nome}",
                        corFont: BaseColor.BLACK,
                        borderless: true,
                        colspan: 3);

                        combo = JsonConvert.DeserializeObject<ComboDto>(testemunha.IdTipoDocumento);

                        documento = combo.Texto == "" ? "Documento" : combo.Texto;

                        table.NewCell($"{documento}: {testemunha.Documento}",
                            corFont: BaseColor.BLACK,
                            borderless: true,
                            colspan: 3);

                        table.NewCell($"RG: {testemunha.Rg}",
                            corFont: BaseColor.BLACK,
                            borderless: true,
                            colspan: 3);

                        table.NewCell($"Parte: {parte.Texto}",
                        corFont: BaseColor.BLACK,
                        borderless: true,
                        colspan: 3);
                    }
                }                

                table.AddSubTitle("dados do(a) noivo(a)",
                               corFont: BaseColor.BLUE);

                table.NewCell($"Nome: {matrimonio.DadosNoivos.Noivos.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosNoivos.Noivos.IdTipoDocumento);

                documento = combo.Texto == "" ? "Documento" : combo.Texto;

                table.NewCell($"{documento}: {matrimonio.DadosNoivos.Noivos.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosNoivos.Noivos.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSubTitle("dados da mãe",
                               corFont: BaseColor.BLUE);                

                table.NewCell($"Nome: {matrimonio.DadosNoivos.MaeNoivos.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosNoivos.MaeNoivos.IdTipoDocumento);

                documento = combo.Texto == "" ? "Documento" : combo.Texto;

                table.NewCell($"{documento}: {matrimonio.DadosNoivos.MaeNoivos.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosNoivos.MaeNoivos.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosNoivos.MaeNoivos.Situacao);

                situacao = combo.Texto == "" ? "" : combo.Texto;

                table.NewCell($"Situação: {situacao}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);                

                table.AddSubTitle("dados do pai",
                               corFont: BaseColor.BLUE);

                table.NewCell($"Nome: {matrimonio.DadosNoivos.PaiNoivos.Nome}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosNoivos.PaiNoivos.IdTipoDocumento);

                documento = combo.Texto == "" ? "Documento" : combo.Texto;

                table.NewCell($"{documento}: {matrimonio.DadosNoivos.PaiNoivos.Documento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.NewCell($"Data de nascimento: {matrimonio.DadosNoivos.PaiNoivos.DataNascimento}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                combo = JsonConvert.DeserializeObject<ComboDto>(matrimonio.DadosNoivos.PaiNoivos.Situacao);

                situacao = combo.Texto == "" ? "" : combo.Texto;

                table.NewCell($"Situação: {situacao}",
                    corFont: BaseColor.BLACK,
                    borderless: true,
                    colspan: 3);

                table.AddSubTitle("dados da testemunha",
                               corFont: BaseColor.BLUE);

                foreach (var testemunha in matrimonio.Testemunhas)
                {
                    parte = JsonConvert.DeserializeObject<ComboDto>(testemunha.Parte);
                    if (parte.Valor == 1)
                    {
                        table.NewCell($"Nome: {testemunha.Nome}",
                        corFont: BaseColor.BLACK,
                        borderless: true,
                        colspan: 3);

                        combo = JsonConvert.DeserializeObject<ComboDto>(testemunha.IdTipoDocumento);

                        documento = combo.Texto == "" ? "Documento" : combo.Texto;

                        table.NewCell($"{documento}: {testemunha.Documento}",
                            corFont: BaseColor.BLACK,
                            borderless: true,
                            colspan: 3);

                        table.NewCell($"RG: {testemunha.Rg}",
                            corFont: BaseColor.BLACK,
                            borderless: true,
                            colspan: 3);

                        table.NewCell($"Parte: {parte.Texto}",
                        corFont: BaseColor.BLACK,
                        borderless: true,
                        colspan: 3);
                    }
                }

                AddPdfPTableToDocument(table);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private OutorgantesDto GetOutorgante()
        {
            try
            {
                var outorganteProcuracao = _solicitacao.procuracoesPartes.FirstOrDefault(x => x.IdTipoProcuracaoParte == (int)TipoProcuracaoParte.Outogante);
                return _mapper.Map<OutorgantesDto>(outorganteProcuracao); 
            }
            catch (Exception)
            {
                throw;
            }
        }
        private OutorgadoDto GetOutorgado()
        {
            try
            {
                var outorgadoProcuracao = _solicitacao.procuracoesPartes.FirstOrDefault(x => x.IdTipoProcuracaoParte == (int)TipoProcuracaoParte.Outogado);
                return _mapper.Map<OutorgadoDto>(outorgadoProcuracao);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DadosMatrimonioDto GetMatrimonio()
        {
            try
            {
                var matrimonio = JsonConvert.DeserializeObject<DadosMatrimonioDto>(_solicitacao.matrimonios.CamposJson);
                matrimonio.IdMatrimonio = _solicitacao.matrimonios.IdMatrimonio;
                return matrimonio;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
