using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;
using System.Linq;
using System.Transactions;
using TGS.Cartorio.Domain.Enumerables;
using System.Linq.Expressions;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using Newtonsoft.Json;

namespace TGS.Cartorio.Domain.Services
{

    public class SolicitacoesService : ISolicitacoesService
    {
        private readonly ISolicitacoesSqlRepository _solicitacoesRepository;
        private readonly IPessoasSqlRepository _pessoasSqlRepository;
        private readonly ISolicitacoesDocumentosSqlRepository _solicitacoesDocumentosSqlRepository;
        private readonly ISolicitacoesEstadosSqlRepository _solicitacoesEstadosSqlRepository;
        private readonly ICartoriosService _cartoriosService;
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IUsuariosSqlRepository _usuariosSqlRepository;
        private readonly ILogSistemaService _logSistemaService;
        private readonly IServiceProvider _serviceProvider;
        Solicitacoes _solicitacao;

        public SolicitacoesService(ISolicitacoesSqlRepository solicitacoesRepository,
                                   IPessoasSqlRepository pessoasSqlRepository,
                                   ISolicitacoesDocumentosSqlRepository solicitacoesDocumentosSqlRepository,
                                   ICartoriosService cartoriosService,
                                   ISolicitacoesEstadosSqlRepository solicitacoesEstadosSqlRepository,
                                   IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                   IUsuariosSqlRepository usuariosSqlRepository, 
                                   ILogSistemaService logSistemaService, 
                                   IServiceProvider serviceProvider)
        {
            _solicitacoesRepository = solicitacoesRepository;
            _pessoasSqlRepository = pessoasSqlRepository;
            _solicitacoesDocumentosSqlRepository = solicitacoesDocumentosSqlRepository;
            _cartoriosService = cartoriosService;
            _solicitacoesEstadosSqlRepository = solicitacoesEstadosSqlRepository;
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _usuariosSqlRepository = usuariosSqlRepository;
            _logSistemaService = logSistemaService;
            _serviceProvider = serviceProvider;
        }

        public async Task Incluir(Solicitacoes solicitacao)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _solicitacoesRepository.Incluir(solicitacao);

                await IncluirSolicitacaoEstado(solicitacao);

                scope.Complete();
            }
        }

        public async Task Atualizar(Solicitacoes solicitacao)
        {
            try
            {
                await _solicitacoesRepository.Atualizar(solicitacao);

                if (solicitacao.IdSolicitacaoEstado > 0)
                    await IncluirSolicitacaoEstado(solicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarSolicitacaoPorJob(Solicitacoes solicitacao)
        {
            try
            {
                _solicitacoesRepository.AtualizarViaJob(solicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FinalizarJob()
        {
            try
            {
                _solicitacoesRepository.FinalizarJob();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitacoes> BuscarId(long id)
        {
            try
            {
                return await _solicitacoesRepository.BuscarId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Solicitacoes>> BuscarTodos(int pagina)
        {
            return await _solicitacoesRepository.BuscarTodos(u => true, pagina);
        }

        public async Task CarregarSolicitacao(long idsolicitacao)
        {
            _solicitacao = await _solicitacoesRepository.BuscarId(idsolicitacao);

            if (_solicitacao == null)
                throw new Exception("Solicitação não encontrada.");
        }

        public async Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes, bool>> func, int pagina)
        {
            try
            {
                return await _solicitacoesRepository.BuscarTodosComNoLock(func, pagina);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EmAtendimento(long idsolicitacao)
        {
            await CarregarSolicitacao(idsolicitacao);

            if (_solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Disponivel_para_Atendimento)
                throw new Exception("A solicitação não está disponível para atendimento.");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _solicitacao.IdSolicitacaoEstado = (int)ESolicitacoesEstadosPC.Em_Atendimento;
                await _solicitacoesRepository.Atualizar(_solicitacao);
                await IncluirSolicitacaoEstado(_solicitacao);
                scope.Complete();
            }
        }

        public async Task AguardandoAprovacaoMinuta(long idsolicitacao)
        {
            await CarregarSolicitacao(idsolicitacao);

            if (_solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Em_Atendimento)
                throw new Exception("A solicitação não está em atendimento.");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _solicitacao.IdSolicitacaoEstado = (int)ESolicitacoesEstadosPC.Aguardando_aprovacao_da_minuta;
                await _solicitacoesRepository.Atualizar(_solicitacao);
                await IncluirSolicitacaoEstado(_solicitacao);
                scope.Complete();
            }
        }

        public async Task<IEnumerable<Solicitacoes>> TodasSolicitacoesAguardandoPagamentoBoleto()
        {
            try
            {
                return await _solicitacoesRepository.TodasSolicitacoesAguardandoPagamentoBoleto();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StatusSolicitacaoHeader> BuscarDadosStatusSolicitacao(long idsolicitacao)
        {
            try
            {
                return await _solicitacoesRepository.BuscarDadosStatusSolicitacao(idsolicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Aprovar(long idsolicitacao)
        {
            await CarregarSolicitacao(idsolicitacao);

            if (_solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Aguardando_aprovacao_da_minuta)
                throw new Exception("A solicitação não está aguardando a aprovação da minuta.");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _solicitacao.IdSolicitacaoEstado = (int)ESolicitacoesEstadosPC.Aprovada;
                await _solicitacoesRepository.Atualizar(_solicitacao);
                await IncluirSolicitacaoEstado(_solicitacao);
                scope.Complete();
            }
        }

        public async Task Reprovar(long idsolicitacao)
        {
            await CarregarSolicitacao(idsolicitacao);

            if (_solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Aguardando_aprovacao_da_minuta)
                throw new Exception("A solicitação não está aguardando a aprovação da minuta.");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _solicitacao.IdSolicitacaoEstado = (int)ESolicitacoesEstadosPC.Reprovada;

                await _solicitacoesRepository.Atualizar(_solicitacao);
                await IncluirSolicitacaoEstado(_solicitacao);
                scope.Complete();
            }
        }


        public async Task IncluirSolicitacaoEstado(Solicitacoes solicitacao)
        {
            var solicitacaoestado = new SolicitacoesEstados
            {
                IdSolicitacao = solicitacao.IdSolicitacao,
                IdEstado = solicitacao.IdSolicitacaoEstado
            };

            await _solicitacoesEstadosSqlRepository.Incluir(solicitacaoestado);
        }

        public async Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long id)
        {
            Usuarios usuario = null;
            try
            {
                usuario = await _usuariosSqlRepository.BuscarId((int)id);
                if (usuario == null || !usuario.IdPessoa.HasValue)
                    throw new Exception("Usuário não localizado ou Usuário não possui Pessoa vinculada!");
            }
            catch (Exception ex)
            {
                var log = LogSistema.Create("Erro_SERVICES_SolicitacoesService_MinhasSolicitacoes_BuscarUsuario",
                    new { IdUsuario = id }, ex);

                await _logSistemaService.Add(log);
                throw;
            }

            List<MinhasSolicitacoes> minhasSolicitacaoes = null;
            try
            {
                minhasSolicitacaoes = await _solicitacoesRepository.MinhasSolicitacoes(usuario.IdPessoa.Value);
            }
            catch (Exception ex)
            {
                var log = LogSistema.Create("Erro_SERVICES_SolicitacoesService_MinhasSolicitacoes_BuscarMinhasSolicitacoes",
                    new { IdUsuario = id, IdPessoa = usuario.IdPessoa.Value }, ex);

                await _logSistemaService.Add(log);
                throw;
            }

            

            List<ProcuracoesPartes> minhasParticipacoes = null;
            try
            {
                if (usuario != null)
                    minhasParticipacoes = await _procuracoesPartesSqlRepository.MinhasParticipacoes(usuario.IdPessoa.Value);
            }
            catch (Exception ex)
            {
                var log = LogSistema.Create("Erro_SERVICES_SolicitacoesService_MinhasSolicitacoes_BuscarMinhasParticipacoes",
                    new { IdUsuario = id, IdPessoa = usuario.IdPessoa.Value }, ex);

                await _logSistemaService.Add(log);
                throw;
            }
            

            if (minhasParticipacoes != null && minhasParticipacoes.Count > 0)
            {
                if (minhasSolicitacaoes == null)
                    minhasSolicitacaoes = new List<MinhasSolicitacoes>();

                minhasParticipacoes.ForEach(async (minhaParticipacao) =>
                {
                    try
                    {
                        if (!minhasSolicitacaoes.Any(ms => ms.IdSolicitacao == minhaParticipacao.IdSolicitacao))
                        {
                            minhasSolicitacaoes.Add(new MinhasSolicitacoes
                            {
                                IdSolicitacao = minhaParticipacao.IdSolicitacao,
                                IdPessoaSolicitante = minhaParticipacao.SolicitacoesNavigation.IdPessoa.Value,
                                Participacao = "Participante",
                                Produto = minhaParticipacao.SolicitacoesNavigation?.IdProdutoNavigation?.Titulo,
                                CamposPagamento = minhaParticipacao.SolicitacoesNavigation?.CamposPagamento,
                                Conteudo = minhaParticipacao.SolicitacoesNavigation?.Conteudo,
                                DataSolicitacao = minhaParticipacao.SolicitacoesNavigation?.DataOperacao,
                                Estado = minhaParticipacao.SolicitacoesNavigation?.IdSolicitacaoEstadoNavigation?.Descricao,
                                UltimaInteracao = minhaParticipacao.SolicitacoesNavigation?.SolicitacoesEstados?
                                                                                           .OrderByDescending(x => x.DataOperacao)?
                                                                                           .First().DataOperacao
                            });
                        }   
                    }
                    catch (Exception ex)
                    {
                        var log = LogSistema.Create("Erro_SERVICES_SolicitacoesService_MinhasSolicitacoes_IncluindoParticipacoesNoModeloDasMinhasSolicitacoes",
                        new { 
                            IdUsuario = id, 
                            IdPessoa = usuario.IdPessoa.Value,
                            MinhaParticipacao = minhaParticipacao,
                            MinhasSolicitacoes = minhasSolicitacaoes
                        }, ex);
                        await _logSistemaService.Add(log);
                    }
                });
            }

            return minhasSolicitacaoes;
        }

        public async Task<MinhaSolicitacao> ConsultarBoleto(long id)
        {
            try
            {
                return await _solicitacoesRepository.ConsultarBoleto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RegistrarNovoBoleto(long id)
        {
            try
            {
                await _solicitacoesRepository.ConsultarBoleto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao)
        {
            try
            {
                var minhasParticipacoes = await _procuracoesPartesSqlRepository.BuscarEstadoDoPedidoPorParticipante(idSolicitacao);

                return minhasParticipacoes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0)
        {
            try
            {
                return await _procuracoesPartesSqlRepository.BuscarTodasProcuracoesPartesEstados(func);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Solicitacoes>> Pesquisar(Expression<Func<Solicitacoes, bool>> func, int pagina = 0)
        {
            return await _solicitacoesRepository.BuscarTodosComNoLock(func, pagina);
        }

        public List<Solicitacoes> BuscarPorSolicitacoesProntasParaEnvioCartorio()
        {
            return _solicitacoesRepository.BuscarPorSolicitacoesProntasParaEnvioCartorio();
        }

        public void SolicitacaoEnviadaAoCartorio(Solicitacoes solicitacao)
        {
            try
            {
                if (solicitacao.IdSolicitacaoEstado != (int)ESolicitacoesEstadosPC.Solicitacao_pronta_para_envio_ao_cartorio)
                    throw new Exception($"A solicitação está no status atual de " +
                        $"{((ESolicitacoesEstadosPC)solicitacao.IdSolicitacaoEstado).ToString()} " +
                        $"e não é possível atualizar para o status de Solicitacao_Enviada_Ao_Cartorio " +
                        $"de acordo com a ordem correta dos estados.");

                solicitacao.IdSolicitacaoEstado = (int)ESolicitacoesEstadosPC.Solicitacao_enviada_ao_cartorio;
                solicitacao.SolicitacoesEstados.Add(new SolicitacoesEstados
                {
                    DataOperacao = DateTime.Now,
                    IdEstado = (int)ESolicitacoesEstadosPC.Solicitacao_enviada_ao_cartorio,
                    IdSolicitacao = solicitacao.IdSolicitacao
                });
                _solicitacoesRepository.AtualizarViaJob(solicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
