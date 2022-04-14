namespace TGS.Cartorio.Application.DTO
{
    public class ValidadorEnvioEmailSolicitacaoCartorioDto
    {
        public long IdSolicitacao { get; set; }
        public string NomeSolicitante { get; set; }
        public string EmailSolicitante { get; set; }
        public string NomeUsuarioCartorio { get; set; }
        public string EmailCartorio { get; set; }
        public byte[] ZipPdfCartorio { get; set; }
        public bool Sucesso { get; set; }
        public ValidadorEnvioEmailSolicitacaoCartorioDto(long idSolicitacao, 
            byte[] zipPdfCartorio,
            string nomeSolicitante,
            string emailSolicitante,
            string nomeUsuarioCartorio,
            string emailCartorio
            )
        {
            this.IdSolicitacao = idSolicitacao;
            this.ZipPdfCartorio = zipPdfCartorio;
            this.NomeSolicitante = nomeSolicitante;
            this.EmailSolicitante = emailSolicitante;
            this.NomeUsuarioCartorio = nomeUsuarioCartorio;
            this.EmailCartorio = emailCartorio;
        }
    }
}
