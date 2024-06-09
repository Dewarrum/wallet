import { createQuery } from "@tanstack/svelte-query";
import type { Transaction } from "./models";
import type { PageResponse } from "$lib/utils/page-response";

export const getTransactionsForProfile = (profileId: string) => createQuery<any, Error, PageResponse<Transaction>, string[]>({
    queryKey: ['profile', profileId, 'transactions',],
    queryFn: async () => await fetch(`/api/transactions?profileId=${profileId}`).then((r) => r.json())
});