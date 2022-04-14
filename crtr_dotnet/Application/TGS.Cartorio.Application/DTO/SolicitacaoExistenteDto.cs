using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacaoExistenteDto
    {
        public long IdSolicitacao { get; set; }
        public long IdPessoaSolicitante { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public List<OutorgadoDto> Outorgados { get; set; }
        public List<OutorgantesDto> Outorgantes { get; set; }
        public string InformacoesImportantes { get; set; }
        public string RepresentacaoPartes { get; set; }
        public DadosMatrimonioDto JsonProduto { get; set; }

        public static SolicitacaoExistenteDto Create(long idSolicitacao, int idProduto)
        {
            var dto = new SolicitacaoExistenteDto
            {
                IdSolicitacao = idSolicitacao,
                IdProduto = idProduto
            };

            dto.Outorgados = new List<OutorgadoDto>();
            dto.Outorgantes = new List<OutorgantesDto>();

            return dto;
        }

        public void CriarPartes(IMapper mapper, IEnumerable<ProcuracoesPartes> procuracoesPartes)
        {
            if (procuracoesPartes != null)
                foreach (var procuracaoParte in procuracoesPartes)
                {
                    var parte = mapper.Map<ProcuracoesPartesDto>(procuracaoParte);
                    parte.ConteudoPessoasContatos = GetConteudoPessoasContatos(procuracaoParte.PessoasNavigation);

                    if (procuracaoParte.IdTipoProcuracaoParte == (int)TipoProcuracaoParte.Outogado)
                        Outorgados.Add(mapper.Map<OutorgadoDto>(parte));
                    else
                        Outorgantes.Add(mapper.Map<OutorgantesDto>(parte));   
                }
        }

        private string GetConteudoPessoasContatos(Pessoas pessoas)
        {
            try
            {
                string conteudo = null;

                if (pessoas != null 
                 && pessoas.PessoasContatos != null
                 && pessoas.PessoasContatos.Count > 0)
                    conteudo = pessoas.PessoasContatos.First().IdContatoNavigation?.Conteudo;

                return conteudo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
