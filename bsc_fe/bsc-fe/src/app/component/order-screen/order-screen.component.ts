import { Component, inject } from '@angular/core';
import { PaymentMethodService } from '../../services/payment-method.service';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute } from '@angular/router';
import { GigDetail } from '../../interface/gig.interface';
import { PaymentMethod } from '../../interfaces/payment-method';
import { GigService } from '../../services/gig.service';
import { concatMap, subscribeOn } from 'rxjs';

@Component({
  selector: 'app-order-screen',
  standalone: true,
  imports: [],
  templateUrl: './order-screen.component.html',
  styleUrl: './order-screen.component.css',
})
export class OrderScreenComponent {
  private gigService = inject(GigService);
  private paymentMethodService = inject(PaymentMethodService);
  private transactionService = inject(TransactionService);
  private route = inject(ActivatedRoute);
  paymentMethodId = +(
    this.route.snapshot.paramMap.get('paymentMethodId') ?? ''
  );
  gigId = +(this.route.snapshot.paramMap.get('gigId') ?? '');
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
  paymentMethod: PaymentMethod = {
    id: 0,
    name: '',
    rate: 0,
  };
  fetchData() {
    this.gigService
    .getGigById(this.gigId)
    .pipe(
      concatMap((g) =>
        this.paymentMethodService.getPaymentMethods(),
      ),
    ).subscribe
  }
}
