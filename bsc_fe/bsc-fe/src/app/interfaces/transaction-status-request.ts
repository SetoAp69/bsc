import { TransactionStatus } from "../enums/transaction-status";

export interface TransactionStatusRequest {
    transactionId: number;
    status: TransactionStatus;
}
