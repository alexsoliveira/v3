import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {

  @Input() titulo: string;
  @Input() conteudo: any;

  @Input() nomeIconeCancelar: string = "clear";
  @Input() nomeBotaoCancelar: string = "Cancelar";

  @Input() nomeIconeOk: string = "delete";
  @Input() nomeBotaoOk: string = "Sim";

  public setBotaoOkIconCheck() {
    this.nomeIconeOk = "check";
  }

}
