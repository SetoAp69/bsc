import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PaymentMethod } from '../interfaces/payment-method';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentMethodService {
  http = inject(HttpClient);
  constructor() { }

  getPaymentMethods() {
    return this.http.get<PaymentMethod[]>(`${environment.apiUrl}/payment-methods`);
  }
}
