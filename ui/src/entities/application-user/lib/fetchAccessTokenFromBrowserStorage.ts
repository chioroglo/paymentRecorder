import {AccessToken } from "../types";
import {AccessTokenClaim, AccessTokenExpirationDateClaim} from "./applicationUserBrowserStorageClaims";

const storageHasToken = (): boolean => localStorage.getItem(AccessTokenClaim) !== null || sessionStorage.getItem(AccessTokenClaim) !== null;


export const fetchAccessTokenFromBrowserStorage = (): (AccessToken | null) => {
    if (storageHasToken()) {
        return {
            token: localStorage.getItem(AccessTokenClaim) || sessionStorage.getItem(AccessTokenClaim) || "",
            expirationDate: localStorage.getItem(AccessTokenExpirationDateClaim) || sessionStorage.getItem(AccessTokenExpirationDateClaim) || ""
        }
    }
    return null;
}
