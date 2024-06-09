import { createQuery } from "@tanstack/svelte-query";
import type { Profile } from "./models";
import type { PageResponse } from "$lib/utils/page-response";

export const getProfiles = () => createQuery<any, Error, PageResponse<Profile>, string[]>({
    queryKey: ['profiles'],
    queryFn: async () => await fetch('/api/profiles', { credentials: 'include' }).then((r) => r.json())
});

export const getProfile = (profileId: string) => createQuery<any, Error, Profile, string[]>({
    queryKey: ['profiles', profileId],
    queryFn: async () => await fetch(`/api/profiles/${profileId}`, { credentials: 'include' }).then((r) => r.json())
})