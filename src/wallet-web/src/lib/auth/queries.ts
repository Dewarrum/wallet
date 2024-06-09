import { createQuery } from "@tanstack/svelte-query";
import type { User } from "./models";

export const getAuth = () => createQuery<any, Error, User, string[]>({
    queryKey: ['auth/profile'],
    queryFn: async () =>
        await fetch('/api/auth/profile', { credentials: 'include' }).then((r) => r.json())
});
