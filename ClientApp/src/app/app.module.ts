import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './account/login/login.component';
import { AppRoutingModule, routes} from './app.routing.module';
import { RegisterComponent } from './account/register/register.component';
import { UsersService, CategoriesService, ProductsService, ProductListsService } from './shared/client-services/typescript-angular-client';
import { AlertComponent } from './_directives';
import { AuthenticationService } from './_services';

import { NavMenuComponent } from './pages/layout/nav-menu/nav-menu.component';
import { FooterMenuComponent } from './pages/layout/footer-menu/footer-menu.component';
import { PagenotfoundComponent } from './pages/pagenotfound/pagenotfound.component';
import { HomeComponent } from './pages/home/home.component';
import { FairComponent } from './pages/men/fair/fair.component';
import { MenComponent } from './pages/men/men.component';


@NgModule({
   declarations: [
      AppComponent,
      LoginComponent, 
      RegisterComponent,
      AlertComponent,
      HomeComponent,
      NavMenuComponent, 
      FooterMenuComponent,
      PagenotfoundComponent,
      MenComponent,
      FairComponent
   ],
   imports: [
    BrowserModule,
    HttpModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [
    UsersService, 
    AuthenticationService, 
    CategoriesService,
    ProductsService,
    ProductListsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
