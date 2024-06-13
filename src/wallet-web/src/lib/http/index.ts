import { API_URL } from "$env/static/private";
import { createCategoriesClient } from "./categories";
import { createProfilesClient } from "./profiles";
import { createTransactionClient } from "./transactions";
import { createUsersClient } from "./users";

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