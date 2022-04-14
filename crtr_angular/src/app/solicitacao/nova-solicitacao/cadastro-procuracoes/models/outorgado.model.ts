import { LocalStorageUtils } from '../../../../utils/localstorage';

export class Outorgado {
  idSolicitacao: number;
  idPessoa: number;
  idUsuario: number;
  email: string;

  constructor(idSolicitacao: number,
              idPessoa: number,
              email: string){
                let dadosUsuarioLogado = new LocalStorageUtils();
                let usuario = dadosUsuarioLogado.lerUsuario();
                if(usuario){
                    this.idUsuario = usuario.idUsuario;
                }

                this.idSolicitacao = idSolicitacao;
                this.idPessoa = idPessoa;
                this.email = email;
  }
}
