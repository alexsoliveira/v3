export class TestemunhaDto {
    nome: string;
    idTipoDocumento: any;
    documento: string;
    rg: string;
    parte: any;

    constructor(nome: string,
        idTipoDocumento: any,
        documento: string,
        rg: string,
        parte: any) {
        
        this.nome = nome;
        this.idTipoDocumento = idTipoDocumento;
        this.documento = documento;
        this.rg = rg;
        this.parte = parte;
    }
}