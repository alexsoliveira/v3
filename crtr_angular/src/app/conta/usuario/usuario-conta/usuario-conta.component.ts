import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { utilsBr } from 'js-brasil';
import { NgBrazilValidators } from 'ng-brazil';
import { BehaviorSubject } from 'rxjs';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { ToastrService } from 'ngx-toastr';
import { ContaService } from '../../services/conta.service';
import { MatDialog } from '@angular/material/dialog';
import { EnderecoNovoComponent } from '../../../shared/endereco-novo/endereco-novo.component';
import { ToastOptions } from '../../../utils/toastr.config';
import { ErrorMessages } from '../../../validators/custom.validator';
import { ValidacoesTelefone } from '../../../validators/telefone.validator';
import { Contato } from './../../models/contato.model';
import { UsuarioDadosPessoais } from './../../models/usuario-dados-pessoais.model';
import { EnderecoItemService } from 'src/app/shared/endereco-item/services/endereco-item.services';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';
import { DateUtils } from 'src/app/utils/date.utils';
import { NovoEndereco } from 'src/app/shared/models/NovoEndereco.model';
import { EnderecoConteudo } from 'src/app/shared/models/EnderecoConteudo.model';
import { NgxSpinnerService } from 'ngx-spinner';
import { PatternInteracaoValidator } from 'src/app/validators/pattern-interacao.validator';

@Component({
  selector: 'app-usuario-conta',
  templateUrl: './usuario-conta.component.html',
  styleUrls: ['./usuario-conta.component.scss']
})
export class UsuarioContaComponent implements OnInit {

  bsRemoverEndereco = new BehaviorSubject<any>(undefined);
  bsAlterarEndereco = new BehaviorSubject<any>(undefined);
  bsSelecionarEndereco = new BehaviorSubject<any>(undefined);
  bsPagamento = new BehaviorSubject<any>(undefined);

  localStorageUtils = new LocalStorageUtils();
  msgErros = ErrorMessages;
  MASKS = utilsBr.MASKS;

  selectTipoDocumentos: any[] = [];
  selectEstadoCivil: any[] = [];
  idContato: number;
  listEnderecos = new Array();
  formGroup: FormGroup;
  exibirRg: boolean = true;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private contaService: ContaService,
    public dialog: MatDialog,
    private enderecoItemService: EnderecoItemService,
    private htmlSelect: HtmlSelect,
    private dateUtils: DateUtils,
    private spinner: NgxSpinnerService,
    public piv: PatternInteracaoValidator,
  ) { }

  ngOnInit() {

    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumento();
    this.selectEstadoCivil = this.htmlSelect.getEstadoCivil();

    this.formGroup = this.fb.group({
      tipoDocumento: ['', Validators.compose([Validators.required])],
      numero: ['', Validators.compose([Validators.required, NgBrazilValidators.cpf])],
      rg: ['', Validators.compose([Validators.required, NgBrazilValidators.rg])],
      nomeCompleto: ['', Validators.required],
      nomeSocial: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      celular: ['', Validators.required],
      nascimento: ['', Validators.required],
      telefoneAlternativo: [''],
      nacionalidade: ['', Validators.required],
      estadoCivil: ['', Validators.required],
      profissao: ['', Validators.required],
    });

    this.formGroup.get('tipoDocumento').disable();
    this.formGroup.get('numero').disable();
    this.formGroup.get('nomeCompleto').disable();
    this.formGroup.get('email').disable();
    this.formGroup.get('rg').disable();

    this.buscarDadosUsuarioLogado();
    this.enderecoItemService.setDataAndRun(
      this.bsAlterarEndereco,
      this.bsRemoverEndereco,
      this.bsSelecionarEndereco,
      this.listEnderecos,
      true);
  }

  SalvarDadosPessoais(): void {
    
    let usuarioDadosPessoais = new UsuarioDadosPessoais();
    usuarioDadosPessoais.contato = new Contato();
    usuarioDadosPessoais.contato.idContato = this.idContato;
    usuarioDadosPessoais.contato.celular = this.formGroup.get('celular').value;
    usuarioDadosPessoais.contato.fixo = this.formGroup.get('telefoneAlternativo').value;
    usuarioDadosPessoais.estadoCivil = this.formGroup.get('estadoCivil').value;
    usuarioDadosPessoais.nomeSocial = this.formGroup.get('nomeSocial').value;
    let nascimento = this.formGroup.get('nascimento').value;
    if (nascimento) {
      let dataConvertida = this.dateUtils.convertDateToBackEnd(nascimento);
      usuarioDadosPessoais.nascimento = dataConvertida;
    }
    usuarioDadosPessoais.nacionalidade = this.formGroup.get('nacionalidade').value;
    usuarioDadosPessoais.profissao = this.formGroup.get('profissao').value;
    let usuario = this.localStorageUtils.lerUsuario();
    //usuarioDadosPessoais.idPessoa = usuario.idPessoa;
    usuarioDadosPessoais.idUsuario = usuario.idUsuario;
    this.contaService.SalvarDadosPessoais(usuarioDadosPessoais)
      .subscribe(() => {
        this.toastr.success("Dados pessoais atualizados com sucesso!", 'Tabelionet', ToastOptions);
      },    
      (err) => {
        try {
          err.error.errors.Mensagens.forEach(erro => {
            this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
          });
        }
        catch {
          this.toastr.error(`${err.status} - ${err.message}`, 'Tabelionet', ToastOptions);
        }        
      });      
  }

  buscarDadosUsuarioLogado(): void {
    this.spinner.show();
    let _u = this.localStorageUtils.lerUsuario();
    if (_u != null) {
      this.contaService.BuscarDadosUsuario(_u.idUsuario)
        .subscribe(usuario => {

          if (usuario) {
            let _tipoDocumento = '';
            let _celular = '';
            let _telAlternativo = '';
            let _nascimento = '';

            if (usuario.idTipoDocumento && usuario.idTipoDocumento > 0) {
              _tipoDocumento = this.selectTipoDocumentos.find(t => t.valor == usuario.idTipoDocumento).texto;
              this.exibirRg = _tipoDocumento === 'CPF';
            }

            if (usuario.contatos
              && usuario.contatos.length
              && usuario.contatos.length > 0) {
              _celular = usuario.contatos[0].celular;
              _telAlternativo = usuario.contatos[0].fixo;
              this.idContato = usuario.contatos[0].idContato;
            }

            if (usuario.nascimento) {
              try{
                _nascimento = this.dateUtils.getValidDate(usuario.nascimento);
              } catch { }
            }


            this.formGroup.patchValue({
              nomeCompleto: usuario.nome,
              tipoDocumento: _tipoDocumento,
              numero: usuario.documento,
              email: usuario.email,
              celular: _celular,
              telefoneAlternativo: _telAlternativo,
              estadoCivil: (usuario.estadoCivil !== null 
                            || usuario.estadoCivil !== undefined) 
                           && usuario.estadoCivil >= 0 ? usuario.estadoCivil : -1,
              nacionalidade: usuario.nacionalidade,
              nascimento: _nascimento,
              profissao: usuario.profissao,
              rg: usuario.rg,
              nomeSocial: usuario.nomeSocial
            });

            if (usuario.enderecos
              && usuario.enderecos.length
              && usuario.enderecos.length > 0) {
              usuario.enderecos.forEach(endereco => {
                this.listEnderecos.push(endereco);
              })
            }
          }

          this.spinner.hide();
        },
        (err) => {
          this.spinner.hide();
        });
    }
    else{
      this.spinner.hide();
    }
  }

  NovoEndereco(): void {
    const dialogRef = this.dialog.open(EnderecoNovoComponent, {
      data: {
        objEndereco: {
          enderecoUsuarioLogado: true,
          novoEndereco: true
        }
      }
    });

    dialogRef.afterClosed().subscribe(item => {
      if (item !== undefined) {
        let endereco = new NovoEndereco();
        endereco.conteudo = new EnderecoConteudo();
        endereco.conteudo.bairro = item.conteudo.bairro;
        endereco.conteudo.localidade = item.conteudo.localidade;
        endereco.conteudo.logradouro = item.conteudo.logradouro;
        endereco.conteudo.cep = item.conteudo.cep;
        endereco.conteudo.numero = item.conteudo.numero;
        endereco.conteudo.complemento = item.conteudo.complemento;
        endereco.conteudo.uf = item.conteudo.uf;
        endereco.flagAtivo = item.flagAtivo;
        endereco.idEndereco = item.idEndereco;
        endereco.idPessoa = item.idPessoa;
        endereco.idUsuario = item.idUsuario;
        //endereco.novoEndereco = true;
        this.listEnderecos.push(endereco);
      }
    });

    dialogRef.disableClose = true;
  }

  validarDataPadraoBrasileiro(formulario: FormGroup, campo: string){
    let isValidDate = this.piv.validarDataPadraoBrasileiro(formulario,campo);
    if(!isValidDate){
      this.formGroup.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida!', 'Tabelionet', ToastOptions);
      return;
    }
    let isValidDateMinimo = this.piv.validarDataMinimo(formulario,campo);
    if(!isValidDateMinimo){
      this.formGroup.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida acima de 1900!', 'Tabelionet', ToastOptions);
      return;
    }  
    let isValidDateMaximo = this.piv.validarDataMaxima(formulario,campo);
    if(!isValidDateMaximo){
      this.formGroup.get(campo).setValue("");
      this.toastr.error('Por favor, insere uma data válida menor que dia de hoje!', 'Tabelionet', ToastOptions);
      return;
    } 
  }

}
