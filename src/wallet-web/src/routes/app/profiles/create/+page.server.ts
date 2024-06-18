import { createProfileSchema } from "$lib/profiles/models.js";
import { fail } from "@sveltejs/kit";
import type { Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types.js";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { getUser } from "$lib/users/http.js";
import { createProfile } from "$lib/profiles/http.js";

export const load: PageServerLoad = async () => {
    return {
        form: await superValidate(zod(createProfileSchema)),
    };
};

export const actions: Actions = {
    default: async (event) => {
        const form = await superValidate(event, zod(createProfileSchema));
        if (!form.valid) {
            return fail(400, {
                form,
            });
        }

        const user = await getUser(event.locals.session?.user?.email!, event.locals.session?.user?.id!, event.fetch);
        await createProfile({
            name: form.data.name,
            description: form.data.description,
            currency: form.data.currency,
            userId: user.id,
        }, event.fetch);

        return {
            form,
        };
    }
}