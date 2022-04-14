import { Cartao } from "./cartao.model";

export class CartaoCredito {
    installments: number;
    value: number;
    card: Cartao;
    client: Cliente;

    constructor(numeroParcelas: number,
                valorTotal: number,
                cartao: Cartao,
                documento: string) {
                    this.installments = numeroParcelas;
                    this.value = valorTotal;
                    this.card = cartao;
                    this.client = new Cliente(documento);
    }   
}


export class Cliente {
    documentNumber: string;

    constructor(documento: string){
        this.documentNumber = documento;
    }
}