using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Templates;
using TGS.Cartorio.Application.Templates.Interfaces;
using TGS.Cartorio.Infrastructure.Utility.Others;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Cartorio.Application.AppServices
{
    public class EmailAppService : IEmailAppService
    {
        private readonly IEmailWebServer _emailWebServer;
        private readonly IPessoasAppService _pessoaAppService;
        private readonly IUsuariosAppService _usuarioAppService;
        private readonly ITemplateReader _reader;
        private readonly ILogSistemaAppService _logSistemaAppService;

        public EmailAppService(
            IEmailWebServer emailWebServer,
            IPessoasAppService pessoaAppService,
            IUsuariosAppService usuariosAppService,
            ITemplateReader reader
, ILogSistemaAppService logSistemaAppService)
        {
            _emailWebServer = emailWebServer;
            _pessoaAppService = pessoaAppService;
            _usuarioAppService = usuariosAppService;
            _reader = reader;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task<string> Send(long? idPessoa, 
            string mensagem, 
            string nomeUsuario = null, 
            string emailUsuario = null, 
            long? idSolicitacao = null,
            string assunto = null,
            string nomeAnexo = "",
            byte[] anexo = null)
        {
            try
            {
                DadosEnvioEmail dados = null;

                if (idPessoa.HasValue)
                {
                    var pessoa = await _pessoaAppService.BuscarId(idPessoa.Value);
                    var usuario = await _usuarioAppService.BuscarId(pessoa.IdUsuario);
                    dados = new DadosEnvioEmail
                    {
                        Nome = usuario.NomeUsuario,
                        Email = usuario.Email,
                        Assunto = string.IsNullOrEmpty(assunto) ? "Notificação Tabelionet" : $"Notificação Tabelionet - {assunto}",
                        Mensagem = mensagem
                    };
                }
                else if(!string.IsNullOrEmpty(nomeUsuario) && !string.IsNullOrEmpty(emailUsuario))
                {
                    dados = new DadosEnvioEmail
                    {
                        Nome = nomeUsuario,
                        Email = emailUsuario,
                        Assunto = string.IsNullOrEmpty(assunto) ? "Notificação Tabelionet" : $"Notificação Tabelionet - {assunto}",
                        Mensagem = mensagem,
                        NomeArquivoAnexo = nomeAnexo,
                        Anexo = anexo
                    };
                }
                else
                    throw new Exception("Parametros não foram preenchidos corretamente");
                
                
                var ret = await _emailWebServer.EnviarMensagem(dados);

                if (!string.IsNullOrEmpty(ret.ObjRetorno))
                    throw new Exception($"Ocorreu um erro ao enviar e-mail:\n\n  {JsonConvert.SerializeObject(ret.Log)}  \n\n  Objeto enviado ao Server:\n\n{dados}");

                await _logSistemaAppService.Add(CodLogSistema.Send_Email, new
                {
                    LogRetornoServico = ret.Log,
                    Parametros = new
                    {
                        idPessoa,
                        idSolicitacao,
                        mensagem,
                        nomeUsuario,
                        emailUsuario
                    }
                });

                return JsonConvert.SerializeObject(dados);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_Send_Email, new
                {
                    Parametros = new
                    {
                        idPessoa,
                        mensagem,
                        nomeUsuario,
                        emailUsuario
                    }
                }, ex);

                return string.Empty;
            }
        }

        public async Task<string> GetTemplateCriarConta(string nomeUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Email/CriarConta.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTemplateAlterarSenha(string nomeUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario),
                    TemplateReader.CreateKeyValue("[dd/mm/aaaa HH:mm]", DateTime.Now.ToString())
                });
                return await _reader.Read("Templates/Email/AlterarSenha.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return await _reader.Read("Templates/Email/CriarSolicitacao.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTemplateCriarEndereco(string nomeUsuario, EnderecoConteudoDto endereco)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario),
                    TemplateReader.CreateKeyValue("[endereco.Cep]", endereco.Cep),
                    TemplateReader.CreateKeyValue("[endereco.Logradouro]", endereco.Logradouro),
                    TemplateReader.CreateKeyValue("[endereco.Numero]", endereco.Numero),
                    TemplateReader.CreateKeyValue("[endereco.Complemento]", endereco.Complemento),
                    TemplateReader.CreateKeyValue("[endereco.Bairro]", endereco.Bairro),
                    TemplateReader.CreateKeyValue("[endereco.Localidade]", endereco.Localidade),
                    TemplateReader.CreateKeyValue("[endereco.Uf]", endereco.Uf)
                });
                return await _reader.Read("Templates/Email/CriarEndereco.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTemplateAlterarEndereco(string nomeUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Email/AlterarEndereco.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTemplateSalvarDadosPessoais(string nomeUsuario)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuario]", nomeUsuario)
                });
                return await _reader.Read("Templates/Email/SalvarDadosPessoais.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTemplateLayoutParaPDFEnvioCartorio(
            string nomeUsuarioCartorio,
            string idSolicitacao,
            string nomeSolicitante,
            string emailSolicitante)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[NomeUsuarioCartorio]", nomeUsuarioCartorio),
                    TemplateReader.CreateKeyValue("[IdSolicitacao]", idSolicitacao),
                    TemplateReader.CreateKeyValue("[NomeSolicitante]", nomeSolicitante),
                    TemplateReader.CreateKeyValue("[EmailSolicitante]", emailSolicitante)
                });
                return _reader.Read("Templates/Email/LayoutParaPDFEnvioCartorio.html", dic).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTemplateConfirmacaoPagamento(
            string idSolicitacao,
            string nomeSolicitante,
            string numeroParcelas,
            string valorPago,
            string ultimosDigitosCartao)
        {
            try
            {
                var dic = _reader.CreateReplaceDictionary(new KeyValuePair<string, string>[] {
                    TemplateReader.CreateKeyValue("[IdSolicitacao]", idSolicitacao),
                    TemplateReader.CreateKeyValue("[NomeSolicitante]", nomeSolicitante),
                    TemplateReader.CreateKeyValue("[NumeroParcelas]", numeroParcelas),
                    TemplateReader.CreateKeyValue("[ValorPago]", valorPago),
                    TemplateReader.CreateKeyValue("[UltimosDigitosCartao]", ultimosDigitosCartao),
                });
                return await _reader.Read("Templates/Email/ConfirmacaoPagamento.html", dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
