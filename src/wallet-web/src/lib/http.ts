import { API_URL } from "$env/static/private";
import { createCategoriesClient } from "./categories/http";
import { createProfilesClient } from "./profiles/http";
import { createTransactionClient } from "./transactions/http";
import { createUsersClient } from "./users/http";

const apiUrl = API_URL;

export const http = {
    transactions: createTransactionClient(apiUrl),
    profiles: createProfilesClient(apiUrl),
    users: createUsersClient(apiUrl),
    categories: createCategoriesClient(apiUrl),
}

export type PageResponse<T> = {
    items: T[];
};