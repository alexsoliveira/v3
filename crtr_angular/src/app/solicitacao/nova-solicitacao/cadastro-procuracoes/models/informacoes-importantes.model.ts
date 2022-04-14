export class InformacoesImportantes {
    informacoes: string;
    idSolicitacao: number;
    constructor(idSolicitacao: number,
        informacoesImportantes: string) {
        this.idSolicitacao = idSolicitacao;
        this.informacoes = informacoesImportantes;
    }
}
