using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Application.ViewModel.Identity;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IContaAppService
    {
        Task<Retorno<object>> Cadastrar(UsuarioRegistro obj);

        Task<UsuarioRespostaLogin> Login(UsuarioLogin obj);

        Task<Retorno<string>> EnviarEmailAtivacao(UsuarioConta obj);

        Task<bool> ConfirmarEmailAtivacao(UsuarioConfirmaConta obj);

        Task<bool> EnviarEmailResetSenha(UsuarioConta obj);

        Task<bool> ResetarSenha(UsuarioResetSenha obj);

        Task<bool> AlterarSenha(UsuarioAlterarSenha obj);

        Task<UsuarioViewModel> BuscarDadosConta(int usuarioId);

        Task<UsuarioContaViewModel> BuscarDadosUsuario(int id);
        Task<UsuarioSolicitanteViewModel> BuscarDadosUsuarioSolicitante(int idTipoDocumento, long documento, int id);

        Task SalvarTelefone(ContatoViewModel obj);
        Task<string> SalvarDadosPessoais(UsuarioDadosPessoaisViewModel usuarioVM);
        Task<bool> ValidarDocumentoComEmail(string email);
        Task<bool> ValidarEmailPertenceAoDocumento(long documento, string email);

    }
}
