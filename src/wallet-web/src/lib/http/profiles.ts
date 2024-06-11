import type { Profile } from "$lib/models/profile";
import type { PageResponse } from ".";

export const createProfilesClient = (apiUrl: string) => {
    async function get(profileId: string) {
        const response = await fetch(`${apiUrl}/profiles/${profileId}`);
        const json: Profile = await response.json();
        return json;
    }

    async function getAll(userId: string) {
        const response = await fetch(`${apiUrl}/profiles?userId=${userId}`);
        const json: PageResponse<Profile> = await response.json();
        return json;
    }

    return {
        get,
        getAll,
    }
}