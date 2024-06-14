import { z } from "zod";

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
    Income = 'Income',
    Expense = 'Expense',
}
export const transactionType = z.nativeEnum(TransactionType);

export enum PaymentMethod {
    Other = 'Other',
    Cash = 'Cash',
    CreditCard = 'CreditCard',
    DebitCard = 'DebitCard',
    BankTransfer = 'BankTransfer',
}

export const paymentMethod = z.nativeEnum(PaymentMethod);

export const createTransactionSchema = z.object({
    profileId: z.string(),
    amount: z.coerce.number().positive(),
    description: z.string().min(1),
    categoryId: z.string(),
    type: transactionType,
    isRecurring: z.boolean(),
    paymentMethod: paymentMethod,
    merchant: z.string().optional(),
});

export type CreateTransactionSchema = typeof createTransactionSchema;
export type CreateTransactionRequest = z.infer<CreateTransactionSchema> & { userId: string };