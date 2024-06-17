import type { PageResponse } from "$lib/http";
import type { CreateProfileRequest, Profile } from "$lib/profiles/models";

export const createProfilesClient = (apiUrl: string) => {
    async function get(profileId: string) {
        const response = await fetch(`${apiUrl}/profiles/${profileId}`);
        const json: Profile = await response.json();
        return json;
    }

    async function getMany(userId: string) {
        const response = await fetch(`${apiUrl}/profiles?userId=${userId}`);
        const json: PageResponse<Profile> = await response.json();
        return json;
    }

    async function create(profile: CreateProfileRequest) {
        const response = await fetch(`${apiUrl}/profiles`, {
            method: "POST",
            body: JSON.stringify(profile),
            headers: {
                "Content-Type": "application/json",
            },
        });
        const json: Profile = await response.json();
        return json;
    }

    return {
        get,
        getAll: getMany,
        create,
    }
}