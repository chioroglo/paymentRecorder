export const AccessTokenStorageClaim = "accToken";

export const AccessTokenExpirationDateStorageClaim = "accTokenExp"

export const UsernameStorageClaim = "username";

export const EmailStorageClaim = "email";

export const RolesStorageClaim = "roles";

export const getStorageClaimNames = () => [RolesStorageClaim, EmailStorageClaim, UsernameStorageClaim, AccessTokenStorageClaim, AccessTokenExpirationDateStorageClaim];