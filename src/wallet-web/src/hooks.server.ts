import { redirect, type Handle, type HandleFetch } from '@sveltejs/kit';
import { handle as authenticationHandle } from './auth';
import { sequence } from '@sveltejs/kit/hooks';
import { authService } from '$lib/auth/authService';

const authorizationHandle: Handle = async ({ event, resolve }) => {
    event.locals.session = await event.locals.auth();
    if (event.url.pathname.startsWith('/app')) {
        if (!event.locals.session) {
            throw redirect(303, '/auth/signin');
        }
    }

    return resolve(event);
}

export const handle: Handle = sequence(authenticationHandle, authorizationHandle);

export const handleFetch: HandleFetch = async ({ request, fetch }) => {
    const token = await authService.getToken();
    request.headers.set('Authorization', `Bearer ${token.access_token}`);
    return fetch(request);
}