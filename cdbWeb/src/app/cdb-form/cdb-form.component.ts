import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-cdb-form',
  standalone: true,
  templateUrl: './cdb-form.component.html',
  styleUrls: ['./cdb-form.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class CdbFormComponent {
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  

  form = this.fb.group({
    valorInicial: [null, [Validators.required, Validators.min(0.01)]],
    prazoEmMeses: [null, [Validators.required, Validators.min(2)]],
  });

  resultado?: any;
  erro?: string;


  private apiUrl = `${environment.apiUrl}/calculadoracdb`;

  calcular() {
    

    this.erro = undefined;
    if (this.form.invalid) {
      this.erro = 'Preencha todos os campos corretamente.';
      return;
    }
    const payload = this.form.value;



    console.log('Payload enviado para a API:', payload);

    console.log('this.apiUrl = ', this.apiUrl);
    console.log('Ambiente em uso:', environment);

    this.http.post(this.apiUrl, payload).subscribe({
      next: res => this.resultado = res,
      error: err => {
        const errors = err?.error?.errors;
        if (errors) {
          this.erro = Object.values(errors).flat().join(' | ');
        } else {
          this.erro = err?.error?.detalhe ?? 'Erro ao calcular';
        }
      }
    });
  }
}
