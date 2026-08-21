import { TransactionStatus } from "../enums/transaction-status";
import { Item } from "./item";
import { Rating } from "./rating";

export interface TransactionResponse {
    id: number;
    gigName: string;
    transactionStatus: TransactionStatus;
    totalPrice: number;
    date: Date;
    rating: Rating;
    item: Item;
}
