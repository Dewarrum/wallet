import { http } from '$lib/http/index.js';

export async function load({ parent }) {
    const { user } = await parent();
    const profiles = await http.profiles.getAll(user.id);

    return {
        profiles: profiles.items,
    }
}