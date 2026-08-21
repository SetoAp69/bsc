import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TransactionService } from '../../services/transaction.service';
import { switchMap } from 'rxjs';
import { TransactionResponse } from '../../interfaces/transaction-response';

@Component({
  selector: 'app-transaction-detail',
  standalone: true,
  imports: [],
  templateUrl: './transaction-detail.component.html',
  styleUrl: './transaction-detail.component.css'
})
export class TransactionDetailComponent implements OnInit {
  route = inject(ActivatedRoute);
  transactionService = inject(TransactionService);
  transactionId: string | null = this.route.snapshot.paramMap.get('id');
  transactionDetail: TransactionResponse | null = null;

  ngOnInit(): void {
    if (this.transactionId) {
        this.route.paramMap.pipe(
        switchMap(params => this.transactionService.getTransactionById(Number(this.transactionId)))
      )    .subscribe({
        next: (transaction) => {
          this.transactionDetail = transaction;
        }
      });
    }
  }
}