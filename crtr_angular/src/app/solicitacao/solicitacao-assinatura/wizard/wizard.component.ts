import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-wizard',
  templateUrl: './wizard.component.html',
  styleUrls: ['./wizard.component.scss']
})
export class WizardComponent implements OnInit {

  @Input() passosWizard;

  constructor() { }

  ngOnInit(): void {
  }

}
