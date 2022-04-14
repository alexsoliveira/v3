import { Component, Input, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';

import { LacunaWebPKI, CertificateModel } from 'web-pki';

import { Certificados } from 'src/app/shared/certificado/models/certificados.model';
import { LacunaPkiService } from 'src/app/shared/services/lacuna-pki.service';
import { CertificadoComponent } from './../../shared/certificado/components/certificado.component';

import { SolicitacaoAssinaturaService } from './services/solicitacao-assinatura.service';
import { environment } from '../../../environments/environment.prod';
import { ActivatedRoute, Router } from '@angular/router';

import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../utils/toastr.config';
import { DirecionamentoService } from '../services/direcionamento.service';
import { SolicitacaoService } from '../services/solicitacao.service';

@Component({
  selector: 'app-solicita-assinatura',
  templateUrl: './solicita-assinatura.component.html',
  styleUrls: ['./solicita-assinatura.component.scss']
})
export class SolicitaAssinaturaComponent implements OnInit, AfterViewInit {

  pki: LacunaWebPKI;

  public loading: boolean = true;
  public selectedCertificate: string;
  public certificateList: CertificateModel[] = [];
  public result: boolean = false;
  public error: boolean = false;

  arquivoSelecionado: File = null;
  assinaturaFormulario: FormGroup = this.fb.group({
    uploadArquivo: ['', Validators.required],
    senha: ['', Validators.required]
  });;
  documentoAssinado: any;
  certificado: Certificados;
  @Input() nomeIconeOk: string = "check_box_outline_blank";
  idMatrimonio: number = undefined;
  idPessoaSolicitante: number = undefined;
  idSolicitacao: number = undefined;
  dadosUsuario: any = undefined;
  hashCertificado: string = "";
  transferData: string = "";
  certificados: Array<Certificados>;
  extensionId = environment.idExtensaoChrome;
  passosWizard = -1;
  flagExtensao: boolean = false;
  flagDownloadHost: boolean = false;
  checkDadosRota: boolean = true;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private dialog: MatDialog,
    private spinner: NgxSpinnerService,
    private solicitacaoAssinaturaService: SolicitacaoAssinaturaService,
    private lacunaPkiService: LacunaPkiService,
    private direcionarStatusSolicitacao: DirecionamentoService,
    private solicitacaoService: SolicitacaoService
  ) { }

  async ngOnInit() {
    this.idMatrimonio = this.activatedRoute.snapshot.params.idMatrimonio;
    this.idPessoaSolicitante = this.activatedRoute.snapshot.params.idPessoaSolicitante;
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;

    this.pki = new LacunaWebPKI(this.lacunaPkiService.getLacunaWebPkiBinary());

    await this.solicitacaoService.atualizarStatusSolicitacao(this.idSolicitacao);

    this.direcionarStatusSolicitacao.direcionarStatusSolicitacao('solicitacao-assinatura', this.idSolicitacao);

    this.spinner.show();
    let localStorage = new LocalStorageUtils();
    this.dadosUsuario = localStorage.lerUsuario();

    if (!this.idMatrimonio || this.idMatrimonio.toString() == 'undefined'
      || !this.idPessoaSolicitante || this.idPessoaSolicitante.toString() == 'undefined'
      || !this.idSolicitacao || this.idSolicitacao.toString() == 'undefined') {
      this.checkDadosRota = false;
      this.toastr.error('Não foi possível capturar os dados da rota!\n\nEntre em contato com a equipe de Suporte do Tabelionet!', 'Tabelionet', ToastOptions);
    }

    this.pki.init({
      ready: this.onWebPkiReady,
      notInstalled: this.onWebPkiNotInstalled,
      // ngZone: this.ngZone,
      // defaultFail: this.onWebPkiError
    });

    this.spinner.hide();
  }

  ngAfterViewInit(): void {
    this.passosWizard = -1;
  }

  onWebPkiReady() {
    console.log(this.pki);
  }
  onWebPkiNotInstalled(status, message) {
    let intervalo = setInterval(() => {
      alert('Para que você possa seguir com a assinatura digital, você será redirecionado para instalar nossa ferramenta de captação de certificado digital.')
      let windowFeatures = 
      window.open(
        'https://get.webpkiplugin.com',
        '_blank',
        'popup,noreferrer'
      );
      clearInterval(intervalo);
    }, 500)
    
  }

  selecionarArquivo(): void {
    this.arquivoSelecionado = this.assinaturaFormulario.get('uploadArquivo').value;
    const reader = new FileReader();
    try {
      reader.onload = this.manipularReader.bind(this);
      reader.readAsBinaryString(this.arquivoSelecionado);
    } catch {
      this.toastr.error('Ocorreu um erro ao processar o arquivo PDF selecionado!', 'Tabelionet', ToastOptions);
    }
  }

  manipularReader(readerEvt: any): void {
    try {
      const binaryString = readerEvt.target.result;
      this.documentoAssinado = btoa(binaryString);
    } catch {
      this.toastr.error('Ocorreu um erro ao processar o arquivo PDF selecionado!', 'Tabelionet', ToastOptions);
    }
  }

  documentoValido(): boolean {
    let valido = false;
    try {
      valido = this.assinaturaFormulario.get('uploadArquivo').touched
        && this.assinaturaFormulario.get('uploadArquivo').status === 'invalid';
    } catch { }
    return valido;
  }

  abrirModalCertificado(): void {
    const modal = this.dialog.open(CertificadoComponent, {
      height: '523px',
      width: '833px',
    });
    modal.afterClosed().subscribe(result => {
      if (result !== undefined) {
        //TODO: guardar certificado selecionado
        this.certificado = result;
        this.nomeIconeOk = "check_box";
        this.mostrarOcultarSenha();
      }
    },
      error => {
        console.log(error);
      }
    );
  }

  mostrarOcultarSenha(): boolean {
    if (this.certificado === undefined) {
      return false;
    }
    if (this.certificado.ModeloCertificado.toString() === "A3") {
      return true;
    }
    return false
  }

  validarBotao(): boolean {
    if (this.arquivoSelecionado === null) {
      return true;
    }
    if (this.certificado === undefined) {
      return true;
    }
    if (!this.assinaturaFormulario.valid && this.mostrarOcultarSenha()) {
      return true;
    }
    return false;
  }

  assinarDocumento(): void {
    this.spinner.show();
    this.certificado.DocumentoPDF = this.documentoAssinado;
    this.certificado.Senha = this.assinaturaFormulario.get("senha").value;
    this.certificado.IdMatrimonio = this.idMatrimonio;
    this.certificado.IdPessoaSolicitante = this.idPessoaSolicitante;
    this.certificado.IdUsuario = this.dadosUsuario.idUsuario;

    console.log(this.certificado);
    console.log(this.documentoAssinado);

    this.solicitacaoAssinaturaService.assinarPrimeiroPasso(this.certificado)
      .subscribe(
        docToSign => {
          this.transferData = docToSign.tranferDataBase64;
          this.pki.signHash({
            thumbprint: this.certificado.ThumbPrintBase64,
            hash: docToSign.toSignHashBase64,
            digestAlgorithm: docToSign.digestAlgorithmOid
          }).success((signature) => {
            let dadosAssinatura = {
              signature: signature,
              transferData: this.transferData,
              idMatrimonioDocumento: docToSign.idMatrimonioDocumento
            }
            this.solicitacaoAssinaturaService.assinarSegundoPasso(dadosAssinatura)
              .subscribe((padesSignature) => {
                this.toastr.success('O documento foi assinado eletronicamente com sucesso!', 'Tabelionet', ToastOptions)
                  .onHidden.pipe().subscribe(() => {
                    this.solicitacaoAssinaturaService.atualizarStatusSolicitacao(this.idSolicitacao)
                      .subscribe(atualizado => {
                        console.log('solicitacao-assinatura status da solicitação foi atualizado com sucesso');
                        this.router.navigate([`/carrinho/${this.idSolicitacao}`]);
                      },
                        error => {
                          this.toastr.error('Ocorreu um erro ao tentar atualizar a solicitação!', 'Tabelionet', ToastOptions);
                        })
                  });
              },
                error => {
                  if (error && error.message) {
                    this.toastr.error(`Ocorreu um erro ao tentar realizar a assinatura!\n\n${error.message}`, 'Tabelionet', ToastOptions);
                  } else {
                    this.toastr.error(`Ocorreu um erro ao tentar realizar a assinatura!`, 'Tabelionet', ToastOptions);
                  }

                  console.log(error);
                })
            this.spinner.hide();

          }).fail((err) => {
              if (err && typeof(err.message) === 'string' && err.message.search('cancelada') > 0) {
                this.toastr.info('Assinatura digital cancelada!', 'Tabelionet', ToastOptions);
              } else if (err && err.message) {
                this.toastr.error('Ocorreu um erro ao tentar realizar a assinatura!', 'Tabelionet', ToastOptions);
                console.log('Erro Lacuna');
                console.log(`Código do Erro Lacuna: ${err.code}`);
                console.log(`Mensagem Erro Lacuna: ${err.message}`);
              } else {
                this.toastr.error(`Ocorreu um erro ao tentar realizar a assinatura!`, 'Tabelionet', ToastOptions);
              }

              console.log(err);
              this.spinner.hide();
            });

        },
        error => {
          this.toastr.error(`Ocorreu um erro ao tentar realizar a assinatura!\n\n${error.message}`, 'Tabelionet', ToastOptions);
          this.spinner.hide();
          console.log(error);
        },
        () => {
          this.spinner.hide();
        }
      );
  }
}
