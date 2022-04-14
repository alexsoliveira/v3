import { DadosFamiliarDto } from "./dados-familiar-dto.model";
import { DadosPessoasMatrimonioDto } from "./dados-pessoas-matrimonio-dto.model";

export class DadosNoivosDto {
    public noivos: DadosPessoasMatrimonioDto;
    public maeNoivos: DadosFamiliarDto;
    public paiNoivos: DadosFamiliarDto;
}