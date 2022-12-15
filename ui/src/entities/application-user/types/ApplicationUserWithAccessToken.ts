export interface ApplicationUserWithAccessToken {
    username: string,
    email: string,
    roles: string[],
    accessToken: string,
    accessTokenExpirationDate: string,
}