import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  baseApiUrl: string = environment.apiUrl + '/api/Language/';
  constructor(private http: HttpClient) { }


  saveLanguage(language: any) {
    return this.http.post(this.baseApiUrl + 'AddLanguage', language);
  }

  GetAllLanguage() {
    return this.http.get(this.baseApiUrl +'GetAllLanguage');
  }
}
