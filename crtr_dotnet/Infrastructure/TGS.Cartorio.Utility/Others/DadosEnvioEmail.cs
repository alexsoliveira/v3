using System.Collections.Generic;

namespace TGS.Cartorio.Infrastructure.Utility.Others
{
    public class DadosEnvioEmail
    {
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NomeArquivoAnexo { get; set; }
        public byte[] Anexo { get; set; }
    }
}
