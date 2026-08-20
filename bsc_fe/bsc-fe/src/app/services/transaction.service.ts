import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { TransactionResponse } from '../interfaces/transaction-response';
import { AuthService } from './auth.service';
import { TransactionStatus } from '../enums/transaction-status';
import { TransactionStatusRequest } from '../interfaces/transaction-status-request';
import { TransactionRequest } from '../interfaces/transaction-request';
import { TransactionRatingRequest } from '../interfaces/transaction-rating-request';
import { Rating } from '../interfaces/rating';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  http = inject(HttpClient);
  authService = inject(AuthService);
  constructor() { }
  getTransactionsByUserId(userId: number) {
    console.log(`Fetching transactions for userId: ${this.authService.getToken()}`);
    return this.http.get<TransactionResponse[]>(`${environment.apiUrl}/transactions/${userId}`);
  }
  addNewTransaction(request: TransactionRequest) {
    return this.http.post(`${environment.apiUrl}/transactions`, request);
  }
  updateTransactionStatus(request: TransactionStatusRequest) {
    return this.http.put(`${environment.apiUrl}/transactions/status`, request);
  }
  updateTransactionRating(request: TransactionRatingRequest) {
    return this.http.put<Rating>(`${environment.apiUrl}/transactions/rating`, request);
  }
  deleteTransaction(transactionId: number) {
    return this.http.delete(`${environment.apiUrl}/transactions/${transactionId}`);
  }
}
