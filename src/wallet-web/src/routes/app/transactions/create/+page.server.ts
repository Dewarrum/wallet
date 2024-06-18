import { fail } from "@sveltejs/kit";
import type { Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types.js";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { type PageResponse } from "$lib/http.js";
import { createTransactionSchema } from "$lib/transactions/models.js";
import { API_URL } from "$env/static/private";
import type { Profile } from "$lib/profiles/models.js";
import type { Category } from "$lib/categories/models.js";
import { getUser } from "$lib/users/http.js";
import { createTransaction, getTransactions } from "$lib/transactions/http.js";

type Fetch = typeof fetch;

async function getProfiles(userId: string, fetch: Fetch) {
    const response = await fetch(`${API_URL}/profiles?userId=${userId}`);
    const profiles: PageResponse<Profile> = await response.json();

    return profiles.items;
}

async function getCategories(userId: string, fetch: Fetch) {
    const response = await fetch(`${API_URL}/categories?userId=${userId}`);
    const categories: PageResponse<Category> = await response.json();

    return categories.items;
}

export const load: PageServerLoad = async ({ parent, fetch }) => {
    const { user } = await parent();
    const profiles = await getProfiles(user.id, fetch);
    const categories = await getCategories(user.id, fetch);

    return {
        profiles,
        categories,
        form: await superValidate(zod(createTransactionSchema)),
    };
};

export const actions: Actions = {
    default: async (event) => {
        const form = await superValidate(event, zod(createTransactionSchema));
        if (!form.valid) {
            return fail(400, {
                form,
            });
        }

        const user = await getUser(event.locals.session?.user?.email!, event.locals.session?.user?.id!, event.fetch);
        await createTransaction({
            profileId: form.data.profileId,
            amount: form.data.amount,
            description: form.data.description,
            categoryId: form.data.categoryId,
            type: form.data.type,
            isRecurring: form.data.isRecurring,
            paymentMethod: form.data.paymentMethod,
            merchant: form.data.merchant,
            userId: user.id,
        }, event.fetch);

        return {
            form,
        };
    }
}