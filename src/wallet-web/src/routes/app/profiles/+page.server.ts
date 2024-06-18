import { getProfiles } from "$lib/profiles/http.js";

export async function load({ parent, fetch }) {
    const { user } = await parent();
    const profiles = await getProfiles(user.id, fetch);

    return {
        profiles: profiles.items,
    }
}