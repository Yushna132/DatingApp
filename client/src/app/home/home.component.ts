import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  
  registerMode = false;
  users: any;
  http = inject(HttpClient);

  ngOnInit(): void {
    this.getUsers();
  }
  
  registerToggle(){
    this.registerMode = !this.registerMode
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }


  getUsers(){
    this.http.get('https://localhost:5001/api/user')
    .subscribe({
      next: response => this.users = response,
      error:err => console.error('Erreur API :', err),
      complete: () => console.log('Requête terminée.')
    });
  }

}



