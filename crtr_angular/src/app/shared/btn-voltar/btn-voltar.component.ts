import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-btn-voltar',
  templateUrl: './btn-voltar.component.html',
  styleUrls: ['./btn-voltar.component.scss']
})
export class BtnVoltarComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit(): void {

  }

  voltar(): void {
    this.location.back();
  }
}
