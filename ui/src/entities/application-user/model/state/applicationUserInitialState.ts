import {IApplicationUserState} from "../types/IApplicationUserState";
import {fetchAccessTokenFromBrowserStorage} from "../../lib/fetchAccessTokenFromBrowserStorage";

export const applicationUserInitialState: IApplicationUserState = {
    username: "",
    email: "",
    roles: [],
    accessToken: fetchAccessTokenFromBrowserStorage()?.token || "",
    accessTokenExpirationDate: fetchAccessTokenFromBrowserStorage()?.expirationDate || "",
    isAuthorized: false,
    errorMessage: "",
    isLoading: true
}