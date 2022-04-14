import { rg } from 'ng-brazil/rg/validator';
import { PatternInteracaoValidator } from 'src/app/validators/pattern-interacao.validator';
import { Component, EventEmitter, Inject, Input, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';
import { ContracaoMatrimonioCadastro } from './models/contracao-matrimonio.model';
import { TestemunhaCadastro } from './cadastro-testemunha/models/testemunha-cadastro.model';
import { CadastroTestemunhaComponent } from './cadastro-testemunha/cadastro-testemunha.component';
import { ActivatedRoute } from '@angular/router';
import { MatrimonioService } from './services/matrimonio.services';
import { TestemunhasDataGrid } from './models/testemunhasDatagrid';
import { TestemunhaDto } from './models/dto/testemunha-dto.model';
import { SolicitacaoCadastroComponent } from 'src/app/solicitacao/nova-solicitacao/cadastro-procuracoes/solicitacao-cadastro.component';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-matrimonio',
  templateUrl: './matrimonio.component.html',
  styleUrls: ['./matrimonio.component.scss']
})

export class MatrimonioComponent {

  listaDataGridTestemunhas: TestemunhasDataGrid[] = [];
  selectRegimeCasamento: any[] = [];
  selectTipoDocumentos: any[] = [];
  selectSituacao: any[] = [];
  regimeCasamento = new FormControl();
  tipoDocumentoRequerente = new FormControl();
  tipoDocumentoMaeRequerente = new FormControl();
  situacaoMaeRequerente = new FormControl();
  tipoDocumentoPaiRequerente = new FormControl();
  situacaoPaiRequerente = new FormControl();
  tipoDocumentoNoivos = new FormControl();
  tipoDocumentoMaeNoivos = new FormControl();
  situacaoMaeNoivos = new FormControl();
  tipoDocumentoPaiNoivos = new FormControl();
  situacaoPaiNoivos = new FormControl();

  formContracaoMatrimonio: FormGroup;
  contracaoMatrimonioFormControl = new FormControl('', [
    Validators.required
  ]);

  contracaoMatrimonioCadastro: ContracaoMatrimonioCadastro;
  @Output() dadosProduto = new EventEmitter();


  valueProclamas: any;
  arquivoProclamasSelecionado: string = '';
  placeholderProclamas: string = 'Inserir documento de proclamas >>>';
  dadosTestemunhas: TestemunhaCadastro[] = [];
  idSolicitacao: number = null;

  @ViewChild(CadastroTestemunhaComponent) cadastroTestemunha;

  solicitacaoEPara: string = 'paramim';
  @Output() solicitacaoEParaMatrimonio = new EventEmitter();
  @Input() dadosOutorgantes: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private htmlSelect: HtmlSelect,
    public piv: PatternInteracaoValidator,
    private matrimonioService: MatrimonioService,
    @Inject(SolicitacaoCadastroComponent) private pai: SolicitacaoCadastroComponent,
    private toastr: ToastrService
  ) { }

  async ngOnInit() {
    this.selectRegimeCasamento = this.htmlSelect.getRegimeCasamento();
    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumentoProduto();
    this.selectSituacao = this.htmlSelect.getSituacao();

    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;

    if (this.idSolicitacao) {
      this.criarFormularioNovaSolicitacao();
      let dados = await this.matrimonioService.BuscarDadosMatrimonio(this.idSolicitacao).toPromise();
      this.criarFormularioComDadosSolicitacaoExistente(dados);
    }
    else {
      this.criarFormularioNovaSolicitacao();
    }
  }

  criarFormularioNovaSolicitacao() {
    this.formContracaoMatrimonio = this.fb.group({
      uploadArquivo: ['', Validators.required],
      regimeCasamento: ['', Validators.required],

      nomeRequerente: ['', Validators.required],
      solicitacaoEPara: ['paraoutra'],
      tipoDocumentoRequerente: ['', Validators.required],
      documentoRequerente: ['', Validators.required],
      dataNascimentoRequerente: ['', Validators.required],

      nomeMaeRequerente: ['', Validators.required],
      tipoDocumentoMaeRequerente: ['', Validators.required],
      documentoMaeRequerente: ['', Validators.required],
      dataNascimentoMaeRequerente: ['', Validators.required],
      situacaoMaeRequerente: ['', Validators.required],

      nomePaiRequerente: ['', Validators.required],
      tipoDocumentoPaiRequerente: ['', Validators.required],
      documentoPaiRequerente: ['', Validators.required],
      dataNascimentoPaiRequerente: ['', Validators.required],
      situacaoPaiRequerente: ['', Validators.required],

      nomeNoivos: ['', Validators.required],
      tipoDocumentoNoivos: ['', Validators.required],
      documentoNoivos: ['', Validators.required],
      dataNascimentoNoivos: ['', Validators.required],

      nomeMaeNoivos: ['', Validators.required],
      tipoDocumentoMaeNoivos: ['', Validators.required],
      documentoMaeNoivos: ['', Validators.required],
      dataNascimentoMaeNoivos: ['', Validators.required],
      situacaoMaeNoivos: ['', Validators.required],

      nomePaiNoivos: ['', Validators.required],
      tipoDocumentoPaiNoivos: ['', Validators.required],
      documentoPaiNoivos: ['', Validators.required],
      dataNascimentoPaiNoivos: ['', Validators.required],
      situacaoPaiNoivos: ['', Validators.required],
    });
  }

  selecionarArquivo(): void {
    const reader = new FileReader();
    reader.onload = this.manipularReader.bind(this);
    reader.readAsBinaryString(this.formContracaoMatrimonio.get('uploadArquivo').value);
  }

  manipularReader(readerEvt: any): void {
    const binaryString = readerEvt.target.result;
    this.arquivoProclamasSelecionado = btoa(binaryString);
    this.adicionarContracaoMatrimonio();
  }

  criarFormularioComDadosSolicitacaoExistente(dados: any) {
    if (dados) {

      let nameFileProclamas = dados.DadosContracaoMatrimonio
        && dados.DadosContracaoMatrimonio.NomeDocumentoProclamas ?
        { name: dados.DadosContracaoMatrimonio.NomeDocumentoProclamas } : '';

      this.formContracaoMatrimonio = this.fb.group({
        uploadArquivo: [nameFileProclamas, Validators.required],
        regimeCasamento: [this.getValorCombo(dados.DadosContracaoMatrimonio.RegimeCasamento),
        Validators.required],

        nomeRequerente: [dados.DadosRequerente.Requerente.Nome, Validators.required],
        tipoDocumentoRequerente: [this.getValorCombo(dados.DadosRequerente.Requerente.IdTipoDocumento),
        Validators.required],
        documentoRequerente: [dados.DadosRequerente.Requerente.Documento, Validators.required],
        dataNascimentoRequerente: [dados.DadosRequerente.Requerente.DataNascimento, Validators.required],

        nomeMaeRequerente: [dados.DadosRequerente.MaeRequerente.Nome, Validators.required],
        tipoDocumentoMaeRequerente: [this.getValorCombo(dados.DadosRequerente.MaeRequerente.IdTipoDocumento),
        Validators.required],
        documentoMaeRequerente: [dados.DadosRequerente.MaeRequerente.Documento, Validators.required],
        dataNascimentoMaeRequerente: [dados.DadosRequerente.MaeRequerente.DataNascimento, Validators.required],
        situacaoMaeRequerente: [this.getValorCombo(dados.DadosRequerente.MaeRequerente.Situacao),
        Validators.required],

        nomePaiRequerente: [dados.DadosRequerente.PaiRequerente.Nome, Validators.required],
        tipoDocumentoPaiRequerente: [this.getValorCombo(dados.DadosRequerente.PaiRequerente.IdTipoDocumento),
        Validators.required],
        documentoPaiRequerente: [dados.DadosRequerente.PaiRequerente.Documento, Validators.required],
        dataNascimentoPaiRequerente: [dados.DadosRequerente.PaiRequerente.DataNascimento, Validators.required],
        situacaoPaiRequerente: [this.getValorCombo(dados.DadosRequerente.PaiRequerente.Situacao),
        Validators.required],

        nomeNoivos: [dados.DadosNoivos.Noivos.Nome, Validators.required],
        tipoDocumentoNoivos: [this.getValorCombo(dados.DadosNoivos.Noivos.IdTipoDocumento),
        Validators.required],
        documentoNoivos: [dados.DadosNoivos.Noivos.Documento, Validators.required],
        dataNascimentoNoivos: [dados.DadosNoivos.Noivos.DataNascimento, Validators.required],

        nomeMaeNoivos: [dados.DadosNoivos.MaeNoivos.Nome, Validators.required],
        tipoDocumentoMaeNoivos: [this.getValorCombo(dados.DadosNoivos.MaeNoivos.IdTipoDocumento),
        Validators.required],
        documentoMaeNoivos: [dados.DadosNoivos.MaeNoivos.Documento, Validators.required],
        dataNascimentoMaeNoivos: [dados.DadosNoivos.MaeNoivos.DataNascimento, Validators.required],
        situacaoMaeNoivos: [this.getValorCombo(dados.DadosNoivos.MaeNoivos.Situacao),
        Validators.required],

        nomePaiNoivos: [dados.DadosNoivos.PaiNoivos.Nome, Validators.required],
        tipoDocumentoPaiNoivos: [this.getValorCombo(dados.DadosNoivos.PaiNoivos.IdTipoDocumento),
        Validators.required],
        documentoPaiNoivos: [dados.DadosNoivos.PaiNoivos.Documento, Validators.required],
        dataNascimentoPaiNoivos: [dados.DadosNoivos.PaiNoivos.DataNascimento, Validators.required],
        situacaoPaiNoivos: [this.getValorCombo(dados.DadosNoivos.PaiNoivos.Situacao),
        Validators.required],
      });

      this.formContracaoMatrimonio.markAsTouched();

      if (dados.DadosContracaoMatrimonio.DocumentoProclamas) {
        this.arquivoProclamasSelecionado = dados.DadosContracaoMatrimonio.DocumentoProclamas.replace('data:application/pdf;base64,', '');
        this.placeholderProclamas = dados.DadosContracaoMatrimonio.DocumentoProclamas.NomeArquivoProclamas;
      }

      this.preencherTestemunhaJaExistente(dados.Testemunhas)

      this.adicionarContracaoMatrimonio();
    } else {
      this.criarFormularioNovaSolicitacao();
    }
  }

  getValorCombo(json) {
    try {
      return JSON.parse(json).valor;
    } catch {
      return '';
    }
  }

  adicionarContracaoMatrimonio() {
    let fileProclamas = this.formContracaoMatrimonio.controls['uploadArquivo'].value;
    this.contracaoMatrimonioCadastro = {
      UploadArquivo: 'data:application/pdf;base64,' + this.arquivoProclamasSelecionado,
      NomeArquivoProclamas: fileProclamas ? fileProclamas.name : '',
      RegimeCasamento: this.htmlSelect.getTextoRegimeSelecionado(this.formContracaoMatrimonio.get('regimeCasamento').value),

      NomeRequerente: this.formContracaoMatrimonio.get('nomeRequerente').value,
      TipoDocumentoRequerente: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoRequerente').value),
      DocumentoRequerente: this.formContracaoMatrimonio.get('documentoRequerente').value,
      DataNascimentoRequerente: this.formContracaoMatrimonio.get('dataNascimentoRequerente').value,

      NomeMaeRequerente: this.formContracaoMatrimonio.get('nomeMaeRequerente').value,
      TipoDocumentoMaeRequerente: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoMaeRequerente').value),
      DocumentoMaeRequerente: this.formContracaoMatrimonio.get('documentoMaeRequerente').value,
      DataNascimentoMaeRequerente: this.formContracaoMatrimonio.get('dataNascimentoMaeRequerente').value,
      SituacaoMaeRequerente: this.htmlSelect.getTextoSituacao(this.formContracaoMatrimonio.get('situacaoMaeRequerente').value),

      NomePaiRequerente: this.formContracaoMatrimonio.get('nomePaiRequerente').value,
      TipoDocumentoPaiRequerente: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoPaiRequerente').value),
      DocumentoPaiRequerente: this.formContracaoMatrimonio.get('documentoPaiRequerente').value,
      DataNascimentoPaiRequerente: this.formContracaoMatrimonio.get('dataNascimentoPaiRequerente').value,
      SituacaoPaiRequerente: this.htmlSelect.getTextoSituacao(this.formContracaoMatrimonio.get('situacaoPaiRequerente').value),

      NomeNoivos: this.formContracaoMatrimonio.get('nomeNoivos').value,
      TipoDocumentoNoivos: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoNoivos').value),
      DocumentoNoivos: this.formContracaoMatrimonio.get('documentoNoivos').value,
      DataNascimentoNoivos: this.formContracaoMatrimonio.get('dataNascimentoNoivos').value,

      NomeMaeNoivos: this.formContracaoMatrimonio.get('nomeMaeNoivos').value,
      TipoDocumentoMaeNoivos: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoMaeNoivos').value),
      DocumentoMaeNoivos: this.formContracaoMatrimonio.get('documentoMaeNoivos').value,
      DataNascimentoMaeNoivos: this.formContracaoMatrimonio.get('dataNascimentoMaeNoivos').value,
      SituacaoMaeNoivos: this.htmlSelect.getTextoSituacao(this.formContracaoMatrimonio.get('situacaoMaeNoivos').value),

      NomePaiNoivos: this.formContracaoMatrimonio.get('nomePaiNoivos').value,
      TipoDocumentoPaiNoivos: this.htmlSelect.getTextoTipoDocumentoProduto(this.formContracaoMatrimonio.get('tipoDocumentoPaiNoivos').value),
      DocumentoPaiNoivos: this.formContracaoMatrimonio.get('documentoPaiNoivos').value,
      DataNascimentoPaiNoivos: this.formContracaoMatrimonio.get('dataNascimentoPaiNoivos').value,
      SituacaoPaiNoivos: this.htmlSelect.getTextoSituacao(this.formContracaoMatrimonio.get('situacaoPaiNoivos').value),

      Testemunhas: this.cadastroTestemunha.testemunhaCadastro,

      formularioValido: this.formContracaoMatrimonio.valid,
      testemunhaRequerenteValido: this.isTestemunhaRequerenteValido(),
      testemunhaNoivoOuNoivaValido: this.isTestemunhaNoivoOuNoivaValido()
    };



    console.log(this.contracaoMatrimonioCadastro);
    this.dadosProduto.emit(this.contracaoMatrimonioCadastro);
  }

  isTestemunhaRequerenteValido(): boolean {
    if (this.cadastroTestemunha.testemunhaCadastro && this.cadastroTestemunha.testemunhaCadastro.length >= 0) {

      let existeParteRequerente = undefined;

      existeParteRequerente = this.cadastroTestemunha.testemunhaCadastro.find(obj => obj.Parte === 0);
      console.log(existeParteRequerente);

      if (existeParteRequerente === undefined) {
        return false;
      }

      return true;
    }
    
    return false;
  }

  isTestemunhaNoivoOuNoivaValido(): boolean {
    if (this.cadastroTestemunha.testemunhaCadastro && this.cadastroTestemunha.testemunhaCadastro.length >= 0) {

      let existeParteNoivoOuNoiva = undefined;

      existeParteNoivoOuNoiva = this.cadastroTestemunha.testemunhaCadastro.find(obj => obj.Parte === 1);
      console.log(existeParteNoivoOuNoiva);

      if (existeParteNoivoOuNoiva === undefined) {
        return false;
      }

      return true;
    }
    
    return false;
  }

  preencherTestemunhaJaExistente(testemunhas: any[]) {
    this.dadosTestemunhas = [];

    testemunhas.forEach((testemunha) => {
      console.log(testemunha);

      let tipoDocumento = {
        valor: 0,
        texto: "",
      };

      let parte = {
        valor: 0,
        texto: "",
      };

      if (testemunha.IdTipoDocumento)
        tipoDocumento = JSON.parse(testemunha.IdTipoDocumento);

      if (testemunha.Parte)
        parte = JSON.parse(testemunha.Parte);

      this.cadastroTestemunha.addTestemunha(
        testemunha.Nome,
        tipoDocumento.valor,
        testemunha.Documento,
        testemunha.Rg,
        parte.valor
      )
    });
  }

  setEstaSolicitacaoEParaMim() {
    this.preencherNomeRequerente("paramim");
    this.preencherDocumentoRequerente("paramim");
    this.preencherNascimentoRequerente("paramim");
    this.formContracaoMatrimonio.get('solicitacaoEPara').setValue('paramim');
    this.solicitacaoEParaMatrimonio.emit('paramim');
  }

  setEstaSolicitacaoEParaOutra() {
    this.formContracaoMatrimonio.get('nomeRequerente').setValue("");
    this.formContracaoMatrimonio.get('tipoDocumentoRequerente').setValue(null);
    this.formContracaoMatrimonio.get('documentoRequerente').setValue("");
    this.formContracaoMatrimonio.get('dataNascimentoRequerente').setValue("");
    this.formContracaoMatrimonio.get('solicitacaoEPara').setValue('paraoutra');

    this.formContracaoMatrimonio.get('nomeRequerente').markAsUntouched();
    this.formContracaoMatrimonio.get('tipoDocumentoRequerente').markAsUntouched();
    this.formContracaoMatrimonio.get('documentoRequerente').markAsUntouched();
    this.formContracaoMatrimonio.get('dataNascimentoRequerente').markAsUntouched();

    this.formContracaoMatrimonio.get('nomeRequerente').setValidators([Validators.required]);
    this.formContracaoMatrimonio.get('tipoDocumentoRequerente').setValidators([Validators.required]);
    this.formContracaoMatrimonio.get('documentoRequerente').setValidators([Validators.required]);
    this.formContracaoMatrimonio.get('dataNascimentoRequerente').setValidators([Validators.required]);

    this.solicitacaoEParaMatrimonio.emit('paraoutra');
  }

  preencherNomeRequerente(tipo: string) {
    let nomePessoa = "";
    if (tipo === "paramim" && this.pai.idPessoaSolicitante !== undefined) {
      nomePessoa = this.pai.validarSolicitacaoEParaMimNomeRequerente(this.pai.idPessoaSolicitante);
      this.formContracaoMatrimonio.get('nomeRequerente').setValue(nomePessoa);
      this.formContracaoMatrimonio.get('nomeRequerente').markAllAsTouched();
      this.formContracaoMatrimonio.get('nomeRequerente').setValidators([]);
      this.formContracaoMatrimonio.get('nomeRequerente').updateValueAndValidity();
    } else {
      this.formContracaoMatrimonio.get('nomeRequerente').setValue(nomePessoa);
    }

  }

  preencherDocumentoRequerente(tipo: string) {
    let documento = { documento: '', idTipoDocumento: '' };
    if (tipo === "paramim" && this.pai.idPessoaSolicitante !== undefined) {
      documento = this.pai.validarSolicitacaoEParaMimDocumentoRequerente(this.pai.idPessoaSolicitante);
      this.formContracaoMatrimonio.get('tipoDocumentoRequerente').setValue(documento.idTipoDocumento);
      this.formContracaoMatrimonio.get('documentoRequerente').setValue(documento.documento);
      this.formContracaoMatrimonio.get('tipoDocumentoRequerente').markAsTouched();
      this.formContracaoMatrimonio.get('documentoRequerente').markAsTouched();
      this.formContracaoMatrimonio.get('tipoDocumentoRequerente').setValidators([]);
      this.formContracaoMatrimonio.get('documentoRequerente').setValidators([]);
      this.formContracaoMatrimonio.get('tipoDocumentoRequerente').updateValueAndValidity();
      this.formContracaoMatrimonio.get('documentoRequerente').updateValueAndValidity();
    } else {
      this.formContracaoMatrimonio.get('tipoDocumentoRequerente').setValue(documento.idTipoDocumento);
      this.formContracaoMatrimonio.get('documentoRequerente').setValue(documento.documento);
    }
  }

  preencherNascimentoRequerente(tipo: string) {
    let nascimento = "";
    if (tipo === "paramim" && this.pai.idPessoaSolicitante !== undefined) {
      nascimento = this.pai.validarSolicitacaoEParaMimNascimentoRequerente(this.pai.idPessoaSolicitante);
      this.formContracaoMatrimonio.get('dataNascimentoRequerente').setValue(nascimento);
      this.formContracaoMatrimonio.get('dataNascimentoRequerente').markAsTouched();
      this.formContracaoMatrimonio.get('dataNascimentoRequerente').setValidators([]);
      this.formContracaoMatrimonio.get('dataNascimentoRequerente').updateValueAndValidity();
    } else {
      this.formContracaoMatrimonio.get('dataNascimentoRequerente').setValue(nascimento);
    }

  }

  validarDataPadraoBrasileiro(formulario: FormGroup, campo: string) {
    let isValidDate = this.piv.validarDataPadraoBrasileiro(formulario, campo);
    if (!isValidDate) {
      this.formContracaoMatrimonio.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida!', 'Tabelionet', ToastOptions);
      return;
    }
    let isValidDateMinimo = this.piv.validarDataMinimo(formulario, campo);
    if (!isValidDateMinimo) {
      this.formContracaoMatrimonio.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida acima de 1900!', 'Tabelionet', ToastOptions);
      return;
    }
    let isValidDateMaximo = this.piv.validarDataMaxima(formulario, campo);
    if (!isValidDateMaximo) {
      this.formContracaoMatrimonio.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida menor que dia de hoje!', 'Tabelionet', ToastOptions);
      return;
    }
  }

}
