import { Component, inject, OnInit } from '@angular/core';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TransactionResponse } from '../../interfaces/transaction-response';
import { TransactionRatingRequest } from '../../interfaces/transaction-rating-request';
import { EditRatingComponent } from '../edit-rating/edit-rating.component';
import { Rating } from '../../interfaces/rating';
import { TransactionStatus } from '../../enums/transaction-status';
import { AuthService } from '../../services/auth.service';
import { UserRole } from '../../enums/user-role';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule, EditRatingComponent],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css',
})
export class TransactionListComponent implements OnInit {
  route = inject(ActivatedRoute);
  authService = inject(AuthService);
  router = inject(Router);
  userRole: UserRole | null = this.authService.getUserRole();
  transactionService = inject(TransactionService);
  transactionList: TransactionResponse[] = [];
  isShowEditRating: boolean = false;
  selectedTransaction: TransactionResponse | null = null;
  ngOnInit(): void {
    const userId = Number(this.route.snapshot.paramMap.get('userId'));
    if (userId == null) {
      this.redirectToLogin();
      return;
    }
    this.transactionService.getTransactionsByUserId().subscribe({
      next: (transactions) => {
        transactions.forEach((transaction) => {
          transaction.date = formatDate(
            transaction.date.toString(),
          ) as unknown as Date;
        });
        this.transactionList = transactions.sort(
          (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime(),
        );
      },
    });
  }

  redirectToLogin(): void {
    this.router.navigate(['/login']);
  }

  showEditRating(transaction: TransactionResponse): void {
    this.isShowEditRating = true;
    this.selectedTransaction = transaction;
  }

  onRatingChanged(newRating: Rating): void {
    console.log('New rating received:', newRating);
    if (this.selectedTransaction !== null) {
      var ratingId = this.selectedTransaction.rating?.id || null;
      const request: TransactionRatingRequest = {
        ratingId: ratingId || 0,
        starRating: newRating.rating,
        comment: newRating.comment,
      };
      this.transactionService.updateTransactionRating(request).subscribe({
        next: () => {
          this.transactionList = this.transactionList.map((transaction) => {
            if (transaction.id === this.selectedTransaction?.id) {
              return { ...transaction, rating: newRating };
            }
            return transaction;
          });
          this.isShowEditRating = false;
          this.selectedTransaction = null;
        },
      });
    }
  }
  getStatusClass(status: TransactionStatus): string {
    let statusClass = '';
    switch (status) {
      case TransactionStatus.CANCELED: {
        statusClass = 'bg-danger border-danger text-white';
        break;
      }
      case TransactionStatus.IN_PROGRESS: {
        statusClass = 'border-warning bg-warning text-white';
        break;
      }
      case TransactionStatus.COMPLETED: {
        statusClass = 'bg-success border-success text-white';
        break;
      }
      case TransactionStatus.FINISHED: {
        statusClass = 'bg-success border-success text-white';
        break;
      }
    }
    return statusClass;
  }

  canEditRating(transaction: TransactionResponse): boolean {
    return (
      transaction.transactionStatus === TransactionStatus.COMPLETED &&
      this.userRole === UserRole.CUSTOMER
    );
  }

  isServiceProvider(): boolean {
    return this.userRole === UserRole.SERVICE_PROVIDER;
  }

  onEditTransactionClick(transaction: TransactionResponse): void {
    this.router.navigate([`/transactions/detail/${transaction.id}`]);
  }
}

function formatDate(dateString: string): string {
  const date = new Date(dateString);
  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
  };
  return date.toLocaleDateString(undefined, options);
}
