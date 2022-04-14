// * Dependencias
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
// * Utils
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { DateUtils } from '../../../../../utils/date.utils';
import { PatternInteracaoValidator } from 'src/app/validators/pattern-interacao.validator';
// * Components
import { EnderecoNovoComponent } from 'src/app/shared/endereco-novo/endereco-novo.component';
import { DatagridOutorganteComponent } from '../datagrid-outorgante/datagrid-outorgante.component';
// * Models
import { OutorganteCadastro } from './models/outorgante-cadastro.model';
import { OutorgantesDataGrid } from '../../models/outorgantesDataGrid';
import { NovoEndereco } from 'src/app/shared/models/NovoEndereco.model';
// * Services
import { EnderecoItemService } from '../../../../../shared/endereco-item/services/endereco-item.services';
import { CadastroOutorganteService } from './../../services/cadastro-outorgante.service';
import { EnderecoService } from 'src/app/shared/services/Endereco.service';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';
import { Console } from 'console';
import { SolicitacaoCadastroComponent } from '../../solicitacao-cadastro.component';
import { Inject } from '@angular/core';
import { NovaSolicitacaoService } from '../../services/nova-solicitacao.service';

@Component({
  selector: 'app-cadastro-outorgante',
  templateUrl: './cadastro-outorgante.component.html',
  styleUrls: ['./cadastro-outorgante.component.scss']
})
export class CadastroOutorganteComponent implements OnInit {

  bsRemoverEndereco = new BehaviorSubject<any>(undefined);
  bsAlterarEndereco = new BehaviorSubject<any>(undefined);
  bsSelecionarEndereco = new BehaviorSubject<any>(undefined);

  pessoaSolicitante: boolean = false;
  formOutorgante: FormGroup;
  outorganteFormControl = new FormControl('', [
    Validators.required
  ]);

  listEnderecos: NovoEndereco[] = [];
  selectTipoDocumentos: any[] = [];
  selectEstadoCivil: any[] = [];
  localStorageUtils = new LocalStorageUtils();
  listaDataGridOutorgantes: OutorgantesDataGrid[] = [];
  @Input() inputDatagridOutorgante: OutorgantesDataGrid[] = [];
  outorgantesCadastro: OutorganteCadastro[] = [];
  dadosUsuarioLogado: boolean;
  exibirRg: boolean = true;
  meuInput: string = "";
  exibirCampos: boolean = false;
  exibirCampoEmail: boolean = false;
  documentoCadastrado: boolean = false;
  emailCadastrado: boolean = false;
  pessoaExiste: number = null;
  @Output() idPessoaSolicitante = new EventEmitter<number>();

  @Output() dadosOutorgantes = new EventEmitter();
  @Output() datagridOutorgante = new EventEmitter();
  @ViewChild(DatagridOutorganteComponent) dataGridOutorgantes;

  constructor(
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private enderecoService: EnderecoService,
    public dialog: MatDialog,
    private outorganteService: CadastroOutorganteService,
    private dateUtils: DateUtils,
    private htmlSelect: HtmlSelect,
    private enderecoItemService: EnderecoItemService,
    public piv: PatternInteracaoValidator,
    private novaSolicitacaoService: NovaSolicitacaoService,
    @Inject(SolicitacaoCadastroComponent) private pai: SolicitacaoCadastroComponent
  ) { }

  ngOnInit() {
    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumento();
    this.selectEstadoCivil = this.htmlSelect.getEstadoCivil();

    this.formOutorgante = this.fb.group({
      idTipoDocumento: ['', Validators.required],
      documento: ['', Validators.required],
      idPessoa: ['', Validators.required],
      rgOutorgante: [''],
      nomeOutorgante: ['', Validators.required],
      emailOutorgante: ['', Validators.compose([Validators.required, Validators.email])],
      nascimentoOutorgante: [''],
      estadoCivilOutorgante: [''],
      nacionalidade: [''],
      profissaoOutorgante: [''],
      celular: [''],
      foneAlternativo: ['']
    });

    this.enderecoItemService.setDataAndRun(
      this.bsAlterarEndereco,
      this.bsRemoverEndereco,
      this.bsSelecionarEndereco,
      this.listEnderecos,
      false);
    
    let intervalOutorgantesVindoDoPai = setInterval(() => {
      if (this.inputDatagridOutorgante.length > 0){
        this.dataGridOutorgantes.dataSource = this.inputDatagridOutorgante;

        this.dataGridOutorgantes.updateGridRows();
      }

      clearInterval(intervalOutorgantesVindoDoPai);
    }, 1000)
  }

  dadosUsuarioSolicitante(): void {

    let usuario = new LocalStorageUtils().lerUsuario();
    let idTipoDocumento = this.formOutorgante.get('idTipoDocumento').value;
    let documento = this.formOutorgante.get('documento').value;
    if (!idTipoDocumento || !documento) {
      return;
    }

    documento = documento.toString().replace('.', '').replace('.', '').replace('-', '');
    this.outorganteService.BuscarDadosSolicitante(idTipoDocumento, documento, usuario.idUsuario)
      .subscribe((usuarioSolicitante) => {

        if (usuarioSolicitante) {
          this.idPessoaSolicitante.emit(usuarioSolicitante.idPessoaSolicitante);
          this.pessoaSolicitante = true;
          this.dadosUsuarioLogado = true;
          this.exibirCampos = true;
          this.exibirCampoEmail = true;
          this.formOutorgante.setValidators([Validators.required]);
          this.pessoaExiste = usuarioSolicitante.idPessoaSolicitante;
          this.formOutorgante.patchValue({
            idTipoDocumento: usuarioSolicitante.idTipoDocumento,
            documento: usuarioSolicitante.documento,
            idPessoa: usuarioSolicitante.idPessoaSolicitante,
            rgOutorgante: usuarioSolicitante.rg,
            nomeOutorgante: usuarioSolicitante.nomeUsuario,
            emailOutorgante: usuarioSolicitante.email,
            nascimentoOutorgante: this.dateUtils.getValidDate(usuarioSolicitante.dataNascimento),
            estadoCivilOutorgante: usuarioSolicitante.estadoCivil,
            nacionalidade: usuarioSolicitante.nacionalidade,
            profissaoOutorgante: usuarioSolicitante.profissao,
            celular: usuarioSolicitante.contato?.celular,
            foneAlternativo: usuarioSolicitante.contato?.fixo,
          });

          this.listEnderecos = [];
          if (usuarioSolicitante.enderecos) {
            usuarioSolicitante.enderecos.forEach((endereco) => {
              this.listEnderecos.push(endereco);
            });
          }
        }
        else {
          this.PessoaExiste(idTipoDocumento, documento);
          this.pessoaSolicitante = false;
        }

        this.validarOutorganteSolicitante();

      }, (err) => {
        this.pessoaExiste = null;
        this.pessoaSolicitante = false;
        this.formOutorgante.get('idPessoa').clearValidators();
        if (err.status === 404) {

          this.PessoaExiste(idTipoDocumento, documento);

        }
      });

  }

  PessoaExiste(idTipoDocumento: number, documento: string) {
    this.outorganteService.PessoaExiste(idTipoDocumento, documento)
      .subscribe((pessoaExiste) => {

        if (pessoaExiste) {
          this.pessoaExiste = pessoaExiste;
          this.formOutorgante.get('idPessoa').setValidators([Validators.required]);
          console.log(this.pessoaExiste);
          if (this.pessoaExiste) {
            this.dadosUsuarioLogado = false;
            this.exibirCampos = false;
            this.exibirCampoEmail = false;
            this.formOutorgante.patchValue({
              idTipoDocumento: this.formOutorgante.get('idTipoDocumento').value,
              documento: this.formOutorgante.get('documento').value,
              idPessoa: pessoaExiste
            });
          }
        } else {
          this.pessoaExiste = null;
          this.formOutorgante.get('idPessoa').clearValidators();
          this.formOutorgante.patchValue({
            idPessoa: null
          });
        }

        this.validarOutorganteSolicitante();

      }, (err) => {
        this.pessoaExiste = null;
        this.formOutorgante.get('idPessoa').clearValidators();
        if (err.status === 404) {

          this.dadosUsuarioLogado = false;
          this.exibirCampos = false;
          this.exibirCampoEmail = true;

          this.formOutorgante.patchValue({
            idTipoDocumento: this.formOutorgante.get('idTipoDocumento').value,
            documento: this.formOutorgante.get('documento').value,
            idPessoa: null
          });
          this.validarOutorganteSolicitante();
        }
      });
  }

  BuscarEnderecos(): any {
    this.spinner.show();

    this.enderecoService.BuscarEnderecos(this.localStorageUtils.lerUsuario().idUsuario)
      .subscribe((ret: any) => {
        ret.forEach(item => {
          let endereco = new NovoEndereco();
          endereco.setConteudo(item.conteudo);
          this.listEnderecos.push(endereco);
        });
      },
        (err) => {
          try {
            try {
              err.error.errors.Mensagens.forEach(erro => {
                this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
              });
            }
            catch {
              Object.keys(err.error.errors).forEach((erro, i, a) => {
                err.error.errors[erro].forEach(msg => {
                  this.toastr.error(`${msg}`, 'Tabelionet', ToastOptions);
                });
              });
            }
          }
          catch {
            this.toastr.error(`${err.status} - ${err.message} - ${err.error}`, 'Tabelionet', ToastOptions);
          }
        },
        () => {
          //this.toastr.success("Endereço alterado com sucesso!", 'Tabelionet', ToastOptions);
        }).add(() => {
          this.spinner.hide();
        });
  }

  NovoEndereco(): void {
    const dialogRef = this.dialog.open(EnderecoNovoComponent, {
      data: {
        objEndereco: {
          enderecoUsuarioLogado: this.dadosUsuarioLogado,
          novoEndereco: true
        }
      }
    });

    dialogRef.afterClosed().subscribe(item => {
      if (item !== undefined) {
        let endereco = new NovoEndereco();
        endereco.setConteudo(item.conteudo);
        Object.assign(endereco, item);
        endereco.novoEndereco = true;
        this.listEnderecos.push(endereco);
      }
    });

    dialogRef.disableClose = true;
  }

  async adicionarOutorgante() {

    this.validarOutorganteSolicitante();

    if (!this.formOutorgante.get('idTipoDocumento').value || !this.formOutorgante.get('documento').value) {
      this.toastr.error('Por favor, inserir os dados do Outorgante', 'Tabelionet', ToastOptions);
      return;
    }

    console.log(this.formOutorgante.valid);

    if (this.formOutorgante.valid) {
      
      if (this.pessoaSolicitante) {
        let emailPertenceAoDocumento = await this.novaSolicitacaoService.emailPerteceAoDocumento(
          this.formOutorgante.get('documento').value,
          this.formOutorgante.get('emailOutorgante').value
        ).toPromise();

        if (!emailPertenceAoDocumento) {
          this.toastr.error('O e-mail informado é diferente do cadastro! Informe o seu e-mail de cadastro.', 'Tabelionet', ToastOptions);
          return;
        }
      } else {
        let emailPossuiConta = await this.novaSolicitacaoService.emailPossuiConta(
          this.formOutorgante.get('emailOutorgante').value
        ).toPromise();
        
        if(emailPossuiConta) {
          this.toastr.error('O e-mail informado já possui uma conta associada a outro documento!', 'Tabelionet', ToastOptions);
          return;
        }
      }

      let enderecoFlagAtivo = this.listEnderecos.find(i => i.flagAtivo === true);
      let enderecoSelecionado: NovoEndereco;
      let idSelecionado = this.enderecoItemService.bsSelecionarEndereco.value;

      if (idSelecionado === undefined) {
        enderecoSelecionado = this.listEnderecos.find(id => id.idEndereco == enderecoFlagAtivo.idEndereco);
      } else {
        enderecoSelecionado = this.listEnderecos.find(id => id.idEndereco == idSelecionado);
      }

      if (this.dadosUsuarioLogado && !enderecoSelecionado) {
        this.toastr.error('Por favor, inserir os dados do endereço do Outorgante', 'Tabelionet', ToastOptions);
        return;
      }

      this.verificarDocumentoJaCadastrado(this.formOutorgante.get('documento').value);
      if(this.documentoCadastrado){
        this.toastr.error('Documento já cadastrado para este outorgado', 'Tabelionet', ToastOptions);
        this.documentoCadastrado = false;
        return;
      }

      this.verificarEmailJaCadastrado(this.formOutorgante.get('emailOutorgante').value);
      if(this.emailCadastrado){
        this.toastr.error('E-mail já cadastrado para este outorgante', 'Tabelionet', ToastOptions);
        this.emailCadastrado = false;
        return;
      }

      if(!this.pai.validarOutorganteEmOutorgado(
        this.formOutorgante.get('documento').value,
        this.formOutorgante.get('emailOutorgante').value
      )) {
        this.toastr.error('O Documento e/ou E-mail já consta como outorgado', 'Tabelionet', ToastOptions);
        return;
      }

      this.addOutorgante(
        this.formOutorgante.get('idPessoa').value,
        this.formOutorgante.get('idTipoDocumento').value,
        this.formOutorgante.get('documento').value,
        this.formOutorgante.get('rgOutorgante').value,
        this.formOutorgante.get('nomeOutorgante').value,
        this.formOutorgante.get('emailOutorgante').value,
        this.formOutorgante.get('nascimentoOutorgante').value,
        this.formOutorgante.get('estadoCivilOutorgante').value,
        this.formOutorgante.get('nacionalidade').value,
        this.formOutorgante.get('profissaoOutorgante').value,
        this.formOutorgante.get('celular').value,
        this.formOutorgante.get('foneAlternativo').value,
        enderecoSelecionado
      );
      
      this.piv.resetarCampos(this.formOutorgante);
      this.pessoaExiste = null;
      this.resetarCamposEnderecos();
    }
  }

  resetarCamposEnderecos(): void {
    this.listEnderecos = [];
    this.enderecoItemService.clearEnderecoSelecionado();
  }

  assinarDigitalmente() {
    // const dialogRef = this.dialog.open(SolicitacaoAssinaturaComponent);

    // dialogRef.afterClosed().subscribe(item => {
    //   if(item !== undefined){
    //     let endereco = new Endereco();
    //     Object.assign(endereco, JSON.parse(item.conteudo));
    //     Object.assign(endereco, item);
    //     endereco.novoEndereco = true;
    //     this.listEnderecos.push(endereco);
    //   }
    // });

    // dialogRef.disableClose = true;
  }

  validarTipoDocumento(option: any): void {
    this.exibirRg = this.selectTipoDocumentos.find(p => p.valor == option.value).texto === 'CPF';
  }

  removerOutorgante(dadosOutorgante) {
    this.listaDataGridOutorgantes = this.dataGridOutorgantes.dataSource;
    this.outorgantesCadastro = this.outorgantesCadastro.filter((value, key) => {
      return value.nomePessoa !== dadosOutorgante.nomePessoa;
    });
    this.dadosOutorgantes.emit(this.outorgantesCadastro);
  }

  validarOutorganteSolicitante(): void {
    if (this.dadosUsuarioLogado) {
      this.camposPreenchimentosObrigatorioOutorganteSolicitante();
    } else {
      this.limparCamposOutorganteNaoSolicitante();
    }
    this.piv.atualizarValidacoesCampos(this.formOutorgante);
  }

  camposPreenchimentosObrigatorioOutorganteSolicitante(): void {
    this.formOutorgante.get('idTipoDocumento').setValidators(Validators.required);
    this.formOutorgante.get('documento').setValidators(Validators.required);
    this.formOutorgante.get('nomeOutorgante').setValidators(Validators.required);
    this.formOutorgante.get('emailOutorgante').setValidators(Validators.compose([Validators.required, Validators.email]));
    this.formOutorgante.get('rgOutorgante').setValidators(Validators.required);
    this.formOutorgante.get('nascimentoOutorgante').setValidators(Validators.required);
    this.formOutorgante.get('estadoCivilOutorgante').setValidators(Validators.required);
    this.formOutorgante.get('nacionalidade').setValidators(Validators.required);
    this.formOutorgante.get('profissaoOutorgante').setValidators(Validators.required);
    this.formOutorgante.get('celular').setValidators(Validators.required);
    this.formOutorgante.get('foneAlternativo').setValue('');
    this.formOutorgante.get('foneAlternativo').clearValidators();
  }

  limparCamposOutorganteNaoSolicitante(): void {
    this.formOutorgante.get('idTipoDocumento').setValidators(Validators.required);
    this.formOutorgante.get('documento').setValidators(Validators.required);
    this.formOutorgante.get('nomeOutorgante').setValidators(Validators.required);
    if (this.pessoaExiste) {
      this.formOutorgante.get('emailOutorgante').setValue('');
      this.formOutorgante.get('emailOutorgante').clearValidators();
    }
    else {
      this.formOutorgante.get('emailOutorgante').setValidators(Validators.compose([Validators.required, Validators.email]));
    }
    this.formOutorgante.get('rgOutorgante').setValue('');
    this.formOutorgante.get('rgOutorgante').clearValidators();
    this.formOutorgante.get('nascimentoOutorgante').setValue('');
    this.formOutorgante.get('nascimentoOutorgante').clearValidators();
    this.formOutorgante.get('estadoCivilOutorgante').setValue('');
    this.formOutorgante.get('estadoCivilOutorgante').clearValidators();
    this.formOutorgante.get('nacionalidade').setValue('');
    this.formOutorgante.get('nacionalidade').clearValidators();
    this.formOutorgante.get('profissaoOutorgante').setValue('');
    this.formOutorgante.get('profissaoOutorgante').clearValidators();
    this.formOutorgante.get('celular').setValue('');
    this.formOutorgante.get('celular').clearValidators();
  }

  verificarDocumentoJaCadastrado(documento: string): void {
    this.dataGridOutorgantes.dataSource.forEach(element => {
      if (parseInt(element.documento) === parseInt(documento)) {
        this.documentoCadastrado = true;
      }
    });
  }

  verificarEmailJaCadastrado(email: string): void {
    this.dataGridOutorgantes.dataSource.forEach(element => {
      if (element.email && element.email == email) {
        this.emailCadastrado = true;
      }
    });
  }

  //metodos auxiliares
  addOutorgante(idPessoa: number,
    idTipoDocumento: number,
    documento: any,
    rg: any,
    nome: string,
    email: string,
    nascimento: any,
    estadoCivil: any,
    nacionalidade: string,
    profissao: string,
    celular: string,
    foneAlternativo: string,
    endereco: any,
    idSolicitacaoParte: number = undefined){
  
      this.outorgantesCadastro.push({
        idPessoa: idPessoa,
        idTipoDocumento: idTipoDocumento,
        documento: documento,
        rg: rg,
        nomePessoa: nome,
        email: email,
        nascimento: nascimento,
        estadoCivil: estadoCivil,
        nacionalidade: nacionalidade,
        profissao: profissao,
        celular: celular,
        foneAlternativo: foneAlternativo,
        endereco: endereco
      });
  
  
  
      this.listaDataGridOutorgantes.push(
        new OutorgantesDataGrid(
          idSolicitacaoParte,
          nome,
          documento,
          email
        )
      );
  
      this.dataGridOutorgantes.updateGridRows();
      this.datagridOutorgante.emit(this.listaDataGridOutorgantes);
      this.dadosOutorgantes.emit(this.outorgantesCadastro);
    }

    validarDataPadraoBrasileiro(formulario: FormGroup, campo: string){
      let isValidDate = this.piv.validarDataPadraoBrasileiro(formulario,campo);
      if(!isValidDate){
        this.formOutorgante.get(campo).setValue("");
        this.toastr.error('Por favor, insere uma data válida!', 'Tabelionet', ToastOptions);
        return;
      }
      let isValidDateMinimo = this.piv.validarDataMinimo(formulario,campo);
      if(!isValidDateMinimo){
        this.formOutorgante.get(campo).setValue("");
        this.toastr.error('Por favor, insere uma data válida acima de 1900!', 'Tabelionet', ToastOptions);
        return;
      }  
      let isValidDateMaximo = this.piv.validarDataMaxima(formulario,campo);
      if(!isValidDateMaximo){
        this.formOutorgante.get(campo).setValue("");
        this.toastr.error('Por favor, insere uma data válida menor que dia de hoje!', 'Tabelionet', ToastOptions);
        return;
      } 
    }
}
