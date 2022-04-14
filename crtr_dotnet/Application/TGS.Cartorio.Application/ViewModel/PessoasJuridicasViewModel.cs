using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.ViewModel
{
    public class PessoasJuridicasViewModel
    {
        public long IdPessoaJuridica { get; set; }
        public long IdPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int IdGenero { get; set; }
        public DateTime DataOperacao { get; set; }
        public long IdUsuario { get; set; }
    }
}
