export interface TransactionRequest {
    userId: number;
    gigId: number;
    description:string;
    paymentMethodId: number;
    totalPrice: number;
}
