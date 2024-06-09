import type { PageResponse } from "$lib/utils/page-response";
import { createQuery } from "@tanstack/svelte-query";
import type { Category } from "./models";

export const getCategories = () => createQuery<any, Error, PageResponse<Category>, string[]>({
    queryKey: ['categories'],
    queryFn: async () => await fetch(`/api/categories`).then((r) => r.json())
});