using System;
using System.Collections.Generic;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.ViewModel
{
    public class UsuarioSolicitanteViewModel
    {
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public long IdPessoaSolicitante { get; set; }
        public int IdTipoDocumento { get; set; }
        public long Documento { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Profissao { get; set; }
        public string Rg { get; set; }
        public string Nacionalidade { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public IList<EnderecosDto> Enderecos { get; set; }
        public ContatoViewModel Contato { get; set; }
    }
}
