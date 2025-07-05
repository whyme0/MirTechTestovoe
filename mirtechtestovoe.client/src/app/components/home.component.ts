import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-home',
  template: `
    <div class='container mt-4'>
      <h1>О компании</h1>
      <p>Информация о компании</p>
    </div>
  `
})
export class HomeComponent { }
