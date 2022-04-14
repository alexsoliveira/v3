import { Outorgante } from './outorgante.model'

export class OutorganteDto {
    idSolicitacao: number;
    idPessoa: number;
    nome: string;
    idTipoDocumento: number;
    documento: number;
    idTipoProcuracaoParte: number;
    idProcuracaoParteEstado: number;
    jsonConteudo: string;
    enderecoEntrega: string;
    email: string;
    idUsuario: number;


    constructor(outorgante: Outorgante) {
        this.documento = outorgante.documento,
        this.email = outorgante.email,
        this.idSolicitacao = this.idSolicitacao,
        this.idPessoa = outorgante.idPessoa,
        this.nome = outorgante.nomePessoa,
        this.idTipoDocumento = outorgante.idTipoDocumento,
        this.idTipoProcuracaoParte = 0,
        this.idProcuracaoParteEstado = 0,
        this.jsonConteudo = ""
    }
}
