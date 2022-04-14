using System.ComponentModel.DataAnnotations;

namespace TGS.Cartorio.Domain.Enumerables
{
    public enum ETipoUsuario
    {
        CPF = 1,
        CNPJ = 2
    }
    public enum ECartoriosEstadosPC
    {
        Ativo = 1,
        Inativo = 2
    }

    public enum EGenerosPC
    {
        Feminino = 1,
        Masculino = 2,
        ONG = 5,
        Outros = 6,
        Privado = 4,
        Publico = 3
    }

    public enum ESolicitacoesNotificacoesEstadospc
    {
        Cadastrada = 1
    }

    public enum ESolicitacoesEstadosPC
    {
        [Display(Name = "Dispon�vel para Atendimento")]
        Disponivel_para_Atendimento = 1,
        [Display(Name = "Em Atendimento")]
        Em_Atendimento = 2,
        Cadastrada = 0,
        [Display(Name = "Aguardando aprova��o da minuta")]
        Aguardando_aprovacao_da_minuta = 3,
        Aprovada = 4,
        Reprovada = 5,
        [Display(Name = "Solicita��o Pronta Para Envio ao Cart�rio")]
        Solicitacao_pronta_para_envio_ao_cartorio = 11,
        [Display(Name = "Solicita�ao Enviada Ao Cart�rio")]
        Solicitacao_enviada_ao_cartorio = 9
    }

    public enum ESolicitacoesPartesEstadosPC
    {
        Cadastrado = 1,
        
        [Display(Name = "Documentos Anexados")]
        Documentos_anexados = 2,
        
        [Display(Name = "Documentos Assinados")]
        Documentos_assinados = 3
    }
    public enum ETiposContatosPC
    {
        Celular = 3,
        [Display(Name = "E-mail")]
        Email = 4,
        [Display(Name = "Telefone Comercial")]
        TelefoneComercial = 2,
        [Display(Name = "Telefone Residencial")]
        TelefoneResidencial = 1
    }

    public enum ETiposDocumentosPC
    {
        RG = 1,
        CPF = 2,
        [Display(Name = "Certid�o de Nascimento")]
        CertidaoNascimento = 3,
        [Display(Name = "Comprovante de Endere�o")]
        ComprovanteEndereco = 4,
        CNPJ = 5
    }

    public enum ETiposEnderecosPC
    {
        [Display(Name = "Endere�o de Cobran�a")]
        EnderecoCobranca = 2,
        [Display(Name = "Endere�o de Entrega")]
        EnderecoEntrega = 1
    }

    public enum ETiposFretesPC
    {
        [Display(Name = "Correios - Carta Registrada")]
        CorreiosCartaRegistrada = 2,
        [Display(Name = "Correios Sedex")]
        CorreiosSedex = 1,
        DHL = 3
    }

    public enum ETiposPartesPC
    {
        Outogardo = 1,
        Outorgante = 2
    }

    public enum EProcuracaoParteEstado
    {
        Cadastrado = 10,
        DocumentosAnexados = 20,
        DocumentosAssinados = 30,
        DocumentoEnviado = 40,
        Concluido = 50
    }

    public enum ETipoProcuracaoParte
    {
        Outogado = 1,
        Outogante = 2
    }

    public enum ETiposTaxasPc
    {
        Emolumentos = 1
    }

    public enum EMatrimoniosTiposDocumentos
    {
        Proclamas = 1,
        CPF = 2,
        RNE = 3
    }
}