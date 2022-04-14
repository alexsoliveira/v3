import { BehaviorSubject } from 'rxjs';
import { AfterViewInit, Component, OnInit, ViewChild, ElementRef, ViewContainerRef, ContentChild, Output, EventEmitter, ComponentRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CadastroOutorganteService } from './services/cadastro-outorgante.service';
import { NovaSolicitacaoService } from './services/nova-solicitacao.service';
import { CadastroOutorgadoService } from './services/cadastro-outorgado.service';
import { Produtos } from 'src/app/vitrine/models/produtos.model';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { UsuarioSolicitanteDTO } from '../models/usuario-solicitante-dto.model';
import { DadosMatrimonioDto } from '../../dynamic-produtos-components/produtos/matrimonio/models/dto/dados-matrimonio-dto.model';
import { SolicitacaoOutorgantes } from './models/solicitacao-outorgantes.model';
import { SolicitacaoOutorgados } from './models/solicitacao-outorgados.model';
import { InformacoesImportantes } from './models/informacoes-importantes.model';
import { ProdutoRenderDirective } from '../../dynamic-produtos-components/produto-render.directive';
import { ProdutoResolveComponent } from '../../dynamic-produtos-components/produto-resolve-component.service';
import { SolicitacaoService } from '../../services/solicitacao.service';
import { SolicitacaoSimplificado } from '../../models/solicitacao-simplificado.model';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../../utils/toastr.config';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';
import { MatrimonioService } from '../../dynamic-produtos-components/produtos/matrimonio/services/matrimonio.services';
import { OutorgadoCadastro } from './components/cadastro-outorgado/models/outorgado-cadastro.model';
import { NgxSpinnerService } from 'ngx-spinner';
import { Console } from 'console';
import { SolicitacaoExistente } from './models/solicitacao-existente.model';
import { OutorgantesDataGrid } from './models/outorgantesDataGrid';
import { OutorgadosDataGrid } from './models/outorgadosDataGrid';
import { DatagridOutorganteComponent } from './components/datagrid-outorgante/datagrid-outorgante.component';
import { DatagridOutorgadoComponent } from './components/datagrid-outorgado/datagrid-outorgado.component';
import { SolicitacaoExistenteService } from './services/solicitacao-existente.service';
import { ProcuracaoParteConteudos } from './models/procuracaoParteConteudos.model';

import { rg } from 'ng-brazil/rg/validator';
import { OutorganteExistente } from './models/outorgante-existente.model';
import { OutorgadoExistente } from './models/outorgado-existente.model';
import { CadastroOutorgadoComponent } from './components/cadastro-outorgado/cadastro-outorgado.component';
import { CadastroOutorganteComponent } from './components/cadastro-outorgante/cadastro-outorgante.component';
import { InformacoesImportantesComponent } from './components/informacoes-importantes/informacoes-importantes.component';
import { DirecionamentoService } from '../../services/direcionamento.service';

@Component({
	selector: 'app-solicitacao-cadastro',
	templateUrl: './solicitacao-cadastro.component.html',
	styleUrls: ['./solicitacao-cadastro.component.scss']
})
export class SolicitacaoCadastroComponent implements OnInit, AfterViewInit {

	listaDataGridOutorgantes: OutorgantesDataGrid[] = [];
	listaDataGridOutorgados: OutorgadosDataGrid[] = [];
	alteracaoDaSolicitacao: boolean = false;
	tituloTela: string = "NOVA SOLICITAÇÃO";
	nomeProduto: string = "";
	nomeModalidade: string = "";
	idSolicitacao: string;
	produto: Produtos = new Produtos();
	mudancasNaoSalvas: boolean;
	localStorageUtils = new LocalStorageUtils();
	idPessoaSolicitante: number;
	dadosOutorgantes: any;
	dadosOutorgados: any;
	dadosProduto: any;
	informacoesImportantes: string;
	representacaoPartesOutorgados: string = '';
	solicitacaoEParaMatrimonio: string = '';
	componentRef: ComponentRef<any>;

	dadosOutorgadoDeuCerto: boolean = false;
	dadosOutorganteDeuCerto: boolean = false;
	dadosProdutoDeuCerto: boolean = false;
	dadosInformacoesImportantesDeuCerto: boolean = false;
	setTimeoutMensagemSucesso = setInterval(() => this.exibirMensagemSucesso(), 500);
	setTimeoutRedirecionarCarrinho = setInterval(async () => await this.redirecionarCarrinho(), 500);
	podeRedirecionarSolicitacaoAssinatura: boolean = false;
	idSolicitacaoCriada: number = undefined;
	novaSolicitacao: boolean = false;
	idMatrimonio: number = undefined;

	datagridOutorgante: any;
	datagridOutorgado: any;

	@ViewChild(DatagridOutorganteComponent) dataGridOutorgantes: DatagridOutorganteComponent;
	@ViewChild(DatagridOutorgadoComponent) dataGridOutorgados: DatagridOutorgadoComponent;

	@ViewChild(CadastroOutorgadoComponent) cadastroOutorgado: CadastroOutorgadoComponent;
	@ViewChild(CadastroOutorganteComponent) cadastroOutorgante: CadastroOutorganteComponent;

	@ViewChild(InformacoesImportantesComponent) informacoesImportantesComponent: InformacoesImportantesComponent;

	@ViewChild('dynamicProduto', { read: ViewContainerRef }) dynamicProduto!: ViewContainerRef;

	dataSourceBehaviorSubjectPai = new BehaviorSubject(OutorgadosDataGrid);

	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private novaSolicitacaoService: NovaSolicitacaoService,
		private cadastroOutorgadoService: CadastroOutorgadoService,
		private cadastroOutorganteService: CadastroOutorganteService,
		private solicitacaoService: SolicitacaoService,
		private produtoResolveComponent: ProdutoResolveComponent,
		private toastr: ToastrService,
		private htmlSelect: HtmlSelect,
		private matrimonioService: MatrimonioService,
		private spinner: NgxSpinnerService,
		private solicitacaoExistenteService: SolicitacaoExistenteService,
		private direcionamento: DirecionamentoService
	) { }

	async ngAfterViewInit() {
		if (this.idSolicitacao) {
			this.spinner.show();
			this.alteracaoDaSolicitacao = true;
			this.idSolicitacaoCriada = parseInt(this.idSolicitacao);
			this.tituloTela = 'ATUALIZAR SOLICITAÇÃO';
			await this.PreencherSolicitacaoExistente(this.idSolicitacaoCriada);

			let spinnerInterval = setInterval(() => {
				this.spinner.hide();
				clearInterval(spinnerInterval);
			}, 6000);
		}
		this.mudancasNaoSalvas = true;
		this.produtoResolveComponent.setComponentOnViewContainer(this.dynamicProduto, this.nomeProduto);
		this.componentRef = this.produtoResolveComponent.componentRef;
	}

	ngOnInit(): void {
		this.VerificarDadosSolicitanteAtualizados();
		this.produtoResolveComponent.activatedRouter = this.activatedRoute;

		this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
		this.nomeModalidade = this.activatedRoute.snapshot.params.modalidade;

		if (this.idSolicitacao){
			this.direcionamento.direcionarStatusSolicitacao('nova-solicitacao', Number.parseInt(this.idSolicitacao));
		}
		else {
			this.alteracaoDaSolicitacao = false;

			this.nomeProduto = this.activatedRoute.snapshot.params.produto;
			if (!this.nomeProduto) {
				this.router.navigate(['/home']);
			}
		}
	}

	VerificarDadosSolicitanteAtualizados(){
		let usuario = new LocalStorageUtils().lerUsuario();
		this.novaSolicitacaoService.VerificarDadosSolicitanteAtualizados(usuario.idUsuario)
			.subscribe((estaAtualizado) => {		
				if(!estaAtualizado){
					this.toastr.warning(`Você precisa atualizar os seus dados cadastrais!`, 'Tabelionet', ToastOptions);
					this.mudancasNaoSalvas = false;
					this.router.navigate(['/usuario-conta']);
				}		
			},
			(err) => {
				this.toastr.error(`Ocorreu um erro ao verificar dados atualizado solicitante\n\n${err.message}`, 'Tabelionet', ToastOptions);
			});
	}

	criarSolicitacao(): void {
		this.spinner.show();
		console.log(this.dadosOutorgantes);
		if (!this.idPessoaSolicitante) {
			this.toastr.warning(`Está faltando o Solicitante entrar como Outorgante na Solicitação!`, 'Tabelionet', ToastOptions);
			this.spinner.hide();
			return;
		}

		if (!this.dadosOutorgantes || !this.dadosOutorgantes.length || this.dadosOutorgantes.length == 0) {
			this.toastr.warning(`Solicitante deve atribuir um outorgante`, 'Tabelionet', ToastOptions);
			this.spinner.hide();
			return;
		}

		if (!this.dadosOutorgados || !this.dadosOutorgados.length || this.dadosOutorgados.length == 0) {
			this.toastr.warning(`Solicitante deve atribuir um outorgado`, 'Tabelionet', ToastOptions);
			this.spinner.hide();
			return;
		}

		this.dadosProduto = this.componentRef.instance.contracaoMatrimonioCadastro;
		if (!this.dadosProduto
			|| !this.dadosProduto.formularioValido
			|| !this.dadosProduto.testemunhaRequerenteValido
			|| !this.dadosProduto.testemunhaNoivoOuNoivaValido) {

			if (!this.dadosProduto.testemunhaRequerenteValido
				&& !this.dadosProduto.testemunhaNoivoOuNoivaValido) {
				this.toastr.warning(`Adicione os dados das testemunhas!`, 'Tabelionet', ToastOptions);
			}
			else if (!this.dadosProduto.testemunhaRequerenteValido) {
				this.toastr.warning(`Adicione uma testemunha para o Requerente!`, 'Tabelionet', ToastOptions);
			}
			else if (!this.dadosProduto.testemunhaNoivoOuNoivaValido) {
				this.toastr.warning(`Adicione uma testemunha para o(a) Noivo(a)!`, 'Tabelionet', ToastOptions);
			}
			else {
				this.toastr.warning(`Preencha corretamente os dados do seu matrimônio!`, 'Tabelionet', ToastOptions);
			}
			this.spinner.hide();
			return;
		}

		let idProduto = this.novaSolicitacaoService.getIdProdutoPeloNome(this.nomeProduto);
		let dadosUsuario = this.localStorageUtils.lerUsuario();
		let solicitacao = new SolicitacaoSimplificado(idProduto, dadosUsuario.idUsuario, this.idPessoaSolicitante);

		if (this.idSolicitacaoCriada) {
			this.novaSolicitacao = false;
			this.setIdSolicitacao(this.dadosOutorgados, this.idSolicitacaoCriada);
			this.setIdSolicitacao(this.dadosOutorgantes, this.idSolicitacaoCriada);

			if (!this.dadosOutorganteDeuCerto) {
				this.adicionarOutorgantes(this.idSolicitacaoCriada);
			}

			if (!this.dadosOutorgadoDeuCerto) {
				this.adicionarOutorgados(this.idSolicitacaoCriada);
			}

			if (!this.dadosProdutoDeuCerto) {
				this.adicionarProduto(this.idSolicitacaoCriada, dadosUsuario.idUsuario);
			}

			if (!this.dadosInformacoesImportantesDeuCerto) {
				this.adicionarInformacoesImportantes(this.idSolicitacaoCriada);
			}
		}
		else {
			this.novaSolicitacao = true;
			this.solicitacaoService.AdicionarNovaSolicitacao(solicitacao)
				.subscribe((idSolicitacao) => {
					if (idSolicitacao) {
						this.idSolicitacaoCriada = idSolicitacao;
						this.setIdSolicitacao(this.dadosOutorgados, idSolicitacao);
						this.setIdSolicitacao(this.dadosOutorgantes, idSolicitacao);
						this.adicionarOutorgantes(idSolicitacao);
						this.adicionarProduto(idSolicitacao, dadosUsuario.idUsuario);
					}
				},
					(err) => {
						this.toastr.error(`Ocorreu um erro ao criar a solicitação\n\n${err.message}`, 'Tabelionet', ToastOptions);
					});
		}
	}

	setIdSolicitacao(lista: any, idSolicitacao: number): void {
		lista.forEach((dado) => {
			dado.idSolicitacao = idSolicitacao;
		})
	}

	adicionarOutorgantes(idSolicitacao: number) {
		let solicitacaoOutorgantes = new SolicitacaoOutorgantes(idSolicitacao,
			this.idPessoaSolicitante,
			this.dadosOutorgantes,
			this.alteracaoDaSolicitacao);
		this.cadastroOutorganteService.AdicionarOutorgantes(solicitacaoOutorgantes)
			.subscribe((success) => {
				this.dadosOutorganteDeuCerto = true;

				if (this.novaSolicitacao) {
					this.adicionarOutorgados(this.idSolicitacaoCriada);
				}


			},
				(err) => {
					this.toastr.error(`Ocorreu um erro ao tentar criar os Outorgantes,\n\n${err.message}`, 'Tabelionet', ToastOptions);
					this.spinner.hide();
				});
	}

	adicionarOutorgados(idSolicitacao: number) {
		let solicitacaoOutorgados = new SolicitacaoOutorgados(idSolicitacao,
			this.representacaoPartesOutorgados,
			this.dadosOutorgados,
			this.alteracaoDaSolicitacao);

		this.cadastroOutorgadoService.AdicionarOutorgados(solicitacaoOutorgados)
			.subscribe((success) => {
				this.dadosOutorgadoDeuCerto = true;
			},
				(err) => {
					this.toastr.error(`Ocorreu um erro ao tentar criar os Outorgados,\n\n${err.message}`, 'Tabelionet', ToastOptions);
					this.spinner.hide();
				});
	}

	adicionarProduto(idSolicitacao: number, idUsuario: number) {
		//this.dadosProduto = this.componentRef.instance.contracaoMatrimonioCadastro;
		let dadosMatrimonio = new DadosMatrimonioDto(this.dadosProduto, idSolicitacao, idUsuario, this.htmlSelect, this.alteracaoDaSolicitacao);

		this.matrimonioService.AdicionarMatrimonio(dadosMatrimonio)
			.subscribe((matrimonio) => {
				this.dadosProdutoDeuCerto = true;
				this.idMatrimonio = matrimonio.idMatrimonio;
				this.adicionarInformacoesImportantes(this.idSolicitacaoCriada);
			},
				(err) => {
					this.toastr.error(`Ocorreu um erro ao tentar criar os dados da Contração de Matrimônio,\n\n${err.message}`, 'Tabelionet', ToastOptions);
					this.spinner.hide();
				});

	}

	adicionarInformacoesImportantes(idSolicitacao: number) {
		if (this.informacoesImportantes) {
			let info = new InformacoesImportantes(idSolicitacao, this.informacoesImportantes);
			this.novaSolicitacaoService.adicionarInformacoesImportantes(info)
				.subscribe((sucesso) => {
					this.dadosInformacoesImportantesDeuCerto = true;
				},
					(err) => {
						this.toastr.error(`Ocorreu um erro ao tentar gravar as Informações Importantes,\n\n${err.message}`, 'Tabelionet', ToastOptions);
						this.spinner.hide();
					})
		} else {
			this.dadosInformacoesImportantesDeuCerto = true;
		}
	}

	exibirMensagemSucesso(): void {
		if (this.dadosOutorganteDeuCerto
			&& this.dadosOutorgadoDeuCerto
			&& this.dadosInformacoesImportantesDeuCerto
			&& this.dadosProdutoDeuCerto) {
			clearInterval(this.setTimeoutMensagemSucesso);
			this.podeRedirecionarSolicitacaoAssinatura = true;
		}
	}

	async redirecionarCarrinho() {
		if (this.podeRedirecionarSolicitacaoAssinatura) {
			clearInterval(this.setTimeoutRedirecionarCarrinho);
			this.spinner.hide();
			this.toastr.success(`A sua solicitação foi criada com sucesso!\n\nVocê será redirecionado para visualizar o Status da Solicitação!`, 'Tabelionet', ToastOptions)
			.onHidden.pipe().subscribe(() => {
				this.router.navigate([`solicitacao-assinatura/${this.idSolicitacaoCriada}/${this.idMatrimonio}/${this.idPessoaSolicitante}`]);
				this.mudancasNaoSalvas = false;
			});
		}
	}

	async PreencherSolicitacaoExistente(idSolicitacaoExistente: number) {
		let solicitacaoExistente: SolicitacaoExistente = null;
		try {
			solicitacaoExistente = await this.solicitacaoService
				.ObterSolicitacaoExistente(idSolicitacaoExistente)
				.toPromise<SolicitacaoExistente>();
			console.log(`Solicitação existente: ${solicitacaoExistente}`);
		} catch (e) {
			console.log(`Ocorreu erro ao buscar solicitação existente: ${e}`);
			if (e == 'Solicitação não está mais disponível para alterações!') {
				this.router.navigate([`carrinho/${this.idSolicitacaoCriada}`]);
			}
		}

		if (solicitacaoExistente) {
			this.idPessoaSolicitante = solicitacaoExistente.idPessoaSolicitante;
			this.nomeProduto = solicitacaoExistente.nomeProduto;
			this.preencherOutorganteJaExistente(solicitacaoExistente.outorgantes);
			this.preencherOutorgadoJaExistente(solicitacaoExistente.outorgados, solicitacaoExistente.representacaoPartes);
			this.informacoesImportantes = solicitacaoExistente.informacoesImportantes;
			this.informacoesImportantesComponent.setInformacoes(solicitacaoExistente.informacoesImportantes);
		}
	}

	preencherOutorganteJaExistente(outorgantes: OutorganteExistente[]) {
		this.dadosOutorgantes = [];
		outorgantes.forEach((outorgante) => {
			let conteudos = this.getDadosPessoasProcuracaoParte(outorgante.jsonConteudo,
				outorgante.conteudoPessoasFisicas,
				outorgante.conteudoPessoasContatos);


			this.cadastroOutorgante.addOutorgante(
				outorgante.idPessoa,
				outorgante.idTipoDocumento,
				outorgante.documento,
				conteudos.dadosConteudoPessoasFisicas.RG,
				outorgante.nome,
				outorgante.email,
				conteudos.dadosConteudoPessoasFisicas.Nascimento,
				conteudos.dadosConteudoPessoasFisicas.EstadoCivil,
				conteudos.dadosConteudoPessoasFisicas.Nacionalidade,
				conteudos.dadosConteudoPessoasFisicas.Profissao,
				conteudos.dadosConteudoPessoaContatos.Celular,
				conteudos.dadosConteudoPessoaContatos.Fixo,
				outorgante.enderecoEntrega,
				outorgante.idProcuracaoParte
			)
		});
	}

	preencherOutorgadoJaExistente(outorgados: OutorgadoExistente[], representacaoParte: string) {
		if (representacaoParte) {
			var representacao = JSON.parse(representacaoParte);
			if (representacao.representacaoPartes === 'todas') {
				this.cadastroOutorgado.setRepresentacaoTodasPartes();
			} else {
				this.cadastroOutorgado.setRepresentacaoQualquerPartes();
			}
		}
		this.dadosOutorgados = [];
		outorgados.forEach((outorgado) => {
			let conteudos = this.getDadosPessoasProcuracaoParte(outorgado.jsonConteudo,
				outorgado.conteudoPessoasFisicas,
				outorgado.conteudoPessoasContatos);

			this.cadastroOutorgado.addOutorgado(
				outorgado.idPessoa,
				outorgado.idTipoDocumento,
				outorgado.documento,
				//conteudos.dadosConteudoPessoasFisicas.rg,
				outorgado.nome,
				outorgado.email,
				outorgado.idProcuracaoParte
			)
		});
	}

	getDadosPessoasProcuracaoParte(jsonConteudo: string,
		conteudoPessoasFisicas: string,
		conteudoPessoasContatos): ProcuracaoParteConteudos {

		let dadosConteudo = {};
		let dadosPessoasFisicas = {
			DataNascimento: "",
			RG: "",
			Profissao: "",
			EstadoCivil: "",
			Nacionalidade: "",
		};
		let dadosPessoaContatos = {
			Celular: "",
			Fixo: ""
		};

		if (jsonConteudo)
			dadosConteudo = JSON.parse(jsonConteudo);

		if (conteudoPessoasFisicas)
			dadosPessoasFisicas = JSON.parse(conteudoPessoasFisicas);

		if (conteudoPessoasContatos)
			dadosPessoaContatos = JSON.parse(conteudoPessoasContatos);

		return {
			dadosConteudo: dadosConteudo,
			dadosConteudoPessoasFisicas: dadosPessoasFisicas,
			dadosConteudoPessoaContatos: dadosPessoaContatos
		}
	}

	validarOutorganteEmOutorgado(documento: string, email: string): boolean {
		let valido = true;
		if (this.datagridOutorgado) {
			this.datagridOutorgado.forEach(element => {
				if (parseInt(element.documento) === parseInt(documento)) {
					valido = false;
				}
				if (element.email === email) {
					valido = false;
				}
			});
		}
		return valido;
	}

	validarOutorgadoEmOutorgante(documento: string, email: string): boolean {
		let valido = true;
		if (this.datagridOutorgante) {
			this.datagridOutorgante.forEach(element => {
				if (parseInt(element.documento) === parseInt(documento)) {
					valido = false;
				}
				if (element.email === email) {
					valido = false;
				}
			});
		}
		return valido;
	}

	validarSolicitacaoEParaMimNomeRequerente(idPessoa: number): string {
		let nomeRequerente = "";
		this.dadosOutorgantes.forEach(element => {
			if (parseInt(element.idPessoa) === idPessoa) {
				nomeRequerente = element.nomePessoa;
			}
		});
		return nomeRequerente;
	}

	validarSolicitacaoEParaMimDocumentoRequerente(idPessoa: number): any {
		let documento = "";
		let idTipoDocumento = 0;
		this.dadosOutorgantes.forEach(element => {
			if (parseInt(element.idPessoa) === idPessoa) {
				if (element.idTipoDocumento === 2) {
					documento = element.rg;
					idTipoDocumento = 0;
				} else {
					documento = element.documento;
					idTipoDocumento = 1;
				}

			}
		});
		return { documento: documento, idTipoDocumento: idTipoDocumento };
	}

	validarSolicitacaoEParaMimNascimentoRequerente(idPessoa: number): string {
		let nascimentoRequerente = "";
		this.dadosOutorgantes.forEach(element => {
			if (parseInt(element.idPessoa) === idPessoa) {
				nascimentoRequerente = element.nascimento;
			}
		});
		return nascimentoRequerente;
	}

}
