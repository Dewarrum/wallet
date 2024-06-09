import { createMutation } from "@tanstack/svelte-query";
import type { CreateTransactionRequest } from "./models";

export const addTransaction = () => createMutation({
    mutationKey: ['transactions', 'add'],
    mutationFn: async (transaction: CreateTransactionRequest) => await fetch(`/api/transactions`, {
        method: 'POST',
        body: JSON.stringify(transaction),
        credentials: 'include',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
});