export interface IApplicationUserState {
    username: string,
    email: string,
    roles: string[],
    accessToken: string,
    accessTokenExpirationDate: string,
    isAuthorized: boolean,
    errorMessage: string,
    isLoading: boolean
}