using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;
using TGS.Cartorio.Infrastructure.Utility.Others;
using TGS.Cartorio.Infrastructure.Utility.Settings;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Contracts;
using TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Interfaces;

namespace TGS.Cartorio.Infrastructure.Utility.WebServerContracts
{
    public class EmailWebServer : IEmailWebServer
    {
        private readonly SettingsEmail _settingsEmail;
        private readonly IEmailContract _serviceClient;
        public EmailWebServer(IOptions<SettingsEmail> settingsEmail)
        {
            _settingsEmail = settingsEmail.Value;

            var binding = new BasicHttpsBinding();
            var endpoint = new EndpointAddress(new Uri(string.Format(_settingsEmail.Url, Environment.MachineName)));
            var channelFactory = new ChannelFactory<IEmailContract>(binding, endpoint);
            _serviceClient = channelFactory.CreateChannel();
        }

        public async Task<Retorno<string>> EnviarMensagem(DadosEnvioEmail dadosEnvioEmail)
        {
            try
            {
                Dv dv = new Dv();
                Liberacao liberacaoPrincipal = new Liberacao(_settingsEmail.IdLiberacao, 
                                                    _settingsEmail.Produto, 
                                                    _settingsEmail.CNPJ, 
                                                    _settingsEmail.Arquivo);

                string strAnexo = "";
                if (dadosEnvioEmail.Anexo != null && dadosEnvioEmail.Anexo.Length > 0)
                    strAnexo = Convert.ToBase64String(dadosEnvioEmail.Anexo);

                Objeto objeto = new Objeto(dadosEnvioEmail.Nome,
                                           dadosEnvioEmail.Email,
                                           dadosEnvioEmail.Assunto,
                                           dadosEnvioEmail.Mensagem,
                                           dadosEnvioEmail.NomeArquivoAnexo,
                                           strAnexo);

                Liberacao liberacao = new Liberacao(_settingsEmail.IdLiberacao);
                liberacao.Objeto.Add(objeto);

                dv.Liberacao.Add(liberacaoPrincipal);
                dv.Liberacao.Add(liberacao);

                var text = Utilities.SerializerXMLObject(dv);
                Encoding iso = Encoding.GetEncoding("iso-8859-1");
                byte[] bytes = iso.GetBytes(text);

                Retorno<string> retorno = new Retorno<string>();
                
                var ret = await _serviceClient.ProcessarPedidoAsync(bytes);

                retorno.Log = LogServicoDto.Create("OperationContractAction: http://tempuri.org/IServicoEmail/ProcessarPedido",
                    "SOUP",
                    JsonConvert.SerializeObject(dv),
                    ret,
                    "Não há tratamento neste serviço");
                
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
