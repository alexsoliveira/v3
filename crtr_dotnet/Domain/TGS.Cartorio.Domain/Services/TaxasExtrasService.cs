using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Auxiliar;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class TaxasExtrasService : ITaxasExtrasService
    {		    
        private readonly ITaxasExtrasSqlRepository _taxasExtrasSqlRepository;

        public TaxasExtrasService(ITaxasExtrasSqlRepository taxasExtrasSqlRepository)
        {
            _taxasExtrasSqlRepository = taxasExtrasSqlRepository;
        }

        public async Task<TaxasExtras> BuscarTaxaEmolumentoPorEstado(string uf)
        {
            try
            {
                if (string.IsNullOrEmpty(uf))
                    throw new Exception("UF do cartório está vazio ou nulo!");

                TaxasExtras taxasExtras = null;
                var taxas = await _taxasExtrasSqlRepository.Pesquisar(t => t.IdTipoTaxa == (int)ETiposTaxasPc.Emolumentos);
                if (taxas != null)
                    taxasExtras = taxas.FirstOrDefault(t => JsonConvert.DeserializeObject<TaxasExtrasConteudo>(t.CamposExtras)?.Uf == uf);

                return taxasExtras;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
