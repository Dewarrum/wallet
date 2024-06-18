import { API_URL, LOGTO_PASSWORD, LOGTO_RESOURCE, LOGTO_USERNAME } from "$env/static/private";
import type { AuthToken } from "./models";

export async function getToken() {
    const response = await fetch(`${API_URL}/oidc/token`, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "Authorization": `Basic ${btoa(`${LOGTO_USERNAME}:${LOGTO_PASSWORD}`)}`,
        },
        body: `grant_type=client_credentials&resource=${LOGTO_RESOURCE}`,
    });

    const json: AuthToken = await response.json();
    return json;
}