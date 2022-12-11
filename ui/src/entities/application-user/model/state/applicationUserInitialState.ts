import {IApplicationUserState} from "../types/IApplicationUserState";

export const applicationUserInitialState: IApplicationUserState = {
    username: "",
    email: "",
    roles: [],
    accessToken: '',
    accessTokenExpirationDate: new Date().toISOString(),
    isAuthorized: false,
    errorMessage: "",
    isLoading: true
}