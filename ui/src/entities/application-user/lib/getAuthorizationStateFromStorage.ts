import {AccessTokenClaim, AccessTokenExpirationDateClaim} from "./applicationUserBrowserStorageClaims";
import {AccessToken} from "../types";

export const getAuthorizationStateFromStorage = (): AccessToken | null => {
    let storage: Storage;
    if (localStorage.getItem(AccessTokenClaim) && localStorage.getItem(AccessTokenExpirationDateClaim)) {
        storage = localStorage;
    } else if (sessionStorage.getItem(AccessTokenClaim) && localStorage.getItem(AccessTokenExpirationDateClaim)) {
        storage = sessionStorage
    } else {
        return null;
    }

    let token = storage.getItem(AccessTokenClaim);
    let tokenExpirationDate = storage.getItem(AccessTokenExpirationDateClaim);

    if (tokenExpirationDate && new Date(tokenExpirationDate) < new Date()) {
        return null;
    }

    if (token && tokenExpirationDate)
    {
        return {
            expirationDate: tokenExpirationDate,
            token: token
        }
    }
    return null;

}