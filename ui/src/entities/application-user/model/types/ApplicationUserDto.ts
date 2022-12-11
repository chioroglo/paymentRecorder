export interface ApplicationUserDto {
    username: string,
    email: string,
    roles: string[],
    accessToken: string,
    accessTokenExpirationDate: string,
}