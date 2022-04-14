using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices
{
    public interface IEmailAppService
    {
        Task<string> Send(long? idPessoa, string mensagem, string nomeUsuario = null, string emailUsuario = null, long? idSolicitacao = null, string assunto = null, string nomeAnexo = "", byte[] anexo = null);       
        Task<string> GetTemplateCriarConta(string nomeUsuario);
        Task<string> GetTemplateAlterarSenha(string nomeUsuario);
        Task<string> GetTemplateCriarSolicitacao(string nomeUsuario, string idSolicitacao);
        Task<string> GetTemplateCriarEndereco(string nomeUsuario, EnderecoConteudoDto endereco);
        Task<string> GetTemplateAlterarEndereco(string nomeUsuario);
        Task<string> GetTemplateSalvarDadosPessoais(string nomeUsuario);
        string GetTemplateLayoutParaPDFEnvioCartorio(string nomeUsuarioCartorio, string idSolicitacao, string nomeSolicitante, string emailSolicitante);
        Task<string> GetTemplateConfirmacaoPagamento(
            string idSolicitacao,
            string nomeSolicitante,
            string numeroParcelas,
            string valorPago,
            string ultimosDigitosCartao);
    }
}