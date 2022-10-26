import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonalInfoService {
  baseApiUrl: string = environment.apiUrl + '/api/MemberInfo/';
  constructor(private http: HttpClient) { }


  savePersonalInfo(info: any) {
    debugger
    let formData = new FormData()
    formData.append('id', info.id)
    formData.append('memberName', info.memberName)

    formData.append('imageFile', info.imageFile,info.imageFile.fileName)
    formData.append('countryId', info.countryId)
    formData.append('cityId', info.cityId)
    formData.append('date', info.date)
    formData.append('languageList', info.languageList)
    return this.http.post(this.baseApiUrl + 'AddPersonalInfo', formData);
  }

  addPersonalLanguage(info: any) {
    return this.http.post(this.baseApiUrl + 'AddPersonalLanguage', info);
  }

  getAllAddPersonalInfo() {
    return this.http.get(this.baseApiUrl +'GetAllAddPersonalInfo');
  }

  deleteMemberById(id:number){
    return this.http.delete(this.baseApiUrl +'DeleteMemberById/'+id);
  }
  GetMemberById(id:number){
    return this.http.get(this.baseApiUrl +'GetMemberById/'+id);
  }

}
