import { http } from "$lib/http";

export async function load({ url, parent }) {
    const { user } = await parent();
    const profiles = await http.profiles.getAll(user.id);

    let profileId = url.searchParams.get('profileId') ?? undefined;
    if (!profileId) {
        profileId = profiles.items.at(0)?.id;
    }

    const transactions = profileId ? await http.transactions.get(profileId) : { items: [] };

    return {
        profiles: profiles.items,
        transactions: transactions.items,
    }
}