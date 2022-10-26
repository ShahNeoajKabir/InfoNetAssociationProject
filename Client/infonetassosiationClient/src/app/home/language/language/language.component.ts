import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CityModel } from 'src/app/model/cityModel';
import { countryModel } from 'src/app/model/countryModel';
import { LanguageModel } from 'src/app/model/languageModel';
import { CityService } from 'src/app/services/city/city.service';
import { LanguageService } from 'src/app/services/language/language.service';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.css']
})
export class LanguageComponent implements OnInit  {
  public selectedItemId: number = 0;
  public moduleForm!:FormGroup;
  public searchText: string='';
  languageModel:LanguageModel | undefined;
  lstLanguage:LanguageModel[]=[];
  constructor(private fb:FormBuilder,
              private _modalService:NgbModal,
              private _toasterService:ToastrService,
              private _languageService:LanguageService,
              ) { }

  ngOnInit(): void {
    this.GetAllLanguage();
    this.creatForm();
  }

  GetAllLanguage() {
    this._languageService.GetAllLanguage().subscribe(res => {
      this.lstLanguage = res as LanguageModel[];
    });
  }

  openModalForSave(event:any){
    this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
  }

  reset(){
    this.GetAllLanguage();
    this.creatForm();
  }

  saveLanguage(){
     if (this.moduleForm?.invalid) {
      this._toasterService.error("Please fill the all required fields", "Invalid submission");
      return;
    }

      this._languageService.saveLanguage(this.formVal).subscribe((res: any) => {
      if (res) {
        this._toasterService.success("Save Successfully");
        this.GetAllLanguage();
        this.reset();
        this._modalService.dismissAll();
      }
    })

  }
  creatForm(){
    this.moduleForm=this.fb.group({
     id:[0,[]],
     languageName:[,[Validators.required]],
    })
  }

  get formVal(){
    return this.moduleForm?.value
  }
    get f(){
    return this.moduleForm?.controls
  }

}
