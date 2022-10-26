

import { NgbDate, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, Output, EventEmitter, Input, OnChanges } from '@angular/core';
import { NgbDateCustomParserFormatter } from '../NgbDateCustomParserFormatter ';


@Component({
  selector: 'app-date-control',
  template: `<div class="input-group" [ngStyle]="{'width.%':widthPercent}">
  <input [(ngModel)]="setNgbDate" name="setNgbDate"  (ngModelChange)="onSelectDate()"
  class="form-control"  placeholder="dd-mm-yyyy" [minDate]="minDate" [maxDate]="maxDate" [placement]="'auto'+'button-left'"
  ngbDatepicker #d1="ngbDatepicker" (click)="d1.toggle();" (keydown.enter)="d1.toggle();" [ngStyle]="{'font-size.px':fontSize}" [id]="focusId"  >

  </div>`,
  styleUrls: []
})
export class DateControlComponent implements OnInit, OnChanges {
  @Input() fontSize:number=14;
  @Input() setNgbDate:any;
  @Input() focusId:string | undefined;
  @Input() tabIndexIs:boolean=true;
  @Input() showCurrentDate:boolean= true;
  @Input() widthPercent:number=100;
  @Input() minDate:NgbDateStruct = {year:1900, month:1, day:1};
  @Input() maxDate:NgbDateStruct = {year:2100, month:12, day:31};
  @Input() disabled:boolean = false;
  @Output() getLocalDate = new EventEmitter<any>();
  @Output() getYyyymmdd = new EventEmitter<any>();
  @Output() getNgbDate = new EventEmitter<any>();
  //Nasrin
  @Input() focusTextFieldName:string | undefined;
  @Input() focusNgSelectFieldName:string | undefined;
    //End

  constructor(
    private dateFormat:NgbDateCustomParserFormatter
  ) { }
  ngOnInit() {
    if(this.showCurrentDate){
      this.setNgbDate = this.dateFormat.getCurrentNgbDate();
      let yyyymmdd = this.dateFormat.getNgbDateToYyyymmdd(this.setNgbDate);
      let localDate = new Date();
      let localDateStr = `${localDate.getMonth()+1}/${localDate.getDate()}/${localDate.getFullYear()}`;
      this.getLocalDate.emit(localDateStr);
    }
  }
  ngOnChanges(){
  }

  onSelectDate(){
    if(this.setNgbDate==null){return;}
    let yyyymmdd = this.dateFormat.getNgbDateToYyyymmdd(this.setNgbDate);
    //let localDate = this.dateFormat.ngbDateToDate(this.setNgbDate).toLocaleDateString();
    let localDate = this.dateFormat.ngbDateToDate(this.setNgbDate);
    let localDateStr = `${localDate.getMonth()+1}/${localDate.getDate()}/${localDate.getFullYear()}`;
    this.getYyyymmdd.emit(yyyymmdd);
    this.getLocalDate.emit(localDateStr);
    this.getNgbDate.emit(this.setNgbDate);
  }

}
