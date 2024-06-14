import type { CreateTransactionRequest, Transaction } from "$lib/models/transaction";
import type { PageResponse } from ".";

export const createTransactionClient = (apiUrl: string) => {
    async function get(profileId: string) {
        const response = await fetch(`${apiUrl}/transactions?profileId=${profileId}`);
        const json: PageResponse<Transaction> = await response.json();
        return json;
    }

    async function create(transaction: CreateTransactionRequest) {
        const response = await fetch(`${apiUrl}/transactions`, {
            method: "POST",
            body: JSON.stringify(transaction),
            headers: {
                "Content-Type": "application/json",
            },
        });
        const json: Transaction = await response.json();
        return json;
    }

    return {
        get,
        create,
    }
}