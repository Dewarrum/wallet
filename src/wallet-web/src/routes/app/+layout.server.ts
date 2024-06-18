import { getUser } from "$lib/users/http.js";
import { redirect } from "@sveltejs/kit";

export async function load({ locals, fetch }) {
    const user = locals.session?.user;
    if (!user?.email || !user?.name) {
        throw redirect(303, '/');
    }

    return {
        user: await getUser(user.email, user.name, fetch),
    }
}