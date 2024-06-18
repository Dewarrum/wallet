import { env } from "$env/dynamic/private";
import type { AuthToken } from "./models";

export async function getToken() {
    const response = await fetch(`${env.API_URL}/oidc/token`, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "Authorization": `Basic ${btoa(`${env.LOGTO_USERNAME}:${env.LOGTO_PASSWORD}`)}`,
        },
        body: `grant_type=client_credentials&resource=${env.LOGTO_RESOURCE}`,
    });

    const json: AuthToken = await response.json();
    return json;
}