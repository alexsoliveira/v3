using System;
using System.Collections.Generic;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.ViewModel
{
    public class UsuarioContaViewModel
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Email { get; set; }
        public DateTime? Nascimento { get; set; }
        public long Documento { get; set; }
        public int IdTipoDocumento { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public string Profissao { get; set; }
        public string RG { get; set; }
        public IList<ContatoViewModel> Contatos { get; set; }
        public IList<EnderecosDto> Enderecos { get; set; }
    }
}
