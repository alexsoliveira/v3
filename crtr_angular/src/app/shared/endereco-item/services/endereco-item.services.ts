import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { NovoEndereco } from '../../models/NovoEndereco.model';
import { EnderecoService } from '../../services/Endereco.service';

@Injectable({
  providedIn: 'root'
})

export class EnderecoItemService {
  public bsAlterarEndereco: BehaviorSubject<any>;
  public bsRemoverEndereco: BehaviorSubject<any>;
  public bsSelecionarEndereco: BehaviorSubject<any>;
  public listEnderecos: Array<NovoEndereco>;
  public enderecoSelecionado: NovoEndereco;
  public atualizarEnderecoPrincipal: boolean;
  public start: boolean = true;
  constructor(
    private enderecoService: EnderecoService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService,
  ) {}

  setDataAndRun(bsAlterarEndereco: BehaviorSubject<any>,
    bsRemoverEndereco: BehaviorSubject<any>,
    bsSelecionarEndereco: BehaviorSubject<any>,
    listEnderecos: Array<any>,
    atualizarEnderecoPrincipal: boolean) {
    this.bsAlterarEndereco = bsAlterarEndereco;
    this.bsRemoverEndereco = bsRemoverEndereco;
    this.bsSelecionarEndereco = bsSelecionarEndereco;
    this.listEnderecos = listEnderecos;
    this.atualizarEnderecoPrincipal = atualizarEnderecoPrincipal;
    this.bsAlterarEnderecoSubscribe();
    this.bsRemoverEnderecoSubscribe();
    this.bsSelecionarEnderecoSubscribe();
  }

  bsAlterarEnderecoSubscribe(): any {
    this.bsAlterarEndereco.subscribe(item => {
      if(item){
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate(['/usuario-conta']);
      }
    });
  }

  bsRemoverEnderecoSubscribe(): any {
    this.bsRemoverEndereco.subscribe(idEndereco => {
      if (idEndereco != undefined) {
        this.listEnderecos.splice(this.listEnderecos.findIndex(e => e.idEndereco === idEndereco), 1);
      }
    });
  }

  bsSelecionarEnderecoSubscribe(): any {
    this.bsSelecionarEndereco.subscribe(idEndereco => {
      if (idEndereco != undefined) {
        this.enderecoSelecionado = this.listEnderecos.find(e => e.idEndereco === idEndereco);
        if (this.atualizarEnderecoPrincipal && this.enderecoSelecionado) {
          this.spinner.show();
          this.enderecoService.AtualizarEnderecoPrincipal(idEndereco)
            .subscribe(endAtualizado => {
              this.spinner.hide();
              this.toastr.success("Endereço principal atualizado com sucesso!", 'Tabelionet', ToastOptions);
            }, (err) => {
              try {
                try {
                  this.spinner.hide();
                  err.error.errors.Mensagens.forEach(erro => {
                    this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
                  });
                }
                catch {
                  this.spinner.hide();
                  Object.keys(err.error.errors).forEach((erro, i, a) => {
                    err.error.errors[erro].forEach(msg => {
                      this.toastr.error(`${msg}`, 'Tabelionet', ToastOptions);
                    });
                  });
                }
              }
              catch {
                this.spinner.hide();
                this.toastr.error(`${err.status} - ${err.message} - ${err.error}`, 'Tabelionet', ToastOptions);
              }
            })
        }
      }
    });
  }

  getEnderecoSelecionado(): NovoEndereco {
    return this.enderecoSelecionado;
  }

  clearEnderecoSelecionado(): any{
    this.bsSelecionarEndereco = new BehaviorSubject(undefined);
  }

}
