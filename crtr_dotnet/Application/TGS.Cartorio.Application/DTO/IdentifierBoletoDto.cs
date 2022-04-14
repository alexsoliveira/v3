using Newtonsoft.Json;
using System;

namespace TGS.Cartorio.Application.DTO
{
    public class IdentifierBoletoDto
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }


        public bool TryConvertJsonObj(string jsonStrObj)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonStrObj))
                {
                    var identifierBoleto = JsonConvert.DeserializeObject<IdentifierBoletoDto>(jsonStrObj);
                    if (identifierBoleto != null)
                        Identifier = identifierBoleto.Identifier;
                }

                return !string.IsNullOrEmpty(Identifier);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
