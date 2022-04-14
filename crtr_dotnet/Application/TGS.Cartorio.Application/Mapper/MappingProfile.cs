using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio;
using System.Linq;

namespace TGS.Cartorio.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Solicitacoes, SolicitacoesViewModel>().ReverseMap();
            CreateMap<SolicitacoesDocumentos, SolicitacoesDocumentosViewModel>().ReverseMap();

            CreateMap<Solicitacoes, SolicitacoesSimplificadoDto>().ReverseMap();
            CreateMap<Solicitacoes, SolicitacoesDto>().ReverseMap();
            CreateMap<OutorgantesDto, Outorgante>().ReverseMap();
            CreateMap<Outorgante, ProcuracoesPartes>().ReverseMap();
            CreateMap<Outorgados, ProcuracoesPartes>().ReverseMap();
            CreateMap<Outorgados, OutorgadoDto>().ReverseMap();
            CreateMap<Matrimonios, MatrimoniosDto>().ReverseMap();

            CreateMap<DadosMatrimonio, DadosMatrimonioDto>().ReverseMap();
            CreateMap<DadosContracaoMatrimonio, DadosContracaoMatrimonioDto>().ReverseMap();
            CreateMap<DadosRequerente, DadosRequerenteDto>().ReverseMap();
            CreateMap<DadosPessoasMatrimonio, DadosPessoasMatrimonioDto>().ReverseMap();
            CreateMap<DadosFamiliar, DadosFamiliarDto>().ReverseMap();
            CreateMap<DadosNoivos, DadosNoivosDto>().ReverseMap();
            CreateMap<Testemunha, TestemunhaDto>().ReverseMap();

            CreateMap<StatusSolicitacaoHeader, StatusSolicitacaoHeaderDto>().ReverseMap();

            CreateMap<ProcuracoesPartesEstados, ProcuracoesPartesEstadosDto>().ReverseMap();

            CreateMap<TermoConcordanciaDTO, Configuracoes>().ReverseMap();

            CreateMap<Usuarios, UsuarioRegistro>()
                .ForMember(ur => ur.Nome, u => u.MapFrom(pm => pm.NomeUsuario))
                .ReverseMap();

            CreateMap<Usuarios, UsuarioConta>()
               .ReverseMap();

            CreateMap<Usuarios, UsuarioLogin>()
              .ReverseMap();

            CreateMap<ProdutosDto, Produtos>()
              .ReverseMap();

            CreateMap<ContatoViewModel, Contatos>()
             .ReverseMap();

            CreateMap<ProdutosModalidades, ProdutosModalidadesDto>()
            .ReverseMap();

            CreateMap<ProdutosModalidadesDto, ProdutosModalidades>()
            .ReverseMap();

            CreateMap<ProdutosImagemDto, ProdutosImagens>()
            .ReverseMap();

            CreateMap<ProdutosModalidadesPcDto, ProdutosModalidadesPc>()
            .ReverseMap();

            CreateMap<EnderecosDto, Enderecos>()
            .ReverseMap();

            CreateMap<MinhasSolicitacoesDto, MinhasSolicitacoes>()
            .ReverseMap();

            CreateMap<ParticipantesDto, Participantes>()
            .ReverseMap();

            CreateMap<ProcuracoesPartes, ProcuracoesPartesDto>()
                .ForMember(x => x.Nome, x => x.MapFrom(e => e.UsuarioNavigation.NomeUsuario))
                .ForMember(x => x.Documento, x => x.MapFrom(e => e.PessoasNavigation.Documento))
                .ForMember(x => x.ConteudoPessoasFisicas, x => x.MapFrom(e => e.PessoasNavigation.PessoasFisicas.Conteudo))
                .ForMember(x => x.IdTipoDocumento, x => x.MapFrom(e => e.PessoasNavigation.IdTipoDocumento))
                .ReverseMap();

            CreateMap<PessoasContatos, PessoasContatosDto>().ReverseMap();

            CreateMap<ProcuracoesPartesDto, OutorgadoDto>()
            .ReverseMap();

            CreateMap<ProcuracoesPartesDto, OutorgantesDto>()
            .ReverseMap();

            CreateMap<SolicitacoesOutorgados, SolicitacoesOutorgadosDto>()
            .ReverseMap();

            CreateMap<SolicitacoesOutorgantes, SolicitacoesOutorgantesDto>()
            .ReverseMap();

            CreateMap<SolicitacoesTaxasDto, SolicitacoesTaxas>().ReverseMap();
        }
    }
}
