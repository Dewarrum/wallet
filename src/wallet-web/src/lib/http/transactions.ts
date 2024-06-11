import type { Transaction } from "$lib/models/transaction";
import type { PageResponse } from ".";

export const createTransactionClient = (apiUrl: string) => {
    async function get(profileId: string) {
        const response = await fetch(`${apiUrl}/transactions?profileId=${profileId}`);
        const json: PageResponse<Transaction> = await response.json();
        return json;
    }

    return {
        get,
    }
}