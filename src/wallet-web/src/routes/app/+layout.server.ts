import { http } from "$lib/http.js";
import { redirect } from "@sveltejs/kit";

export async function load({ locals }) {
    const user = locals.session?.user;
    if (!user?.email || !user?.name) {
        throw redirect(303, '/');
    }

    return {
        user: await http.users.get(user.email, user.name),
    }
}