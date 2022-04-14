namespace TGS.Cartorio.Application.DTO
{
    public class DocumentoParaAssinarDto
    {
        public string ToSignHashBase64 { get; set; }
        public string TranferDataBase64 { get; set; }
        public string DigestAlgorithmOid { get; set; }
        public long IdMatrimonioDocumento { get; set; }
    }
}
