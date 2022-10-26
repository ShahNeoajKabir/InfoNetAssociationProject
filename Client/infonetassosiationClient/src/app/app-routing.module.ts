import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityComponent } from './home/city/city/city.component';
import { CountryComponent } from './home/country/country/country.component';
import { LanguageComponent } from './home/language/language/language.component';
import { PersonalInformationComponent } from './home/PersonalInformation/personal-information/personal-information.component';

const routes: Routes = [
  {path:'country',component:CountryComponent},
  {path:'city',component:CityComponent},
  {path:'language',component:LanguageComponent},
  {path:'',component:PersonalInformationComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
