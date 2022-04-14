import { OutorgadoCadastro } from '../components/cadastro-outorgado/models/outorgado-cadastro.model';
import { LocalStorageUtils } from '../../../../utils/localstorage';
import { OutorgadoDto } from './outorgado-dto.model';

export class SolicitacaoOutorgados {
    idSolicitacao: number;
    representacaoPartes: string;
    outorgados: OutorgadoDto[] = [];
    alteracaoDaSolicitacao: boolean;

    constructor(idSolicitacao: number,
        representacaoPartes: string,
        outorgados: OutorgadoCadastro[],
        alteracaoDaSolicitacao: boolean
      ) {
        this.idSolicitacao = idSolicitacao;
        this.representacaoPartes = representacaoPartes;
        this.setOutorgados(outorgados);
        this.alteracaoDaSolicitacao = alteracaoDaSolicitacao;
    }


    setOutorgados(outorgados: OutorgadoCadastro[]): void {
        if (outorgados) {
            let usuario = new LocalStorageUtils().lerUsuario();
            outorgados.forEach((outorgado) => {
                let outorgadoDto = new OutorgadoDto(outorgado, this.idSolicitacao);
                if (usuario) {
                    outorgadoDto.idUsuario = usuario.idUsuario;
                }
                // if (outorgado.rgOutorgado) {
                //     outorgadoDto.jsonConteudo = `{"rg":"${outorgado.rgOutorgado}"}`
                // }
                this.outorgados.push(outorgadoDto);
            })
        }
    }
}
