import { fail } from "@sveltejs/kit";
import type { Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types.js";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { createCategorySchema } from "$lib/categories/models.js";
import { getUser } from "$lib/users/http.js";
import { createCategory } from "$lib/categories/http.js";

export const load: PageServerLoad = async () => {
    return {
        form: await superValidate(zod(createCategorySchema)),
    };
};

export const actions: Actions = {
    default: async (event) => {
        const form = await superValidate(event, zod(createCategorySchema));
        if (!form.valid) {
            return fail(400, {
                form,
            });
        }

        const user = await getUser(event.locals.session?.user?.email!, event.locals.session?.user?.id!, event.fetch);
        await createCategory({
            name: form.data.name,
            description: form.data.description,
            userId: user.id,
        }, event.fetch);

        return {
            form,
        };
    }
}