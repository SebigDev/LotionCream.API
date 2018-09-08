import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { PagenotfoundComponent } from './pages/pagenotfound/pagenotfound.component';
import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '../../node_modules/@angular/core';
import { FairComponent } from './pages/men/fair/fair.component';
import { MenComponent } from './pages/men/men.component';



export const routes: Routes = [
  
  { path: '', component: HomeComponent},
  { path: 'account/login', component: LoginComponent},
  { path: 'account/register', component: RegisterComponent},
  {
    path: '', 
    runGuardsAndResolvers: 'always',
    children: [
      { path: 'men-category/:id', component: MenComponent},
      { path: 'men-category/fair/:id', component: FairComponent},
    ]
  },
  {path: '**', component: PagenotfoundComponent}
];

@NgModule({
  imports: [
  RouterModule.forRoot(routes)
 ],
 exports: [
   RouterModule
 ]
})
export class AppRoutingModule { }

