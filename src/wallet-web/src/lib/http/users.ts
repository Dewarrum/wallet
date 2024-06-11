import type { User } from "$lib/models/user";

export const createUsersClient = (apiUrl: string) => {
    async function get(email: string, name: string) {
        let response = await fetch(`${apiUrl}/users?email=${email}`);
        if (response.ok) {
            const json: User = await response.json();
            return json;
        }

        response = await fetch(`${apiUrl}/users`, {
            method: 'POST',
            body: JSON.stringify({
                name,
                email,
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        const json = await response.json();
        return json;
    }

    return {
        get,
    }
}