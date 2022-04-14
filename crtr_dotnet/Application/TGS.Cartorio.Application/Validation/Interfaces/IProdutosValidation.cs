using System;
using System.Collections.Generic;
using System.Text;

namespace TGS.Cartorio.Application.Validation.Interfaces
{
    public interface IProdutosValidation
    {
        bool ValidarProduto(int IdProduto);
        bool ValidarProduto(long IdProduto);
    }
}
