using Lacuna.Pki;
using Lacuna.Pki.Pades;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TGS.Cartorio.Application.CertificadoDigital.interfaces;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.CertificadoDigital
{
    public class RepresentacaoVisual : IRepresentacaoVisual
    {
        private readonly IConfiguration _configuration;
        public RepresentacaoVisual(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public PadesVisualRepresentation2 Set(PKCertificate pkCertificate = null, X509Certificate x509 = null, long idValidadorLong = 0, string validadorGuid = null)
        {
            try
            {
                string path = _configuration.GetValue<string>(EnumAppSettings.PathCertificado.ToString());
                string linkValidador = _configuration.GetValue<string>(EnumAppSettings.LinkValidadorDocumentoAssinado.ToString());
                string textoValidadeDigitalDoCertificado = _configuration.GetValue<string>(EnumAppSettings.TextoValidadeDigitalDoCertificado.ToString());

                if (idValidadorLong > 0)
                    linkValidador += idValidadorLong.ToString();
                else if (!string.IsNullOrEmpty(validadorGuid))
                    linkValidador += validadorGuid;

                var currentDirectory = Environment.CurrentDirectory;
                var pathCertificado = Path.Combine(currentDirectory, path, "img-certificado.png");

                var visualRepresentation = new PadesVisualRepresentation2()
                {   
                    Text = new PadesVisualText()
                    {
                        CustomText = String.Format("Assinado eletronicamente por\n{0}\nem {2} e portador do {5} {1}\n\n{3}\n{4}", pkCertificate.SubjectDisplayName, 
                                                                                                        pkCertificate.PkiBrazil.CPF,
                                                                                                        $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                                                                                                        linkValidador,
                                                                                                        textoValidadeDigitalDoCertificado,
                                                                                                        "CPF"),
                                                                                               
                        FontSize = 9.5,
                        IncludeSigningTime = false,
                        HorizontalAlign = PadesTextHorizontalAlign.Left,
                        Container = new PadesVisualRectangle()
                        {
                            Left = 0.5,
                            Top = 0.5,
                            Right = 0.5,
                            Bottom = 0.5
                        },
                        FontColor = Color.FromArgb(0,0,0,0)
                    },
                    Image = new PadesVisualImage()
                    {
                        Content = File.ReadAllBytes(pathCertificado),
                        HorizontalAlign = PadesHorizontalAlign.Center,
                        VerticalAlign = PadesVerticalAlign.Center,
                        Opacity = 40
                    }
                };

                var visualPositioning = PadesVisualAutoPositioning.GetFootnote();
                visualPositioning.Container.Height = 4;
                visualPositioning.SignatureRectangleSize.Width = 16;
                visualPositioning.SignatureRectangleSize.Height = 4;
                visualRepresentation.Position = visualPositioning;

                return visualRepresentation;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
