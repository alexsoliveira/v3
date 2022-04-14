using TGS.Cartorio.Application.Enumerables;
namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesSimplificadoDto
    {
        public long IdSolicitacao { get; set; }
        public int IdProduto { get; set; }
        public long IdUsuario { get; set; }
        public long? IdPessoaSolicitante { get; set; }
        public int IdSolicitacaoEstadoInicial { get { return (int)EstadosSolicitacao.Cadastrada; } }
    }
}
