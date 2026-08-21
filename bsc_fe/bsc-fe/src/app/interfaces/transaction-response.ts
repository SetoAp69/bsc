import { TransactionStatus } from "../enums/transaction-status";
import { Rating } from "./rating";

export interface TransactionResponse {
    id: number;
    gigName: string;
    transactionStatus: TransactionStatus;
    totalPrice: number;
    date: Date;
    rating: Rating;
}
