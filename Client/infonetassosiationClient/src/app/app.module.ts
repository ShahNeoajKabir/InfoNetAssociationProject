import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { ToastrModule } from 'ngx-toastr';
import { SidenavModule } from 'src/vendor/libs/sidenav/sidenav.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { CountryComponent } from './home/country/country/country.component';
import { DeleteConfirmationComponent } from './shared/delete-confirmation/delete-confirmation.component';
import { CityComponent } from './home/city/city/city.component';
import { LanguageComponent } from './home/language/language/language.component';
import { PersonalInformationComponent } from './home/PersonalInformation/personal-information/personal-information.component';
import { DateControlComponent } from './shared/date-control-component/date-control-component.component';
import { NgbDateCustomParserFormatter } from './shared/NgbDateCustomParserFormatter ';
import { ImageControlComponent } from './shared/image-control/image-control.component';

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    CountryComponent,
    DeleteConfirmationComponent,
    CityComponent,
    LanguageComponent,
    PersonalInformationComponent,
    DateControlComponent,
    ImageControlComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgSelectModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    SidenavModule,
    Ng2SearchPipeModule,
    ToastrModule.forRoot(),
  ],
  providers: [NgbDateCustomParserFormatter],
  bootstrap: [AppComponent]
})
export class AppModule { }
