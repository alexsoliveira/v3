using ExtensaoCertificado.Enumerables;
using ExtensaoCertificado.Models;
using ExtensaoCertificado.Util;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Configuration;

namespace TGS.Extensao.Certificado
{
    public class Certificados
    {
        public List<eCertificado> BuscarCertificadosUsuario()
        {
            List<eCertificado> certificados = new List<eCertificado>();

            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;

            if (collection.Count > 0)
            {
                for (var i = 0; i < collection.Count; i++)
                {

                    eCertificado certificado = new eCertificado();
                    certificado.CertificadoSelecionado = collection[i];
                    var emissor = TratarString(certificado.CertificadoSelecionado.IssuerName.Name);
                    var sujeito = TratarString(certificado.CertificadoSelecionado.Subject);
                    certificado.Sujeito = sujeito;
                    certificado.Emissor = emissor;
                    certificado.Valor = sujeito + " (emitido por " + emissor + ")";
                    certificado.DescricaoCertificado = certificado.CertificadoSelecionado.IssuerName.Name;
                    certificado.NumeroSerie = certificado.CertificadoSelecionado.SerialNumber;
                    certificado.ValidadeCertificado = certificado.CertificadoSelecionado.NotBefore + " à " + certificado.CertificadoSelecionado.NotAfter;
                    certificado.ModeloCertificado = (EnumModeloCertificado)InfoCertificate.ModelCertificate(certificado.CertificadoSelecionado);

                    try
                    {
                        switch (certificado.ModeloCertificado)
                        {
                            case EnumModeloCertificado.A1:
                                certificado.DocumentoCertificado = certificado.CertificadoSelecionado.Export(X509ContentType.Pkcs12, ConfigurationManager.AppSettings["Token"].ToString());
                                break;

                            case EnumModeloCertificado.A3:
                                certificado.DocumentoCertificado = certificado.CertificadoSelecionado.Export(X509ContentType.SerializedCert, ConfigurationManager.AppSettings["Token"].ToString());
                                break;
                        }

                        certificados.Add(certificado);
                    }
                    catch
                    {

                    }
                }
            }
            store.Close();

            return certificados;
        }

        private string TratarString(string objetos)
        {
            string[] arrayObjeto;

            if (objetos.Contains(" + "))
            {
                arrayObjeto = SepararPorSinalMais(objetos);
            }
            else
            {
                arrayObjeto = SepararPorVirgula(objetos);
            }

            var objetoDeRetorno = RetornarValor(arrayObjeto);
            return objetoDeRetorno;
        }

        private string[] SepararPorSinalMais(string objetos)
        {
            string[] colunas = objetos.Split(new string[] { " + " }, StringSplitOptions.RemoveEmptyEntries);
            return colunas;
        }

        private string[] SepararPorVirgula(string objetos)
        {
            string[] colunas = objetos.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            return colunas;
        }

        private string RetornarValor(string[] arrayObjeto)
        {
            foreach (var objeto in arrayObjeto)
            {
                if (objeto.Contains("CN="))
                {
                    var novoObjeto = objeto.Remove(0, 3);
                    return novoObjeto;
                }
            }

            return "";
        }
    }
}
