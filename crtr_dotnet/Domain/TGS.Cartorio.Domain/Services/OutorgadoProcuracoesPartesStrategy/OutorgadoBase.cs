using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Enumerables;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy
{
    public abstract class OutorgadoBase
    {
        protected IMapper _mapper;
        public OutorgadoBase(IMapper mapper)
        {
            _mapper = mapper;
        }
        public abstract Task Create(Outorgados outorgado);

        protected ProcuracoesPartes ToDomain(Outorgados outorgado, long idPessoa)
        {
            try
            {
                var procuracaoParte = _mapper.Map<ProcuracoesPartes>(outorgado);
                procuracaoParte.IdPessoa = idPessoa;
                procuracaoParte.IdUsuario = outorgado.IdUsuario;
                procuracaoParte.IdProcuracaoParteEstado = (int)EProcuracaoParteEstado.Cadastrado;
                procuracaoParte.IdTipoProcuracaoParte = (int)ETipoProcuracaoParte.Outogado;
                procuracaoParte.IdSolicitacao = outorgado.IdSolicitacao;
                procuracaoParte.DataOperacao = DateTime.Now;
                procuracaoParte.Email = outorgado.Email;
                procuracaoParte.EnderecoEntrega = outorgado.EnderecoEntrega;

                return procuracaoParte;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
