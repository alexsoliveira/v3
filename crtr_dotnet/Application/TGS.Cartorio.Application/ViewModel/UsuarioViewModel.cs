using System;
using System.Collections.Generic;

namespace TGS.Cartorio.Application.ViewModel
{
    public class UsuarioViewModel
    {
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public long Documento { get; set; }
        public IList<ContatoViewModel> Contatos { get; set; }
    }
}
