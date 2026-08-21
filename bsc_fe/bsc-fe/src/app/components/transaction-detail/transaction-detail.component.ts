import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionService } from '../../services/transaction.service';
import { switchMap } from 'rxjs';
import { TransactionResponse } from '../../interfaces/transaction-response';
import { TransactionStatus } from '../../enums/transaction-status';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-transaction-detail',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './transaction-detail.component.html',
  styleUrl: './transaction-detail.component.css'
})
export class TransactionDetailComponent implements OnInit {
  route = inject(ActivatedRoute);
  transactionService = inject(TransactionService);
  transactionId: string | null = this.route.snapshot.paramMap.get('id');
  transactionDetail: TransactionResponse | null = null;
  transactionStatusOptions = Object.values(TransactionStatus);
  router = inject(Router);

  fb = inject(FormBuilder);
  transactionForm = this.fb.group({
    itemName: [''],
    itemPath: [''],
    itemDescription: [''],
    status: ['']
  });

  ngOnInit(): void {
    if (this.transactionId) {
        this.route.paramMap.pipe(
        switchMap(params => this.transactionService.getTransactionById(Number(this.transactionId)))
      )    .subscribe({
        next: (transaction) => {
          this.transactionDetail = transaction;
          console.log('Transaction detail fetched:', this.transactionDetail);
          // Patch the form values with the fetched transaction detail
          this.transactionForm.patchValue({
            itemName: this.transactionDetail?.item?.name || '',
            itemPath: this.transactionDetail?.item?.path || '',
            itemDescription: this.transactionDetail?.item?.description || '',
            status: this.transactionDetail?.transactionStatus || ''
          });
        }
      });
    }
  }
  onSubmit() {
    if (this.transactionForm.valid && this.transactionDetail) {
      const updatedTransaction = {
        id: this.transactionDetail.id,
        transactionStatus: this.transactionForm.value.status as TransactionStatus,
        item: {
          ...this.transactionDetail.item,
          name: this.transactionForm.value.itemName || '',
          path: this.transactionForm.value.itemPath || '',
          description: this.transactionForm.value.itemDescription || ''
        }
      };
      this.transactionService.updateTransactionItem(updatedTransaction).subscribe({
        next: (response) => {
          this.router.navigate(['/transactions']);
        }
      });
    }
  }
}