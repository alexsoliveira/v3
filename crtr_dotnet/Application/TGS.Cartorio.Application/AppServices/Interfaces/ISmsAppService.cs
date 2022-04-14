using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISmsAppService
    {        
        Task Send(long? idPessoa, string mensagem);
        Task<string> GetTemplateCriarConta(string nomeUsuario, string idUsuario);
        Task<string> GetTemplateAlterarSenha(string nomeUsuario, string idUsuario);
        Task<string> GetTemplateCriarSolicitacao(string nomeUsuario, string idSolicitacao);
        Task<string> GetTemplateCriarEndereco(string nomeUsuario, string idPessoa);
        Task<string> GetTemplateAlterarEndereco(string nomeUsuario, string idUsuario);
        Task<string> GetTemplateSalvarDadosPessoais(string nomeUsuario, string idContato);
    }
}
