import { DadosNoivosDto } from "./dados-noivos-dto.model";
import { DadosContracaoMatrimonioDto } from "./dados-contracao-matrimonio-dto.model";
import { DadosRequerenteDto } from "./dados-requerente-dto.model";
import { TestemunhaDto } from "./testemunha-dto.model";
import { ContracaoMatrimonioCadastro } from '../contracao-matrimonio.model';
import { DadosFamiliarDto } from "./dados-familiar-dto.model";
import { DadosPessoasMatrimonioDto } from "./dados-pessoas-matrimonio-dto.model";
import { HtmlSelect } from "src/app/shared/services/HtmlSelect.service";

export class DadosMatrimonioDto {
    public idSolicitacao: number;
    public idUsuario: number;
    public dadosContracaoMatrimonio: DadosContracaoMatrimonioDto
    public dadosRequerente: DadosRequerenteDto;
    public dadosNoivos: DadosNoivosDto;
    public testemunhas: TestemunhaDto[] = [];
    public alteracaoDaSolicitacao: boolean;

    constructor(contracaoMatrimonio: ContracaoMatrimonioCadastro,
        idSolicitacao: number,
        idUsuario: number,
        private htmlSelect: HtmlSelect,
        alteracaoDaSolicitacao: boolean
    ) {

        this.idSolicitacao = idSolicitacao;
        this.idUsuario = idUsuario;
        this.dadosContracaoMatrimonio = new DadosContracaoMatrimonioDto();
        this.preecherDadosContracaoMatrimonio(contracaoMatrimonio);
        this.preecherDadosRequerente(contracaoMatrimonio);
        this.preecherDadosConjuge(contracaoMatrimonio);
        this.preecherTestemunhas(contracaoMatrimonio);
        this.alteracaoDaSolicitacao = alteracaoDaSolicitacao;
    }

    preecherDadosContracaoMatrimonio(obj: ContracaoMatrimonioCadastro) {
        this.dadosContracaoMatrimonio.documentoProclamas = obj.UploadArquivo;
        this.dadosContracaoMatrimonio.nomeDocumentoProclamas = obj.NomeArquivoProclamas;
        this.dadosContracaoMatrimonio.regimeCasamento = obj.RegimeCasamento;
    }

    preecherDadosRequerente(obj: ContracaoMatrimonioCadastro) {
        this.dadosRequerente = new DadosRequerenteDto();
        this.dadosRequerente.requerente = new DadosPessoasMatrimonioDto();
        this.dadosRequerente.maeRequerente = new DadosFamiliarDto();
        this.dadosRequerente.paiRequerente = new DadosFamiliarDto();
        this.preencherDadosMaeRequerente(obj);
        this.preencherDadosPaiRequerente(obj);
        this.preencherDadosRequerente(obj);
    }

    preecherDadosConjuge(obj: ContracaoMatrimonioCadastro) {
        this.dadosNoivos = new DadosNoivosDto();
        this.dadosNoivos.noivos = new DadosPessoasMatrimonioDto();
        this.dadosNoivos.maeNoivos = new DadosFamiliarDto();
        this.dadosNoivos.paiNoivos = new DadosFamiliarDto();
        this.preencherDadosMaeConjuge(obj);
        this.preencherDadosPaiConjuge(obj);
        this.preencherDadosConjuge(obj);
    }

    preecherTestemunhas(obj: ContracaoMatrimonioCadastro) {
        if (obj.Testemunhas) {
            obj.Testemunhas.forEach((testemunha) => {
                this.testemunhas.push(new TestemunhaDto(
                    testemunha.NomeTestemunha,
                    this.htmlSelect.getTextoTipoDocumento(testemunha.TipoDocumentoTestemunha),
                    testemunha.DocumentoTestemunha,
                    testemunha.RgTestemunha,
                    this.htmlSelect.getTextoParte(testemunha.Parte)
                ))
            })
        }
    }


    preencherDadosRequerente(obj: any) {
        this.dadosRequerente.requerente.dataNascimento = obj.DataNascimentoRequerente;
        this.dadosRequerente.requerente.documento = obj.DocumentoRequerente;
        this.dadosRequerente.requerente.idTipoDocumento = obj.TipoDocumentoRequerente;
        this.dadosRequerente.requerente.nome = obj.NomeRequerente;
        this.dadosRequerente.requerente.camposExtras = "";
    }
    preencherDadosMaeRequerente(obj: any) {
        this.dadosRequerente.maeRequerente.nome = obj.NomeMaeRequerente;
        this.dadosRequerente.maeRequerente.documento = obj.DocumentoMaeRequerente;
        this.dadosRequerente.maeRequerente.idTipoDocumento = obj.TipoDocumentoMaeRequerente;
        this.dadosRequerente.maeRequerente.dataNascimento = obj.DataNascimentoMaeRequerente;
        this.dadosRequerente.maeRequerente.situacao = obj.SituacaoMaeRequerente;
        this.dadosRequerente.maeRequerente.camposExtras = "";
    }

    preencherDadosPaiRequerente(obj: any) {
        this.dadosRequerente.paiRequerente.nome = obj.NomePaiRequerente;
        this.dadosRequerente.paiRequerente.documento = obj.DocumentoPaiRequerente;
        this.dadosRequerente.paiRequerente.idTipoDocumento = obj.TipoDocumentoPaiRequerente;
        this.dadosRequerente.paiRequerente.dataNascimento = obj.DataNascimentoPaiRequerente;
        this.dadosRequerente.paiRequerente.situacao = obj.SituacaoPaiRequerente;
        this.dadosRequerente.paiRequerente.camposExtras = "";
    }




    preencherDadosConjuge(obj: any) {
        this.dadosNoivos.noivos.dataNascimento = obj.DataNascimentoNoivos;
        this.dadosNoivos.noivos.documento = obj.DocumentoNoivos;
        this.dadosNoivos.noivos.idTipoDocumento = obj.TipoDocumentoNoivos;
        this.dadosNoivos.noivos.nome = obj.NomeNoivos;
        this.dadosNoivos.noivos.camposExtras = "";
    }
    preencherDadosMaeConjuge(obj: any) {
        this.dadosNoivos.maeNoivos.nome = obj.NomeMaeNoivos;
        this.dadosNoivos.maeNoivos.documento = obj.DocumentoMaeNoivos;
        this.dadosNoivos.maeNoivos.idTipoDocumento = obj.TipoDocumentoMaeNoivos;
        this.dadosNoivos.maeNoivos.dataNascimento = obj.DataNascimentoMaeNoivos;
        this.dadosNoivos.maeNoivos.situacao = obj.SituacaoMaeNoivos;
        this.dadosNoivos.maeNoivos.camposExtras = "";
    }
    preencherDadosPaiConjuge(obj: any) {
        this.dadosNoivos.paiNoivos.nome = obj.NomePaiNoivos;
        this.dadosNoivos.paiNoivos.documento = obj.DocumentoPaiNoivos;
        this.dadosNoivos.paiNoivos.idTipoDocumento = obj.TipoDocumentoPaiNoivos;
        this.dadosNoivos.paiNoivos.dataNascimento = obj.DataNascimentoPaiNoivos;
        this.dadosNoivos.paiNoivos.situacao = obj.SituacaoPaiNoivos;
        this.dadosNoivos.paiNoivos.camposExtras = "";
    }
}
