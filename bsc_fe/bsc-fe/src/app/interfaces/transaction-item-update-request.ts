import { TransactionStatus } from "../enums/transaction-status";
import { Item } from "./item";

export interface TransactionItemUpdateRequest {
    id: number;
    transactionStatus: TransactionStatus;
    item: Item;
}