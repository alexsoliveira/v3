using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGS.Cartorio.Application.ViewModel.Identity
{
    public class UsuarioRegistro
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public int IdGenero { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RG { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }
        public bool ErroLogado { get; set; }
    }

    public class UsuarioLogin
    {       
        public string Email { get; set; }       
        public string Senha { get; set; }
    }

    public class UsuarioRespostaLogin
    {       
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public bool ContaAtivada { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
        public long IdUsuario { get; set; }
        public long? IdPessoa { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UsuarioConta
    {    
        public string Email { get; set; }        
    }

    public class UsuarioResetSenha
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioConfirmaConta
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }

    public class UsuarioAlterarSenha
    {
        public string UserId { get; set; }     
        public string SenhaAtual { get; set; }        
        public string NovaSenha { get; set; }
    }
}