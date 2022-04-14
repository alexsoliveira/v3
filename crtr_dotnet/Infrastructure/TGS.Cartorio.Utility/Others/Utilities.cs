using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace TGS.Cartorio.Infrastructure.Utility.Others
{
    public static class Utilities
    {
        public static string GetValidEmail(this string email)
        {
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(email) || !@regex.IsMatch(email)) 
                throw new Exception("O e-mail informado não é válido.");

            return email;
        }

        public static string RemoverCaracteresEspeciais(string texto)
        {
            return Regex.Replace(texto, @"[^\d]", "");
        }

        public static string FormataCPF(string texto)
        {
            return Convert.ToInt64(texto).ToString(@"000\.000\.000\.-00");
        }

        public static string FormataCNPJ(string texto)
        {
            return Convert.ToInt64(texto).ToString(@"00\.000\.000\/0000\-00");
        }


        public static string SerializerXMLObject<T>(T messageContent)
        {
            var xsSubmit = new XmlSerializer(typeof(T));

            using (var sww = new StringWriter())
            {
                //Create our own namespaces for the output
                var ns = new XmlSerializerNamespaces();

                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };

                //Add an empty namespace and empty value
                ns.Add("", "");
                XmlWriter writer = XmlWriter.Create(sww, settings);
                xsSubmit.Serialize(writer, messageContent, ns);

                var xml = sww.ToString();
                return xml;
            }
        }
    }
}
