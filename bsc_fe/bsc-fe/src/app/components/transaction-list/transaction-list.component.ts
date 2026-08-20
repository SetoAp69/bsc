import { Component, inject, OnInit } from '@angular/core';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TransactionResponse } from '../../interfaces/transaction-response';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css'
})
export class TransactionListComponent implements OnInit {
  route = inject(ActivatedRoute);
  transactionService = inject(TransactionService);
  transactionList: TransactionResponse[] = [];
  ngOnInit(): void {
    const userId = Number(this.route.snapshot.paramMap.get('userId'));
    this.transactionService.getTransactionsByUserId(userId).subscribe((transactions) => {
      transactions.forEach((transaction) => {
        transaction.date = formatDate(transaction.date.toString()) as unknown as Date;
      });
      this.transactionList = transactions.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
    });
  }
}

function formatDate(dateString: string): string {
  const date = new Date(dateString);
  const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit', second: '2-digit' };
  return date.toLocaleDateString(undefined, options);
}