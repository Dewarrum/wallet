import { env } from "$env/dynamic/private";
import type { Category, CreateCategoryRequest } from "$lib/categories/models";
import type { Fetch, PageResponse } from "$lib/http";

export async function getCategories(userId: string, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/categories?userId=${userId}`);
    const json: PageResponse<Category> = await response.json();
    return json;
}

export async function createCategory(request: CreateCategoryRequest, fetch: Fetch) {
    const response = await fetch(`${env.API_URL}/categories`, {
        method: "POST",
        body: JSON.stringify(request),
        headers: {
            "Content-Type": "application/json",
        },
    });
    const json: Category = await response.json();
    return json;
}