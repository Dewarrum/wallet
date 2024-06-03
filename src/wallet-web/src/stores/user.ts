import { writable } from "svelte/store";

export const userStore = writable<UserStore>({
    isLoading: false,
    user: null,
});

interface User {
    id: string,
    name: string,
    email: string,
}

interface UserStore {
    user: User | null,
    isLoading: boolean,
}