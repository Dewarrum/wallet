import { API_URL } from "$env/static/private";
import type { Fetch } from "$lib/http";
import type { User } from "$lib/users/models";

export async function getUser(email: string, name: string, fetch: Fetch) {
    let response = await fetch(`${API_URL}/users?email=${email}`);
    if (response.ok) {
        const json: User = await response.json();
        return json;
    }

    response = await fetch(`${API_URL}/users`, {
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

    const json: User = await response.json();
    return json;
}