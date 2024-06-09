export interface Transaction {
    id: string,
    profileId: string,
    amount: number,
    createdAt: Date,
    description: string,
    categoryId: string,
    type: string,
    isRecurring: boolean,
    paymentMethod: number,
    merchant?: string,
}

export interface CreateTransactionRequest {
    profileId: string,
    amount: number,
    description: string,
    categoryId: string,
    transactionType: number,
    isRecurring: boolean,
    paymentMethod: number,
    merchant?: string,
}