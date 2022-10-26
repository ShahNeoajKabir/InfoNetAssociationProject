import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CityModel } from 'src/app/model/cityModel';
import { countryModel } from 'src/app/model/countryModel';
import { LanguageModel } from 'src/app/model/languageModel';
import { CityService } from 'src/app/services/city/city.service';
import { CountryService } from 'src/app/services/country/country.service';
import { LanguageService } from 'src/app/services/language/language.service';
import { PersonalInfoService } from 'src/app/services/personalInfo/personal-info.service';
import { NgbDateCustomParserFormatter } from 'src/app/shared/NgbDateCustomParserFormatter ';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.css']
})
export class PersonalInformationComponent implements OnInit {
    public selectedItemId: number = 0;
    public moduleForm!:FormGroup;
    public searchText: string='';
    public lstCityModel:CityModel[]=new Array<CityModel>();
    public lstCountrysModel:countryModel[]=new Array<countryModel>();
    public lstPersonInfo:any[]=[];
  lstLanguage:LanguageModel[]=[];
  selectionLanguage:LanguageModel[]=[];
  public languageFormArray:any;
  previewImg:string = '';
  memberPersonInfoId:any;
  memberInfo:any;
  getMemberinfowithId:any;
    cityModel:CityModel | undefined;
    constructor(private fb:FormBuilder,
                private _modalService:NgbModal,
                private _toasterService:ToastrService,
                private _cityService:CityService,
                private _countryService:CountryService,
              private _languageService:LanguageService,
    public dateFormat: NgbDateCustomParserFormatter,
    private _personalInfoService:PersonalInfoService


                ) { }

    ngOnInit(): void {
      this.getAllCountry();
      this.creatForm();
      this.GetAllLanguage();
      this.getAllMemberPersonInfo();
    }

    getAllMemberPersonInfo(){
      this._personalInfoService.getAllAddPersonalInfo().subscribe(res=>{
        this.lstPersonInfo=res as any[];
        console.log(this.lstPersonInfo);

      })
    }
    downloadPdf(base64String:any, fileName:any){

        // Download PDF in Chrome etc.
        const source = `data:application/pdf;base64,${base64String}`;
        const link = document.createElement("a");
        link.href = source;
        link.download = `${fileName}.pdf`
        link.click();

    }

    getAllCountry() {
      this._countryService.getAllCountry().subscribe(res => {
        this.lstCountrysModel = res as countryModel[];
      });
    }
    GetAllLanguage() {
      this._languageService.GetAllLanguage().subscribe(res => {
        this.lstLanguage = res as LanguageModel[];
      });
    }
    getByCountryId(countryId:number){
      this._cityService.getByCountryId(countryId).subscribe(res=>{
        this.lstCityModel = res as CityModel[];
      })
    }



    openModalForSave(event:any){
      this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
    }

    getSelection(item:any) {
      return this.selectionLanguage.findIndex(s => s.id === item.id) !== -1;
    }
    changeHandler(item: any) {
      const id = item.id;
      const index = this.selectionLanguage.findIndex(u => u.id === id);
      if (index === -1) {
        this.selectionLanguage = [...this.selectionLanguage, item];
        this.languageFormArray = this.fb.array([]);
        this.selectionLanguage.forEach(p=>{
          this.languageFormArray.push(new FormGroup({
            id: new FormControl(p.id),
            languageName: new FormControl(p.languageName),
     }))
        })

      } else {
        this.selectionLanguage = this.selectionLanguage.filter(language => language.id !== item.id)
        this.languageFormArray = this.fb.array([]);

        this.selectionLanguage.forEach(p=>{
          this.languageFormArray.push(new FormGroup({
            id: new FormControl(p.id),
            languageName: new FormControl(p.languageName),
     }))
        })
      }
    }
    editMemberInfo(id: number, event: any) {
      this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
      this._personalInfoService.GetMemberById(id).subscribe((res:any) => {
        this.getMemberinfowithId = res as any;
        let memberPatchValue= res.memberPersonalLanguageViewModel as any;
      this.selectionLanguage = res.languageList as LanguageModel[];

        this.getByCountryId(memberPatchValue.countryId)

      this.moduleForm.patchValue(memberPatchValue)
      this.moduleForm.patchValue({
        reqDateNgb: this.dateFormat.getYyyymmddToNgbDate(this.memberInfo.date),
      })

      console.log("udg",this.selectionLanguage);
        this.languageFormArray = this.fb.array([]);
        this.selectionLanguage.forEach(p=>{
          this.languageFormArray.push(new FormGroup({
            id: new FormControl(p.id),
            languageName: new FormControl(p.languageName),
     }))
        })
      })



    }
    reset(){
      this.getAllCountry();
      this.creatForm();
    }
    _handleReaderLoaded(readerEvt:any) {
      var binaryString = readerEvt.target.result;
      this.previewImg=btoa(binaryString);  // Converting file to binary string data.
    }
    viewInfo(info:any,event:any){
      this._modalService.open(event, { size: 'lg', windowClass: 'modal-xl' });
      console.log("event",info);
      this.memberInfo=info;
    }
    saveMemberPerson(){
     this.moduleForm.patchValue({
        languageList:this.languageFormArray.value
      })

       if (this.moduleForm?.invalid) {
        this._toasterService.error("Please fill the all required fields", "Invalid submission");
        return;
      }
        this._personalInfoService.savePersonalInfo(this.formVal).subscribe((res)=>{
          this.memberPersonInfoId=res;
          this.moduleForm.patchValue({
            memberPersonInfoId:res
          })

          this._personalInfoService.addPersonalLanguage(this.formVal).subscribe(res=>{
            this._toasterService.success("Added Successfully");
            this._modalService.dismissAll();
            this.reset();
          })

        })

    }
    delete(index: number, modal: any) {
      this.selectedItemId = index;
      this._modalService.open(modal);
    }
    removeDetailFormRow(id: number) {
      this._personalInfoService.deleteMemberById(id).subscribe(res => {
        this._toasterService.success("Delete Successfully");
        this.getAllMemberPersonInfo();

      })
    }
    creatForm(){
      this.moduleForm=this.fb.group({
       id:[0,[]],
       countryId:[,[Validators.required]],
       memberPersonInfoId:[,[]],
       cityId:[,[Validators.required]],
       date:[this.dateFormat.getDateToYyyymmdd(new Date()),[Validators.required]],
       reqDateNgb:[,[]],
       languageList:[[],[Validators.required]],
       document:[,[]],
       memberName:[,[Validators.required]],
       imageFile:[,[Validators.required]]
      })
  this.languageFormArray = this.fb.array([]);

    }

    get formVal(){
      return this.moduleForm?.value
    }
      get f(){
      return this.moduleForm?.controls
    }
    get detailsFormAllVal() {
      return this.languageFormArray.value;
    }
  }
