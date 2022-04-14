using System;
using TGS.Cartorio.Application.Enumerables;

namespace TGS.Cartorio.Application.DTO
{
    public class ConteudoPessoasFisicasDto
    {
        public DateTime? DataNascimento { get; set; }
        public string Profissao { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public string RG { get; set; }
        public string Nacionalidade { get; set; }
    }
}