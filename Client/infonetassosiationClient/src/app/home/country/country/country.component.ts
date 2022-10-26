import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import {  countryModel } from 'src/app/model/countryModel';
import { CountryService } from 'src/app/services/country/country.service';

@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  public selectedItemId: number = 0;
  public moduleForm!:FormGroup;
  public searchText: string='';
  public lstCountrysModel:countryModel[]=new Array<countryModel>();
  countryModel:countryModel | undefined;
  constructor(private fb:FormBuilder,
              private _modalService:NgbModal,
              private _toasterService:ToastrService,
              private _countryService:CountryService) { }

  ngOnInit(): void {
    this.getAllCountry();
    this.creatForm();
  }

  getAllCountry() {
    this._countryService.getAllCountry().subscribe(res => {
      this.lstCountrysModel = res as countryModel[];
    });
  }

  openModalForSave(event:any){
    this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
  }

  reset(){
    this.getAllCountry();
    this.creatForm();
  }

  saveCountry(){
     if (this.moduleForm?.invalid) {
      this._toasterService.error("Please fill the all required fields", "Invalid submission");
      return;
    }

      this._countryService.saveCountry(this.formVal).subscribe((res: any) => {
      if (res) {
        this._toasterService.success("Save Successfully");
        this.getAllCountry();
        this.reset();
        this._modalService.dismissAll();
      }
    })

  }

  delete(index: number, modal: any) {
    this.selectedItemId = index;
    this._modalService.open(modal);
  }

  removeDetailFormRow(id: number) {
    this._countryService.deleteCountryById(id).subscribe(res => {
      this._toasterService.success("Delete Successfully");
      this.getAllCountry();

    })
  }
  // editModulesById(id:number,event:any){
  //   console.log("id",id)
  //  this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
  //  this._moduleService.getModulesById(id).subscribe(res=>{
  //  this.modulesModel = res as ModulesModel;
  //  this.moduleForm.patchValue(this.modulesModel);
  //  })
  // }
  creatForm(){
    this.moduleForm=this.fb.group({
     id:[0,[]],
     countryName:[,[Validators.required]],
    })
  }

  get formVal(){
    return this.moduleForm?.value
  }
    get f(){
    return this.moduleForm?.controls
  }

}
