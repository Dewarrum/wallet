import { redirect, type Handle } from '@sveltejs/kit';
import { handle as authenticationHandle } from './auth';
import { sequence } from '@sveltejs/kit/hooks';

const authorizationHandle: Handle = async ({ event, resolve }) => {
    event.locals.session = await event.locals.auth();
    if (event.url.pathname.startsWith('/app')) {
        if (!event.locals.session) {
            throw redirect(303, '/auth/signin');
        }
    }

    return resolve(event);
}

export const handle: Handle = sequence(authenticationHandle, authorizationHandle)