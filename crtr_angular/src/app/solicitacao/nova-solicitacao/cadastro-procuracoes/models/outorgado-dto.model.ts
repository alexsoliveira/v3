import { OutorgadoCadastro } from '../components/cadastro-outorgado/models/outorgado-cadastro.model';

export class OutorgadoDto {
        idSolicitacao: number;
        idUsuario: number;
        idPessoa: number;
        nome: string;
        email: string;
        documento: string;
        idTipoDocumento: number;
        jsonConteudo: string;

    constructor(outorgado: OutorgadoCadastro, idSolicitacao: number) {
        this.documento = outorgado.documentoOutorgado,
        this.email = outorgado.emailOutorgado,
        this.idSolicitacao = idSolicitacao,
        this.idPessoa = outorgado.idPessoa,
        this.nome = outorgado.nomeOutorgado,
        this.idTipoDocumento = outorgado.idTipoDocumento
    }
}
