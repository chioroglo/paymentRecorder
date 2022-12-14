import {IApplicationUserState} from "../types";
import {
    AccessTokenExpirationDateStorageClaim,
    AccessTokenStorageClaim,
    EmailStorageClaim,
    getStorageAuthenticationClaimNames,
    RolesStorageClaim,
    UsernameStorageClaim
} from "../../lib";

export const applicationUserInitialState: IApplicationUserState = ((): IApplicationUserState => {

    const state: IApplicationUserState = {
        username: "",
        email: "",
        roles: [],
        accessToken: "",
        accessTokenExpirationDate: "",
        isAuthorized: false,
        errorMessage: "",
        isLoading: true
    }

    state.isLoading = false;
    // check if one of the claims (preferrably token) exists in one of the storages, then this storage will be chosen as
    // a source of all other info for initial storage or set to null if has no saved claims
    let preferredStorage: Storage | null | any = null;

    const storageUserStateClaimNames = getStorageAuthenticationClaimNames();

    const availableStorages = [localStorage, sessionStorage];

    availableStorages.forEach((storage) => {
        if (storageUserStateClaimNames.filter((claim) => storage.getItem(claim)).length === storageUserStateClaimNames.length) {
            preferredStorage = storage;
        }
    })


    if (preferredStorage instanceof Storage) {

        state.username = preferredStorage.getItem(UsernameStorageClaim) || "";
        state.email = preferredStorage.getItem(EmailStorageClaim) || "";
        state.roles = preferredStorage.getItem(RolesStorageClaim)?.split(',') || [];
        state.accessToken = preferredStorage.getItem(AccessTokenStorageClaim) || "";
        state.accessTokenExpirationDate = preferredStorage.getItem(AccessTokenExpirationDateStorageClaim) || "";
        state.isAuthorized = true;
        state.errorMessage = "";

        return state;
    }


    return state;

})();