import type { User } from "$lib/models/user";

export const createUsersClient = (apiUrl: string) => {
    async function get(email: string) {
        const response = await fetch(`${apiUrl}/users?email=${email}`);
        const json: User = await response.json();
        return json;
    }

    return {
        get,
    }
}