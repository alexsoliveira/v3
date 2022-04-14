using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.Others;

namespace TGS.Cartorio.Application.AppServices
{
    public class ContaAppService : IContaAppService
    {
        private readonly ApiIdentity _apiIdentity;
        private readonly IUsuariosService _usuarioService;
        private readonly IPessoasAppService _pessoasAppService;
        private readonly IPessoasFisicasAppService _pessoasFisicasAppService;
        private readonly IContatosAppService _contatosAppService;
        private readonly IEnderecosAppService _enderecosAppService;
        private readonly IPessoasEnderecosAppService _pessoasEnderecosAppService;
        private readonly IPessoasContatosAppService _pessoasContatosAppService;
        private readonly IUsuariosContatosAppService _usuariosContatosAppService;
        private readonly IMapper _mapper;
        private readonly ISmsAppService _smsAppService;
        private readonly IEmailAppService _emailAppService;
        private readonly ILogSistemaAppService _logSistemaAppService;


        public ContaAppService(
            ApiIdentity apiIdentity,
            IUsuariosService usuarioService,
            IPessoasAppService pessoasAppService,
            IContatosAppService contatosAppService,
            IUsuariosContatosAppService usuariosContatosAppService,
            IEnderecosAppService enderecosAppService,
            IPessoasEnderecosAppService pessoasEnderecosAppService,
            IPessoasContatosAppService pessoasContatosAppService,
            IPessoasFisicasAppService pessoasFisicasAppService,
            IMapper mapper,
            ISmsAppService smsAppService,
            IEmailAppService emailAppService,
            ILogSistemaAppService logSistemaAppService)
        {
            _apiIdentity = apiIdentity;
            _usuarioService = usuarioService;
            _pessoasAppService = pessoasAppService;
            _contatosAppService = contatosAppService;
            _usuariosContatosAppService = usuariosContatosAppService;
            _mapper = mapper;
            _enderecosAppService = enderecosAppService;
            _pessoasEnderecosAppService = pessoasEnderecosAppService;
            _pessoasContatosAppService = pessoasContatosAppService;
            _pessoasFisicasAppService = pessoasFisicasAppService;
            _smsAppService = smsAppService;
            _emailAppService = emailAppService;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task<Retorno<object>> Cadastrar(UsuarioRegistro obj)
        {
            try
            {
                Retorno<object> responseCadastroUsuario = null;
                try
                {
                    responseCadastroUsuario = await _apiIdentity.PostRetorno<object>("api/identidade/cadastrar", obj);

                    await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_ApiIdentity_CadastroContaUsuario, responseCadastroUsuario.Log);
                }
                catch (Exception ex)
                {
                    obj.ErroLogado = true;
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_ApiIdentity_CadastroContaUsuario, obj, ex);
                    throw;
                }

                if (responseCadastroUsuario != null && responseCadastroUsuario.Sucesso)
                {
                    var usuario = _mapper.Map<Usuarios>(obj);
                    try
                    {
                        await _usuarioService.Incluir(usuario);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_InclusaoUsuario,
                            new { Sucesso = true, Usuario = usuario });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_InclusaoUsuario,
                            new { Sucesso = false, Usuario = usuario }, ex);
                        throw;
                    }


                    Pessoas pessoa = new Pessoas
                    {
                        Documento = Convert.ToInt64(Utilities.RemoverCaracteresEspeciais(obj.Documento)),
                        IdTipoDocumento = obj.IdTipoDocumento,
                    };
                    pessoa.IdUsuario = usuario.IdUsuario;

                    try
                    {
                        await _pessoasAppService.Incluir(pessoa);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_InclusaoPessoa, new { Sucesso = true, Pessoa = pessoa });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_InclusaoPessoa, new { Sucesso = false, Pessoa = pessoa }, ex);
                        throw;
                    }


                    Contatos contato = new Contatos
                    {
                        FlagAtivo = true,
                        DataOperacao = DateTime.Now,
                        IdUsuario = usuario.IdUsuario,
                        Conteudo = JsonConvert.SerializeObject(new ContatosConteudo { Email = usuario.Email })
                    };
                    try
                    {
                        await _contatosAppService.Incluir(contato);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_InclusaoContato, new { Sucesso = true, Contato = contato });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_InclusaoContato, new { Sucesso = false, Contato = contato }, ex);
                        throw;
                    }


                    PessoasContatos pessoasContato = new PessoasContatos
                    {
                        FlagAtivo = true,
                        DataOperacao = DateTime.Now,
                        IdContato = contato.IdContato,
                        IdPessoa = pessoa.IdPessoa,
                        IdUsuario = usuario.IdUsuario
                    };
                    try
                    {
                        await _pessoasContatosAppService.Incluir(pessoasContato);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_InclusaoPessoasContato,
                            new { Sucesso = true, PessoasContato = pessoasContato });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_InclusaoPessoasContato,
                            new { Sucesso = false, PessoasContato = pessoasContato }, ex);
                        throw;
                    }

                    PessoasFisicas pessoaFisica = new PessoasFisicas();
                    if (!string.IsNullOrEmpty(obj.RG))
                        pessoaFisica.Conteudo = $"{{\"rg\": {obj.RG.Replace(".", "").Replace("-", "")}}}";
                    pessoaFisica.IdPessoa = pessoa.IdPessoa;
                    pessoaFisica.IdUsuario = usuario.IdUsuario;
                    pessoaFisica.NomePessoa = obj.Nome;
                    pessoaFisica.NomeSocial = obj.NomeSocial;
                    pessoaFisica.IdGenero = obj.IdGenero;
                    pessoaFisica.DataOperacao = DateTime.Now;
                    try
                    {
                        await _pessoasFisicasAppService.Incluir(pessoaFisica);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_InclusaoPessoaFisica,
                            new { Sucesso = true, PessoaFisica = pessoaFisica });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_InclusaoPessoaFisica,
                            new { Sucesso = false, PessoaFisica = pessoaFisica }, ex);
                        throw;
                    }

                    try
                    {
                        usuario.IdPessoa = pessoa.IdPessoa;
                        await _usuarioService.Atualizar(usuario);
                        await _logSistemaAppService.Add(CodLogSistema.ContaAppService_Cadastrar_AtualizarUsuarioComIdPessoa,
                            new { Sucesso = false, Usuario = usuario, Pessoa = pessoa });
                    }
                    catch (Exception ex)
                    {
                        obj.ErroLogado = true;
                        await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_AtualizarUsuarioComIdPessoa,
                            new { Sucesso = false, Usuario = usuario, Pessoa = pessoa }, ex);
                        throw;
                    }

                    // Enviar SMS
                    var templateSms = await _smsAppService.GetTemplateCriarConta(usuario.NomeUsuario, usuario.IdUsuario.ToString());
                    await _smsAppService.Send(usuario.IdPessoa, templateSms);

                    // Enviar Email
                    var templateEmail = await _emailAppService.GetTemplateCriarConta(usuario.NomeUsuario);
                    await _emailAppService.Send(usuario.IdPessoa, templateEmail, assunto: "Nova Conta");

                }

                return responseCadastroUsuario;
            }
            catch (Exception ex)
            {
                if (!obj.ErroLogado)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_Cadastrar_Geral, obj, ex);
                throw;
            }
        }


        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin obj)
        {
            Retorno<UsuarioRespostaLogin> response = null;
            try
            {
                response = await _apiIdentity.PostRetorno<UsuarioRespostaLogin>("api/identidade/login", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_EfetuarLogin, response.Log);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_AoLogar, obj, ex);
                throw;
            }

            try
            {

                if (response.Sucesso && response.ObjRetorno != null && response.ObjRetorno.ContaAtivada)
                {

                    var usuarioMapper = _mapper.Map<Usuarios>(obj);

                    var usuario = await _usuarioService.Buscar(usuarioMapper);

                    response.ObjRetorno.UsuarioToken.IdUsuario = usuario.IdUsuario;
                    response.ObjRetorno.UsuarioToken.IdPessoa = usuario.IdPessoa;
                    response.ObjRetorno.UsuarioToken.Nome = usuario.NomeUsuario;
                }

                return response.ObjRetorno;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_AoBuscarUsuarioLogin, obj, ex);
                throw;
            }
        }

        public async Task<Retorno<string>> EnviarEmailAtivacao(UsuarioConta obj)
        {
            try
            {
                var res = await _apiIdentity.PostRetorno<string>("api/identidade/EnviarEmailAtivacao", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_EnviarEmailAtivacao, res.Log);
                return res;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_EnviarEmailAtivacao, obj, ex);
                throw;
            }
        }

        public async Task<bool> ConfirmarEmailAtivacao(UsuarioConfirmaConta obj)
        {
            Retorno<UsuarioConta> res = null;
            try
            {
                res = await _apiIdentity.PostRetorno<UsuarioConta>("api/identidade/ConfirmarEmailAtivacao", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_ConfirmarEmailAtivacao, res.Log);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_ConfirmarEmailAtivacao, obj, ex);
                throw;
            }

            try
            {

                if (res.Sucesso)
                {
                    var usuario = _mapper.Map<Usuarios>(res.ObjRetorno);

                    var usuarioBD = await _usuarioService.Buscar(usuario);
                    usuarioBD.FlagAtivo = true;

                    await _usuarioService.Atualizar(usuarioBD);
                    await _logSistemaAppService.Add(CodLogSistema.ContaAppService_ConfirmarEmailAtivacao_AtualizacaoUsuario, usuarioBD);

                    return true;
                }

                return res.Sucesso;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ContaAppService_ConfirmarEmailAtivacao_AtualizacaoUsuario, obj, ex);
                throw;
            }
        }

        public async Task<bool> EnviarEmailResetSenha(UsuarioConta obj)
        {
            try
            {
                var res = await _apiIdentity.PostRetorno<bool>("api/identidade/EnviarEmailResetSenha", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_EnviarEmailResetSenha, res.Log);
                return res.Sucesso;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_EnviarEmailResetSenha, obj, ex);
                throw;
            }
        }

        public async Task<bool> ResetarSenha(UsuarioResetSenha obj)
        {
            try
            {
                var res = await _apiIdentity.PostRetorno<bool>("api/identidade/ResetarSenha", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_ResetarSenha, res.Log);

                //var email = await _apiIdentity.Post<string>("api/identidade/ObterEmail", obj.UserId);

                //var usuario = await _usuarioService.BuscarEmail(email);

                //// Enviar SMS
                //var templateSms = await _smsAppService.GetTemplateAlterarSenha(usuario.NomeUsuario);
                //await _smsAppService.Send(usuario.IdPessoa, templateSms);

                //// Enviar Email
                //var templateEmail = await _emailAppService.GetTemplateAlterarSenha(usuario.NomeUsuario);
                //await _emailAppService.Send(usuario.IdPessoa, templateEmail);

                return res.Sucesso;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_ResetarSenha, obj, ex);
                throw;
            }
        }

        public async Task<bool> AlterarSenha(UsuarioAlterarSenha obj)
        {
            Retorno<bool> res = null;
            try
            {
                res = await _apiIdentity.PostRetorno<bool>("api/identidade/AlterarSenha", obj);
                await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_AlterarSenha, res.Log);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_AlterarSenha, obj, ex);
                throw ex;
            }

            Retorno<string> resObterEmail = null;
            try
            {
                if (res.Sucesso)
                {
                    resObterEmail = await _apiIdentity.PostRetorno<string>("api/identidade/ObterEmail", obj);
                    await _logSistemaAppService.Add(CodLogSistema.ApiIdentity_ContaAppService_ObterEmail, new { Parametros = obj, RetornoServico = res.Log });
                }

            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_ObterEmail, obj, ex);
                throw;
            }

            Usuarios usuario = null;
            if (resObterEmail != null && resObterEmail.Sucesso)
            {
                try
                {
                    usuario = await _usuarioService.BuscarEmail(resObterEmail.ObjRetorno);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_BuscaUsuarioParaEnvioEmailSMS, obj, ex);
                    throw;
                }

                try
                {
                    if (usuario == null)
                        throw new Exception("Usuário não foi localizado!");
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_BuscaUsuarioParaEnvioEmailSMS, obj, ex);
                    throw;
                }
                
                    
                try
                {
                    // Enviar SMS
                    var templateSms = await _smsAppService.GetTemplateAlterarSenha(usuario.NomeUsuario, usuario.IdUsuario.ToString());
                    await _smsAppService.Send(usuario.IdPessoa, templateSms);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_EnvioSMS, obj, ex);
                    throw;
                }

                try
                {
                    // Enviar Email
                    var templateEmail = await _emailAppService.GetTemplateAlterarSenha(usuario.NomeUsuario);
                    await _emailAppService.Send(usuario.IdPessoa, templateEmail, assunto: "Alteração de Senha");
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_EnvioEmail, obj, ex);
                    throw ex;
                }
            }

            return res != null && res.Sucesso;
        }

        public async Task<UsuarioViewModel> BuscarDadosConta(int idUsuario)
        {
            var vm = new UsuarioViewModel();
            Usuarios usuario = null;
            try
            {
                usuario = await _usuarioService.BuscarId(idUsuario);
                if (usuario == null || !usuario.IdPessoa.HasValue)
                    throw new Exception("Usuário não localizado ou usuário sem pessoa vinculada!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarUsuario, idUsuario, ex);
                throw;
            }

            Pessoas pessoa = null;
            List<Contatos> contatos = null;
            try
            {   
                pessoa = await _pessoasAppService.BuscarId(usuario.IdPessoa.Value);
                if (pessoa == null)
                    throw new Exception("Pessoa não foi localizada!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarPessoa, idUsuario, ex);
                throw;
            }

            try
            {
                contatos = await _contatosAppService.BuscarTodosPorUsuario(usuario.IdUsuario);
                if (pessoa == null)
                    throw new Exception("A lista de contatos não foi localizada!");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ApiIdentity_ContaAppService_BuscarDadosConta_BuscarListaContatos, idUsuario, ex);
                throw;
            }

            return new UsuarioViewModel
            {
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Documento = pessoa.Documento,
                Contatos = _mapper.Map<List<ContatoViewModel>>(contatos)
            }; ;
        }

        public async Task<UsuarioContaViewModel> BuscarDadosUsuario(int id)
        {
            //PAREI AQUI
            var usuario = await _usuarioService.BuscarId(id);
            if (usuario == null || usuario.IdPessoa == null)
                return null;

            var pessoa = await _pessoasAppService.BuscarId(usuario.IdPessoa.Value);
            if (pessoa == null)
                return null;

            List<ContatoViewModel> contatos = new List<ContatoViewModel>();
            var contatosPessoas = await _pessoasContatosAppService.BuscarPorPessoa(usuario.IdPessoa.Value);
            if (contatosPessoas != null)
            {
                foreach (var contatoPessoa in contatosPessoas)
                {
                    var contato = JsonConvert.DeserializeObject<ContatoViewModel>(contatoPessoa.IdContatoNavigation.Conteudo);
                    contato.IdContato = contatoPessoa.IdContato;
                    contato.FlagAtivo = contatoPessoa.FlagAtivo;
                    contatos.Add(contato);
                }
            }

            var pessoaFisica = await _pessoasFisicasAppService.BuscarPorIdPessoa(usuario.IdPessoa.Value);
            ConteudoPessoasFisicasDto dadosPessoaFisica = null;
            if (pessoaFisica != null && !string.IsNullOrEmpty(pessoaFisica.Conteudo))
                dadosPessoaFisica = JsonConvert.DeserializeObject<ConteudoPessoasFisicasDto>(pessoaFisica.Conteudo);

            List<EnderecosDto> enderecos = await GetEnderecos(usuario.IdPessoa.Value);

            var usuarioVM = new UsuarioContaViewModel
            {
                Nome = usuario.NomeUsuario,
                Email = usuario.Email,
                IdTipoDocumento = pessoa.IdTipoDocumento,
                Documento = pessoa.Documento,
                Contatos = contatos,
                Enderecos = enderecos
            };

            if (dadosPessoaFisica != null)
            {
                usuarioVM.NomeSocial = pessoaFisica != null ? pessoaFisica.NomeSocial : "";
                usuarioVM.Nascimento = dadosPessoaFisica.DataNascimento;
                usuarioVM.EstadoCivil = dadosPessoaFisica.EstadoCivil;
                usuarioVM.Nacionalidade = dadosPessoaFisica.Nacionalidade;
                usuarioVM.Profissao = dadosPessoaFisica.Profissao;
                usuarioVM.RG = dadosPessoaFisica.RG;
            }

            return usuarioVM;
        }

        public async Task<UsuarioSolicitanteViewModel> BuscarDadosUsuarioSolicitante(int idTipoDocumento, long documento, int id)
        {
            try
            {
                var solicitante = new UsuarioSolicitanteViewModel();

                var usuario = await _usuarioService.BuscarId(id);
                if (usuario == null || usuario.IdPessoa == null)
                    return null;

                solicitante.NomeUsuario = usuario.NomeUsuario;
                solicitante.Email = usuario.Email;
                solicitante.IdPessoaSolicitante = usuario.IdPessoa.Value;

                var pessoa = await _pessoasAppService.BuscarId(usuario.IdPessoa.Value);
                if (pessoa == null)
                    return null;

                if (documento != pessoa.Documento || idTipoDocumento != pessoa.IdTipoDocumento)
                    return null;

                solicitante.Documento = pessoa.Documento;
                solicitante.IdTipoDocumento = idTipoDocumento;

                var pessoaFisica = await _pessoasFisicasAppService.BuscarPorIdPessoa(pessoa.IdPessoa);
                if (pessoaFisica != null && !string.IsNullOrEmpty(pessoaFisica.Conteudo))
                {
                    var conteudo = JsonConvert.DeserializeObject<ConteudoPessoasFisicasDto>(pessoaFisica.Conteudo);
                    solicitante.DataNascimento = conteudo.DataNascimento;
                    solicitante.EstadoCivil = conteudo.EstadoCivil;
                    solicitante.Profissao = conteudo.Profissao;
                    solicitante.Rg = conteudo.RG;
                    solicitante.Nacionalidade = conteudo.Nacionalidade;
                }

                var contatosPessoas = await _pessoasContatosAppService.BuscarPorPessoa(usuario.IdPessoa.Value);
                if (contatosPessoas != null && contatosPessoas.Any())
                {
                    var contato = contatosPessoas.FirstOrDefault(x => x.FlagAtivo.HasValue && x.FlagAtivo.Value);
                    if (contato != null && contato.IdContatoNavigation != null)
                        solicitante.Contato = JsonConvert.DeserializeObject<ContatoViewModel>(contato.IdContatoNavigation.Conteudo);
                }

                solicitante.Enderecos = await GetEnderecos(usuario.IdPessoa.Value);

                return solicitante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> ValidarDocumentoComEmail(string email)
        {
            try
            {
                var usuario = await _usuarioService.BuscarEmail(email);
                return usuario != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ValidarEmailPertenceAoDocumento(long documento, string email)
        {
            try
            {
                var usuario = await _usuarioService.BuscarEmail(email);
                if (usuario == null || !usuario.IdPessoa.HasValue)
                    return false;

                var pessoa = await _pessoasAppService.BuscarId(usuario.IdPessoa.Value);
                if (pessoa == null)
                    return false;

                return pessoa.Documento == documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SalvarTelefone(ContatoViewModel contatoVM)
        {
            try
            {
                var contato = await _contatosAppService.BuscarId(contatoVM.IdContato);
                if (contato != null)
                {
                    var conteudo = JsonConvert.DeserializeObject<ContatoViewModel>(contato.Conteudo);
                    conteudo.Celular = contatoVM.Celular;
                    conteudo.Fixo = contatoVM.Fixo;
                    contato.Conteudo = JsonConvert.SerializeObject(conteudo);
                    await _contatosAppService.Atualizar(contato);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SalvarDadosPessoais(UsuarioDadosPessoaisViewModel usuarioVM)
        {
            try
            {
                if (usuarioVM.Contato != null)
                {
                    var contato = await _contatosAppService.BuscarId(usuarioVM.Contato.IdContato);

                    //Novo contato
                    if (contato == null)
                    {
                        var conteudo = new ContatosConteudo();
                        conteudo.Celular = usuarioVM.Contato.Celular;
                        conteudo.Fixo = usuarioVM.Contato.Fixo;
                        //conteudo.Email = usuarioVM.Email;
                        conteudo.Email = null;
                        contato = new Contatos();
                        contato.Conteudo = JsonConvert.SerializeObject(conteudo);
                        contato.IdUsuario = usuarioVM.IdUsuario;
                        await _contatosAppService.Incluir(contato);

                        if (contato.IdContato > 0)
                        {
                            var pessoasContato = new PessoasContatos();
                            pessoasContato.IdPessoa = usuarioVM.IdPessoa;
                            pessoasContato.IdUsuario = usuarioVM.IdUsuario;
                            pessoasContato.IdContato = contato.IdContato;
                            pessoasContato.DataOperacao = DateTime.Now;
                            pessoasContato.FlagAtivo = true;
                            await _pessoasContatosAppService.Incluir(pessoasContato);
                        }
                    }
                    //Atualizar contato
                    else
                    {
                        var conteudo = JsonConvert.DeserializeObject<ContatosConteudo>(contato.Conteudo);
                        conteudo.Celular = usuarioVM.Contato.Celular;
                        conteudo.Fixo = usuarioVM.Contato.Fixo;
                        contato.Conteudo = JsonConvert.SerializeObject(conteudo);
                        await _contatosAppService.Atualizar(contato);
                    }
                }

                var usuario = await _usuarioService.BuscarId(usuarioVM.IdUsuario);
                var pessoa = await _pessoasFisicasAppService.BuscarPorIdPessoa(usuario.IdPessoa.Value);

                string emailReturn = "";
                if (pessoa != null)
                {
                    ConteudoPessoasFisicasDto conteudo = JsonConvert.DeserializeObject<ConteudoPessoasFisicasDto>(pessoa.Conteudo);
                    if (conteudo == null)
                        conteudo = new ConteudoPessoasFisicasDto();
                    conteudo.DataNascimento = usuarioVM.Nascimento;
                    conteudo.EstadoCivil = usuarioVM.EstadoCivil;
                    conteudo.Nacionalidade = usuarioVM.Nacionalidade;
                    conteudo.Profissao = usuarioVM.Profissao;
                    pessoa.NomeSocial = usuarioVM.NomeSocial;
                    pessoa.Conteudo = JsonConvert.SerializeObject(conteudo);
                    await _pessoasFisicasAppService.Atualizar(pessoa);

                    // Enviar SMS
                    var templateSms = await _smsAppService.GetTemplateSalvarDadosPessoais(usuario.NomeUsuario, usuarioVM.Contato.IdContato.ToString());
                    await _smsAppService.Send(usuario.IdPessoa, templateSms);

                    // Enviar Email
                    var templateEmail = await _emailAppService.GetTemplateSalvarDadosPessoais(usuario.NomeUsuario);
                    emailReturn = await _emailAppService.Send(usuario.IdPessoa, templateEmail, assunto: "Dados Pessoais Alterados");
                }

                return emailReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<EnderecosDto>> GetEnderecos(long idPessoa)
        {
            List<EnderecosDto> enderecos = new List<EnderecosDto>();
            var pessoasEnderecos = await _pessoasEnderecosAppService.BuscarPorPessoa(idPessoa);
            if (pessoasEnderecos != null)
            {
                var enderecoPrincipal = pessoasEnderecos.FirstOrDefault(e => e.IdEnderecoNavigation != null
                                                                          && e.IdEnderecoNavigation.FlagAtivo.HasValue
                                                                          && e.IdEnderecoNavigation.FlagAtivo.Value)?.IdEnderecoNavigation;
                if (enderecoPrincipal != null)
                {
                    var enderecoPrincipalDto = new EnderecosDto();
                    enderecoPrincipalDto.SetDomainData(enderecoPrincipal);
                    enderecos.Add(enderecoPrincipalDto);
                }

                foreach (var pessoaEndereco in pessoasEnderecos.Where(e => e.IdEnderecoNavigation != null
                                                                        && e.IdEnderecoNavigation.FlagAtivo.HasValue
                                                                        && e.IdEnderecoNavigation.FlagAtivo.Value == false))
                {
                    var endereco = new EnderecosDto();
                    endereco.SetDomainData(pessoaEndereco.IdEnderecoNavigation);
                    enderecos.Add(endereco);
                }
            }

            return enderecos;
        }
    }
}