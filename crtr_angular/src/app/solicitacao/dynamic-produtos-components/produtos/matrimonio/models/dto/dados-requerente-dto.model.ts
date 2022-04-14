import { DadosFamiliarDto } from "./dados-familiar-dto.model";
import { DadosPessoasMatrimonioDto } from "./dados-pessoas-matrimonio-dto.model";

export class DadosRequerenteDto {
    requerente: DadosPessoasMatrimonioDto;
    maeRequerente: DadosFamiliarDto;
    paiRequerente: DadosFamiliarDto;
}