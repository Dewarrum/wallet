import { REDIS_URL } from '$env/static/private';
import * as authHttp from '$lib/auth/http';
import { createClient } from 'redis';
import type { AuthToken } from './models';
import { logger } from '$lib/server/logger';

const redis = createClient({
    url: REDIS_URL,
});
await redis.connect();

async function getToken() {
    const tokenJson = await redis.get('token');
    const cached: AuthToken | null = JSON.parse(tokenJson);
    if (cached) {
        return cached;
    }

    logger.info('No cached token found, generating new one');

    const token: AuthToken = await authHttp.getToken();

    await redis.set('token', JSON.stringify(token), {
        EX: token.expires_in * 5 / 6,
    });
    logger.info('Token stored in redis');

    return token;
}

export const authService = {
    getToken,
}