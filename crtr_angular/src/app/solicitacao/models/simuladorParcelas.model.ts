import { Parcela } from './parcela.model';

export class SimuladorParcelas {
  valorTotal: number;
  parcelas: Parcela[] = [];

  zeroParcelas(): void {
    this.valorTotal = 0;
    this.parcelas.push({
      numero: 0,
      valorParcela: 0,
      valorTotal: 0,
      juros: 0
    });
  }

  mockarParcelas(valorTotal: number): void {
    this.valorTotal = valorTotal;
    this.parcelas.push({
      numero: 1,
      valorParcela: valorTotal,
      valorTotal: valorTotal,
      juros: 0
    });
  }
}
