import { env } from "$env/dynamic/private";
import type { Fetch, PageResponse } from "$lib/http";
import type { CreateProfileRequest, Profile } from "$lib/profiles/models";

export async function getProfiles(userId: string, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/profiles?userId=${userId}`);
    const json: PageResponse<Profile> = await response.json();
    return json;
}

export async function getProfileById(id: string, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/profiles/${id}`);
    const json: Profile = await response.json();
    return json;
}

export async function createProfile(request: CreateProfileRequest, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/profiles`, {
        method: "POST",
        body: JSON.stringify(request),
        headers: {
            "Content-Type": "application/json",
        },
    });
    const json: Profile = await response.json();
    return json;
}