import { http } from '$lib/http/index.js';

export async function load({ parent }) {
    const { user } = await parent();
    const categories = await http.categories.getAll(user.id);

    return {
        categories: categories.items,
    }
}