import { createProfileSchema } from "$lib/models/profile.js";
import { fail } from "@sveltejs/kit";
import type { Actions } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types.js";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { http } from "$lib/http/index.js";

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

        const user = await http.users.get(event.locals.session?.user?.email!, event.locals.session?.user?.id!);
        await http.profiles.create({
            name: form.data.name,
            description: form.data.description,
            currency: form.data.currency,
            userId: user.id,
        });

        return {
            form,
        };
    }
}