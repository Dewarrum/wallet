import { getCategories } from "$lib/categories/http.js";

export async function load({ parent, fetch }) {
    const { user } = await parent();
    const categories = await getCategories(user.id, fetch);

    return {
        categories: categories.items,
    }
}