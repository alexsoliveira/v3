using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.DTO
{
    public class SolicitacoesPartesDto
    {
        private List<object> _partes;

        public List<object> Partes
        {
            get
            {
                if (_partes == null) _partes = new List<object>();
                
                return _partes;
            }   
            set
            {
                _partes = value;
            }

        }
    }
}
