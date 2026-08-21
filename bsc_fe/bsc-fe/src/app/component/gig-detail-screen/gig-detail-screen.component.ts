import { Component, inject, OnInit } from '@angular/core';
import { GigDetail, GigRating } from '../../interface/gig.interface';
import { GigService } from '../../services/gig.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { GigDetailRatingCommentComponent } from '../gig-detail-rating-comment/gig-detail-rating-comment.component';
import { PaymentMethodService } from '../../services/payment-method.service';
import { PaymentMethod } from '../../interfaces/payment-method';
import { TransactionService } from '../../services/transaction.service';
import { AuthService } from '../../services/auth.service';
import { ɵInternalFormsSharedModule, FormsModule } from '@angular/forms';
import { UserRole } from '../../enums/user-role';

@Component({
  selector: 'app-gig-detail-screen',
  standalone: true,
  imports: [
    CommonModule,
    GigDetailRatingCommentComponent,
    RouterLink,
    ɵInternalFormsSharedModule,
    FormsModule,
  ],
  templateUrl: './gig-detail-screen.component.html',
  styleUrl: './gig-detail-screen.component.css',
})
export class GigDetailScreenComponent implements OnInit {
  ngOnInit(): void {
    this.fetchDetail(this.id);
    this.fetchPaymentMethods();
  }

  private route = inject(ActivatedRoute);
  private gigService = inject(GigService);
  private paymentMethodService = inject(PaymentMethodService);
  private transactionService = inject(TransactionService);
  private authService = inject(AuthService);

  isCustommer = this.authService.getUser()?.userRole == UserRole.CUSTOMER;
  description = '';
  id = +(this.route.snapshot.paramMap.get('id') ?? '');
  isDetailFailed = false;
  isDetailLoading = false;
  isRatingsLoading = false;
  paymentMethodButtonText: string = 'Payment';
  isPaymentMethodSelected: boolean = false;
  isPaymentError: boolean = false;
  paymentMethods: PaymentMethod[] = [];
  totalPrice: number = 0;
  gigDetail: GigDetail = {
    id: 0,
    name: '',
    description: '',
    duration: 0,
    price: 0,
    stars: 0,
    gigCreator: {
      id: 0,
      name: '',
    },
    types: [],
  };
  ratings: GigRating[] = [];
  fetchDetail(id: number) {
    this.isDetailLoading = true;
    this.gigService.getGigById(id).subscribe({
      next: (res) => {
        this.isDetailLoading = false;
        this.isDetailFailed = false;
        this.gigDetail = res;
        this.totalPrice = this.gigDetail.price; // Initialize totalPrice with the base price of the gig
      },
      error: (res) => {
        this.isDetailFailed = true;
      },
    });
  }
  fetchPaymentMethods() {
    this.paymentMethodService.getPaymentMethods().subscribe({
      next: (res) => {
        this.paymentMethods = res;
        console.log('Payment methods fetched:', this.paymentMethods);
      },
    });
  }
  onPaymentMethodSelected(paymentMethod: PaymentMethod) {
    this.paymentMethodButtonText = paymentMethod.name;
    this.validatePayment();
    this.totalPrice = calculateTotalPrice(
      this.gigDetail.price,
      paymentMethod.rate,
    );
  }

  onOrderNow() {
    if (!this.validatePayment()) return;
    try {
      console.log(this.authService.getUserId());
      this.transactionService
        .addNewTransaction({
          gigId: this.gigDetail.id,
          description: this.description,
          paymentMethodId:
            this.paymentMethods.find(
              (pm) => pm.name === this.paymentMethodButtonText,
            )?.id ?? 0,
          totalPrice: this.totalPrice,
        })
        .subscribe({
          next: (res) => {
            console.log('Transaction successful:', res);
          },
        });
    } catch (error) {
      console.error('Error occurred while adding transaction:', error);
    }
  }
  validatePayment(): boolean {
    const valid = this.paymentMethodButtonText != 'Payment';
    this.isPaymentMethodSelected = valid;
    this.isPaymentError = !valid;
    return valid;
  }
}

function calculateTotalPrice(
  basePrice: number,
  paymentMethodRate: number,
): number {
  return basePrice + basePrice * paymentMethodRate;
}
