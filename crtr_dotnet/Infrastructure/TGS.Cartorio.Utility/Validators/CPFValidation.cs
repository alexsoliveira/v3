using NetDevPackBr.Documentos.Validacao;

namespace TGS.Cartorio.Infrastructure.Utility.Validators
{
    public class CPFValidation
    {
        public static bool isValid(string numero)
        {
            return new CpfValidador(numero).EstaValido();
        }
    }
}
