import { Pagador } from "./pagador.model";

export class Boleto {

    //dataVencimento: string;
    mensagem: string;
    valor: number;
    urlNotificacao: string;
    pagador: Pagador;
    //emails: string[] = [];

    constructor (
        //dataVencimento: string,
        mensagem: string,
        valorTotal: number,
        urlNotificacao: string,
        pagador: Pagador,
        //emails: string[]
    ){
        //this.dataVencimento = dataVencimento;
        this.mensagem = mensagem;
        this.valor = Number.parseFloat(valorTotal.toString()) + 1.99;
        this.urlNotificacao = urlNotificacao;
        this.pagador = pagador;
        //this.emails = emails;
    }

}