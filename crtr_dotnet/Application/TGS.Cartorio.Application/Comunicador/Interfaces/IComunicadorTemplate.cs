using System.Collections.Generic;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.Comunicador.Interfaces
{
    public interface IComunicadorTemplate
    {
        string GetTemplate(TipoComunicador tipoComunicador, TipoMensagem tipoMensagem, Dictionary<string, string> dados);
    }
}
