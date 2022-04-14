using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.ViewModel
{
    public class PessoasFisicasViewModel
    {
        public long IdPessoaFisica { get; set; }
        public long IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string NomeSocial { get; set; }
        public int IdGenero { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }

    }
}
