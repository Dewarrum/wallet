import { env } from "$env/dynamic/private";
import type { Fetch, PageResponse } from "$lib/http";
import type { CreateTransactionRequest, Transaction } from "$lib/transactions/models";

export async function getTransactions(profileId: string, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/transactions?profileId=${profileId}`);
    const json: PageResponse<Transaction> = await response.json();
    return json;
}

export async function createTransaction(request: CreateTransactionRequest, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/transactions`, {
        method: "POST",
        body: JSON.stringify(request),
        headers: {
            "Content-Type": "application/json",
        },
    });
    const json: Transaction = await response.json();
    return json;
}