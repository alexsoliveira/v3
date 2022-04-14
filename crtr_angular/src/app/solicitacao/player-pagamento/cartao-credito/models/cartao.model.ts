import { DonoCartao } from "./dono-cartao.model";

export class Cartao {
    number: string;
    exp_month: string;
    exp_year: string;
    security_code: string;
    holder: DonoCartao;
    constructor(numeroCartao: string,
                mesExpiracao: string,
                anoExpiracao: string,
                codigoSeguranca: string,
                donoCartao: DonoCartao) {
                    this.number = numeroCartao;
                    this.exp_month = mesExpiracao;
                    this.exp_year = anoExpiracao;
                    this.security_code = codigoSeguranca;
                    this.holder = donoCartao;
    }
}