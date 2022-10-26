import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CityModel } from 'src/app/model/cityModel';
import { countryModel } from 'src/app/model/countryModel';
import { CityService } from 'src/app/services/city/city.service';
import { CountryService } from 'src/app/services/country/country.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  public selectedItemId: number = 0;
  public moduleForm!:FormGroup;
  public searchText: string='';
  public lstCityModel:CityModel[]=new Array<CityModel>();
  public lstCountrysModel:countryModel[]=new Array<countryModel>();
  cityModel:CityModel | undefined;
  constructor(private fb:FormBuilder,
              private _modalService:NgbModal,
              private _toasterService:ToastrService,
              private _cityService:CityService,
              private _countryService:CountryService
              ) { }

  ngOnInit(): void {
    this.getAllCountry();
    this.creatForm();
    this.getAllCity();
  }

  getAllCountry() {
    this._countryService.getAllCountry().subscribe(res => {
      this.lstCountrysModel = res as countryModel[];
    });
  }

  getAllCity() {
    this._cityService.GetAllCity().subscribe(res => {
      this.lstCityModel = res as CityModel[];
    });
  }

  openModalForSave(event:any){
    this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
  }

  reset(){
    this.getAllCountry();
    this.creatForm();
  }

  saveCity(){
     if (this.moduleForm?.invalid) {
      this._toasterService.error("Please fill the all required fields", "Invalid submission");
      return;
    }

      this._cityService.saveCity(this.formVal).subscribe((res: any) => {
      if (res) {
        this._toasterService.success("Save Successfully");
        this.getAllCity();
        this.reset();
        this._modalService.dismissAll();
      }
    })

  }
  creatForm(){
    this.moduleForm=this.fb.group({
     id:[0,[]],
     cityName:[,[Validators.required]],
     countryId:[,[Validators.required]],
    })
  }

  get formVal(){
    return this.moduleForm?.value
  }
    get f(){
    return this.moduleForm?.controls
  }

}
