using System;
using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            AssinaturaDigitalLog = new HashSet<AssinaturaDigitalLog>();
            Cartorios = new HashSet<Cartorios>();
            CartoriosContatos = new HashSet<CartoriosContatos>();
            CartoriosEnderecos = new HashSet<CartoriosEnderecos>();
            Configuracoes = new HashSet<Configuracoes>();
            Contatos = new HashSet<Contatos>();
            Enderecos = new HashSet<Enderecos>();
            Perfis = new HashSet<Perfis>();
            PerfisPermissoes = new HashSet<PerfisPermissoes>();
            Pessoas = new HashSet<Pessoas>();
            PessoasContatos = new HashSet<PessoasContatos>();
            PessoasEnderecos = new HashSet<PessoasEnderecos>();
            PessoasFisicas = new HashSet<PessoasFisicas>();
            PessoasJuridicas = new HashSet<PessoasJuridicas>();
            Produtos = new HashSet<Produtos>();
            ProdutosDocumentos = new HashSet<ProdutosDocumentos>();
            ProdutosImagens = new HashSet<ProdutosImagens>();
            ProdutosModalidades = new HashSet<ProdutosModalidades>();
            Solicitacoes = new HashSet<Solicitacoes>();
            SolicitacoesDocumentos = new HashSet<SolicitacoesDocumentos>();
            SolicitacoesNotificacoes = new HashSet<SolicitacoesNotificacoes>();
            UsuariosContatos = new HashSet<UsuariosContatos>();
            UsuariosPerfisIdUsuarioNavigation = new HashSet<UsuariosPerfis>();
            UsuariosPerfisIdUsuarioOperacaoNavigation = new HashSet<UsuariosPerfis>();
            TaxasExtras = new HashSet<TaxasExtras>();
            ProcuracoesPartes = new HashSet<ProcuracoesPartes>();
        }

        public long IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public DateTime DataOperacao { get; set; }
        public bool FlagAtivo { get; set; }
        public long? IdPessoa { get; set; }

        public virtual Pessoas IdPessoaNavigation { get; set; }
        public virtual ICollection<AssinaturaDigitalLog> AssinaturaDigitalLog { get; set; }
        public virtual ICollection<Cartorios> Cartorios { get; set; }
        public virtual ICollection<CartoriosContatos> CartoriosContatos { get; set; }
        public virtual ICollection<CartoriosEnderecos> CartoriosEnderecos { get; set; }
        public virtual ICollection<Configuracoes> Configuracoes { get; set; }
        public virtual ICollection<Contatos> Contatos { get; set; }
        public virtual ICollection<Enderecos> Enderecos { get; set; }
        public virtual ICollection<Perfis> Perfis { get; set; }
        public virtual ICollection<PerfisPermissoes> PerfisPermissoes { get; set; }
        public virtual ICollection<Pessoas> Pessoas { get; set; }
        public virtual ICollection<PessoasContatos> PessoasContatos { get; set; }
        public virtual ICollection<PessoasEnderecos> PessoasEnderecos { get; set; }
        public virtual ICollection<PessoasFisicas> PessoasFisicas { get; set; }
        public virtual ICollection<PessoasJuridicas> PessoasJuridicas { get; set; }
        public virtual ICollection<Produtos> Produtos { get; set; }
        public virtual ICollection<ProdutosDocumentos> ProdutosDocumentos { get; set; }
        public virtual ICollection<ProdutosImagens> ProdutosImagens { get; set; }
        public virtual ICollection<ProdutosModalidades> ProdutosModalidades { get; set; }
        public virtual ICollection<Solicitacoes> Solicitacoes { get; set; }
        public virtual ICollection<SolicitacoesDocumentos> SolicitacoesDocumentos { get; set; }
        public virtual ICollection<SolicitacoesNotificacoes> SolicitacoesNotificacoes { get; set; }
        public virtual ICollection<UsuariosContatos> UsuariosContatos { get; set; }
        public virtual ICollection<UsuariosPerfis> UsuariosPerfisIdUsuarioNavigation { get; set; }
        public virtual ICollection<UsuariosPerfis> UsuariosPerfisIdUsuarioOperacaoNavigation { get; set; }
        public virtual ICollection<TaxasExtras> TaxasExtras { get; set; }
        public virtual ICollection<ProcuracoesPartes> ProcuracoesPartes { get; set; }
    }
}
