import type { Category, CreateCategoryRequest } from "$lib/models/categories";
import type { PageResponse } from ".";

export const createCategoriesClient = (apiUrl: string) => {
    async function getAll(userId: string) {
        const response = await fetch(`${apiUrl}/categories?userId=${userId}`);
        const json: PageResponse<Category> = await response.json();
        return json;
    }

    async function create(data: CreateCategoryRequest) {
        const response = await fetch(`${apiUrl}/categories`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const json: Category = await response.json();
        return json;
    }

    return {
        getAll,
        create,
    }
}