export type Transaction = {
    id: string;
    profileId: string;
    amount: number;
    createdAt: string;
    description: string;
    category: string;
    type: TransactionType;
    isRecurring: boolean;
    paymentMethod: PaymentMethod;
    merchant: string;
};

export enum TransactionType {
    Income,
    Expense,
};

export function getTransactionTypeDisplayName(type: TransactionType): string {
    switch (type) {
        case TransactionType.Income:
            return "Income";
        case TransactionType.Expense:
            return "Expense";
        default:
            return "Unknown";
    }
}

export enum PaymentMethod {
    OTHER = "other",
    CASH = "cash",
    CREDIT_CARD = "credit_card",
    DEBIT_CARD = "debit_card",
    BANK_TRANSFER = "bank_transfer",
};