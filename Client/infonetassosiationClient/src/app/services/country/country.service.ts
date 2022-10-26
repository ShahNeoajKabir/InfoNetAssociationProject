import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  baseApiUrl: string = environment.apiUrl + '/api/Country/';
  constructor(private http: HttpClient) { }


  saveCountry(country: any) {
    return this.http.post(this.baseApiUrl + 'AddCountry', country);
  }

  getAllCountry() {
    return this.http.get(this.baseApiUrl + 'GetAllCountry');
  }

  deleteCountryById(countryId:number){
    return this.http.delete(this.baseApiUrl + 'deleteCountryById/'+countryId);

  }

}
