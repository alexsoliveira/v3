using System;

namespace TGS.Cartorio.Domain.Entities
{
    public class StatusSolicitacao
    {
        public long IdSolicitacao { get; set; }
        public string Participante { get; set; }
        public long IdPessoa { get; set; }
        public long Documento { get; set; }
        public int IdTipo { get; set; }
        public string Tipo { get; set; }
        public int? IdEstadoParticipante { get; set; }
        public string EstadoParticipante { get; set; }
        public DateTime UltimaInteracao { get; set; }
    }
}
