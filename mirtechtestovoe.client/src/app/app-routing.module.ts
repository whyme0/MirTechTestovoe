import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadComponent: () => import('./components/home.component').then(m => m.HomeComponent) },
  { path: 'employees', loadComponent: () => import('./components/employees.component').then(m => m.EmployeesComponent) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
