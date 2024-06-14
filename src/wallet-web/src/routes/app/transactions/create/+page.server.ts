import { fail } from "@sveltejs/kit";
import type { Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types.js";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { http } from "$lib/http/index.js";
import { createTransactionSchema } from "$lib/models/transaction.js";

export const load: PageServerLoad = async ({ parent }) => {
    const { user } = await parent();
    const profiles = await http.profiles.getAll(user.id).then(p => p.items);
    const categories = await http.categories.getAll(user.id).then(p => p.items);

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

        const user = await http.users.get(event.locals.session?.user?.email!, event.locals.session?.user?.id!);
        await http.transactions.create({
            profileId: form.data.profileId,
            amount: form.data.amount,
            description: form.data.description,
            categoryId: form.data.categoryId,
            type: form.data.type,
            isRecurring: form.data.isRecurring,
            paymentMethod: form.data.paymentMethod,
            merchant: form.data.merchant,
            userId: user.id,
        });

        return {
            form,
        };
    }
}