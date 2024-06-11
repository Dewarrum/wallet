import { http } from "$lib/http/index.js";
import { redirect } from "@sveltejs/kit";

export async function load({ locals }) {
    const email = locals.session?.user?.email;
    if (!email) {
        throw redirect(303, '/auth/signin');
    }

    const user = await http.users.get(email);

    return {
        user,
    }
}