using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class ProcuracoesPartesService : IProcuracoesPartesService
    {
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IPessoasSqlRepository _pessoasSqlRepository;
        private readonly IUsuariosSqlRepository _usuariosSqlRepository;
        private readonly IMatrimoniosSqlRepository _matrimonioSqlRepository;
        public ProcuracoesPartesService(IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository, 
            IPessoasSqlRepository pessoasSqlRepository, 
            IUsuariosSqlRepository usuariosSqlRepository, 
            IMatrimoniosSqlRepository matrimonioSqlRepository)
        {
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _pessoasSqlRepository = pessoasSqlRepository;
            _usuariosSqlRepository = usuariosSqlRepository;
            _matrimonioSqlRepository = matrimonioSqlRepository;
        }

        public async Task<ProcuracoesPartes> BuscarPorId(long id)
        {
            try
            {
                return await _procuracoesPartesSqlRepository.BuscarPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Outorgante>> AtualizarOutorgantes(SolicitacoesOutorgantes solicitacaoOutorgantes)
        {
            try
            {
                var procuracoesPartes = await _procuracoesPartesSqlRepository.BuscarPorIdSolicitacao(solicitacaoOutorgantes.IdSolicitacao);
                var outorgantes = procuracoesPartes.Where(p => p.IdTipoProcuracaoParte == (int)ETipoProcuracaoParte.Outogante).ToList();
                if (procuracoesPartes == null || !outorgantes.Any())
                    return solicitacaoOutorgantes.Outogantes;

                foreach (var parte in outorgantes)
                {
                    var outorganteExistente = solicitacaoOutorgantes.Outogantes.FirstOrDefault(o => o.Email == parte.Email
                                                                                                 && o.Documento == parte.PessoasNavigation.Documento);
                    if (outorganteExistente != null)
                    {
                        parte.EnderecoEntrega = outorganteExistente.EnderecoEntrega;
                        parte.JsonConteudo = outorganteExistente.JsonConteudo;
                        await _procuracoesPartesSqlRepository.Atualizar(parte);
                        continue;
                    }

                    await _procuracoesPartesSqlRepository.Remover(parte);
                }

                return solicitacaoOutorgantes.Outogantes.Where(o => !procuracoesPartes.Any(p => p.PessoasNavigation.Documento == o.Documento || p.Email == o.Email));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Outorgados>> AtualizarOutorgados(SolicitacoesOutorgados solicitacaoOutorgados)
        {
            try
            {
                var procuracoesPartes = await _procuracoesPartesSqlRepository.BuscarPorIdSolicitacao(solicitacaoOutorgados.IdSolicitacao);
                
                if (procuracoesPartes == null || !procuracoesPartes.Any(p => p.IdTipoProcuracaoParte == (int)ETipoProcuracaoParte.Outogado))
                    return solicitacaoOutorgados.Outorgados;

                var outorgados = procuracoesPartes.Where(p => p.IdTipoProcuracaoParte == (int)ETipoProcuracaoParte.Outogado);

                foreach (var parte in outorgados)
                {
                    var outorgadoExistente = solicitacaoOutorgados.Outorgados.FirstOrDefault(o => o.Email == parte.Email
                                                                                               && o.Documento == parte.PessoasNavigation.Documento);
                    if (outorgadoExistente != null)
                    {
                        parte.EnderecoEntrega = outorgadoExistente.EnderecoEntrega;
                        parte.JsonConteudo = outorgadoExistente.JsonConteudo;
                        await _procuracoesPartesSqlRepository.Atualizar(parte);
                        continue;
                    }

                    await _procuracoesPartesSqlRepository.Remover(parte);
                }

                return solicitacaoOutorgados.Outorgados.Where(o => !procuracoesPartes.Any(p => p.PessoasNavigation.Documento == o.Documento || p.Email == o.Email));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidarSeExisteProcuracaoParteComMatrimonio(long idPessoaSolicitante, long idMatrimonio)
        {
            try
            {
                return await _procuracoesPartesSqlRepository.ValidarSeExisteProcuracaoParteComMatrimonio(idPessoaSolicitante, idMatrimonio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProcuracoesPartes>> BuscarPorIdSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _procuracoesPartesSqlRepository.BuscarPorIdSolicitacao(idSolicitacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ProcuracoesPartes> BuscarPorIdSolicitacaoByJob(long idSolicitacao)
        {
            try
            {
                return _procuracoesPartesSqlRepository.BuscarPorIdSolicitacaoByJob(idSolicitacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProcuracoesPartes> BuscarSolicitantePorMatrimonio(long idMatrimonio, long idPessoaSolicitante)
        {
            try
            {
                var matrimonio = await _matrimonioSqlRepository.BuscarPorId(idMatrimonio);
                if (matrimonio == null)
                    throw new Exception("Não foi possível localizar os dados do Matrimônio!");

                return await _procuracoesPartesSqlRepository.BuscarPorSolicitante(matrimonio.IdSolicitacao, idPessoaSolicitante);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Incluir(ProcuracoesPartes procuracoesParte)
        {
            try
            {
                await _procuracoesPartesSqlRepository.Incluir(procuracoesParte);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Participantes>> ObterParticipantes(long idSolicitacao)
        {
            try
            {
                return await _procuracoesPartesSqlRepository.ObterParticipantes(idSolicitacao);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
