using NetDevPackBr.Documentos.Validacao;

namespace TGS.Cartorio.Infrastructure.Utility.Validators
{
    public class CNPJValidation
    {
        public static bool isValid(string numero)
        {
            return new CnpjValidador(numero).EstaValido();
        }
    }
}
