import { Rating } from "./rating";

export interface TransactionResponse {
    id: number;
    gigName: string;
    transactionStatus: string;
    totalPrice: number;
    date: Date;
    rating: Rating;
}
