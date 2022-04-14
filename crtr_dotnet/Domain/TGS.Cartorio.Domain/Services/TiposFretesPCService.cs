using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	public class TiposFretesPCService : ITiposFretesPCService
    {		    
        private readonly ITiposFretesPCSqlRepository _tiposFretesPCRepository;

        public TiposFretesPCService(ITiposFretesPCSqlRepository tiposFretesPCRepository)
        {
            _tiposFretesPCRepository = tiposFretesPCRepository;
        }
        public async Task<List<TiposFretesPc>> BuscarTodos(int pagina)
        {
            return await _tiposFretesPCRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<TiposFretesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposFretesPCRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<List<TiposFretesPc>> BuscarCustos(long cep)
        {
            List<TiposFretesPc> tiposFretesPcs = new List<TiposFretesPc>();

            var retorno = await _tiposFretesPCRepository.BuscarTodosComNoLock(u => true, 0);
            

            //lixo
            //mock
            Decimal _custo = 0;
            string _prazo;

            foreach (var item in retorno)
            {
                if (item.Descricao.Contains("Correios - Carta Registrada"))
                {
                    _custo = (Decimal)12.52;
                    _prazo = "De 10 a 15 dias úteis.";
                }
                else if (item.Descricao.Contains("Correios - Sedex"))
                {
                    _custo = (Decimal)24.40;
                    _prazo = "De 1 a 3 dias úteis.";
                }
                else
                {
                    _prazo = "1 dia útil.";
                    _custo = (Decimal)25.00;
                }
                item.CustosFretes = new CustosFretes { Cep = cep, IdTipoFrete = item.IdTipoFrete, Valor = _custo, Prazo = _prazo }; /*lixo*/
            }
            
            return retorno;
        }

    }
}
