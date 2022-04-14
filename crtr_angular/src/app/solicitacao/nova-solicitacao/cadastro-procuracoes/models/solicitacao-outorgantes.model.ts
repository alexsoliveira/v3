import { Outorgante } from './outorgante.model';
import { OutorganteDto } from './outorgante-dto.model';
import { LocalStorageUtils } from '../../../../utils/localstorage';

export class SolicitacaoOutorgantes {
    idSolicitacao: number;
    idPessoaSolicitante: number;
    outogantes: OutorganteDto[] = [];
    alteracaoDaSolicitacao: boolean;

    constructor(idSolicitacao: number,
        idPessoaSolicitante: number,
        outorgantes: Outorgante[],
        alteracaoDaSolicitacao: boolean
    ) {
        this.idSolicitacao = idSolicitacao;
        this.idPessoaSolicitante = idPessoaSolicitante;
        this.setOutorgantes(outorgantes);
        this.alteracaoDaSolicitacao = alteracaoDaSolicitacao;
    }


    setOutorgantes(outorgantes: Outorgante[]): void {
        if (outorgantes) {
            let usuario = new LocalStorageUtils().lerUsuario();
            outorgantes.forEach((outorgante) => {
                let outorganteDto = new OutorganteDto(outorgante);
                outorganteDto.idSolicitacao = this.idSolicitacao;
                if (usuario) {
                    outorganteDto.idUsuario = usuario.idUsuario;
                }
                if (outorgante.endereco) {
                    outorganteDto.enderecoEntrega = JSON.stringify(outorgante.endereco);
                }
                this.outogantes.push(outorganteDto);
            })
        }
    }
}
