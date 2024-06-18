import { getProfiles } from "$lib/profiles/http.js";
import { getTransactions } from "$lib/transactions/http.js";

export async function load({ url, parent, depends, fetch }) {
    depends('api/transactions');
    const { user } = await parent();
    const profiles = await getProfiles(user.id, fetch);

    let profileId = url.searchParams.get('profileId') ?? undefined;
    if (!profileId) {
        profileId = profiles.items.at(0)?.id;
    }

    const transactions = profileId ? await getTransactions(profileId, fetch) : { items: [] };

    return {
        profiles: profiles.items,
        transactions: transactions.items,
    }
}