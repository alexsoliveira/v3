using ExtensaoCertificado.Enumerables;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ExtensaoCertificado.Models
{
    public class eCertificado
    {
        public byte[] DocumentoPDF { get; set; }
        public string DescricaoCertificado { get; set; }
        public string NumeroSerie { get; set; }
        public string ValidadeCertificado { get; set; }
        public string Sujeito { get; set; }
        public string Emissor { get; set; }
        public string Valor { get; set; }        
        public EnumModeloCertificado ModeloCertificado { get; set; }        
        public X509Certificate2 CertificadoSelecionado { get; set; }
        public byte[] DocumentoCertificado { get; set; }
    }
}