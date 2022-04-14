using System;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.ViewModel
{
    public class UsuarioDadosPessoaisViewModel
    {
        public ContatoViewModel Contato { get; set; }
        public int IdPessoa { get; set; }
        public long IdUsuario { get; set; }
        public DateTime Nascimento { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public string NomeSocial { get; set; }
        //public string Email { get; set; }
    }
}
