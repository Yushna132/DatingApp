import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone : true,
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit{
  http = inject(HttpClient);
  users: any;
  title = 'DatingApp';

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/user')
    .subscribe({
      next: response => this.users = response,
      error:err => console.error('Erreur API :', err),
      complete: () => console.log('Requête terminée.')
    });
  }
  
  
}
