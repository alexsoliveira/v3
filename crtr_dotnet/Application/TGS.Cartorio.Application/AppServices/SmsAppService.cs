using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Templates;
using TGS.Cartorio.Application.Templates.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.Others;

namespace TGS.Cartorio.Application.AppServices
{
    public class SmsAppService : ISmsAppService
    {
        private readonly ApiSMS _apiComunicacaoSms;
        private readonly IPessoasContatosService _pessoasContatosService;
        private readonly IContatosService _contatosService;
        private readonly ITemplateReader _reader;
        private readonly ILogSistemaAppService _logSistemaAppService;


        public SmsAppService(
            IPessoasContatosService pessoasContatosService,
            IContatosService contatosService,
            ApiSMS apiComunicacaoSms,
            ITemplateReader reader, 
            ILogSistemaAppService logSistemaAppService)
        {
            _pessoasContatosService = pessoasContatosService;
            _contatosService = contatosService;
            _apiComunicacaoSms = apiComunicacaoSms;
            _reader = reader;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task Send(long? idPessoa, string mensagem)
        {
            try
            {
                if (idPessoa.HasValue)
                {
                    var pessoasContatos = await _pessoasContatosService.BuscarPorPessoa(idPessoa.Value);
                    if (pessoasContatos != null && pessoasContatos.Any())
                    {
                        var contato = pessoasContatos.FirstOrDefault(x => x.FlagAtivo.HasValue && x.FlagAtivo.Value);
                        if (contato != null && contato.IdContatoNavigation != null)
                        {
                            var campos = JsonConvert.DeserializeObject<ContatosConteudo>(contato.IdContatoNavigation.Conteudo);
                            if (String.IsNullOrEmpty(campos.Celular))
                                return;
                            
                            var itens = new List<SmsItem>();
                            var item = new SmsItem
                            {
                                Celular = campos.Celular,
                                Mensagem = mensagem
                            };
                            itens.Add(item);

                            Retorno<string> ret = await _apiComunicacaoSms.EnviarMensagem(itens, true);
                            await _logSistemaAppService.Add(CodLogSistema.Send_SMS, new { Sucesso = true, Log = ret.Log });
#if Debug
                            if (!ret.Sucesso)
                                throw new Exception($"Ocorreu um erro ao tentar enviar SMS:\n\n{JsonConvert.SerializeObject(ret)}\n\nObjetoRequest:\n\n{JsonConvert.SerializeObject(itens)}");
#else
                            if (!ret.Sucesso)
                                throw new Exception($"Ocorreu um erro ao tentar enviar SMS!");
#endif
                        }                       
                    }                   
                }
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS,
                        new { Sucesso = false, IdPessoa = idPessoa, Mensagem = mensagem }, ex);
                throw ex;
            }
        }

        public async Task<string> GetTemplateCriarConta(string nomeUsuario, string idUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Sms/CriarConta.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateCriarConta,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdUsuario = idUsuario }, ex);
                throw;
            }            
        }

        public async Task<string> GetTemplateAlterarSenha(string nomeUsuario, string idUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Sms/AlterarSenha.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateAlterarSenha,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdUsuario = idUsuario }, ex);
                throw;
            }            
        }

        public async Task<string> GetTemplateCriarSolicitacao(string nomeUsuario, string idSolicitacao)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario),
                    TemplateReader.CreateKeyValue("[solicitacao.IdSolicitacao]", idSolicitacao),
                });
                return await _reader.Read("Templates/Sms/CriarSolicitacao.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateCriarSolicitacao,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdSolicitacao = idSolicitacao }, ex);
                throw;
            }            
        }

        public async Task<string> GetTemplateCriarEndereco(string nomeUsuario, string idPessoa)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario),                    
                });
                return await _reader.Read("Templates/Sms/CriarEndereco.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateCriarEndereco,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdPessoa = idPessoa }, ex);
                throw;
            }            
        }

        public async Task<string> GetTemplateAlterarEndereco(string nomeUsuario, string idUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Sms/AlterarEndereco.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateAlterarEndereco,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdUsuario = idUsuario }, ex);
                throw;
            }            
        }

        public async Task<string> GetTemplateSalvarDadosPessoais(string nomeUsuario, string idContato)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Sms/SalvarDadosPessoais.html", dic);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_SMS_AoGerarTemplateSalvarDadosPessoais,
                                new { Sucesso = false, NomeUsuario = nomeUsuario, IdContato = idContato }, ex);
                throw;
            }
        }
    }
}
