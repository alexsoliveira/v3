using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.DTO
{
    public class eCertificadoDTO
    {
        public long IdMatrimonio { get; set; }
        public long IdPessoaSolicitante { get; set; }
        public long IdUsuario { get; set; }
        public string DescricaoCertificado { get; set; }
        public byte[] DocumentoPDF { get; set; }
        public string Emissor { get; set; }
        public EnumModeloCertificado ModeloCertificado { get; set; }
        public string Senha { get; set; }
        public string Sujeito { get; set; }
        public string ValidadeCertificado { get; set; }                
        public string CertificadoBase64 { get; set; }
        public string ThumbPrintBase64 { get; set; }
    }
}
