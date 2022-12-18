import {IApplicationUserState} from "../types/IApplicationUserState";
import {
    AccessTokenExpirationDateStorageClaim,
    AccessTokenStorageClaim,
    EmailStorageClaim,
    getStorageClaimNames,
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

    const storageUserStateClaimNames = getStorageClaimNames();

    const availableStorages = [localStorage, sessionStorage];

    availableStorages.map((storage) => {
        if (storageUserStateClaimNames.filter((claim) => storage.getItem(claim)).length === storageUserStateClaimNames.length) {
            preferredStorage = storage;
        }
    })


    if (preferredStorage instanceof Storage) {
        // compare dates for access token expiration time
        // if (new Date(preferredStorage.getItem(AccessTokenExpirationDateStorageClaim) || new Date()) <= new Date()) {
        //     state.isLoading = false;
        //     state.isAuthorized = false;
        //     return state;
        // }

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