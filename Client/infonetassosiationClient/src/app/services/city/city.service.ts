import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  baseApiUrl: string = environment.apiUrl + '/api/City/';
  constructor(private http: HttpClient) { }


  saveCity(city: any) {
    return this.http.post(this.baseApiUrl + 'AddCity', city);
  }

  GetAllCity() {
    return this.http.get(this.baseApiUrl + 'GetAllCity');
  }

  getByCountryId(countryId:number) {
    return this.http.get(this.baseApiUrl + 'GetAllCityByCountry/'+countryId);
  }

}
