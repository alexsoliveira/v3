using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TGS.Cartorio.Infrastructure.Utility.Others
{
    [XmlRoot(ElementName = "DV")]
    public class Dv
    {
        public Dv()
        {
            Liberacao = new List<Liberacao>();
        }

        [XmlElement(ElementName = "liberacao")]
        public List<Liberacao> Liberacao { get; set; }
    }

    [XmlRoot(ElementName = "liberacao")]
    public class Liberacao
    {
        public Liberacao() 
        {
            Objeto = new List<Objeto>();
        }

        public Liberacao(string liberacaoId, string produto, string cnpj, string arquivo)
        {
            SetLiberacao(liberacaoId);
            Produto = Encoding.Default.GetString(Encoding.Default.GetBytes(produto));
            Cnpj = cnpj;
            Arquivo = arquivo;
            Objeto = new List<Objeto>();
        }

        public Liberacao(string liberacaoId)
        {
            SetLiberacao(liberacaoId);
            Objeto = new List<Objeto>();
        }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "produto")]
        public string Produto { get; set; }

        [XmlElement(ElementName = "arquivo")]
        public string Arquivo { get; set; }

        [XmlElement(ElementName = "cnpj")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "objeto")]
        public List<Objeto> Objeto { get; set; }

        private void SetLiberacao(string liberacaoId)
        {
            Id = string.Format("{0}{1:ddMMyyyy}", liberacaoId, DateTime.Today);
        }
    }

    [XmlRoot(ElementName = "objeto")]
    public class Objeto
    {
        public Objeto() { }

        public Objeto(string nome, 
            string email, 
            string assunto, 
            string mensagem,
            string nomeArquivoAnexo = "",
            string anexo = "")
        {
            Nome = nome;
            Email = email;
            Assunto = assunto;
            EmailCorpo = mensagem;
            
            long num = 0;
            while (num < 1)
                num = new Random().Next(1, 99999999) * new Random().Next(1, 99999999);

            Nr_documento = $"{num}";
            NomeArquivoAnexo = nomeArquivoAnexo;
            Base64Binary = anexo;
        }
        [XmlElement(ElementName = "nr_documento")]
        public string Nr_documento { get; set; }

        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "assunto")]
        public string Assunto { get; set; }

        [XmlElement(ElementName = "nomeAnexo")]
        public string NomeArquivoAnexo { get; set; }

        [XmlElement(ElementName = "base64Binary")]
        public string Base64Binary { get; set; }

        [XmlElement(ElementName = "emailCorpo")]
        public string EmailCorpo { get; set; }
    }
}
