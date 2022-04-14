// * Dependências
import { Component, OnInit, ViewChild, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
// * Components
import { DatagridOutorgadoComponent } from '../datagrid-outorgado/datagrid-outorgado.component';
// * Models
import { OutorgadosDataGrid } from '../../models/outorgadosDataGrid';
import { OutorgadoCadastro } from './models/outorgado-cadastro.model';
import { UsuarioSolicitante } from 'src/app/solicitacao/nova-solicitacao/cadastro-procuracoes/components/cadastro-outorgado/models/usuario-solicitante.model';
// * Services
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';
import { PatternInteracaoValidator } from 'src/app/validators/pattern-interacao.validator';
import { SolicitacaoService } from 'src/app/solicitacao/services/solicitacao.service';
import { PessoasService } from '../../../../../shared/services/Pessoas.service';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { CadastroOutorgadoService } from '../../services/cadastro-outorgado.service';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { Inject } from '@angular/core';
import { SolicitacaoCadastroComponent } from '../../solicitacao-cadastro.component';
import { NovaSolicitacaoService } from '../../services/nova-solicitacao.service';


@Component({
  selector: 'app-cadastro-outorgado',
  templateUrl: './cadastro-outorgado.component.html',
  styleUrls: ['./cadastro-outorgado.component.scss']
})
export class CadastroOutorgadoComponent implements OnInit {

  //Estar sendo usado no Component HTML
  outorgadoFormControl = new FormControl('', [
    Validators.required
  ]);
  formOutorgado: FormGroup;
  outorgadoCadastro: OutorgadoCadastro[] = [];
  listaDataGridOutorgados: OutorgadosDataGrid[] = [];
  @Input() inputDatagridOutorgado: OutorgadosDataGrid[] = [];
  selectTipoDocumentos: any[] = [];
  exibirOutrosCampos: boolean = false;
  exibirRg: boolean = false;
  documentoCadastrado: boolean = false;
  emailCadastrado: boolean = false;
  pessoaExiste: number = null;
  representacaoPartes: string = 'todas';
  excecaoAtualizacaoCampos: string[] = [];
  usuarioSolicitante: UsuarioSolicitante = null;

  @Output() dadosOutorgados = new EventEmitter();
  @Output() representacaoPartesOutorgados = new EventEmitter();
  @Output() datagridOutorgado = new EventEmitter();
  @ViewChild(DatagridOutorgadoComponent) dataGridOutorgados;


  constructor(
    private fb: FormBuilder,
    private htmlSelect: HtmlSelect,
    public piv: PatternInteracaoValidator,
    public solicitacaoService: SolicitacaoService,
    public pessoasService: PessoasService,
    private outorgadoService: CadastroOutorgadoService,
    private novaSolicitacaoService: NovaSolicitacaoService,
    private toastr: ToastrService,
    @Inject(SolicitacaoCadastroComponent) private pai: SolicitacaoCadastroComponent
  ) { }

  ngOnInit() {
    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumento();

    this.formOutorgado = this.fb.group({
      idTipoDocumento: ['', Validators.required],
      documentoOutorgado: ['', Validators.required],      
      nomeOutorgado: ['', Validators.required],
      emailOutorgado: ['', Validators.compose([Validators.required,Validators.email])],
      representacaoPartes: ['todas']
    });

    this.excecaoAtualizacaoCampos.push('representacaoPartes');

    this.setRepresentacaoTodasPartes();

    let intervalOutorgadosVindoDoPai = setInterval(() => {
      if (this.inputDatagridOutorgado.length > 0){
        this.dataGridOutorgados.dataSource = this.inputDatagridOutorgado;

        this.dataGridOutorgados.updateGridRows();
      }

      clearInterval(intervalOutorgadosVindoDoPai);
    }, 1000)
  }

  async adicionarOutorgado() {
    this.atualizarValidacoesCamposOutorgados();

    if (this.formOutorgado.valid) {


      let emailPossuiConta = await this.novaSolicitacaoService.emailPossuiConta(
        this.formOutorgado.get('emailOutorgado').value
      ).toPromise();

      if(emailPossuiConta) {
        this.toastr.error('O e-mail informado já possui uma conta associada a outro documento!', 'Tabelionet', ToastOptions);
        return;
      }

      this.verificarDocumentoJaCadastrado(this.formOutorgado.get('documentoOutorgado').value);
      if(this.documentoCadastrado){
        this.toastr.error('Documento já cadastrado para este outorgado', 'Tabelionet', ToastOptions);
        this.documentoCadastrado = false;
        return;
      }

      this.verificarEmailJaCadastrado(this.formOutorgado.get('emailOutorgado').value);
      if(this.emailCadastrado){
        this.toastr.error('E-mail já cadastrado para este outorgado', 'Tabelionet', ToastOptions);
        this.emailCadastrado = false;
        return;
      }

      if(!this.pai.validarOutorgadoEmOutorgante(
        this.formOutorgado.get('documentoOutorgado').value,
        this.formOutorgado.get('emailOutorgado').value
      )) {
        this.toastr.error('O Documento e/ou E-mail já consta como outorgante', 'Tabelionet', ToastOptions);
        return;
      }

      if(parseInt(this.formOutorgado.get('documentoOutorgado').value) == parseInt(this.usuarioSolicitante.documento)){
        this.toastr.error(`O solicitante, não pode se cadastrar como outorgado`, 'Tabelionet', ToastOptions);
        return;
      }

      this.addOutorgado(
        this.pessoaExiste,
        this.formOutorgado.get('idTipoDocumento').value,
        this.formOutorgado.get('documentoOutorgado').value,        
        this.formOutorgado.get('nomeOutorgado').value,
        this.formOutorgado.get('emailOutorgado').value
      )
      
      this.piv.resetarCampos(this.formOutorgado, this.excecaoAtualizacaoCampos);
      
    }
  }

  

  removerOutorgado(dadosOutorgado) {
    this.listaDataGridOutorgados = this.dataGridOutorgados.dataSource;
    this.outorgadoCadastro = this.outorgadoCadastro.filter((value, key) => {
      return value.nomeOutorgado !== dadosOutorgado.nomePessoa;
    });

    this.dadosOutorgados.emit(this.outorgadoCadastro);
  }

  atualizarValidacoesCamposOutorgados(): void {
    this.formOutorgado.get('idTipoDocumento').setValidators(Validators.required);
    this.formOutorgado.get('documentoOutorgado').setValidators(Validators.required);
    this.formOutorgado.get('nomeOutorgado').setValidators(Validators.required);
    if(!this.exibirOutrosCampos) {
      this.formOutorgado.get('emailOutorgado').setValue('');
      this.formOutorgado.get('emailOutorgado').clearValidators();
    }
    else {
      this.formOutorgado.get('emailOutorgado').setValidators(Validators.compose([Validators.required,Validators.email]));
    }

    this.piv.atualizarValidacoesCampos(this.formOutorgado);
  }

  validarTipoDocumento(option: any) {
    this.exibirRg = this.selectTipoDocumentos.find(p => p.valor == option.value).texto === 'CPF';
  }

  checkIfPessoaExiste(){
    let usuario = new LocalStorageUtils().lerUsuario();
    this.verificarUsuarioSolicitante(usuario.idUsuario);
    console.log(this.usuarioSolicitante.documento);

    let idTipoDocumento = this.formOutorgado.get('idTipoDocumento').value;
    let doc = this.formOutorgado.get('documentoOutorgado').value;

    console.log(this.formOutorgado.get('documentoOutorgado').value);

    if(parseInt(doc) == parseInt(this.usuarioSolicitante.documento)){
      this.toastr.error(`O solicitante, não pode se cadastrar como outorgado`, 'Tabelionet', ToastOptions);
      return;
    }

    if (idTipoDocumento && doc) {
      this.pessoasService.PessoaExiste(idTipoDocumento, doc)
        .subscribe((ret) => {
          console.log(ret);
          this.pessoaExiste = ret;
          this.exibirOutrosCampos = false;
          this.exibirRg = true;
          return;
        }, (err) => {
          if (err.status === 404) {
            this.exibirOutrosCampos = true;

            if(idTipoDocumento == 2){
              this.exibirRg = true;
            }
            else{
              this.exibirRg = false;
            }
          }
        });
      }
  }

  setRepresentacaoTodasPartes() {
    this.formOutorgado.get('representacaoPartes').setValue('todas');
    this.representacaoPartesOutorgados.emit('todas');
  }

  setRepresentacaoQualquerPartes() {
    this.formOutorgado.get('representacaoPartes').setValue('qualquer');
    this.representacaoPartesOutorgados.emit('qualquer');
  }

  verificarUsuarioSolicitante(idUsuario: number): void{
    this.outorgadoService.BuscarDadosUsuario(idUsuario)
    .subscribe((ret: UsuarioSolicitante) => {
      this.usuarioSolicitante = ret;
    });
  }

  verificarDocumentoJaCadastrado(documento: string): void {
    this.dataGridOutorgados.dataSource.forEach(element => {
      if (parseInt(element.documento) === parseInt(documento)) {
        this.documentoCadastrado = true;
      }
    });
  }

  verificarEmailJaCadastrado(email: string): void {
    this.dataGridOutorgados.dataSource.forEach(element => {
      if (element.email && element.email == email) {
        this.emailCadastrado = true;
      }
    });
  }




  //metodos auxiliares
  addOutorgado(idPessoa:number, idTipoDocumento: number, documento: any, nome: string, email: string, idSolicitacaoParte: number = undefined) {
    this.outorgadoCadastro.push({
      idPessoa : idPessoa,
      idTipoDocumento: idTipoDocumento,
      documentoOutorgado: documento,      
      nomeOutorgado: nome,
      emailOutorgado: email,
    });

    this.listaDataGridOutorgados.push(
      new OutorgadosDataGrid(
        idSolicitacaoParte,
        nome,
        documento,
        email
      )
    );

    this.dataGridOutorgados.updateGridRows();
    this.datagridOutorgado.emit(this.listaDataGridOutorgados);
    this.dadosOutorgados.emit(this.outorgadoCadastro);
  }

}
