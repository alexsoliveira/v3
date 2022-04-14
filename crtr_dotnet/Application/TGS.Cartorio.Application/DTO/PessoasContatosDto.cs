namespace TGS.Cartorio.Application.DTO
{
    public class PessoasContatosDto
    {
        public PessoasContatosDto()
        {
            IdContatoNavigation = new ContatosDto();
        }
        public ContatosDto IdContatoNavigation { get; set; }
    }
}